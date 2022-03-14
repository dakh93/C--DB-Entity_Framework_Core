
namespace TeamBuilder.App
{
    public class ErrorMessages
    {
        public const string InvalidArgsCount = "Invalid arguments count!";

        public const string InvalidAgeFormat = "Age not valid!";

        public const string InvalidGenderFormat = "Gender should be either \"Male\" or \"Female\"!";

        public const string InvalidUsername = "Username {0} not valid!";

        public const string ExistingUsername = "Username {0} is already taken!";

        public const string InvalidPassword = "Password {0} is not valid!";

        public const string PasswordMismatch = "Passwords do not match!";

        public const string AlreadyLoggedUser = "You should logout first!";

        public const string InvalidCommand = "Command {0} not valid!";

        public const string UsernameLoginFail = "Invalid username or password!";

        public const string NoLoggedUser = "You should login first!";

        public const string InvalidDateTimeFormat = "Please insert the dates in format: [{0}]!";

        public const string StartDateShouldBeBeforeEndDate = "Start date should be before end date.";

        public const string ExistingTeam = "Team {0} exists!";

        public const string InvalidAcronymLenght = "Acronym {0} not valid!";

        public const string NotAllowedException = "Not allowed!";

        public const string TeamInviteAlreadySent = "Invite is already sent!";

        public const string NoInviteToTeam= "Invite from {0} is not found!";

        public const string CannotKickCreator = "Command not allowed. Use DisbandTeam instead.";

        public const string UserNotPartOfTeam = "User {0} is not a member in {1}!";

        public const string TeamDoesntExistsWhenDisband = "Team {0} not found!";

        public const string EvenetDoesntExists = "Event {0} not found!";

        public const string AlreadyAddedTeamToEvent = "Cannot add same team twice!";

    }
}
