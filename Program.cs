using DocumentFormat.OpenXml.Wordprocessing;
using Role_Management;
using System;

public class Program
{
    private static UserManager userManager;
    private static EvenOdd evenOdd;

    public static void Main(string[] args)
    {
        userManager = new UserManager();
        userManager.CreateUser("System", "Admin", "sysadmin", "adMIN123/", 0);
        userManager.CreateUser("User", "Local", "user", "usER123/", 1);
        
        evenOdd = new EvenOdd();
        Login();
    }

    private static void ShowAdminMenu()
    {
        Console.WriteLine("==BASIC AUTHENTICATION==");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. Show User");
        Console.WriteLine("3. Search User");
        Console.WriteLine("4. Logout");
        Console.WriteLine("5. Exit");
        Console.Write("Input: ");
    }

    private static int GetUserChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("ERROR : Input Not Valid");
        }

        return choice;
    }

    private static void CreateUserMenu()
    {
        Console.Clear();
        string firstName;
        do
        {
            Console.Write("First Name: ");
            firstName = Console.ReadLine();
            if (firstName.Length < 2)
            {
                Console.WriteLine("Name has to be at least consisting 2 characters or more.");
            }
        } while (firstName.Length < 2);

        string lastName;
        do
        {
            Console.Write("Last Name: ");
            lastName = Console.ReadLine();
            if (lastName.Length < 2)
            {
                Console.WriteLine("Name has to be at least consisting 2 characters or more.");
            }
        } while (lastName.Length < 2);

        string username;
        username = $"{firstName.ToLower()}";

        string password;
        bool isValidPassword;
        do
        {
            Console.Write("Password: ");
            password = Console.ReadLine();
            isValidPassword = userManager.ValidatePassword(password);
            if (!isValidPassword)
            {
                Console.WriteLine("Password must have at least 8 characters with at least one Capital letter, one lower case letter, one symbol or one number.");
            }
        } while (!isValidPassword);

        int role;
        bool isValidRole;
        do
        {
            Console.Write("Role (0 for Admin, 1 for User): ");
            role = Convert.ToInt32(Console.ReadLine());
            isValidRole = userManager.ValidateRole(role);
            if (!isValidRole)
            {
                Console.WriteLine("Role must 0 for Admin and 1 for Users");
            }
        } while (!isValidRole);

        if (userManager.CreateUser(firstName, lastName, username, password, role))
        {
            Console.WriteLine("User Success to Created!!!");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Failed to created user");
            Console.ReadLine();
        }
    }


    private static void UsersManagementMenu()
    {
        bool exitMenu = false;

        do
        {
            Console.Clear();
            userManager.ShowUsers();

            Console.WriteLine("Menu:");
            Console.WriteLine("1. Edit User");
            Console.WriteLine("2. Delete User");
            Console.WriteLine("3. Show/Hide Password");
            Console.WriteLine("4. Back");

            Console.Write("Input : ");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.WriteLine();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    EditUserMenu();
                    break;
                case ConsoleKey.D2:
                    DeleteUserMenu();
                    break;
                case ConsoleKey.D3:
                    TogglePasswordVisibility();
                    break;
                case ConsoleKey.D4:
                    exitMenu = true;
                    break;
                default:
                    Console.WriteLine("ERROR : Input Not Valid");
                    break;
            }
        } while (!exitMenu);
    }

    private static void EditUserMenu()
    {
        int userID;
        bool isValidID = false;

        do
        {
            Console.Write("ID yang ingin diubah: ");
            string input = Console.ReadLine();

            isValidID = int.TryParse(input, out userID);

            if (!isValidID)
            {
                Console.WriteLine("ERROR : Input Not Valid");
            }
        } while (!isValidID);

        User user = userManager.GetUserByID(userID);
        if (user != null)
        {
            string firstName;
            do
            {
                Console.Write("Enter First Name: ");
                firstName = Console.ReadLine();
                if (firstName.Length < 2)
                {
                    Console.WriteLine("Name has to be at least consisting 2 characters or more.");
                }
            } while (firstName.Length < 2);

            string lastName;
            do
            {
                Console.Write("Enter Last Name: ");
                lastName = Console.ReadLine();
                if (lastName.Length < 2)
                {
                    Console.WriteLine("Name has to be at least consisting 2 characters or more.");
                }
            } while (lastName.Length < 2);

            string password;
            bool isValidPassword;
            do
            {
                Console.Write("Enter Password: ");
                password = Console.ReadLine();
                isValidPassword = userManager.ValidatePassword(password);
                if (!isValidPassword)
                {
                    Console.WriteLine("Password must have at least 8 characters with at least one Capital letter, one lower case letter, one symbol and one number.");
                }
            } while (!isValidPassword);

            if (userManager.UpdateUser(userID, firstName, lastName, password))
            {
                Console.WriteLine("User Success to Edited!!!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Failed to update user");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("User not found.");
            Console.ReadLine();
        }
    }


    private static void DeleteUserMenu()
    {
        int userID;
        bool isValidID = false;

        do
        {
            Console.Write("Masukan ID yang ingin dihapus: ");
            string input = Console.ReadLine();

            isValidID = int.TryParse(input, out userID);

            if (!isValidID)
            {
                Console.WriteLine("ERROR : Input Not Valid");
            }
        } while (!isValidID);

        if (userManager.DeleteUser(userID))
        {
            Console.WriteLine("User deleted successfully.");
        }
        else
        {
            Console.WriteLine("User not found.");
        }

        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }



    private static void SearchUsersMenu()
    {
        Console.Clear();
        Console.WriteLine("==Cari Akun==");
        Console.Write("Masukan Nama : ");
        string searchName = Console.ReadLine();
        userManager.SearchUsers(searchName);

        Console.ReadLine();
    }

    private string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Enter)
            {
                password += "*";
                Console.Write("*");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        //Console.WriteLine();
        return password;
    }

    public static void Login()
    {
        Console.Clear();
        Console.WriteLine("==LOGIN==");
        Console.Write("USERNAME: ");
        string username = Console.ReadLine();

        Console.Write("PASSWORD: ");

        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
            {
                password += key.KeyChar.ToString();
                Console.Write("*");
            }
            if (key.Key == ConsoleKey.Backspace && password.Length != 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        if (userManager.LoginUser(username, password))
        {
            if (userManager.GetRole(username, password) == 0)
            {
                int choice;
                do
                {
                    Console.Clear();
                    ShowAdminMenu();
                    choice = GetUserChoice();

                    switch (choice)
                    {
                        case 1:
                            CreateUserMenu();
                            break;
                        case 2:
                            UsersManagementMenu();
                            break;
                        case 3:
                            SearchUsersMenu();
                            break;
                        case 4:
                            Login();
                            break;
                        case 5:
                            ExitMenu();
                            break;
                        default:
                            Console.WriteLine("ERROR : Input Not Valid");
                            break;
                    }

                    Console.WriteLine();
                } while (choice != 5);
            }
            else
            {
                evenOdd.Menu();
            }
        }
        else
        {
            Console.WriteLine("\nMESSAGE : Username atau Password Tidak Ditemukan");
            Console.ReadLine();
            Login();
        }

        Console.ReadLine();
    }

    private static void TogglePasswordVisibility()
    {
        userManager.TogglePasswordVisibility();
    }


    private static void ExitMenu()
    {
        Console.WriteLine("Exiting the program...");
        Environment.Exit(0);
    }
}
