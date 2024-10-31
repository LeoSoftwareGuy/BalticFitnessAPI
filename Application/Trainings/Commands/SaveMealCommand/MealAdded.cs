using Application.Support.Interfaces;
using Application.Support.Models;
using MediatR;

namespace Application.Trainings.Commands.SaveMealCommand
{
    public class MealAdded : INotification
    {
        public string UserId { get; set; }

        public class MealAddedHandler : INotificationHandler<MealAdded>
        {
            private readonly INotificationService _notificationService;

            public MealAddedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            public async Task Handle(MealAdded notification, CancellationToken cancellationToken)
            {
                await _notificationService.SendAsync(new Message());
            }
        }
    }
}
