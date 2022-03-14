
namespace TeamBuilder.Models.Contracts
{
    internal interface ITeam
    {
        int TeamId { get; }
        string Name { get; }
        string Description { get; }
        string Acronym { get; }
        int CreatorId { get; }
    }
}
