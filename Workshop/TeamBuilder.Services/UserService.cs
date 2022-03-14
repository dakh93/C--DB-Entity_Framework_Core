
using Microsoft.EntityFrameworkCore;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Models.Enums;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.Services
{
    public class UserService : IUserService
    {
        private readonly TeamBuilderContext context;

        public UserService(TeamBuilderContext context)
        {
            this.context = context;
        }


        public void AcceptInvite(string teamName)
        {
           var user = this.context.Users
                .Include(u => u.Invitations)
                .Where(u => u.IsLogged == true)
                .FirstOrDefault();

            var team = this.context.Teams
                .Where(t => t.Name == teamName)
                .FirstOrDefault();

            var invitation = user.Invitations.
                Where(i => i.Team.Name == teamName)
                .FirstOrDefault();

            invitation.IsActive = false;

            var userTeam = new UserTeam
            {
                Team = team,
                TeamId = team.TeamId,
                User = user,
                UserId = user.UserId,
            };
            user.Teams.Add(userTeam);

            this.context.SaveChanges();
        }

        public void AddTeamTo(string eventName, string teamName)
        {
            var team = this.context.Teams
                .Where(t => t.Name == teamName)
                .FirstOrDefault();

            var eventObj = this.context.Event
                .Where(e => e.Name == eventName)
                .OrderByDescending(e => e.StartDate)
                .FirstOrDefault();

            var eventTeam = new EventTeam
            {
                EventId = eventObj.EventId,
                TeamId = team.TeamId
            };

            team.Events.Add(eventTeam);
            eventObj.Teams.Add(eventTeam);

            this.context.EventsTeams.Add(eventTeam);

            this.context.SaveChanges();
        }

        public void CreateEvent(string name, string description, DateTime startDate, DateTime endDate)
        {
            var loggedUser = this.context.Users
                .Where(u => u.IsLogged == true)
                .FirstOrDefault();

            var evn = new Event
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Creator = loggedUser,
                CreatorId = loggedUser.UserId
            };

            this.context.Event.Add(evn);
            this.context.SaveChanges();
        }

        public void CreateTeam(string name, string acronym, string description, User creator)
        {
            var team = new Team
            {
                Name = name,
                Acronym = acronym,
                Description = description,
                CreatorId = creator.UserId,
                IsDeleted = false

            };

            this.context.Teams.Add(team);
            this.context.SaveChanges();

            var userTeam = new UserTeam
            {
                
                TeamId = team.TeamId,
               
                UserId = creator.UserId
            };

            this.context.Teams
                .FirstOrDefault(t => t.Name == name)
                .Users.Add(userTeam);

            this.context.SaveChanges();
        }

        public void DeclineInvite(string teamName)
        {
            var user = this.context.Users
                .Include(u => u.Invitations)
                .Where(u => u.IsLogged == true)
                .FirstOrDefault();

            var team = this.context.Teams
                .Where(t => t.Name == teamName)
                .FirstOrDefault();

            var invitation = user.Invitations.
                Where(i => i.TeamId == team.TeamId)
                .FirstOrDefault();

            invitation.IsActive = false;

            this.context.SaveChanges();
        }

        public string DeleteUser()
        {
            var username = String.Empty;
            using (var context = new TeamBuilderContext())
            {
                var loggedUser = context.Users
                    .Where(u => u.IsLogged == true)
                    .FirstOrDefault();

                loggedUser.IsLogged = false;
                loggedUser.IsDeleted = true;

                context.SaveChanges();
                username = loggedUser.Username;
            }
            return username;
        }

        public void Disband(string teamName)
        {
            var team = this.context.Teams
                .Where(t => t.Name == teamName)
                .FirstOrDefault();

            team.IsDeleted = true;

            this.context.SaveChanges();
        }

        public void InviteToTeam(Team team, User user)
        {
            var invitation = new Invitation
            {
                TeamId = team.TeamId,
                InvitedUserId = user.UserId,
                IsActive = true
            };

            this.context.Invitations.Add(invitation);
            this.context.SaveChanges();
        }

        public void KickMember(string teamName, string username)
        {
            var user = this.context.Users
                .Where(u => u.IsLogged == true)
                .FirstOrDefault();

            var team = this.context.Teams
                .Where(t => t.Name == teamName)
                .FirstOrDefault();

            var userTeam = this.context.UsersTeams
                .Where(ut => ut.UserId == user.UserId && ut.TeamId == team.TeamId)
                .FirstOrDefault();

            this.context.UsersTeams.Remove(userTeam);

            user.Teams.Remove(userTeam);
            team.Users.Remove(userTeam);

            this.context.SaveChanges();
        }

        public void Login(string username, string password)
        {
            var user = context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            user.IsLogged = true;

            context.SaveChanges();
        }

        public string Logout()
        {
            var user = this.context.Users
                .Where(u => u.IsLogged == true)
                .FirstOrDefault();

            user.IsLogged = false;

            this.context.SaveChanges();

            return user.Username;
        }

        public void RegisterUser(string username, string password, string repeatedPassword, string firstName, string lastName, int age, Gender gender)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public string ShowEvent(string eventName)
        {

            var currEvenet = this.context.Event
                .Include(t => t.Teams)
                .Where(e => e.Name == eventName)
                .FirstOrDefault();

            var sb = new StringBuilder();

            sb.AppendLine($"[{currEvenet.Name}] [{currEvenet.StartDate}] [{currEvenet.EndDate}]");
            sb.AppendLine($"{currEvenet.Description}");
            sb.AppendLine("Teams:");
            foreach (var team in currEvenet.Teams)
            {
                sb.AppendLine($"-[{team.Team.Name}]");
            }

            return sb.ToString();
            
        }

        public string ShowTeam(string teamName)
        {
            var team = this.context.Teams
                .Include(tu => tu.Users)
                .ThenInclude(u => u.User)
                .Where(t => t.Name == teamName)
                .FirstOrDefault();

            var sb = new StringBuilder();

            sb.AppendLine($"[{team.Name}] [{team.Acronym}]");
            sb.AppendLine("Members:");
            foreach (var player in team.Users
                .Select(u => u.User.FirstName + " " + u.User.LastName ).ToList())
            {
                sb.AppendLine($"--[{player}]");
            }

            return sb.ToString().TrimEnd();
        }
    }
}