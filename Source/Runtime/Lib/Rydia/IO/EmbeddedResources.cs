using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Rydia.IO
{

    public class EmbeddedResources<TTypeInAssetsAssembly>
    {

        private Assembly _assembly = null;
        private string _prefix;

        public EmbeddedResources()
        {
            Debug.Assert(this._assembly == null);
            if (this._assembly == null)
                this._assembly = Assembly.GetAssembly(typeof(TTypeInAssetsAssembly));
            Debug.Assert(this._assembly != null);
            this._prefix = this._assembly.GetName().Name + ".";
        }

        public Stream Open(string filename)
        {
            Debug.Assert(this._assembly != null, "Did you forget to call Initialize()?");
            var names = this._assembly.GetManifestResourceNames();
            var modules = this._assembly.GetLoadedModules();
            return this._assembly.GetManifestResourceStream(this._prefix + filename); //(_prefix + filename);
        }

        public byte[] Load(string filename)
        {
            Stream stream = Open(filename);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        public string LoadText(string filename)
        {
            var buffer = Load(filename);
            return Encoding.UTF8.GetString(buffer);
        }

    }

}

