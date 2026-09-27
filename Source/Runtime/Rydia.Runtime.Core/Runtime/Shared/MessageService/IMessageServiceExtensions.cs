using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    public static class IMessageServiceExtensions
    {

        public static DialogResult ShowMessage(this IMessageService service, string message, string title, MessageBoxButtons buttons)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message, title, buttons));
        }

        public static DialogResult ShowMessage(this IMessageService service, string message, MessageBoxButtons buttons)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message, buttons));
        }

        public static DialogResult ShowMessage(this IMessageService service, string message, string title)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message, title));
        }

        public static DialogResult ShowMessage(this IMessageService service, string message)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message));
        }

    }

}
