using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestingSystem.Models;
using TestingSystem.TestingSystemDbContext;

namespace TestingSystem.ViewModels
{
    class AdminTreeViewModel : Conductor<Screen>.Collection.OneActive
    {
        private dynamic _selectedItem;

        public AdminTreeViewModel()
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
                NotifyOfPropertyChange(() => CanSelectedItemDelete);
                NotifyOfPropertyChange(() => CanSelectedItemAdd);
            }
        }

        public void SelectedItemChanged(object obj)
        {
            IsInEditMode = false;
            SelectedItem = obj;
            if (obj is Topic)
            {
                ActivateItem(new AdminTopicViewModel(SelectedItem));
            }
        }

        public void SelectedItemDelete()
        {
            if (SelectedItem is Section)
            {
                Context.Sections.Remove(SelectedItem);
            }
            else
            if (SelectedItem is Part)
            {
                Context.Parts.Remove(SelectedItem);
            }
            else
            if (SelectedItem is Topic)
            {
                Context.Topics.Remove(SelectedItem);
            }
            Context.SaveChanges();
        }

        public bool CanSelectedItemDelete
        {
            get
            {
                if (SelectedItem is Section)
                {
                    return Context.Sections.Local.Count() > 1 ? true : false;
                }
                else
                if (SelectedItem is Part)
                {
                    var sectionId = SelectedItem.SectionId;
                    return Context.Parts.Local.Count(part => part.SectionId == sectionId) > 1 ? true : false;
                }
                else
                if (SelectedItem is Topic)
                {
                    var partId = SelectedItem.PartId;
                    return Context.Topics.Local.Count(topic => topic.PartId == partId) > 1 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SelectedItemAdd()
        {
            if (SelectedItem is Section)
            {
                var countSection = Context.Sections.Count();
                var newTitleSection = $"Section ({++countSection})";

                var newSection = new Section() { Title = newTitleSection };
                var newPart = new Part() { Title = "Part 1", Section = newSection };
                var newTopic = new Topic { Title = "Topic 1", Part = newPart };

                Context.Sections.Add(newSection);
                Context.Parts.Add(newPart);
                Context.Topics.Add(newTopic);
            }
            else
            if (SelectedItem is Part)
            {
                int sectionId = SelectedItem.SectionId;
                var countParts = Context.Parts.Count(part => part.SectionId == sectionId);
                var newTitlePart = $"Part ({++countParts})";

                var newPart = new Part() { Title = newTitlePart, SectionId = sectionId };
                var newTopic = new Topic { Title = "Topic 1", Part = newPart };

                Context.Parts.Add(newPart);
                Context.Topics.Add(newTopic);
            }
            else
            if (SelectedItem is Topic)
            {
                int partId = SelectedItem.PartId;
                var countTopics = Context.Topics.Count(topic => topic.PartId == partId);
                var newTitleTopic = $"Topic ({++countTopics})";

                var newTopic = new Topic { Title = newTitleTopic, PartId = partId };

                Context.Topics.Add(newTopic);
            }
            Context.SaveChanges();
        }

        public bool CanSelectedItemAdd
        {
            get {
                return SelectedItem != null ? true : false;
            }
        }


        bool isInEditMode = false;
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                isInEditMode = value;
                NotifyOfPropertyChange(() => isInEditMode);

            }
        }

        public void MouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (FindTreeItem(e.OriginalSource as DependencyObject).IsSelected)
            {
                IsInEditMode = true;
                e.Handled = true;

            }
        }

        static TreeViewItem FindTreeItem(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source as TreeViewItem;
        }

        public void IsVisibleChanged(object sender)
        {
            var tb = sender as TextBox;
            if (tb.IsVisible)
            {
                tb.Focus();
                tb.SelectAll();
            }
        }

        public void LostFocus(object obj)
        {
            IsInEditMode = false;
            SelectedItem.Title = obj.ToString();
            Context.SaveChanges();
        }
    }
}
