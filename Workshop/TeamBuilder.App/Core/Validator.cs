
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.App.Core
{
    public static class Validator
    {
        
        public static void CheckForArgumentCount(string[] args, int expectedElements)
        {
            if (args.Length != expectedElements)
            {
                throw new FormatException(ErrorMessages.InvalidArgsCount);
            }
        }

        public static void CheckForArgumentCount(string[] args, int expectedElementsMIN, int expectedElementsMAX)
        {
            if (args.Length < expectedElementsMIN && args.Length > expectedElementsMAX)
            {
                throw new FormatException(ErrorMessages.InvalidArgsCount);
            }
        }

        public static int CheckAge(string input)
        {
            int age;
            var isParsed = int.TryParse(input, out age);

            if (age < 0 || !isParsed)
            {
                throw new ArgumentException(ErrorMessages.InvalidAgeFormat);
            }
            return age;
        }

        public static Gender CheckGender(string input)
        {
            var formatInput = Char.ToUpper(input[0]) + input.Substring(1).ToLower();

            Gender gender;
            var isParsed = Enum.TryParse(formatInput, out gender);

            if (!isParsed)
            {
                throw new ArgumentException(ErrorMessages.InvalidGenderFormat);
            }

            return gender;
        }

        public static void CheckUsername(string username)
        {
            var MIN_SYMBOLS = 3;
            var MAX_SYMBOLS = 25;

            var isCorrectSymbolsCount = username.Length > MAX_SYMBOLS || username.Length < MIN_SYMBOLS;

            if (isCorrectSymbolsCount)
            {
                throw new ArgumentException(string.Format(ErrorMessages.InvalidUsername, username));
            }

            using (var context = new TeamBuilderContext())
            {
                var isUsernameExists = context.Users
                    .Any(u => u.Username == username);

                if (isUsernameExists)
                {
                    throw new InvalidOperationException(String.Format(ErrorMessages.ExistingUsername, username));
                }

            }
        }

        public static void CheckPasswordFormat(string password, string repeatedPassword)
        {
            var MIN_SYMBOLS = 6;
            var MAX_SYMBOLS = 30;

            var isCorrectCount = password.Length > MIN_SYMBOLS ||
                                 password.Length < MAX_SYMBOLS;
            var isCorrectFormat = password.Any(c => char.IsUpper(c)) &&
                                  password.Any(c => char.IsDigit(c));

            if (!isCorrectCount || !isCorrectFormat)
            {
                throw new ArgumentException(String.Format(ErrorMessages.InvalidPassword, password));
            }

            if (password != repeatedPassword)
            {
                throw new InvalidOperationException(ErrorMessages.PasswordMismatch);
            }
        }

        public static void CheckIfPasswordCorrect(string username, string password)
        {
            using (var context = new TeamBuilderContext())
            {
                var user = context.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                var samePassword = user.Password == password;

                if (!samePassword)
                {
                    throw new ArgumentException(ErrorMessages.UsernameLoginFail);
                }

            }
        }

        public static void CheckIfUserDeleted(string username)
        {
            var user = Database.GetUserByName(username);

            if (user.IsDeleted == true)
            {
                throw new ArgumentException(ErrorMessages.UsernameLoginFail);
            }
        }

        public static void CheckIfUserExists(string username)
        {
            var user = Database.GetUserByName(username);

            if (user == null)
            {
                throw new ArgumentException(String.Format(ErrorMessages.InvalidUsername, username));
            }
        }

        public static void CheckForLoggedUserRegister()
        {
            using (var context = new TeamBuilderContext())
            {
                var loggedUser = context.Users
                    .Any(u => u.IsLogged == true);

                if (loggedUser)
                {
                    throw new InvalidOperationException(ErrorMessages.AlreadyLoggedUser);
                }
            }
        }

        public static void CheckForLoggedUserLogout()
        {
            using (var context = new TeamBuilderContext())
            {
                var loggedUser = context.Users
                    .Any(u => u.IsLogged == true);

                if (!loggedUser)
                {
                    throw new InvalidOperationException(ErrorMessages.NoLoggedUser);
                }
            }
        }



        public static void CheckDate(string startDate, string endDate)
        {
            var validDateFormat = "dd/MM/yyyy HH:mm";

            DateTime? startDateCheck = DateTime.ParseExact(startDate, validDateFormat, null);
            DateTime? endDateCheck = DateTime.ParseExact(endDate, validDateFormat, null);

            if (startDate == null || endDate == null)
            {
                throw new ArgumentException(String.Format(ErrorMessages.InvalidDateTimeFormat, validDateFormat));
            }
        }

        public static void CheckStartDateBeforeEndDate(DateTime startDate, DateTime endDate)
        {
           
            if (startDate > endDate)
            {
                throw new ArgumentException(ErrorMessages.StartDateShouldBeBeforeEndDate);
            }
        }

        public static void CheckIfTeamExists(string teamName)
        {
          
            var team = Database.GetTeamByName(teamName);

            if (team == null)
            {
                throw new ArgumentException(String.Format(ErrorMessages.ExistingTeam, teamName));
            }
            
        }

        public static void CheckIfTeamExistsWhenInvite(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                var team = Database.GetTeamByName(teamName);

                if (team != null && team.IsDeleted == true)
                {
                    throw new ArgumentException(String.Format(ErrorMessages.ExistingTeam, teamName));
                }
            }
        }

        public static void CheckIfTeamExistsWhenDisband(string teamName)
        {
            var team = Database.GetTeamByName(teamName);

                if (team == null)
                {
                    throw new ArgumentException(String.Format(ErrorMessages.TeamDoesntExistsWhenDisband, teamName));
                }
            
        }


        public static void CheckAcronymLength(string acronym)
        {
            var FIX_LENGTH = 3;

            if (acronym.Length != FIX_LENGTH)
            {
                throw new ArgumentException(String.Format(ErrorMessages.InvalidAcronymLenght,acronym));
            }
        }

        public static void CheckIfLoggedUserIsCreatorOrPartOfTeam(string teamName, User invitedUser)
        {
            var loggedUser = Database.GetLoggedUser();
            var team = Database.GetTeamByName(teamName);

            var teamCreator = team.CreatorId;

            var isPartOfTeam = loggedUser.Teams
                .Any(t => t.TeamId == team.TeamId);

            var isAlreadyMember = team.Users
                .Any(u => u.UserId == invitedUser.UserId);

            if (teamCreator != loggedUser.UserId || !isPartOfTeam || isAlreadyMember
                )
            {
                throw new InvalidOperationException(ErrorMessages.NotAllowedException);
            }

        }

        public static void CheckForActiveTeamInvite(User user, Team team)
        {
            var invitations = user.Invitations.ToList();

            var activeInvite = invitations
                .Any(i => i.TeamId == team.TeamId && i.IsActive == true);

            if (activeInvite)
            {
                throw new ArgumentException(ErrorMessages.TeamInviteAlreadySent);
            }
        }

        public static void CheckForInviteFromTeam(User user, Team team)
        {
            var invitations = user.Invitations.ToList();

            var activeInvite = invitations
                .Any(i => i.TeamId == team.TeamId && i.IsActive == true);

            if (!activeInvite)
            {
                throw new ArgumentException(String.Format(ErrorMessages.NoInviteToTeam, team.Name));
            }
        }
        
        public static void CheckIfLoggedUserIsTeamCreator(User user, Team team)
        {
            var isCreator = team.CreatorId == user.UserId;

            if (!isCreator)
            {
                throw new InvalidOperationException(ErrorMessages.NotAllowedException);
            }
        }

        public static void CheckIfUserToKickIsCreator(string username, Team team)
        {
            var userToKick = Database.GetUserByName(username);

            var isCreator = userToKick.UserId == team.CreatorId;

            if (isCreator)
            {
                throw new InvalidOperationException(ErrorMessages.CannotKickCreator);
            }

        }

        public static void CheckIfUserIsPartOfTeam(User user, Team team)
        {
            var isPartOfTeam = team.Users
                .Any(u => u.UserId == user.UserId);

            if (!isPartOfTeam)
            {
                throw new ArgumentException(String.Format(ErrorMessages.UserNotPartOfTeam, user.Username, team.Name));
            }
        }

        public static void CheckIfEventExists(string eventName)
        {
            var isExists = Database.GetEventByName(eventName);

            if (isExists == null)
            {
                throw new ArgumentException(String.Format(ErrorMessages.EvenetDoesntExists, eventName));
            }
        }

        public static void CheckIfLoggedUserIsCreatorOfEvent(string eventName)
        {
            var loggedUser = Database.GetLoggedUser();
            var eventObj = Database.GetEventByName(eventName);

            var isEventCreator = loggedUser.Events
                .Any(e => e.EventId == eventObj.EventId);

            if (!isEventCreator)
            {
                throw new InvalidOperationException(ErrorMessages.NotAllowedException);
            }
        }

        public static void CheckIfTeamIsAlreadyAdddedToEvent(string eventName,string teamName)
        {
            var eventObj = Database.GetEventByName(eventName);
            var team = Database.GetTeamByName(teamName);    
            
            var isAlreadyAdded = eventObj.Teams
                .Any(t => t.TeamId == team.TeamId);

            if (isAlreadyAdded)
            {
                throw new InvalidOperationException(ErrorMessages.AlreadyAddedTeamToEvent);
            }
        }
    }
}
