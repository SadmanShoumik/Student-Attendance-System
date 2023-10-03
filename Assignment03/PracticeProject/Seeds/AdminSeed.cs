using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Seeds
{
    public static class AdminSeed
    {
        public static List<User> Admin
        {
            get
            {
                return new List<User>()
                {
                    new User{ Id = -1, Name = "AdminName", Username = "admin", Password = "123456", UserType = "Admin" }
                };
            }
        }
    }
}
