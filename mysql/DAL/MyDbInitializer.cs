using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyApplication.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MyApplication.DAL
{
    public class MyDbInitializer :  DropCreateDatabaseAlways<MyDbContext> //CreateDatabaseIfNotExists<MyDbContext>
    {
        public override void InitializeDatabase(MyDbContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
            string.Format("DROP TABLE IF EXISTS `enrollment`",
                            context.Database.Connection.Database));

            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("DROP TABLE IF EXISTS `student`;" +
                              "CREATE TABLE `student` (" +
                              "`Id` int(11) NOT NULL," +
                              "`LastName` longtext," +
                              "`FirstName` longtext," +
                              "`EnrollmentDate` datetime NOT NULL," +
                              "PRIMARY KEY(`Id`)" +
                              ") ENGINE = InnoDB DEFAULT CHARSET = utf8", 
                            context.Database.Connection.Database));

            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("DROP TABLE IF EXISTS `course`;" + 
                              "CREATE TABLE `course` (" +
                              "`Id` int(11) NOT NULL," +
                              "`Title` longtext," +
                              "`Credits` int(11) NOT NULL, " +
                              "PRIMARY KEY(`Id`)" +
                            ") ENGINE = InnoDB DEFAULT CHARSET = utf8",
                            context.Database.Connection.Database));
            context.SaveChanges();

            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
            string.Format("CREATE TABLE `enrollment` (" +
                          "`Id` int(11) NOT NULL," +
                          "`CourseId` int(11) NOT NULL," +
                          "`StudentId` int(11) NOT NULL," +
                          "`Grade` tinytext," +
                          "FOREIGN KEY (`StudentId`) REFERENCES `student`(`Id`)" +
                          " ON DELETE CASCADE ON UPDATE CASCADE," +
                          "FOREIGN KEY (`CourseId`) REFERENCES `course`(`Id`)" +
                          " ON DELETE CASCADE ON UPDATE CASCADE," +
                          "PRIMARY KEY(`Id`)" +
                          ") ENGINE = InnoDB DEFAULT CHARSET = utf8",
                            context.Database.Connection.Database));

            context.SaveChanges();

            Seed(context);
        }
        protected override void Seed(MyDbContext context)
        {
            var students = new List<Student>
            {
                new Student{Id=1,FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{Id=2,FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{Id=3,FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{Id=4,FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{Id=5,FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{Id=6,FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{Id=7,FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{Id=8,FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{Id=1050,Title="Chemistry",Credits=3,},
                new Course{Id=4022,Title="Microeconomics",Credits=3,},
                new Course{Id=4041,Title="Macroeconomics",Credits=3,},
                new Course{Id=1045,Title="Calculus",Credits=4,},
                new Course{Id=3141,Title="Trigonometry",Credits=4,},
                new Course{Id=2021,Title="Composition",Credits=3,},
                new Course{Id=2042,Title="Literature",Credits=4,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                //new Enrollment{Id=1,StudentId=1,CourseId=1050},
                //new Enrollment{Id=2,StudentId=1,CourseId=4022},
                //new Enrollment{Id=3,StudentId=1,CourseId=4041},
                //new Enrollment{Id=4,StudentId=2,CourseId=1045},
                //new Enrollment{Id=5,StudentId=2,CourseId=3141},
                //new Enrollment{Id=6,StudentId=2,CourseId=2021},
                //new Enrollment{Id=7,StudentId=3,CourseId=1050},
                //new Enrollment{Id=8,StudentId=4,CourseId=1050},
                //new Enrollment{Id=9,StudentId=4,CourseId=4022},
                //new Enrollment{Id=10,StudentId=5,CourseId=4041},
                //new Enrollment{Id=11,StudentId=6,CourseId=1045},
                //new Enrollment{Id=12,StudentId=7,CourseId=3141}

                new Enrollment{Id=1,StudentId=1,CourseId=1050, Grade=Grade.A},
                new Enrollment{Id=2,StudentId=1,CourseId=4022},
                new Enrollment{Id=3,StudentId=1,CourseId=4041, Grade=Grade.B},
                new Enrollment{Id=4,StudentId=2,CourseId=1045, Grade=Grade.C},
                new Enrollment{Id=5,StudentId=2,CourseId=3141},
                new Enrollment{Id=6,StudentId=2,CourseId=2021, Grade=Grade.A},
                new Enrollment{Id=7,StudentId=3,CourseId=1050, Grade=Grade.B},
                new Enrollment{Id=8,StudentId=4,CourseId=1050},
                new Enrollment{Id=9,StudentId=4,CourseId=4022, Grade=Grade.C},
                new Enrollment{Id=10,StudentId=5,CourseId=4041},
                new Enrollment{Id=11,StudentId=6,CourseId=1045},
                new Enrollment{Id=12,StudentId=7,CourseId=3141}
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}