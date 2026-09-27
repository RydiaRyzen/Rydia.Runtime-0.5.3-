using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Resources;
using Rydia.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Rydia.Resources
{
    public class FontCharSet
    {

        [Ignore]
        private readonly Dictionary<char, NativeTexture> _textures = new Dictionary<char, NativeTexture>();

        [Ignore]
        private Dictionary<char, PixelData> CharSet
        {
            get;
            set;
        }

        [FieldOrder(1)]
        [FieldCount(nameof(CharCount))]
        public List<CharEntry> Chars
        {
            get;
            set;
        }

        [FieldOrder(0)]
        public int CharCount
        {
            get;
            set;
        }

        public class CharEntry
        {

            [FieldOrder(0)]
            public char Character { get; set; }
            [FieldOrder(1)]
            public PixelData PixelData { get; set; }
        }

        public FontCharSet()
        {
            CharSet = new Dictionary<char, PixelData>();
        }

        public void Add(char v, PixelData pixelData)
        {
            CharSet[v] = pixelData;
        }

        public void CreateCharEntries()
        {
            Chars = new List<CharEntry>();
            foreach (var kvp in CharSet)
            {
                Chars.Add(new CharEntry()
                {
                    Character = kvp.Key,
                    PixelData = kvp.Value
                });
            }
            CharCount = Chars.Count;
        }

        public void CreateCharSet()
        {
            foreach(var entry in Chars)
            {
                CharSet.Add(entry.Character, entry.PixelData);
            }
        }

        public void CreateTextures(IGLES20 gl)
        {
            foreach(var kvp in CharSet)
            {
                var texture = new NativeTexture();
                texture.Bind();
                texture.MinificationFilter = TextureMinFilter.Linear;
                texture.MagnificationFilter = TextureMagFilter.Linear;
                gl.PixelStore(PixelStoreParameter.UnpackAlignment, PixelStoreValue.One);
                texture.TexImage2D(PixelFormat.Rgba, kvp.Value.Width, kvp.Value.Height, kvp.Value.Data);
                this._textures[kvp.Key] = texture;
            }
        }

        public void DisposeTextures()
        {
            foreach (var kvp in this._textures)
            {
                kvp.Value.Dispose();
            }
            this._textures.Clear();
        }

        public PixelData GetPixelData(char v)
        {
            if (CharSet.ContainsKey(v))
            {
                return CharSet[v];
            }
            return null;
        }

        public bool TryGetPixelData(char v, out PixelData pixelData)
        {
            return CharSet.TryGetValue(v, out pixelData);
        }

        public NativeTexture GetTexture(char v)
        {
            if (this._textures.ContainsKey(v))
            {
                return this._textures[v];
            }
            return null;
        }

    }
}