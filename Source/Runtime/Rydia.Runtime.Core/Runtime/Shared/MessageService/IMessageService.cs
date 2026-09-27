using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    public interface IMessageService
    {

        DialogResult ShowMessage(MessageDialogEventArgs args);

    }

}
