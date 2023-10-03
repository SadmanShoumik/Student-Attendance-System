using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject
{
    public class Teacher
    {
        public int Id { get; set; }
        public Teacher(int _id)
        {
            Id = _id;
            while (true)
            {
                int currentOption = 1;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("  Teacher User Account: Choose an Action (Use the Arrow Keys to Navigate):");
                    Console.WriteLine("  1. Check the Courses You are Assigned To");
                    Console.WriteLine("  2. Check the Attendance Report of a Course");
                    Console.WriteLine("  3. LogOut");


                    Console.SetCursorPosition(0, currentOption);
                    Console.Write('>');

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        currentOption--;
                        if (currentOption < 1)
                        {
                            currentOption = 3;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        currentOption++;
                        if (currentOption > 3)
                        {
                            currentOption = 1;
                        }
                    }
                }

                if (currentOption == 1)
                    CheckCourse();
                else if (currentOption == 2)
                    AttendanceReport();
                else if (currentOption == 3)
                    break;
            }
        }

        public void CheckCourse()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            TeacherCourse[] teacherCourse = context.TeacherCourses.Where(x => x.TeacherId == Id).ToArray();

            if (teacherCourse.Length == 0)
            {
                Console.WriteLine("\nYou Are Not Assigned to Any Courses Yet.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("The Courses You Teach Are as Follows:\n\n");
            int i = 0;
            foreach (TeacherCourse tc in teacherCourse)
            {
                i++;
                Course course = context.Courses.Where(x => x.Id == tc.CourseId).FirstOrDefault();
                Console.WriteLine($"{i}. Course Name: {course.CourseName}, ID: {course.Id}");
            }

            Console.Write("\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void AttendanceReport()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            Console.WriteLine("Checking the Attendance Report of a Course:\n\n");

            Console.Write("Enter the Course ID: ");
            int courseId = int.Parse(Console.ReadLine());
            TeacherCourse? teacherCourse = context.TeacherCourses.Where(x => x.CourseId == courseId && x.TeacherId == Id).FirstOrDefault();
            if (teacherCourse == null)
            {
                Console.WriteLine("\nYou Do Not Teach This Course.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Attendance[] attendance = context.Attendances
                .OrderBy(x => x.Id)
                .Where(x => x.CourseId == courseId)
                .ToArray();
            HashSet<string> dates = new HashSet<string>();
            foreach (Attendance att in attendance)
            {
                dates.Add(att.Date);
            }

            StudentCourse[] studentCourse = context.StudentCourses
                .Where(x => x.CourseId == courseId)
                .ToArray();
            HashSet<int> studentIds = new HashSet<int>();
            foreach (StudentCourse sc in studentCourse)
            {
                studentIds.Add(sc.StudentId);
            }

            Console.Write("\n\nName                                    ");
            foreach (string date in dates)
            {
                Console.Write($"{date}     ");
            }
            Console.WriteLine();

            foreach (int id in studentIds)
            {
                int length = 40;
                User student = context.Users.Where(x => x.Id == id).FirstOrDefault();

                Console.Write(student.Name);
                length = length - student.Name.Length;
                while (length > 0)
                {
                    Console.Write(' ');
                    length--;
                }

                foreach (string date in dates)
                {

                    Attendance? attendance1 = context.Attendances
                    .Where(x => x.StudentId == student.Id && x.Date == date && x.CourseId == courseId).FirstOrDefault();

                    if (attendance1 == null)
                    {
                        Console.Write("    (X)        ");
                    }
                    else
                    {
                        Console.Write("    (V)        ");
                    }
                }
                Console.WriteLine();
            }

            Console.Write("\nPress Enter to Continue.");
            Console.ReadLine();
        }
    }
}
