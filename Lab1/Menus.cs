using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Menus : StudentManagement 
    {
        public static bool mainMenuLoop;
        public static int mainMenuSelection;
        public static int gradesMenuSelection;
        public static int studentsMenuSelection;
        public static bool MainMenu(bool mainMenuLoop)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Headers.MainMenuHeader();
                    Console.WriteLine("\n\n" +
                        "\n                 1. Grades" +
                        "\n                 2. Students" +
                        "\n                 3. Exit");
                    if (mainMenuSelection < 1 || mainMenuSelection > 3)
                    {
                        mainMenuSelection = ReadKeyInputTest();
                    }
                    if (mainMenuSelection == 1) // GOTO Grades
                    {
                        mainMenuLoop = GradesMenu();
                    }
                    else if (mainMenuSelection == 2) // GOTO Student
                    {
                        mainMenuLoop = StudentsMenu();
                    }
                    else if (mainMenuSelection == 3) // Exit Program
                    {
                        mainMenuLoop = false;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
                break;
            }
            return mainMenuLoop;
        }
        public static bool GradesMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Headers.GradesHeader();
                    DataManagement.ViewStudentInfoWithoutID();
                    DataManagement.OverallGradeInformation();
                    Console.WriteLine("\n" +
                        "\n           1. Add/Edit Grades" +
                        "\n           2. Go to Students Menu" +
                        "\n           3. Return to Main Menu" +
                        "\n           4. Quit");
                    if (gradesMenuSelection < 1 || gradesMenuSelection > 4)
                    {
                        gradesMenuSelection = ReadKeyInputTest();
                    }
                    if (gradesMenuSelection == 1) // GOTO Add/Edit Grades
                    {
                        DataManagement.AddEditGrades();
                        gradesMenuSelection = 0;
                    }
                    else if (gradesMenuSelection == 2) // GOTO Students Menu
                    {
                        gradesMenuSelection = 0;
                        mainMenuSelection = 2;                        
                        break;
                    }
                    else if (gradesMenuSelection == 3) // GOTO Main Menu
                    {
                        gradesMenuSelection = 0;
                        mainMenuSelection = 0;
                        //mainMenuLoop = true;                        
                        break;
                    }
                    else if (gradesMenuSelection == 4) // Exit Program
                    {
                        gradesMenuSelection = 0;
                        mainMenuLoop = false;                        
                        return mainMenuLoop;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }        
        public static bool StudentsMenu()
        {            
            while (true)
            {
                try
                {
                    Console.Clear();
                    Headers.StudentHeaderMenu();
                    DataManagement.ViewStudentInfoWithoutID();
                    Console.WriteLine("\n" +
                        "\n           1. Add New Student" +
                        "\n           2. Edit Student" +
                        "\n           3. Delete Student" +
                        "\n           4. Go to Grades Menu" +
                        "\n           5. Return to Main Menu" +
                        "\n           6. Quit");
                    if (studentsMenuSelection < 1 || studentsMenuSelection > 6)
                    {
                        studentsMenuSelection = ReadKeyInputTest();
                    }
                    if (studentsMenuSelection == 1) // Add Student
                    {
                        DataManagement.AddStudent();
                        studentsMenuSelection = 0;
                    }
                    else if (studentsMenuSelection == 2) // Edit Student
                    {
                        DataManagement.EditStudent();
                        studentsMenuSelection = 0;
                    }
                    else if (studentsMenuSelection == 3) // Delete Student
                    {
                        DataManagement.DeleteStudent();
                        studentsMenuSelection = 0;
                    }
                    else if (studentsMenuSelection == 4) // GOTO Grades Menu
                    {
                        studentsMenuSelection = 0;
                        mainMenuSelection = 1;
                        //mainMenuLoop = true;
                        break;
                    }
                    else if (studentsMenuSelection == 5) // Return to Main Menu
                    {
                        studentsMenuSelection = 0;
                        mainMenuSelection = 0;
                        //mainMenuLoop = true;
                        break;
                    }
                    else if (studentsMenuSelection == 6) // Quit application
                    {
                        mainMenuSelection = 0;
                        studentsMenuSelection = 0;
                        mainMenuLoop = false;
                        return mainMenuLoop;
                    }                
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }
    }
}
