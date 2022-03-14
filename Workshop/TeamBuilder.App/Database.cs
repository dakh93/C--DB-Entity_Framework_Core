using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App
{
    public static class Database
    {
        public static User GetLoggedUser()
        {
            using (var context= new TeamBuilderContext())
            {
                var user = context.Users
                    .Include(u => u.Invitations)
                    .Include(u => u.Events)
                    .Include(u => u.CreatedTeams)
                    .Include(u => u.Teams)
                    .Where(u => u.IsLogged == true)
                    .FirstOrDefault();

                return user;
            }
        }

        public static User GetUserByName(string username)
        {
            using (var context = new TeamBuilderContext())
            {
                var user = context.Users
                    .Include(u => u.Invitations)
                    .Include(u => u.Events)
                    .Include(u => u.CreatedTeams)
                    .Include(u => u.Teams)
                    .Where(u => u.Username == username && u.IsDeleted == false)
                    .FirstOrDefault();

                return user;
            }
            return null;
        }

        public static Team GetTeamByName(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                var team = context.Teams
                    .Include(u => u.Invitations)
                    .Include(u => u.Events)
                    .Include(u => u.Users)
                    .Where(t => t.Name.Equals(teamName) && t.IsDeleted == false)
                    .FirstOrDefault();

                return team;
            }
        }

        public static Event GetEventByName(string eventName)
        {
            using (var context = new TeamBuilderContext())
            {
                var eventObj = context.Event
                    .Include(u => u.Teams)
                    .Where(e => e.Name == eventName)
                    .FirstOrDefault();

                return eventObj;
            }
        }
    }
}
