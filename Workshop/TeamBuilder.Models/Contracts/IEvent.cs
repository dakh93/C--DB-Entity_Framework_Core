
namespace TeamBuilder.Models.Contracts
{
    public interface IEvent
    {
        int EventId { get; }
        string Name { get; }
        string Description { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        int CreatorId { get; }
    }
}
