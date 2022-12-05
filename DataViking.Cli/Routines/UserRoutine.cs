using DataViking.Cli.Helpers;
using DataViking.Data;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DataViking.Cli.Routines;

public static class UserRoutine
{
    public static void Run()
        {

            ConsoleKey key = ConsoleKey.M;

            while (!key.Equals(ConsoleKey.X))
            {
                switch (key)
                { 
                    case ConsoleKey.A: case ConsoleKey.Add:
                        AddUser();
                        break;
                    case ConsoleKey.E:
                        EditUser();
                        break;
                    case ConsoleKey.G:
                        GetUser();
                        break;
                case ConsoleKey.M:
                        Common.DisplayMenu(MenuMode.UserMenu);
                        break;
                    default:
                        Common.DisplayError("Invalid option");
                        break;
                }

                Console.Write("> ");
                key = Console.ReadKey().Key;
                Console.WriteLine();
            }

        }

    private static UserProfile Finduser()
    {
        UnitOfWork _uow = new UnitOfWork()
        {
            ConnectionString = Properties.Resources.MainConnection,
            AutoCreateOption = AutoCreateOption.DatabaseAndSchema
        };
        Console.Write("Enter User Name or Oid: ");
        var ident = Console.ReadLine();

        int oid = 0;
        UserProfile p;
        if (int.TryParse(ident, out oid))
        {
            p = new XPQuery<UserProfile>(_uow).FirstOrDefault(i => i.Oid == oid);
        }
        else
        {
            p = new XPQuery<UserProfile>(_uow).FirstOrDefault(i => i.UserName == ident);
        }

        if (p == null)
        {
            Common.DisplayError("User not found");
            return null;
        }

        return p;

    }

    private static void GetUser()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        UserProfile p = Finduser();
            
        Console.WriteLine($"FOUND USER:  ({p.Oid}) {p.UserName} {p.FirstName} {p.LastName}");
        Console.ResetColor();
    }

    private static void EditUser()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        UnitOfWork _uow = new UnitOfWork()
            {
                ConnectionString = Properties.Resources.MainConnection,
                AutoCreateOption = AutoCreateOption.DatabaseAndSchema
            };
            UserProfile p = Finduser();

            Console.WriteLine($@"FOUND USER:  ({p.Oid}) {p.UserName} {p.FirstName} {p.LastName}");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            
            if(string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("No changes made");
                return;
            }
            
            using(_uow) {
                p.FirstName = string.IsNullOrEmpty(firstName) ? p.FirstName : firstName;
                p.LastName = string.IsNullOrEmpty(lastName) ? p.LastName : lastName;
                _uow.CommitChanges();
            }
            Common.DisplayMessage($"User '{p.UserName}' edited successfully");
            
            Console.ResetColor();
        }

        private static void AddUser()
        {
            UnitOfWork _uow = new UnitOfWork()
            {
                ConnectionString = Properties.Resources.MainConnection,
                AutoCreateOption = AutoCreateOption.DatabaseAndSchema
            };
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("User Name: ");
            var userName = Console.ReadLine()?.Trim().ToLower();
            Console.Write("First Name: ");
            var firstName = Console.ReadLine()?.Trim();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine()?.Trim();

            try
            {
                using(_uow) {

                    var p = new XPQuery<UserProfile>(_uow).FirstOrDefault(i => i.UserName == userName.ToLower());
                    if (p != null)
                    {
                        throw new Exception("Username already exists");
                    }
                    new UserProfile(_uow)
                    {
                        UserName = userName.ToLower(),
                        FirstName = firstName,
                        LastName = lastName
                    };
                    _uow.CommitChanges();
                    Common.DisplayMessage($"User '{userName}' added successfully");
                }
            }
            catch (Exception e)
            {
                Common.DisplayError(e.Message);
            }
            Console.ResetColor();
        }


        

}