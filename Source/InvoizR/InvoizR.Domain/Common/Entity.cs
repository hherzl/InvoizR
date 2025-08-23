using System.Collections.ObjectModel;
using MediatR;

namespace InvoizR.Domain.Common;

public abstract partial class Entity
{
    public Collection<INotification> Notifications { get; }

    protected Entity()
    {
        Notifications = [];
    }

    public void AddNotification(INotification notification)
    {
        Notifications.Add(notification);
    }

    public void ClearNotifications()
    {
        Notifications.Clear();
    }
}
