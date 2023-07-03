namespace EventBus.Messages;

interface IIntegrationEvent
{
  DateTime CreationDate { get; }
  Guid Id { get; set; }
}
