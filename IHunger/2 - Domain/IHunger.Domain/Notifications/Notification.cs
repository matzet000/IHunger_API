using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
