using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Models;

namespace TestingSystem.TestingSystemDbContext
{
    class TestingSystemInitializer : DropCreateDatabaseAlways<TestingSystemContext>
    {
        protected override void Seed(TestingSystemContext context)
        {
            Section s1 = new Section { Title = "Section (1)" };
            Part p1 = new Part { Title = "Part (1)", Section = s1  };
            Topic t1 = new Topic { Title = "Topic (1)", Part = p1 };
            context.Parts.Add(p1);
            context.Sections.Add(s1);
            context.Topics.Add(t1);


            context.SaveChanges();
        }
    }
}
