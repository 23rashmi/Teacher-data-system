using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeacherDataSystem
{
    // Teacher class
    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ClassAndSection { get; set; }
    }

    public class TeacherDataSystem
    {
        private const string FilePath = "teacher_data.txt";

        public static void SaveTeacherData(List<Teacher> teachers)
        {
            using (StreamWriter writer = new StreamWriter("F://MPHASIS DOCUMENTS//TeacherDataSystem//TeacherDataSystem//teacher_data.txt.txt"))
            {
                foreach (var teacher in teachers)
                {
                    writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.ClassAndSection}");
                }
            }
        }

        public static List<Teacher> LoadTeacherData()
        {
            List<Teacher> teachers = new List<Teacher>();

            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader("F://MPHASIS DOCUMENTS//TeacherDataSystem//TeacherDataSystem//teacher_data.txt.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        teachers.Add(new Teacher
                        {
                            ID = int.Parse(values[0]),
                            Name = values[1],
                            ClassAndSection = values[2]
                        });
                    }
                }
            }

            return teachers;
        }

        public static void UpdateTeacherData(List<Teacher> teachers, int teacherId, string newName, string newClassAndSection)
        {
            var teacherToUpdate = teachers.Find(t => t.ID == teacherId);

            if (teacherToUpdate != null)
            {
                teacherToUpdate.Name = newName;
                teacherToUpdate.ClassAndSection = newClassAndSection;
            }
            else
            {
                Console.WriteLine($"Teacher with ID {teacherId} not found.");
            }
        }
    }


    class Program
    {
        static void Main()
        {
            // Load existing data or create a new list
            List<Teacher> teachers = TeacherDataSystem.LoadTeacherData();

            // Display existing teachers
            Console.WriteLine("Existing Teachers:");
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"{teacher.ID}: {teacher.Name} - {teacher.ClassAndSection}");
            }

            // Add a new teacher
            Console.WriteLine("\nAdding a new teacher...");
            Teacher newTeacher = new Teacher { ID = 101, Name = "New Teacher", ClassAndSection = "ClassX" };
            teachers.Add(newTeacher);
            TeacherDataSystem.SaveTeacherData(teachers);

            // Update teacher data
            Console.WriteLine("\nUpdating teacher data...");
            TeacherDataSystem.UpdateTeacherData(teachers, 101, "Updated Teacher", "ClassY");
            TeacherDataSystem.SaveTeacherData(teachers);

            // Display updated teachers
            Console.WriteLine("\nUpdated Teachers:");
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"{teacher.ID}: {teacher.Name} - {teacher.ClassAndSection}");
            }
        }
    }
}
    
