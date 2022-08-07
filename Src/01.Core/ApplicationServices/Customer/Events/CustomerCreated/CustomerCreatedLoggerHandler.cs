using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Customer.Events.CustomerCreated
{
    public class CustomerCreatedLoggerHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly ILogger<CustomerCreatedLoggerHandler> _logger;

        public CustomerCreatedLoggerHandler(ILogger<CustomerCreatedLoggerHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"New customer has been created : {notification.FirstName} {notification.LastName}");

            return Task.CompletedTask;
        }
    }
}
