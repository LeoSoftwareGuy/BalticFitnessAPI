using Application.Support.Interfaces;
using Application.Support.Models;
using MediatR;

namespace Application.Trainings.Commands.SaveTrainingCommand
{

    /// <summary>
    /// otify other parts of the system after the training has been added.
    /// The TrainingAddedHandler correctly handles this event, and the INotificationService is used to send notifications.
    /// </summary>
    public class TrainingAdded : INotification
    {
        public string UserId { get; set; }

        public class TrainingAddedHandler : INotificationHandler<TrainingAdded>
        {
            private readonly INotificationService _notificationService;

            public TrainingAddedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }
            public async Task Handle(TrainingAdded notification, CancellationToken cancellationToken)
            {
                await _notificationService.SendAsync(new Message());
            }
        }
    }
}
