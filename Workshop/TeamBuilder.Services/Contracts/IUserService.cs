
using TeamBuilder.Models;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.Services.Contracts
{
    public interface IUserService
    {
        void RegisterUser(string username, string password, string repeatedPassword, string firstName, string lastName, int age, Gender gender);

        void Login(string username, string password);

        string Logout();

        string DeleteUser();

        void CreateEvent(string name, string description, DateTime startDate, DateTime endDate);

        void CreateTeam(string name, string acronym, string description, User creator);

        void InviteToTeam(Team team, User user);

        void AcceptInvite(string teamName);

        void DeclineInvite(string teamName);

        void KickMember(string teamName, string username);

        void Disband(string teamName);

        void AddTeamTo(string eventName, string teamName);

        string ShowEvent(string eventName);

        string ShowTeam(string teamName);


    }
}
