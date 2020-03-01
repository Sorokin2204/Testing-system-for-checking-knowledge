using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Models;
using TestingSystem.Configs;

namespace TestingSystem.TestingSystemDbContext
{
        public class TestingSystemContext : DbContext
    {
        static TestingSystemContext()
        {
            Database.SetInitializer(new TestingSystemInitializer());
        }

        public TestingSystemContext(string connectionString = "DefaultConnectionString")
            : base(connectionString)
        {

        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new SectionConfig());
            modelBuilder.Configurations.Add(new PartConfig());
            modelBuilder.Configurations.Add(new TopicConfig());
            modelBuilder.Configurations.Add(new QuestionConfig());
            modelBuilder.Configurations.Add(new AnswerConfig());
        }
    }
}
