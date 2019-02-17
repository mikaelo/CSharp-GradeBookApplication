using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            var threshold = (int)(Math.Ceiling(Students.Count * 0.2));
            var grades = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToArray();

            var gradesRange = new List<(char, int)>()
            {
                ('A', threshold * 1),
                ('B', threshold * 2),
                ('C', threshold * 3),
                ('D', threshold * 4),
            };
            
            foreach(var kvp in gradesRange)
            {
                if (grades[kvp.Item2 - 1] <= averageGrade)
                    return kvp.Item1;
            }

            return 'F';
        }
    }
}
