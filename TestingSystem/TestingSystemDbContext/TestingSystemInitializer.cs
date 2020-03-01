﻿using System;
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

            Question q1 = new Question { Part = p1 };

            Answer answer1 = new Answer {  IsCorrect = true, Question = q1 };
            Answer answer2 = new Answer {  IsCorrect = false, Question = q1 };

            context.Sections.Add(s1);
            context.Parts.Add(p1);
            context.Topics.Add(t1);
            context.Questions.Add(q1);
            context.Answers.AddRange(new List<Answer> { answer1, answer2});

            context.SaveChanges();
        }
    }
}
