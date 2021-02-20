using IHunger.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
