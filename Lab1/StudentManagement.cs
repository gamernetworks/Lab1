using System;
using System.Collections.Generic;

namespace Lab1
{
    internal class StudentManagement
        // Copyright 2022, Manuel A. Montalvo, All rights reserved.
    {
        public class StudentInfo
        {
            public int studentID;
            public string studentFirstName;
            public string studentLastName;
            public int studentGrade;
            public StudentInfo()
            {
                studentID = 0;
                studentFirstName = "";
                studentLastName = "";
                studentGrade = 0;
            }
            public StudentInfo(int studentID, string studentFirstName, string studentLastName, int studentGrade)
            {
                this.studentID = studentID;
                this.studentFirstName = studentFirstName;
                this.studentLastName = studentLastName;
                this.studentGrade = studentGrade;
            }
        }
        public static void ErrorMessage()
        {
            ColorChangerWarning();
            Console.Write(" You have made an invalid input.Please try again: ");
        }
        public static int ReadKeyInputTest()
        {
            int selectionResult;
            var selectionInput = Console.ReadKey(); // Take a Key Input from User
            if (char.IsDigit(selectionInput.KeyChar))
            {
                // If the KeyInput can be converted into an int, then:
                selectionResult = int.Parse(selectionInput.KeyChar.ToString());
                return selectionResult;
            }
            else
            {
                return 0;
            }
        }
        public static void ColorChangerCaution()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void ColorChangerWarning()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }        
        static void Main(string[] args)
        {
            bool mainMenuLoop = true;
            while (mainMenuLoop == true)
            {
                mainMenuLoop = Menus.MainMenu(mainMenuLoop);
            }   
        }
    }
}
