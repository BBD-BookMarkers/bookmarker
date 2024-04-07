using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frontendProgram
{
    internal class MessageService
    {
        public event EventHandler<string> MessageReceived;

        public void sendMessage(string message)
        {
            MessageReceived?.Invoke(this, message);
        }
    }
}
