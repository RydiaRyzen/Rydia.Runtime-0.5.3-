using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    public class MessageDialogEventArgs
    {

        public string Message
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public MessageBoxButtons Buttons
        {
            get;
            set;
        }

        public MessageDialogEventArgs(string message)
            : this(message, "Rydia Engine", MessageBoxButtons.OK)
        {

        }

        public MessageDialogEventArgs(string message, MessageBoxButtons buttons)
            : this(message, "Rydia Engine", buttons)
        {

        }

        public MessageDialogEventArgs(string message, string title)
            : this(message, title, MessageBoxButtons.OK)
        {

        }

        public MessageDialogEventArgs(string message, string title, MessageBoxButtons buttons)
        {
            Message = message;
            Title = title;
            Buttons = buttons;
        }

    }

}
