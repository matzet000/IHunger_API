using IHunger.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Notifications
{
    public class Notifier : INotifier
    {
        readonly ILogger<Notifier> _log;
        private List<Notification> _notifications;

        public Notifier(ILogger<Notifier> log)
        {
            _log = log;
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _log.LogInformation(notification.message);
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
