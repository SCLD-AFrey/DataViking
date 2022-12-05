using DataViking.Cli.Helpers;

namespace DataViking.Cli.Routines;

public static class Common
{
    public static void DisplayMenu(MenuMode p_mode)
    {

        switch (p_mode)
        {
            case MenuMode.MainMenu:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;

        }

        switch (p_mode)
        {
            case MenuMode.MainMenu:
                Console.WriteLine("Main Menu");
                Console.WriteLine("---------");
                Console.WriteLine("A - Administration");
                Console.WriteLine("D - Data Administration");
                Console.WriteLine("U - User Management");
                Console.WriteLine("X - Exit");
                break;
            case MenuMode.UserMenu:
                Console.WriteLine("User Menu");
                Console.WriteLine("---------");
                Console.WriteLine("A - Add User");
                Console.WriteLine("E - Edit User");
                Console.WriteLine("G - Get User");
                Console.WriteLine("M - Main Menu");
                Console.WriteLine("X - Exit");
                break;
            case MenuMode.AdminMenu:
                Console.WriteLine("Administration Menu");
                Console.WriteLine("---------");
                Console.WriteLine("X - Exit");
                break;
                break;
            case MenuMode.DataMenu:
                Console.WriteLine("Data Administration Menu");
                Console.WriteLine("---------");
                Console.WriteLine("X - Exit");
                break;
        }
            
            
            

        Console.ResetColor();
    }
    public static void DisplayMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}