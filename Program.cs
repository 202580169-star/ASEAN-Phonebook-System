namespace ASEANPhonebook;

public class Program
{
    private static readonly Phonebook phonebook = new();

    public static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== ASEAN PHONEBOOK ==========");
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");

            Console.Write("Enter choice: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    StoreStudents();
                    break;

                case "2":
                    EditStudent();
                    break;

                case "3":
                    SearchByCountry();
                    break;

                case "4":
                    running = false;
                    Console.WriteLine(
                        "Thank you for using the ASEAN Phonebook!");
                    break;

                default:
                    Console.WriteLine(
                        "Invalid option. Please choose 1 to 4.");
                    break;
            }
        }
    }

    // STORE STUDENTS
    private static void StoreStudents()
    {
        bool addAnother;

        do
        {
            Console.WriteLine("\n--- STORE TO ASEAN PHONEBOOK ---");

            string studentNumber =
                ReadRequired("Enter student number: ");

            while (phonebook.FindByStudentNumber(studentNumber) != null)
            {
                Console.WriteLine(
                    "Student number already exists.");

                studentNumber =
                    ReadRequired("Enter student number: ");
            }

            string surname =
                ReadRequired("Enter surname: ");

            string firstName =
                ReadRequired("Enter first name: ");

            string occupation =
                ReadRequired("Enter occupation: ");

            char gender = ReadGender();

            string countryCode = ReadCountryCode();

            string areaCode =
                ReadDigits("Enter area code: ");

            string number =
                ReadDigits("Enter number: ");

            Student student = new Student(
                studentNumber,
                surname,
                firstName,
                occupation,
                gender,
                countryCode,
                areaCode,
                number
            );

            phonebook.AddStudent(student);

            Console.WriteLine(
                "Student successfully added!");

            addAnother = ReadYesNo(
                "Do you want to enter another entry [Y/N]? ");

        } while (addAnother);
    }

    // EDIT STUDENT
    private static void EditStudent()
    {
        Console.WriteLine("\n--- EDIT ENTRY IN ASEAN PHONEBOOK ---");

        string studentNumber =
            ReadRequired("Enter student number: ");

        Student? student =
            phonebook.FindByStudentNumber(studentNumber);

        if (student == null)
        {
            Console.WriteLine(
                "Error: Student number was not found.");
            return;
        }

        bool editing = true;

        while (editing)
        {
            Console.WriteLine(
                $"\nHere is the existing information about " +
                $"{student.StudentNumber}:");

            Console.WriteLine(
                $"{student.GetFullName()} is a {student.Occupation}. " +
                $"The phone number is {student.GetPhoneNumber()}");

            Console.WriteLine("\nWhich information do you wish to change?");

            Console.WriteLine(
                "[1] Student number  [2] Surname  [3] Gender  [4] Occupation");

            Console.WriteLine(
                "[5] Country code    [6] Area code [7] Phone number");

            Console.WriteLine(
                "[8] None - Go back to main menu");

            Console.Write("Enter choice: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":

                    string newStudentNumber =
                        ReadRequired("Enter new student number: ");

                    if (phonebook.IsStudentNumberAvailable(
                        newStudentNumber, student))
                    {
                        student.StudentNumber = newStudentNumber;

                        Console.WriteLine(
                            "Student number updated.");
                    }
                    else
                    {
                        Console.WriteLine(
                            "Student number already exists.");
                    }

                    break;

                case "2":

                    student.Surname =
                        ReadRequired("Enter new surname: ");

                    Console.WriteLine("Surname updated.");

                    break;

                case "3":

                    student.Gender = ReadGender();

                    Console.WriteLine("Gender updated.");

                    break;

                case "4":

                    student.Occupation =
                        ReadRequired("Enter new occupation: ");

                    Console.WriteLine("Occupation updated.");

                    break;

                case "5":

                    student.CountryCode = ReadCountryCode();

                    Console.WriteLine("Country code updated.");

                    break;

                case "6":

                    student.AreaCode =
                        ReadDigits("Enter new area code: ");

                    Console.WriteLine("Area code updated.");

                    break;

                case "7":

                    student.PhoneNumber =
                        ReadDigits("Enter new phone number: ");

                    Console.WriteLine("Phone number updated.");

                    break;

                case "8":

                    editing = false;

                    break;

                default:

                    Console.WriteLine(
                        "Invalid choice. Please choose 1 to 8.");

                    break;
            }
        }
    }

    // SEARCH STUDENTS BY COUNTRY
    private static void SearchByCountry()
    {
        Console.WriteLine(
            "\n--- SEARCH ASEAN PHONEBOOK BY COUNTRY ---");

        Console.WriteLine("From which country?");

        Console.WriteLine(
            "[1] Philippines [2] Thailand [3] Singapore [4] Indonesia");

        Console.WriteLine(
            "[5] Malaysia [6] ALL [0] No More");

        HashSet<string> selectedCodes = new();

        bool allCountries = false;

        int choiceNumber = 1;

        while (true)
        {
            Console.Write($"Enter choice {choiceNumber}: ");

            string? input = Console.ReadLine()?.Trim();

            if (input == "0")
            {
                break;
            }

            if (input == "6")
            {
                allCountries = true;
                break;
            }

            string? code = input switch
            {
                "1" => "63",
                "2" => "66",
                "3" => "65",
                "4" => "62",
                "5" => "60",
                _ => null
            };

            if (code == null)
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }

            selectedCodes.Add(code);

            choiceNumber++;
        }

        List<Student> results;

        if (allCountries)
        {
            results = phonebook.GetAllStudents();
        }
        else
        {
            results =
                phonebook.GetStudentsByCountries(selectedCodes);
        }

        if (results.Count == 0)
        {
            Console.WriteLine(
                "No student entries found.");
            return;
        }

        string countryNames = allCountries
            ? "all ASEAN countries"
            : string.Join(" and ",
                selectedCodes.Select(CountryInfo.GetName));

        Console.WriteLine(
            $"\nHere are the students from {countryNames}:");

        foreach (Student student in results)
        {
            Console.WriteLine(student);
        }
    }

    // VALIDATE COUNTRY CODE
    private static string ReadCountryCode()
    {
        while (true)
        {
            string code = ReadRequired(
                "Enter country code (60, 62, 63, 65, 66): ");

            if (CountryInfo.IsValid(code))
            {
                return code;
            }

            Console.WriteLine(
                "Invalid country code. Please enter 60, 62, 63, 65, or 66.");
        }
    }

    // VALIDATE GENDER
    private static char ReadGender()
    {
        while (true)
        {
            Console.Write(
                "Enter gender (M for male, F for female): ");

            string? input =
                Console.ReadLine()?.Trim().ToUpperInvariant();

            if (input == "M" || input == "F")
            {
                return input[0];
            }

            Console.WriteLine("Invalid gender. Enter M or F.");
        }
    }

    // VALIDATE NUMBERS
    private static string ReadDigits(string prompt)
    {
        while (true)
        {
            string value = ReadRequired(prompt);

            if (value.All(char.IsDigit))
            {
                return value;
            }

            Console.WriteLine("Please enter digits only.");
        }
    }

    // VALIDATE EMPTY INPUT
    private static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string? value = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine(
                "This field cannot be blank.");
        }
    }

    // VALIDATE YES OR NO
    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            string? answer =
                Console.ReadLine()?.Trim().ToUpperInvariant();

            if (answer == "Y")
            {
                return true;
            }

            if (answer == "N")
            {
                return false;
            }

            Console.WriteLine("Please enter Y or N.");
        }
    }
}