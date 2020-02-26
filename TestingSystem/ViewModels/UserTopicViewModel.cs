using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Models;

namespace TestingSystem.ViewModels
{
    class UserTopicViewModel : Screen
    {
        public UserTopicViewModel(Topic topic)
        {
            Topic = topic;
        }

        public Topic Topic { get; set; }

    }
}
