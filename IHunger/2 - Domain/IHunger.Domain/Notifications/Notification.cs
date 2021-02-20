using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Notifications
{
    public class Notification
    {
        public string message { get; }

        public Notification(string message)
        {
            this.message = message;
        }

    }
}
