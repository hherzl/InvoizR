using InvoizR.Domain.Common;
using InvoizR.Infrastructure.Persistence;
using MediatR;

namespace InvoizR.Infrastructure;

public static class MediatRExtensions
{
    public static async Task DispatchNotificationsAsync(this IMediator mediator, InvoizRDbContext dbContext)
    {
        var entities = dbContext
                .ChangeTracker
                .Entries<Entity>()
                .Where(entry => entry.Entity.Notifications.Count > 0)
                .Select(entry => entry.Entity)
                .ToList()
                ;

        foreach (var entity in entities)
        {
            foreach (var notification in entity.Notifications)
                await mediator.Publish(notification);

            entity.ClearNotifications();
        }
    }
}
