using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lab1
{
    internal class DataManagement : StudentManagement
    {
        public static List<StudentInfo> DataReader()
        {
            List<StudentInfo> studentList = new List<StudentInfo>();
            var data = File.ReadAllLines("studentmanagement.csv").ToList();
            foreach (var person in data)
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
        public static void ViewStudentInfoWithoutID()
        {
            Headers.StudentHeaderWithoutID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {
                Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", "||", student.studentFirstName, student.studentLastName,
                    student.studentGrade));
            }
        }
        public static void ViewStudentInfoWithID()
        {
            Headers.StudentHeaderWithID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {
                Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                student.studentFirstName, student.studentLastName, student.studentGrade));
            }
        }
        public static void ViewStudentInfoAfterChange(string studentID)
        {
            Headers.StudentHeaderWithID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {
                if (student.studentID == int.Parse(studentID))
                {
                    ColorChangerWarning();
                    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                    student.studentFirstName, student.studentLastName, student.studentGrade));
                    Console.ResetColor();
                }
                else if (student.studentID != int.Parse(studentID))
                {
                    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                    student.studentFirstName, student.studentLastName, student.studentGrade));
                }
            }
            ColorChangerWarning();
            Console.Write(" \n Data Updated! Press any key to continue.");
            Console.ReadKey();
            Console.ResetColor();
        }
        public static void ViewStudentInfoDuringUpdate(string studentID)
        {
            Headers.StudentHeaderWithID();
            List<StudentInfo> studentList = DataReader();
            foreach (var student in studentList)
            {
                if (student.studentID == int.Parse(studentID))
                {
                    ColorChangerWarning();
                    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                    student.studentFirstName, student.studentLastName, student.studentGrade));
                    Console.ResetColor();
                }
                //else if (student.studentID != int.Parse(studentID))
                //{
                //    Console.WriteLine(string.Format(" {0,-6}{1,-15}{2,-16}{3,5}", student.studentID,
                //    student.studentFirstName, student.studentLastName, student.studentGrade));
                //}
            }
        }
        public static void OverallGradeInformation()
        {
            List<StudentInfo> studentList = DataManagement.DataReader();
            int studentListCount = studentList.Count, topStudentCount =0, topStudentHelp = 0, bottomStudentCount = 0,
                bottomStudentHelp = 0;
            double studentListGrades = 0;

            foreach (var student in studentList) // Adds all the student grades in list studentList
            {
                studentListGrades += student.studentGrade;
            }
            double averageGrade = Math.Round(studentListGrades / studentListCount, 2);
            ColorChangerWarning();
            Console.Write("\n Class Average: ");
            Console.ResetColor();
            Console.Write(averageGrade);
            ColorChangerWarning();
            int maxGrade = studentList.Max(x => x.studentGrade);
            Console.Write("\n Top Grade ({0}) earned by: ", maxGrade);
            foreach (var student in studentList)
            {
                if(student.studentGrade == maxGrade)
                {
                    topStudentCount++;
                }
            }
            Console.ResetColor();
            foreach (var student in studentList)
            {
                if(student.studentGrade == maxGrade)
                {
                    topStudentHelp++;                    
                    if (topStudentHelp < topStudentCount)
                    {
                        Console.Write(student.studentFirstName + " " + student.studentLastName.Substring(0, 1) + "., ");
                    }
                    else if (topStudentCount == 1)
                    {
                        Console.Write(student.studentFirstName + " " + student.studentLastName.Substring(0, 1) + ".");
                    }
                    else
                    {
                        Console.Write("and " + student.studentFirstName + " " + student.studentLastName.Substring(0, 1) + ".");
                    }
                }
            }

            ColorChangerWarning();            
            int minGrade = studentList.Min(x => x.studentGrade);
            Console.Write("\n Bottom Grade ({0}) earned by: ", minGrade);
            foreach (var student in studentList)
            {
                if (student.studentGrade == minGrade)
                {
                    bottomStudentCount++;
                }
            }
            Console.ResetColor();            
            foreach (var student in studentList)
            {
                if (student.studentGrade == minGrade)
                {
                    bottomStudentHelp++;
                    if (bottomStudentHelp < bottomStudentCount)
                    {
                        Console.WriteLine(student.studentFirstName + " " + student.studentLastName.Substring(0, 1) + "., ");
                    }
                    else if (bottomStudentCount == 1)
                    {
                        Console.WriteLine(student.studentFirstName + " " + student.studentLastName.Substring(0, 1) + ".");
                    }
                    else
                    {
                        Console.WriteLine("and " + student.studentFirstName + " " + student.studentLastName.Substring(0, 1) + ".");
                    }
                }
            }
            Console.ResetColor();
        }
        public static string GetStudentID(string studentIDEntry, int studentListCountTest)
        {
            while (true)
            {
                try
                {
                    if (studentIDEntry == "q")
                    {
                        studentIDEntry = "q";
                        return studentIDEntry;
                    }
                    if (int.TryParse(studentIDEntry, out int testVariable) == true &&
                            (int.Parse(studentIDEntry) < 0 ||
                            int.Parse(studentIDEntry) > studentListCountTest) ||
                            int.TryParse(studentIDEntry, out testVariable) == false)
                    {
                        ColorChangerWarning();
                        Console.Write(" You have made an invalid input.Please try again: ");
                        studentIDEntry = Console.ReadLine().Trim().ToLower();
                        throw new Exception();
                    }
                    else
                    {
                        return studentIDEntry;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void AddEditGrades()
        {
            List<StudentInfo> studentList = DataReader();
            int studentListCount = studentList.Count, studentListCountTest = studentList.Max(x => x.studentID);                
            string studentIDEntry, studentGradeEntry, studentID;
            
            Console.Clear();
            Headers.AddGradesHeader();
            ViewStudentInfoWithID();
            ColorChangerWarning();
            Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
            ColorChangerCaution();
            Console.Write(" Type the Student ID for which you want to add or edit a grade to: ");
            studentIDEntry = Console.ReadLine().Trim().ToLower();
            studentID = GetStudentID(studentIDEntry, studentListCountTest);   
            
            Console.Clear();
            Headers.AddGradesHeader();
            ViewStudentInfoDuringUpdate(studentID);
            ColorChangerWarning();
            Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
            Console.ResetColor();
            ColorChangerCaution();
            Console.Write(" Type in the new grade: ");
            ColorChangerWarning();
            studentGradeEntry = Console.ReadLine().Trim().ToLower();

            while (true) // Add/Edit Grade
            {
                try
                {
                    ColorChangerWarning();
                    Console.ResetColor();
                    if (studentGradeEntry == "q")
                    {
                        return;
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
                            if (student.studentID == int.Parse(studentID))
                            {
                                studentList[studentList.IndexOf(student)].studentGrade = int.Parse(studentGradeEntry);
                                WriteToFile(studentList);
                                Console.Clear();
                                Headers.AddGradesHeader();
                                ViewStudentInfoAfterChange(studentID);
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
        public static void AddStudent()
        {            
            List<StudentInfo> studentList = DataManagement.DataReader();
            int studentListCount = studentList.Count, studentListCountTest = studentList.Max(x => x.studentID),
                newStudentID = 0;
            string studentID, newStudentFirstName = "", newStudentLastName = "";

            Console.Clear();
            Headers.AddStudentHeader();
            ViewStudentInfoWithoutID();
            ColorChangerCaution();
            newStudentID = studentListCountTest + 1;
            studentID = newStudentID.ToString();
            Console.Write("\n Enter the First Name: ");
            ColorChangerWarning();
            newStudentFirstName = Console.ReadLine();
            newStudentFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newStudentFirstName.ToLower());
            ColorChangerCaution();
            Console.Write(" Enter the Last Name: ");
            ColorChangerWarning();
            newStudentLastName = Console.ReadLine();
            newStudentLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newStudentLastName.ToLower());

            Console.ResetColor();
            studentList.Add(new StudentInfo { studentID = newStudentID,
                studentFirstName = newStudentFirstName,
                studentLastName = newStudentLastName});
            WriteToFile(studentList);
            Console.Clear();
            Headers.AddStudentHeader();
            ViewStudentInfoAfterChange(studentID);
        }
        public static void EditStudent()
        {
            List<StudentInfo> studentList = DataManagement.DataReader();
            int studentListCount = studentList.Count, studentListCountTest = studentList.Max(x => x.studentID);
            string studentID, studentIDEntry, editStudentFirstName = "", editStudentLastName = "";

            Console.Clear();
            Headers.EditStudentHeader();
            ViewStudentInfoWithID();
            ColorChangerWarning();
            Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
            Console.ResetColor();
            ColorChangerCaution();
            Console.Write("\n Type the Student ID of the student you want to edit: ");
            studentIDEntry = Console.ReadLine().Trim().ToLower();
            studentID = GetStudentID(studentIDEntry, studentListCountTest);

            if (studentID == "q")
            {
                return;
            }
            else
            {
                Console.Clear();
                Headers.EditStudentHeader();
                ViewStudentInfoDuringUpdate(studentID);
                ColorChangerCaution();
                Console.Write("\n Enter the First Name: ");
                ColorChangerWarning();
                editStudentFirstName = Console.ReadLine();
                editStudentFirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editStudentFirstName.ToLower());
                ColorChangerCaution();
                Console.Write(" Enter the Last Name: ");
                ColorChangerWarning();
                editStudentLastName = Console.ReadLine();
                editStudentLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editStudentLastName.ToLower());
                foreach (StudentInfo student in studentList)
                {
                    if (student.studentID == int.Parse(studentID))
                    {
                        studentList[studentList.IndexOf(student)].studentFirstName = editStudentFirstName;
                        studentList[studentList.IndexOf(student)].studentLastName = editStudentLastName;
                    }
                }
            }
            WriteToFile(studentList);
            Console.Clear();
            Headers.EditStudentHeader();
            ViewStudentInfoAfterChange(studentID);
        }
        public static void DeleteStudent()
        {
            List<StudentInfo> studentList = DataManagement.DataReader();
            int studentListCount = studentList.Count, studentListCountTest = studentList.Max(x => x.studentID);
            string studentID, studentIDEntry, deleteStudent = "";

            Console.Clear();
            Headers.DeleteStudentHeader();
            ViewStudentInfoWithID();
            ColorChangerWarning();
            Console.WriteLine("\n Type \"Q\" to cancel this operation: ");
            Console.ResetColor();
            ColorChangerCaution();
            Console.Write("\n Type the Student ID of the student you want to delete: ");
            studentIDEntry = Console.ReadLine().Trim().ToLower();
            studentID = GetStudentID(studentIDEntry, studentListCountTest);

            if (studentID == "q")
            {
                return;
            }
            else
            {
                Console.Clear();
                Headers.DeleteStudentHeader();
                ViewStudentInfoDuringUpdate(studentID);
                foreach (StudentInfo student in studentList)
                {
                    if (student.studentID == int.Parse(studentID))
                    {
                        ColorChangerWarning();
                        Console.WriteLine("\n Are you sure you want to delete this student? ");
                        do
                        {
                            ColorChangerWarning();
                            Console.Write(" Type \"Yes\" to proceed or \"No\" to Cancel: ");
                            ColorChangerCaution();
                            deleteStudent = Console.ReadLine().ToLower();
                            if (deleteStudent == "yes")
                            {
                                studentList.RemoveAt(studentList.IndexOf(student));
                                WriteToFile(studentList);
                                Console.Clear();
                                Headers.DeleteStudentHeader();
                                ViewStudentInfoWithID();
                                ColorChangerWarning();
                                Console.Write("\n Student has been deleted. Press any button to continue.");
                                Console.ReadKey();
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
            }
        }
    }
}
