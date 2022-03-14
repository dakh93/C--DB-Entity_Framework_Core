
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Models.Contracts
{
    internal interface IUser
    {
        int UserId { get; }
        string Username { get; }
        string Password { get; }
        string FirstName { get; }
        string LastName { get; }
        int Age { get; }
        Gender Gender { get; }
        bool IsDeleted { get; }
    }
}
