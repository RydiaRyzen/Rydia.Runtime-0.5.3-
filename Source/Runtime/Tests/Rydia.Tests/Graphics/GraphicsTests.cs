using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Tests.Graphics
{
    public class GraphicsTests
    {

        [Test(Description = "NativeBufferが正しく動作するかをテストします")]
        public void NativeBufferTest1()
        {
            var data = new int[] { 1, 2, 3, 4, 5 };
            using (var buffer = new Rydia.Graphics.NativeBuffer<int>(data))
            {
                // Capacityが5であることを確認します
                Assert.AreEqual(5, buffer.Capacity);
                // Strideが4であることを確認します（intのサイズは4バイト）
                Assert.AreEqual(4, buffer.Stride);
                // 各要素が正しく格納されていることを確認します
                for (int i = 0; i < data.Length; i++)
                {
                    Assert.AreEqual(data[i], buffer[i]);
                }
                // 要素を変更して正しく反映されることを確認します
                buffer[2] = 10;
                Assert.AreEqual(10, buffer[2]);
            }
        }

    }
}
