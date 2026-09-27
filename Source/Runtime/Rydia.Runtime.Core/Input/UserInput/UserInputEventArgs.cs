using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    public class UserInputEventArgs : EventArgs
    {
        private IUserInput inputChannel;

        public IUserInput InputChannel
        {
            get { return this.inputChannel; }
        }

        public UserInputEventArgs(IUserInput inputChannel)
        {
            this.inputChannel = inputChannel;
        }
    }
}
