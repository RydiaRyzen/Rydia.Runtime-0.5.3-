using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Runtime.Shared;
namespace Rydia.Runtime
{
    public interface IPlatform : IMessageService
    {

        float ScreenScale { get; }

        AppRunner App
        {
            get;
        }

    }

}
