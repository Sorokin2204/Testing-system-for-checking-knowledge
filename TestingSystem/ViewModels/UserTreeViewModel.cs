using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Models;
using TestingSystem.TestingSystemDbContext;

namespace TestingSystem.ViewModels
{
    class UserTreeViewModel : Conductor<Screen>.Collection.OneActive
    {
        private dynamic _selectedItem;

        public UserTreeViewModel()
        {
            Context = new TestingSystemContext();
            Context.Sections.Load();
        }

        public TestingSystemContext Context { get; set; }

        public dynamic SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public void Handle(NavigateToViewModel message)
        {
        
        }

        public void SelectedItemChanged(object obj)
        {
            SelectedItem = obj;
            if (obj is Topic)
            {
                ActivateItem(new UserTopicViewModel(SelectedItem));
            }
        }
    }


}
