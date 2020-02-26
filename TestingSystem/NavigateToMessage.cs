using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem
{
    public enum NavigationToEnum
    {
        Login,
        Admin,
        User
    }

    public sealed class NavigateToViewModel
    {
        public NavigateToViewModel(NavigationToEnum navigationTo)
        {
            NavigateTo = navigationTo;
        }

        public NavigationToEnum NavigateTo { get; }
    }
       
}

