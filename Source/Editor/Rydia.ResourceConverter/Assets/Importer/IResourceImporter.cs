using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.ResourceConverter.Assets.Importer
{

    public interface IResourceImporter<TResource>
    {

        bool TryImport(byte[] data, out TResource resource);

    }

}
