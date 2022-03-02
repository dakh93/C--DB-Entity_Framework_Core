using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.App.Command.Contracts
{
    internal interface ICommand
    {
        string Execute(params string[] args);
    }
}
