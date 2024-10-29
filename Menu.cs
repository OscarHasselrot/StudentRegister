using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StudentRegister
{
    public class Menu
    {
        StudentDbContext dbCtx;
        DbOptions options;
        ConsoleKeyInfo keyPressed;

        string[] mainMenuChoices = { "Lägg till student", "Ändra en student", "Alla studenter" };
        string[] changeStudentChoices = { "Ändra förnamn ", "Ändra efternamn", "Ändra stad" };


        public Menu()
        {
            this.dbCtx = new StudentDbContext();
            this.options = new DbOptions(dbCtx);
            MainMenu();
        }
        public void AddStudent()
        {
            Console.Clear();
            Console.WriteLine($"***{mainMenuChoices[0]}***");
            Console.Write("Förnamn: ");
            string? firstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            string? lastName = Console.ReadLine();
            Console.Write("Stad: ");
            string? city = Console.ReadLine();
            options.AddStudent(firstName, lastName, city);
        }

        public void ChangeFirstName(Student student)
        {
            Console.Write("Förnamn: ");
            string? firstName = Console.ReadLine();
            options.ChangeFirstName(student, firstName);
        }
        public void ChangeLastName(Student student)
        {
            Console.Write("Efternamn: ");
            string? lastName = Console.ReadLine();
            options.ChangeLastName(student, lastName);
        }
        public void ChangeCity(Student student)
        {

            Console.Write("Ändra ort: ");
            string? city = Console.ReadLine();
            options.ChangeCity(student, city);
        }

        public void ChangeStudent()
        {
            bool isRunning = true;
            int menuChoice = 0;
            Console.WriteLine($"{mainMenuChoices[1]}");
            Console.WriteLine("Student-ID:");

            int studentId = int.Parse(Console.ReadLine());

            var student = options.ChangeStudent(studentId);

            while (isRunning == true)
            {
                Console.Clear();
                Console.WriteLine($"Student-ID: {student.StudentId}, {student.FirstName} {student.LastName}, {student.City}");
                int newMenuChoice = MenuHandler(changeStudentChoices, menuChoice);

                if (newMenuChoice == menuChoice)
                {
                    menuChoice = newMenuChoice;
                    switch (menuChoice)
                    {
                        case 0:
                            ChangeFirstName(student);
                            isRunning = false;
                            break;
                        case 1:
                            ChangeLastName(student);
                            isRunning = false;
                            break;
                        case 2:
                            ChangeCity(student);
                            isRunning = false;
                            break;
                    }
                }
                else if (newMenuChoice == -1)
                {

                    isRunning = false;

                }
                else if( newMenuChoice == -2)
                {

                }
                else
                {
                    menuChoice = newMenuChoice;
                }
            }
        }

        public void AllStudents()
        {
            Console.Clear();
            Console.WriteLine($"***{mainMenuChoices[2]}***");
            Console.WriteLine();
            foreach (var s in dbCtx.Students)
            {
                Console.WriteLine($"ID: {s.StudentId}, {s.FirstName} {s.LastName}, {s.City}. ");
            }
            Console.WriteLine();
            Console.WriteLine("Tryck på valfri knapp för att återgå till menyn.");
            Console.ReadKey();
        }
        public void MainMenu()
        {
            bool isRunning = true;
            int menuChoice = 0;
            while (isRunning == true)
            {
                Console.Clear();
                int newMenuChoice = MenuHandler(mainMenuChoices, menuChoice);

                if (newMenuChoice == menuChoice)
                {
                    menuChoice = newMenuChoice;
                    switch (menuChoice)
                    {
                        case 0:
                            AddStudent();
                            break;
                        case 1:
                            ChangeStudent();
                            break;
                        case 2:
                            AllStudents();
                            break;
                    }
                }
                else if (newMenuChoice == -1)
                {
                    Console.Clear();
                    isRunning = false;
                    Console.WriteLine("aAvslutar..");
                }
                else if (newMenuChoice == -2)
                {

                }
                else
                {
                    menuChoice = newMenuChoice;
                }
            }
        }
        public int MenuHandler(string[] menuChoices, int menuChoice)
        {
            int menuLength = menuChoices.Length;

            
            for (int i = 0; i < menuChoices.Length; i++)
            {
                if (menuChoice == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {menuChoices[i]} <");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(menuChoices[i]);
                }
            }
            int newMenuChoice = MenuSelector(menuLength, menuChoice);
            return newMenuChoice;

        }
        public int MenuSelector(int menuLength, int menuChoice)
        {
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.LeftArrow)
            {
                if (menuChoice == 0)
                {
                    menuChoice = menuLength - 1;
                    return menuChoice;
                }
                else
                {
                    menuChoice--;
                    return menuChoice;
                }
            }
            if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.RightArrow)
            {
                if (menuChoice == menuLength - 1)
                {
                    menuChoice = 0;
                    return menuChoice;
                }
                else
                {
                    menuChoice++;
                    return menuChoice;
                }
            }
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                return menuChoice;
            }
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                return -1;
            }
            else
            {
                return -2;
            }
        }
    }
}
