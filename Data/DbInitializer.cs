using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            var players = new Player[]
            {
                new Player { FirstMidName = "Carson",   LastName = "Alexander",
                    ContractDate = DateTime.Parse("2016-09-01") },
                new Player { FirstMidName = "Meredith", LastName = "Alonso",
                    ContractDate = DateTime.Parse("2018-09-01") },
                new Player { FirstMidName = "Arturo",   LastName = "Anand",
                    ContractDate = DateTime.Parse("2019-09-01") },
                new Player { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    ContractDate = DateTime.Parse("2018-09-01") },
                new Player { FirstMidName = "Yan",      LastName = "Li",
                    ContractDate = DateTime.Parse("2018-09-01") },
                new Player { FirstMidName = "Peggy",    LastName = "Justice",
                    ContractDate = DateTime.Parse("2017-09-01") },
                new Player { FirstMidName = "Laura",    LastName = "Norman",
                    ContractDate = DateTime.Parse("2019-09-01") },
                new Player { FirstMidName = "Nino",     LastName = "Olivetto",
                    ContractDate = DateTime.Parse("2011-09-01") }
            };

            context.Players.AddRange(players);
            context.SaveChanges();

            var coaches = new Coach[]
            {
                new Coach { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Coach { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Coach { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Coach { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Coach { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            context.Coaches.AddRange(coaches);
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    CoachID  = coaches.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    CoachID  = coaches.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    CoachID  = coaches.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    CoachID  = coaches.Single( i => i.LastName == "Kapoor").ID }
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var teams = new Team[]
            {
                new Team {TeamID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
                },
                new Team {TeamID = 4022, Title = "Microeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
                },
                new Team {TeamID = 4041, Title = "Macroeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
                },
                new Team {TeamID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Team {TeamID = 3141, Title = "Trigonometry",   Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Team {TeamID = 2021, Title = "Composition",    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
                new Team {TeamID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
            };

            context.Teams.AddRange(teams);
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    CoachID = coaches.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    CoachID = coaches.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    CoachID = coaches.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" },
            };

            context.OfficeAssignments.AddRange(officeAssignments);
            context.SaveChanges();

            var teamCoaches = new TeamAssignment[]
            {
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Chemistry" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Kapoor").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Chemistry" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Harui").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Microeconomics" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Zheng").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Macroeconomics" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Zheng").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Calculus" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Fakhouri").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Trigonometry" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Harui").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Composition" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Abercrombie").ID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.Title == "Literature" ).TeamID,
                    CoachID = coaches.Single(i => i.LastName == "Abercrombie").ID
                    },
            };

            context.TeamAssignments.AddRange(teamCoaches);
            context.SaveChanges();

            var contracts = new Contract[]
            {
                new Contract {
                    PlayerID = players.Single(s => s.LastName == "Alexander").ID,
                    TeamID = teams.Single(c => c.Title == "Chemistry" ).TeamID,
                    Grade = Grade.A
                },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Alexander").ID,
                    TeamID = teams.Single(c => c.Title == "Microeconomics" ).TeamID,
                    Grade = Grade.C
                    },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Alexander").ID,
                    TeamID = teams.Single(c => c.Title == "Macroeconomics" ).TeamID,
                    Grade = Grade.B
                    },
                    new Contract {
                        PlayerID = players.Single(s => s.LastName == "Alonso").ID,
                    TeamID = teams.Single(c => c.Title == "Calculus" ).TeamID,
                    Grade = Grade.B
                    },
                    new Contract {
                        PlayerID = players.Single(s => s.LastName == "Alonso").ID,
                    TeamID = teams.Single(c => c.Title == "Trigonometry" ).TeamID,
                    Grade = Grade.B
                    },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Alonso").ID,
                    TeamID = teams.Single(c => c.Title == "Composition" ).TeamID,
                    Grade = Grade.B
                    },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Anand").ID,
                    TeamID = teams.Single(c => c.Title == "Chemistry" ).TeamID
                    },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Anand").ID,
                    TeamID = teams.Single(c => c.Title == "Microeconomics").TeamID,
                    Grade = Grade.B
                    },
                new Contract {
                    PlayerID = players.Single(s => s.LastName == "Barzdukas").ID,
                    TeamID = teams.Single(c => c.Title == "Chemistry").TeamID,
                    Grade = Grade.B
                    },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Li").ID,
                    TeamID = teams.Single(c => c.Title == "Composition").TeamID,
                    Grade = Grade.B
                    },
                    new Contract {
                    PlayerID = players.Single(s => s.LastName == "Justice").ID,
                    TeamID = teams.Single(c => c.Title == "Literature").TeamID,
                    Grade = Grade.B
                    }
            };

            foreach (Contract e in contracts)
            {
                var contractInDataBase = context.Contracts.Where(
                    s =>
                            s.Player.ID == e.PlayerID &&
                            s.Team.TeamID == e.TeamID).SingleOrDefault();
                if (contractInDataBase == null)
                {
                    context.Contracts.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
