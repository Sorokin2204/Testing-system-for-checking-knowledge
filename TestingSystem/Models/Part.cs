using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestingSystem.Models
{

    public class Part
    {
        public int PartId { get; set; }
        public string Title { get; set; }

        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
        public Part()
        {
            Topics = new ObservableCollection<Topic>();
        }
    }
}
