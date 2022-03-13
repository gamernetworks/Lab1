using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Lab1
{
    internal class StudentManagement
        // Copyright 2022, Manuel A. Montalvo, All rights reserved.
    {
        private static int mainMenuSelection;
        private static int gradesMenuSelection;
        private static int studentsMenuSelection;
        private static int studentSelection;
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
        public static void ViewStudentInfoWithoutID()
        {
            StudentHeaderWithoutID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {
                Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}","||", student.studentFirstName, student.studentLastName,
                    student.studentGrade));
            }
        }
        public static void ViewStudentInfoWithID()
        {
            StudentHeaderWithID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {   
                    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                    student.studentFirstName, student.studentLastName, student.studentGrade));
            }
        }
        public static void ViewStudentInfoUpdated(int studentID)
        {
            Console.Clear();
            GradesHeader();
            StudentHeaderWithID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {
                if (student.studentID == studentID)
                {
                    ColorChangerWarning();
                    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                    student.studentFirstName, student.studentLastName, student.studentGrade));
                    Console.ResetColor();
                }
                else if (student.studentID != studentID)
                {
                    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                    student.studentFirstName, student.studentLastName, student.studentGrade));
                }
            }            
            ColorChangerWarning();
            Console.Write(" \n Grade Updated. Press any key to continue.");
            Console.ReadKey();
            Console.ResetColor();
        }
        public static List<StudentInfo> DataReader()
        {
            List<StudentInfo> studentList = new List<StudentInfo>();
            var data = File.ReadAllLines("studentmanagement.csv").ToList();
            foreach(var person in data)
            {
                var split = person.Split(',');
                StudentInfo student = new StudentInfo();
                student.studentID = int.Parse(split[0]);
                student.studentFirstName = split[1];
                student.studentLastName = split[2];
                try
                {
                    student.studentGrade = int.Parse(split[3]);
                }
                catch
                {
                    student.studentGrade = 0;
                }

                studentList.Add(student);
            }
            return studentList;
        }
        public static void OverallGradeInformation()
        {
            List<StudentInfo> studentList = DataReader();
            int studentListCount = studentList.Count; // Counts the amount of elelemnts in a list
            double studentListGrades = 0;
            foreach (var student in studentList) // Adds all the student grades in list studentList
            {
                studentListGrades += student.studentGrade;
            }
            double averageGrade = Math.Round(studentListGrades / studentListCount, 2);
            ColorChanger();
            Console.WriteLine("\n Class Average: {0}", averageGrade);
            int maxGrade = studentList.Max(x => x.studentGrade);
            Console.WriteLine(" Top Grade: {0}",maxGrade);
            int minGrade = studentList.Min(x => x.studentGrade);
            Console.WriteLine(" Bottom Grade: {0}", minGrade);
            Console.ResetColor();
        }
        public static int GetStudentID(string ID, int studentCount)
        {
            int studentID;
            while (true)
            {
                try
                {
                    if (ID == "q")
                    {
                        break;
                    }
                    else if (int.TryParse(ID, out int testVariable) == true &&
                            (int.Parse(ID) < 0 ||
                            int.Parse(ID) > studentCount) ||
                            int.TryParse(ID, out testVariable) == false)
                    {
                        ColorChangerWarning();
                        Console.Write(" You have made an invalid input.Please try again: ");
                        ID = Console.ReadLine().Trim().ToLower();
                        throw new Exception();
                    }
                    else
                    {
                        studentID = int.Parse(ID);
                        return studentID;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return 0;
        }
        public static void WriteToFile(List<StudentInfo> list)
        {
            List<string> updatedStudentStringLines = new List<string>();
            foreach (StudentInfo student in list)
            {
                updatedStudentStringLines.Add($"{student.studentID},{student.studentFirstName}," +
                    $"{student.studentLastName},{student.studentGrade}");
            }
            // Writes CSV file                
            File.WriteAllLines("studentmanagement.csv", updatedStudentStringLines);

        }
        public static void ErrorMessage()
        {
            ColorChangerWarning();
            Console.Write(" You have made an invalid input.Please try again: ");
        }
        public static void AddEditNewGrades(List<StudentInfo> studentList, int studentID, string studentGradeEntry)
        {
            while (true)
            {
                try
                {
                    ColorChangerWarning();                    
                    Console.ResetColor();
                    if (studentGradeEntry == "q")
                    {
                        break;
                    }
                    if ((int.Parse(studentGradeEntry) < 0 || int.Parse(studentGradeEntry) > 100) ||
                        int.TryParse(studentGradeEntry, out int testVariable) == false)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        foreach (StudentInfo student in studentList)
                        {
                            if (student.studentID == studentID)
                            {
                                studentList[studentList.IndexOf(student)].studentGrade = int.Parse(studentGradeEntry);
                                WriteToFile(studentList);
                                ViewStudentInfoUpdated(studentID);
                                break;
                            }
                        }
                        break;
                    }
                }
                catch
                {
                    ColorChangerWarning();
                    Console.Write(" You have made an invalid input.Please try again: ");
                    studentGradeEntry = Console.ReadLine().Trim().ToLower();
                    continue;
                }
            }
        }
        public static void Grades()
        {
            List<StudentInfo> studentList = DataReader();
            int studentListCount = studentList.Count, studentID, studentListCountTest = studentList.Max(x => x.studentID);
            string studentIDEntry, studentGradeEntry;

            ColorChangerWarning();
            Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
            Console.ResetColor();
            ColorChanger();
            Console.Write("\n Type the Student ID for which you want to add or edit a grade to: ");
            ColorChangerWarning();
            studentIDEntry = Console.ReadLine().Trim().ToLower();
            studentID = GetStudentID(studentIDEntry, studentListCountTest);
            Console.ResetColor();
            ColorChanger();
            Console.Write(" Type in the new grade: ");
            ColorChangerWarning();
            studentGradeEntry = Console.ReadLine().Trim().ToLower();
            AddEditNewGrades(studentList, studentID, studentGradeEntry);                    
        }
        public static bool Students()
        {
            List<StudentInfo> studentList = DataReader();
            int studentListCount = studentList.Count, studentListCountTest = studentList.Max(x => x.studentID), studentID = 0, newStudentID;
            string newStudentFirstName = "", newStudentLastName = "", editStudentFirstName = "", editStudentLastName = "", deleteStudent = "";
            bool quitOption = false;
            while (true)
            {
                try
                {
                    Console.Clear();
                    StudentMenu();
                    ViewStudentInfoWithoutID();
                    Console.WriteLine("\n" +
                        "\n           1. Add New Student" +
                        "\n           2. Edit Student" +
                        "\n           3. Delete Student" +
                        "\n           4. Return to Previous Menu" +
                        "\n           5. Quit");
                    studentSelection = int.Parse(Console.ReadLine());
                    if (studentSelection == 1) // Add Student
                    {
                        Console.Clear();
                        StudentMenu();
                        ViewStudentInfoWithoutID();
                        ColorChanger();
                        newStudentID = studentListCountTest + 1;
                        Console.Write("\n Enter the First Name: ");
                        ColorChangerWarning();
                        newStudentFirstName = Console.ReadLine();
                        newStudentFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newStudentFirstName.ToLower());
                        ColorChanger();
                        Console.Write(" Enter the Last Name: ");
                        ColorChangerWarning();
                        newStudentLastName = Console.ReadLine();
                        newStudentLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newStudentLastName.ToLower());
                        Console.ResetColor();
                        studentList.Add(new StudentInfo { studentID = newStudentID, studentFirstName = newStudentFirstName,
                            studentLastName = newStudentLastName });

                        List<string> updatedStudentStringLines = new List<string>();
                        foreach (StudentInfo student in studentList)
                        {
                            updatedStudentStringLines.Add($"{student.studentID},{student.studentFirstName}," +
                                $"{student.studentLastName},{student.studentGrade}");
                        }
                        studentListCountTest = studentList.Max(x => x.studentID);
                        File.WriteAllLines("studentmanagement.csv", updatedStudentStringLines);
                        Console.ResetColor();
                    }
                    else if (studentSelection == 2) // Edit student
                    {
                        Console.Clear();
                        StudentMenu();
                        ViewStudentInfoWithID();
                        ColorChangerWarning();
                        Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
                        Console.ResetColor();
                        ColorChanger();
                        Console.Write("\n Type the Student ID of the student you want to edit: ");
                        while (true) // Takes Student ID
                        {
                            try
                            {
                                ColorChangerWarning();
                                string studentIDEntry = Console.ReadLine().Trim().ToLower();
                                Console.ResetColor();
                                if (studentIDEntry == "q")
                                {
                                    break;
                                }
                                else if (int.TryParse(studentIDEntry, out int testVariable) == true &&
                                        (int.Parse(studentIDEntry) < 0 ||
                                        int.Parse(studentIDEntry) > studentListCountTest) ||
                                        int.TryParse(studentIDEntry, out testVariable) == false)
                                {
                                    ColorChangerWarning();
                                    Console.Write(" You have made an invalid input.Please try again: ");
                                    throw new Exception();
                                }
                                else
                                {
                                    studentID = int.Parse(studentIDEntry);
                                    Console.Clear();
                                    StudentMenu();
                                    ViewStudentInfoWithID();
                                    ColorChanger();
                                    Console.Write("\n Enter the First Name: ");
                                    ColorChangerWarning();
                                    editStudentFirstName = Console.ReadLine();
                                    editStudentFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editStudentFirstName.ToLower());
                                    ColorChanger();
                                    Console.Write(" Enter the Last Name: ");
                                    ColorChangerWarning();
                                    editStudentLastName = Console.ReadLine();
                                    editStudentLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editStudentLastName.ToLower());
                                    foreach (StudentInfo studenta in studentList)
                                    {
                                        if (studenta.studentID == int.Parse(studentIDEntry))
                                        {
                                            studentList[studentList.IndexOf(studenta)].studentFirstName = editStudentFirstName;
                                            studentList[studentList.IndexOf(studenta)].studentLastName = editStudentLastName;
                                            // Converts list items into string so it can be passed into a CSV file
                                            List<string> updatedStudentStringLines = new List<string>();
                                            foreach (StudentInfo studentb in studentList)
                                            {
                                                updatedStudentStringLines.Add($"{studentb.studentID},{studentb.studentFirstName}," +
                                                    $"{studentb.studentLastName},{studentb.studentGrade}");
                                            }
                                            // Writes CSV file                
                                            File.WriteAllLines("studentmanagement.csv", updatedStudentStringLines);
                                            Console.ResetColor();
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    else if (studentSelection == 3) // delete student
                    {
                        Console.Clear();
                        StudentMenu();
                        ViewStudentInfoWithID();
                        ColorChangerWarning();
                        Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
                        Console.ResetColor();
                        ColorChanger();
                        Console.Write("\n Type the Student ID of the student you want to delete: ");
                        while (true)
                        {
                            try
                            {
                                ColorChangerWarning();
                                string studentIDEntry = Console.ReadLine().Trim().ToLower();
                                Console.ResetColor();
                                if (studentIDEntry == "q")
                                {
                                    break;
                                }
                                else if (int.TryParse(studentIDEntry, out int testVariable) == true &&
                                        (int.Parse(studentIDEntry) < 0 ||
                                        int.Parse(studentIDEntry) > studentListCountTest) ||
                                        int.TryParse(studentIDEntry, out testVariable) == false)
                                {
                                    ColorChangerWarning();
                                    Console.Write(" You have made an invalid input.Please try again: ");
                                    throw new Exception();
                                }
                                else
                                {
                                    foreach (StudentInfo studenta in studentList)
                                    {
                                        if (studenta.studentID == int.Parse(studentIDEntry))
                                        {
                                            ColorChanger();
                                            Console.WriteLine(" Are you sure you want to delete this student? ");
                                            do
                                            {
                                                ColorChangerWarning();
                                                Console.Write(" Type Yes to proceed. Type No to Cancel: ");
                                                ColorChanger();
                                                deleteStudent = Console.ReadLine().ToLower();
                                                if (deleteStudent == "yes")
                                                {
                                                    Console.Write(" Student has been deleted. Press any button to continue.");
                                                    Console.ReadKey();
                                                    studentList.RemoveAt(studentList.IndexOf(studenta));
                                                    List<string> updatedStudentStringLines = new List<string>();
                                                    foreach (StudentInfo studentb in studentList)
                                                    {
                                                        updatedStudentStringLines.Add($"{studentb.studentID},{studentb.studentFirstName}," +
                                                            $"{studentb.studentLastName},{studentb.studentGrade}");
                                                    }
                                                    File.WriteAllLines("studentmanagement.csv", updatedStudentStringLines);
                                                    Console.ResetColor();
                                                    break;
                                                }
                                                else if (deleteStudent == "no")
                                                {
                                                    break;
                                                }
                                            } while (deleteStudent != "no" || deleteStudent != "yes");
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    else if (studentSelection == 4) // Return to Main Menu
                    {
                        break;
                    }
                    else if (studentSelection == 5) // Quit application
                    {
                        quitOption = true;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return quitOption;
        }
        public static void ColorChanger()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void ColorChangerWarning()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void StudentHeaderWithoutID()
        {
            ColorChanger();
            Console.WriteLine(" **      First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void StudentHeaderWithID()
        {
            ColorChanger();
            Console.WriteLine(" ID    First Name     Last Name       Grade ");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static void StudentMenu()
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
        public static void NewStudentHeader()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n               ADD NEW STUDENT");
            Console.ResetColor();
            Console.WriteLine("********************************************");
        }
        public static bool MainMenu(bool mainMenuLoop)
        {
            while(true)
            {
                try
                {
                    if(mainMenuSelection < 1 || mainMenuSelection > 3)
                    {
                        Console.Clear();
                        MainMenuHeader();
                        Console.WriteLine("\n\n" +
                            "\n                 1. Grades" +
                            "\n                 2. Students" +
                            "\n                 3. Exit");
                        mainMenuSelection = int.Parse(Console.ReadLine());
                    }
                    if (mainMenuSelection == 1) // GOTO Grades
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                GradesHeader();
                                OverallGradeInformation();
                                Console.WriteLine("\n" +
                                    "\n           1. Add/Edit Grades" +
                                    "\n           2. Go to Students Menu" +
                                    "\n           3. Return to Main Menu" +
                                    "\n           4. Quit");
                                gradesMenuSelection = int.Parse(Console.ReadLine());
                                if (gradesMenuSelection == 1) // GOTO Add/Edit Grades
                                {
                                    Console.Clear();
                                    GradesHeader();
                                    ViewStudentInfoWithID();
                                    Grades();
                                }
                                else if (gradesMenuSelection == 2) // GOTO Students Menu
                                {
                                    mainMenuSelection = 2;
                                    mainMenuLoop = true;
                                    break;
                                }
                                else if (gradesMenuSelection == 3) // GOTO Main Menu
                                {
                                    mainMenuSelection = 0;
                                    mainMenuLoop = true;
                                    break;
                                }
                                else if (gradesMenuSelection == 4) // Exit Program
                                {
                                    mainMenuLoop = false;
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    else if (mainMenuSelection == 2) // GOTO Student
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                StudentMenu();
                                ViewStudentInfoWithoutID();
                                Console.WriteLine("\n" +
                                    "\n           1. Add/Edit/Delete Students" +
                                    "\n           2. Go to Grades Menu" +
                                    "\n           3. Return to Main Menu" +
                                    "\n           4. Quit");
                                    studentsMenuSelection = int.Parse(Console.ReadLine());
                                if (studentsMenuSelection == 1) // GOTO Add/Edit/Delete Students
                                {
                                    Console.Clear();
                                    NewStudentHeader();
                                    ViewStudentInfoWithID();
                                    bool quitOption = Students();
                                    if (quitOption == true)
                                    {
                                        mainMenuLoop = false;
                                        break;
                                    }
                                }
                                else if (studentsMenuSelection == 2) // GOTO Main Menu
                                {
                                    mainMenuSelection = 1;
                                    mainMenuLoop = true;
                                    break;
                                }
                                else if (studentsMenuSelection == 3) // GOTO Main Menu
                                {
                                    mainMenuSelection = 0;
                                    mainMenuLoop = true;
                                    break;
                                }
                                else if (studentsMenuSelection == 4) // Exit Program
                                {
                                    mainMenuLoop = false;
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
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
        static void Main(string[] args)
        {
            bool mainMenuLoop = true;
            while (mainMenuLoop == true)
            {
                mainMenuLoop = MainMenu(mainMenuLoop);
            }   
        }
    }
}
