
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateEventCommand : ICommand
    {
        private readonly IUserService userService;
        //PLUS 2 beacause the hour of the date
        private readonly int EXPECTED_ELEMENTS = 4 + 2;
        public CreateEventCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            
            Validator.CheckForArgumentCount(args, EXPECTED_ELEMENTS);
            Validator.CheckForLoggedUserLogout();

            var eventName = args[0];
            var description = args[1];
            var startDate = args[2] + " " + args[3];
            var endDate = args[4] + " " + args[5];

            Validator.CheckDate(startDate, endDate);

            var validDateFormat = "dd/MM/yyyy HH:mm";
            DateTime validStartDate = DateTime.ParseExact(startDate, validDateFormat, null);
            DateTime validEndDate = DateTime.ParseExact(endDate, validDateFormat, null);

            Validator.CheckStartDateBeforeEndDate(validStartDate, validEndDate);

            userService.CreateEvent(eventName, description, validStartDate, validEndDate);

            var message = String.Format(SuccessMessages.SuccessfullyCreatedEvent, eventName);

            return message;

        }
    }
}
