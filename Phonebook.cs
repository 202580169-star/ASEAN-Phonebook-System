namespace ASEANPhonebook;

public class Phonebook
{
    private readonly List<Student> students = new();

    public int Count
    {
        get { return students.Count; }
    }

    // Add a student
    public bool AddStudent(Student student)
    {
     ArgumentNullException.ThrowIfNull(student);

        if (FindByStudentNumber(student.StudentNumber) != null)
        return false;

     students.Add(student);
        return true;
    }

    // Find a student using student number
    public Student? FindByStudentNumber(string studentNumber)
    {
        return students.FirstOrDefault(s =>
            s.StudentNumber.Equals(
                studentNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    // Check if student number is available
    public bool IsStudentNumberAvailable(
        string studentNumber, Student currentStudent)
    {
        Student? found = FindByStudentNumber(studentNumber);

        return found == null ||
               ReferenceEquals(found, currentStudent);
    }

    // Search students by selected countries
    public List<Student> GetStudentsByCountries(
        IEnumerable<string> countryCodes)
    {
        var selected = new HashSet<string>(countryCodes);

        return students
            .Where(s => selected.Contains(s.CountryCode))
            .OrderBy(s => s.Surname,
                StringComparer.OrdinalIgnoreCase)
            .ThenBy(s => s.FirstName,
                StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    // Display all students alphabetically
    public List<Student> GetAllStudents()
    {
        return students
            .OrderBy(s => s.Surname,
                StringComparer.OrdinalIgnoreCase)
            .ThenBy(s => s.FirstName,
                StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}