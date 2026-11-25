namespace ASEANPhonebook;

public class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public char Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(string studentNumber, string surname,
        string firstName, string occupation, char gender,
        string countryCode, string areaCode, string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    public string GetFullName()
    {
        return $"{FirstName} {Surname}";
    }

    public string GetPhoneNumber()
    {
        return $"{CountryCode}-{AreaCode}-{PhoneNumber}";
    }

    public string GetCountryName()
    {
        return CountryInfo.GetName(CountryCode);
    }

    public override string ToString()
    {
        string pronoun = Gender == 'M' ? "His" : "Her";

        return $"{Surname}, {FirstName}, with student number " +
               $"{StudentNumber}, is a {Occupation}. " +
               $"{pronoun} phone number is {GetPhoneNumber()} " +
               $"({GetCountryName()}).";
    }
}