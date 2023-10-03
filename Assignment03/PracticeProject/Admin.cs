using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject
{
    public class Admin
    {
        public int Id { get; set; }

        public Admin(int _id)
        {
            Id = _id;
            while(true)
            {
                int currentOption = 1;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("  Admin User Account: Choose an Action (Use the Arrow Keys to Navigate):");
                    Console.WriteLine("  1. Create a Student User Account");
                    Console.WriteLine("  2. Create a Teacher User Account");
                    Console.WriteLine("  3. Create a Course");
                    Console.WriteLine("  4. Create a Schedule for a Course");
                    Console.WriteLine("  5. Assign a Student to a Course");
                    Console.WriteLine("  6. Assign a Teacher to a Course");
                    Console.WriteLine("  7. LogOut");


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
                            currentOption = 7;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        currentOption++;
                        if (currentOption > 7)
                        {
                            currentOption = 1;
                        }
                    }
                }

                if (currentOption == 1)
                    CreateStudent();
                else if (currentOption == 2)
                    CreateTeacher();
                else if (currentOption == 3)
                    CreateCourse();
                else if (currentOption == 4)
                    AssignScheduleCourse();
                else if (currentOption == 5)
                    AssignStudentCourse();
                else if (currentOption == 6)
                    AssignTeacherCourse();
                else if (currentOption == 7)
                    break;
            }
        }

        public void CreateStudent()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            Console.WriteLine("Creating a New Student Account\n\n");
            Console.Write("Enter the Name of the Student: ");
            string name = Console.ReadLine();
            Console.Write("\nEnter an Username for the Student Account: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter a Password for the Student Account: ");
            string password = Console.ReadLine();
            string usertype = "Student";

            User exist = context.Users.Where(x => x.Username == username).FirstOrDefault();
            if(exist != null)
            {
                Console.WriteLine("\nThis Username Exists. Try a Different Username.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\n\nIs the Information Correct? (Yes/No): ");
            string confirm = Console.ReadLine();
            if (confirm == "No")
            {
                Console.WriteLine("\nData Insertion Cancelled.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            context.Users.Add(new User { Name = name, Username = username, Password = password, UserType = usertype });
            context.SaveChanges();

            Console.Write("\nThe Student User Account was Added Succesfully.\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void CreateTeacher()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            Console.WriteLine("Creating a New Teacher Account\n\n");
            Console.Write("Enter the Name of the Teacher: ");
            string name = Console.ReadLine();
            Console.Write("\nEnter an Username for the Teacher Account: ");
            string username = Console.ReadLine();
            Console.Write("\nEnter a Password for the Teacher Account: ");
            string password = Console.ReadLine();
            string usertype = "Teacher";

            User exist = context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (exist != null)
            {
                Console.WriteLine("\nThis Username Exists. Try a Different Username.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\n\nIs the Information Correct? (Yes/No): ");
            string confirm = Console.ReadLine();
            if (confirm == "No")
            {
                Console.WriteLine("\nData Insertion Cancelled.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            context.Users.Add(new User { Name = name, Username = username, Password = password, UserType = usertype });
            context.SaveChanges();

            Console.Write("\nThe Teacher User Account was Added Succesfully.\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void CreateCourse()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            Console.WriteLine("Creating a New Course\n\n");
            Console.Write("Enter the Name of the Course: ");
            string courseName = Console.ReadLine();
            Console.Write("\nEnter the Course Fee: ");
            int fees = int.Parse(Console.ReadLine());
            Console.Write("\nEnter the Course Duration (No. of Classes): ");
            int totalClass = int.Parse(Console.ReadLine());

            Course exist = context.Courses.Where(x => x.CourseName == courseName).FirstOrDefault();
            if (exist != null)
            {
                Console.WriteLine("\nThis Course Already Exists.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\n\nIs the Information Correct? (Yes/No): ");
            string confirm = Console.ReadLine();
            if (confirm == "No")
            {
                Console.WriteLine("\nData Insertion Cancelled.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            context.Courses.Add(new Course { CourseName = courseName, Fees = fees, TotalClass = totalClass});
            context.SaveChanges();

            Console.Write("\nThe Course was Added Succesfully.\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void AssignStudentCourse()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            Console.WriteLine("Assigning a Student to a Course\n\n");

            Console.Write("Enter the Username of the Student: ");
            string userName = Console.ReadLine();
            User student = context.Users.Where(x => x.Username == userName).FirstOrDefault();
            if (student == null)
            {
                Console.WriteLine("\nThis Username Does Not Exist.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter the Name of the Course: ");
            string courseName = Console.ReadLine();
            Course course = context.Courses.Where(x => x.CourseName == courseName).FirstOrDefault();
            if (course == null)
            {
                Console.WriteLine("\nThis Course Does Not Exist.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            StudentCourse studentCourse = context.StudentCourses.Where(x => x.CourseId == course.Id && x.StudentId == student.Id).FirstOrDefault();
            if (studentCourse != null)
            {
                Console.WriteLine("\nThis Student is Already Enrolled in This Course.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\n\nIs the Information Correct? (Yes/No): ");
            string confirm = Console.ReadLine();
            if (confirm == "No")
            {
                Console.WriteLine("\nData Insertion Cancelled.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            context.StudentCourses.Add(new StudentCourse { CourseId = course.Id, StudentId = student.Id });
            context.SaveChanges();

            Console.Write("\nThe Student Was Enrolled in This Course Succesfully.\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void AssignTeacherCourse()
        {
            Console.Clear();

            PracticeDBContext context = new PracticeDBContext();

            Console.WriteLine("Assigning a Teacher to a Course\n\n");

            Console.Write("Enter the Username of the Teacher: ");
            string userName = Console.ReadLine();
            User teacher = context.Users.Where(x => x.Username == userName).FirstOrDefault();
            if (teacher == null)
            {
                Console.WriteLine("\nThis Username Does Not Exist.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter the Name of the Course: ");
            string courseName = Console.ReadLine();
            Course course = context.Courses.Where(x => x.CourseName == courseName).FirstOrDefault();
            if (course == null)
            {
                Console.WriteLine("\nThis Course Does Not Exist.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\n\nIs the Information Correct? (Yes/No): ");
            string confirm = Console.ReadLine();
            if (confirm == "No")
            {
                Console.WriteLine("\nData Insertion Cancelled.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            context.TeacherCourses.Add(new TeacherCourse { CourseId = course.Id, TeacherId = teacher.Id });
            context.SaveChanges();

            Console.Write("\nThe Teacher Was Assigned to This Course Succesfully.\nPress Enter to Continue.");
            Console.ReadLine();
        }

        public void AssignScheduleCourse()
        {
            Console.Clear();

            Console.WriteLine("Assigning a Schedule for a Course\n\n");

            PracticeDBContext context = new PracticeDBContext();

            Console.Write("\nEnter the Course Name: ");
            string courseName = Console.ReadLine();

            Course course = context.Courses.Where(x => x.CourseName == courseName).FirstOrDefault();
            if (course == null)
            {
                Console.WriteLine("\nThis Course Does Not Exist. Create an Entry First.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\nEnter the Day of the Week: ");
            string day = Console.ReadLine();

            Console.Write("\nEnter the Class Start Time (Ex.09:00 PM/09:00 AM): ");
            string startTimeString = Console.ReadLine();

            Console.Write("\nEnter the Class End Time (Ex. 09:00 PM/09:00 AM): ");
            string endTimeString = Console.ReadLine();

            Schedule? exist = context.Schedules
                .Where(x => x.CourseId == course.Id && x.Day == day && x.StartTime == startTimeString)
                .FirstOrDefault();
            if (exist != null)
            {
                Console.WriteLine("\nThis Schedule for this Course Already Exists.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("\n\nIs the Information Correct? (Yes/No): ");
            string confirm = Console.ReadLine();
            if(confirm == "No")
            {
                Console.WriteLine("\nData Insertion Cancelled.\nPress Enter to Continue.");
                Console.ReadLine();
                return;
            }

            context.Schedules
                .Add(new Schedule { CourseId = course.Id, Day = day, StartTime = startTimeString, EndTime = endTimeString});
            context.SaveChanges();

            Console.WriteLine($"\nSuccesfully Assigned a New Schedule for the Course: {courseName}\nPress Enter to Continue.");
            Console.ReadLine();
        }
    }
}
