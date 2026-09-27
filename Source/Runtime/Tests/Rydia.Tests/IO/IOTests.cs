using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Tests.IO
{
    public class IOTests
    {

        [Test(Description = "OpenStreamが正しく動作するかをテストします")]
        public void EmbeddedResourcesTest_OpenStream()
        {
            var resources = new Rydia.IO.EmbeddedResources<IOTests>();

            using (var stream = resources.Open("Assets.TestResource.txt"))
            {
                Assert.IsNotNull(stream, "ストリームがnullです");
                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    Assert.AreEqual("This is a test resource file.", content.Trim(),
                        "リソースの内容が正しくありません");
                }
            }
        }

        [Test(Description = "OpenBytesが正しく動作するかをテストします")]
        public void EmbeddedResourcesTest_OpenBytes()
        {
            var resources = new Rydia.IO.EmbeddedResources<IOTests>();

            var bytes = resources.Load("Assets.TestResource.txt");
            Assert.IsNotNull(bytes, "バイト配列がnullです");

            var text = Encoding.UTF8.GetString(bytes);
            if (text.Length > 0 && text[0] == '\uFEFF')
                text = text.Substring(1);

            Assert.That("This is a test resource file.", Is.EqualTo(text.Trim()),
                "バイト配列から取得したリソースの内容が正しくありません");
        }

        [Test(Description = "LoadTextが正しく動作するかをテストします")]
        public void EmbeddedResourcesTest_LoadText()
        {
            var resources = new Rydia.IO.EmbeddedResources<IOTests>();

            var text = resources.LoadText("Assets.TestResource.txt");
            if (text.Length > 0 && text[0] == '\uFEFF')
                text = text.Substring(1);

            Assert.That("This is a test resource file.", Is.EqualTo(text.Trim()),
                "LoadTextで取得したリソースの内容が正しくありません");
        }

    }
}
