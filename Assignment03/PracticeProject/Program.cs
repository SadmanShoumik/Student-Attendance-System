using PracticeProject;
using System.Globalization;

while (true)
{
    Console.Clear();

    Console.WriteLine("Welcome to the Attendance System Login Page\n\n");
    Console.Write("Enter Your Username: ");
    string username = Console.ReadLine();
    Console.Write("Enter Your Password: ");
    string password = Console.ReadLine();

    PracticeDBContext context = new PracticeDBContext();
    User? user = context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

    if (user == null)
    {
        Console.WriteLine("\n\nLogin Failed.\nWrong Username or Password.\nOr Contact the Admin if You Have Not Yet Registered.\n");
        Console.WriteLine("\nPress Enter to Continue.");
        Console.ReadLine();
        Console.Clear();
        continue;
    }
    switch (user.UserType)
    {
        case "Admin":
            Admin admin = new Admin(user.Id);
            break;
        case "Student":
            Student student = new Student(user.Id);
            break;
        case "Teacher":
            Teacher teacher = new Teacher(user.Id);
            break;
        default:
            break;
    }
}