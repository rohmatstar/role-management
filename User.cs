using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Role_Management
{
    public class User
    {
        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public int Role { get; private set; }

        public User(int id, string firstName, string lastName, string userName, string password, int role)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Role = role;
        }

        public string GetName()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetUsername()
        {
            return UserName;
        }

        public int GetUserRole()
        {
            return Role;
        }

        public string GetHiddenPassword()
        {
            return new string('*', Password.Length);
        }

        public void UpdateUser(string firstName, string lastName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

    }

}
