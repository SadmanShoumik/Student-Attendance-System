using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject
{
    public class Student
    {
        public int Id { get; set; }
        public Student(int _id)
        {
            Id = _id;
            while (true)
            {
                int currentOption = 1;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("  Student User Account: Choose an Action (Use the Arrow Keys to Navigate):");
                    Console.WriteLine("  1. Check the Courses You Are Enrolled In");
                    Console.WriteLine("  2. Give Attendance in a Course");
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
                    GiveAttendance();
                else if (currentOption == 3)
                    break;
            }
        }

        public void CheckCourse()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            StudentCourse[] studentCourse = context.StudentCourses.Where(x => x.StudentId == Id).ToArray();

            if (studentCourse.Length == 0)
            {
                Console.WriteLine("\nYou Are Not Enrolled in Any Courses.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("The Courses You Are Enrolled in Are as Follows:\n\n");
            int i = 0;
            foreach (StudentCourse sc in studentCourse)
            {
                i++;
                Course course = context.Courses.Where(x => x.Id == sc.CourseId).FirstOrDefault();
                Console.WriteLine($"{i}: Course Name: {course.CourseName}, ID: {course.Id}");
            }

            Console.Write("\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void GiveAttendance()
        {
            Console.Clear();

            Console.WriteLine("Student Attendance Form\n\n");

            DateTime dateTime = DateTime.Now;
            string date = dateTime.Date.ToString("dd/MM/yyyy");
            string day = dateTime.DayOfWeek.ToString();
            string curtime = dateTime.ToString("hh:mm tt");

            PracticeDBContext context = new PracticeDBContext();
            StudentCourse? studentCourse = context.StudentCourses.Where(x => x.StudentId == Id).FirstOrDefault();

            if (studentCourse != null)
            {
                Console.Write("Enter Course Id: ");
                int courseID = int.Parse(Console.ReadLine());

                studentCourse = context.StudentCourses
                    .Where(x => x.StudentId == Id && x.CourseId == courseID)
                    .FirstOrDefault();

                Schedule? schedule = context.Schedules
                    .Where(x => x.CourseId == courseID && x.Day == day && x.StartTime.CompareTo(curtime) <= 0 && x.EndTime.CompareTo(curtime) >= 0)
                    .FirstOrDefault();

                if (studentCourse != null && schedule != null)
                {
                    context.Attendances.Add(new Attendance { CourseId = courseID, StudentId = Id, Date = date });
                    context.SaveChanges();

                    Console.Write("\nAttendance Added Succesfully.\nPress Enter to Continue.");
                    Console.ReadLine();
                    return;
                }
                else if(studentCourse != null && schedule == null)
                {
                    Console.Write("\nError. Check the Schedule for the Class.\nPress Enter to Continue.");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.Write("\nError. You Are Not Enrolled in this Course.\nPress Enter to Continue.");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.Write("\nYou Are Not Enrolled in Any Courses. Enroll in a Course First.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }
        }
    }
}
