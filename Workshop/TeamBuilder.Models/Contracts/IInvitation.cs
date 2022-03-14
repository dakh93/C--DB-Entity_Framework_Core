

namespace TeamBuilder.Models.Contracts
{
    internal interface IInvitation
    {
        int InvitationId { get; }
        int InvitedUserId { get; }
        int TeamId { get; }
        bool IsActive { get; }
    }
}
