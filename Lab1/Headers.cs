using System;

namespace Lab1
{
    internal class Headers : StudentManagement
    {
        public static void StudentHeaderWithoutID()
        {
            ColorChangerCaution();
            Console.WriteLine(" **      First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }        
        public static void StudentHeaderWithID()
        {
            ColorChangerCaution();
            Console.WriteLine(" ID    First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void StudentHeaderMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n               STUDENT MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void MainMenuHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n         STUDENT MANAGEMENT MAIN MENU");
            Console.ResetColor();
            Console.WriteLine("*********************************************");
        }
        public static void GradesHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                GRADES MENU");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void AddGradesHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                ADD GRADES");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void AddStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                ADD STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void EditStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n               EDIT STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void DeleteStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n              DELETE STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void NewStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n               ADD NEW STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
    }

    
}
