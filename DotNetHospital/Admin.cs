using System.Text.RegularExpressions;

namespace DotNetHospital;

public class Admin : User
{
    private enum UserType
    {
        Doctor,
        Patient
    }

    public Admin(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
    }

    public override void Menu()
    {
        int input = 0;
        Manager.DrawSquare("Administrator Menu");
        Manager.WriteAt("\nWelcome to DOTNET Hospital" + FullName + "\n" +
                        "Please Choose an option \n" +
                        "1. List all doctors \n" +
                        "2. Check doctor details\n" +
                        "3. List all patients\n" +
                        "4. Check patient details\n" +
                        "5. Add doctor\n" +
                        "6. Add patient\n" +
                        "7. Exit to Login\n" +
                        "8. Exit System\n", 4, 6);

        while (true)
        {
            input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    PrintAllDoctors();
                    break;
                case 2:
                    PrintDoctorDetails();
                    break;
                case 3:
                    PrintAllPatients();
                    break;
                case 4:
                    PrintPatientDetails();
                    break;
                case 5:
                    AddUser(UserType.Doctor);
                    break;
                case 6:
                    AddUser(UserType.Patient);
                    break;
                case 7:
                    Hospital.Login().Menu();
                    return;
                case 8:
                    return;
                default:
                    Console.WriteLine("Error, Please try again");
                    break;
            }
        }
    }

    private void PrintAllDoctors()
    {
        int row = 6;
        Manager.DrawSquare("All Doctors");
        Manager.WriteAt(
            " Name                  | Email Address                             | Phone               | Address       ",
            4, ++row);
        foreach (User user in FileMgr.UserList)
        {
            if (user is Doctor)
            {
                Manager.WriteAt(user.ToString(), 0, ++row);
            }
        }

        Console.ReadKey();
        Menu();
    }

    private void PrintDoctorDetails()
    {
        int row = 6;
        int input;
        bool patientExists = false;
        Doctor searchDoctor = null;

        Manager.DrawSquare("Check Patient Details");

        while (!patientExists)
        {
            Manager.WriteAt("Enter the ID of the patient to check : ", 0, ++row);
            input = Convert.ToInt32(Console.ReadLine());

            foreach (User patient in FileMgr.UserList)
            {
                if (patient.Id == input)
                {
                    patientExists = true;
                    searchDoctor = (Doctor)patient;
                    break;
                }
            }

            if (!patientExists)
            {
                Manager.WriteAt("Patient with ID #" + input + " does not exist. Please try again.", 0, ++row);
            }
        }

        Manager.WriteAt("Name          | Email Address                  | Phone            | Address           ", 0,
            ++row);
        Manager.WriteAt(searchDoctor.ToString(), 0, ++row);

        Console.ReadKey();
        Menu();
    }

    private void PrintAllPatients()
    {
        int row = 6;
        Manager.DrawSquare("All Patients");
        Manager.WriteAt(
            " Name                  | Email Address                             | Phone               | Address       ",
            4, ++row);
        foreach (User user in FileMgr.UserList)
        {
            if (user is Patient)
            {
                Manager.WriteAt(user.ToString(), 0, ++row);
            }
        }

        Console.ReadKey();
        Menu();
    }

    private void PrintPatientDetails()
    {
        int row = 6;
        int input;
        bool patientExists = false;
        Patient searchPatient = null;

        Manager.DrawSquare("Check Patient Details");

        while (!patientExists)
        {
            Manager.WriteAt("Enter the ID of the patient to check : ", 0, ++row);
            input = Convert.ToInt32(Console.ReadLine());

            foreach (User patient in FileMgr.UserList)
            {
                if (patient.Id == input)
                {
                    patientExists = true;
                    searchPatient = (Patient)patient;
                    break;
                }
            }

            if (!patientExists)
            {
                Manager.WriteAt("Patient with ID #" + input + " does not exist. Please try again.", 0, ++row);
            }
        }

        Manager.WriteAt("Name          | Doctor             | Email Address                  | Phone            | Address           ", 0,
            ++row);
        Manager.WriteAt(searchPatient.ToString(), 0, ++row);

        Console.ReadKey();
        Menu();
    }

    private void AddUser(UserType user)
    {
        int id, row = 6;
        string firstName, lastName, streetNum, streetName, city, email, phone, password = null;
        Regex regex = new Regex("[^a-zA-Z]");
        Regex numRegEx = new Regex("[^0-9]");

        if (user == UserType.Patient)
        {
            Manager.DrawSquare("Add Doctor");
        }
        else
        {
            Manager.DrawSquare("Add Patient");
        }

        while (true)
        {
            Manager.WriteAt("ID : ", 0, ++row);
            id = Convert.ToInt32(Console.ReadLine());
            if (id < 10000000 || id > 99999999)
            {
                Manager.WriteAt("Invalid Id, Please write a 8 digits number", 0, ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("First Name : ", 0, ++row);
            firstName = Console.ReadLine();
            Match match = regex.Match(firstName);
            if (match.Success)
            {
                Manager.WriteAt("Name should not contains any number or special character, Please try again", 0, ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("Last Name : ", 0, ++row);
            lastName = Console.ReadLine();
            Match match = regex.Match(lastName);
            if (match.Success)
            {
                Manager.WriteAt("Name should not contains any number or special character, Please try again", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("Email : ", 0, ++row);
            email = Console.ReadLine();
            if (!lastName.Contains("@"))
            {
                Manager.WriteAt("invalid email address. ", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("Street Number : ", 0, ++row);
            streetNum = Console.ReadLine();
            Match match = numRegEx.Match(streetNum);
            if (!match.Success)
            {
                Manager.WriteAt("Invalid Street Number. ", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("Street Name : ", 0, ++row);
            streetName = Console.ReadLine();
            Match match = regex.Match(streetName);
            if (!match.Success)
            {
                Manager.WriteAt("Invalid street name. ", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("City : ", 0, ++row);
            city = Console.ReadLine();
            Match match = regex.Match(city);
            if (!match.Success)
            {
                Manager.WriteAt("Invalid city name. ", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("Phone : ", 0, ++row);
            phone = Console.ReadLine();
            Match match = numRegEx.Match(phone);
            if (!match.Success)
            {
                Manager.WriteAt("Invalid phone number. ", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            Manager.WriteAt("Password : ", 0, ++row);
            password = Console.ReadLine();
            while (true)
            {
                Manager.WriteAt("Confirm Password : ", 0, ++row);
                string confirmPassword = Console.ReadLine();
                if (!password.Equals(confirmPassword))
                {
                    Manager.WriteAt("Password not match. Please try again ", 0,
                        ++row);
                }
                else
                {
                    break;
                }
            }

            break;
        }

        if (user == UserType.Doctor)
        {
            Doctor doctor = new Doctor(id, firstName + " " + lastName, streetNum + ", " + streetName + ", " + city,
                email, phone, password);
            FileMgr.UserList.Add(doctor);
            FileMgr.AddUserList.Add(doctor);
        }else if (user == UserType.Patient)
        {
            Patient patient = new Patient(id, firstName + " " + lastName, streetNum + ", " + streetName + ", " + city,
                email, phone, password);
            FileMgr.UserList.Add(patient);
            FileMgr.AddUserList.Add(patient);
        }
        
        Manager.WriteAt(firstName + " " + lastName + " added successfully.", 0, ++row);
        Console.ReadKey();
        Menu();
    }

    ~Admin()
    {
        Console.WriteLine("Destructor Test - Admin");
        if (FileMgr.AddUserList.Count != 0)
        {
            foreach (User temp in FileMgr.AddUserList)
            {
                char role = ' ';
                if (temp is Doctor) role = 'D';
                else if (temp is Patient) role = 'P';
                    
                string addString = temp.Id + "," + temp.FullName + "," + temp.Address + "," + temp.Email + "," +
                                   temp.Phone + "," + role + "," + temp.Password + "," + " ";
                FileMgr.WriteIntoFile(FileType.USER, addString);
            }
        }
    }
}