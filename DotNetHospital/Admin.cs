﻿using System.Text.RegularExpressions;

namespace DotNetHospital;

public class Admin : User
{
    //Type indicator
    private enum UserType 
    {
        Doctor,
        Patient
    }
    
    //Constructor
    public Admin(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
    }
    
    //Standard Menu based on while(true) loop.
    public override bool Menu()
    {
        while (true)
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
            input = Convert.ToInt32(Console.ReadLine());
            
            //Call method based on input number
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
                    return false;
                case 8:
                    return true;
                default:
                    Console.WriteLine("Error, Please try again");
                    break;
            }
        }
    }
    
    //Print all doctors from userList
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
    }
    
    //Print doctor in userList with doctorId received
    private void PrintDoctorDetails()
    {
        int row = 6;
        int input;
        bool patientExists = false;
        Doctor searchDoctor = null;

        Manager.DrawSquare("Check Doctor Details");

        while (!patientExists)
        {
            Manager.WriteAt("Enter the ID of the doctor to check : ", 0, ++row);
            input = Convert.ToInt32(Console.ReadLine());

            foreach (User user in FileMgr.UserList)
            {
                if (user.Id == input && user is Doctor)
                {
                    patientExists = true;
                    searchDoctor = (Doctor)user;
                    break;
                }
            }

            if (!patientExists)
            {
                Manager.WriteAt("Doctor with ID #" + input + " does not exist. Please try again.", 0, ++row);
            }
        }

        Manager.WriteAt("Name          | Email Address                  | Phone            | Address           ", 0,
            ++row);
        Manager.WriteAt(searchDoctor.ToString(), 0, ++row);

        Console.ReadKey();
    }
    
    //Print all patient in userList
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
    }
    
    //Search patient info in userList with patientId received
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
                if (patient.Id == input && patient is Patient)
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

        Manager.WriteAt(
            "Name          | Doctor             | Email Address                  | Phone            | Address           ",
            0,
            ++row);
        Manager.WriteAt(searchPatient.ToString(), 0, ++row);

        Console.ReadKey();
    }

    //Receive user input(input validation with regEx)
    //Create new instance with type based on UserType and add to static userList
    private void AddUser(UserType user)
    {
        int id, row = 6;
        List<int> idList = new List<int>();

        foreach (User temp in FileMgr.UserList)
        {
            idList.Add(temp.Id);
        }
        string firstName, lastName, streetNum, streetName, city, email, phone, password = null;
        Regex regex = new Regex("[^a-zA-Z0-9\\.]");
        Regex numRegEx = new Regex("[^0-9]");

        if (user == UserType.Patient)
        {
            Manager.DrawSquare("Add Patient");
        }
        else
        {
            Manager.DrawSquare("Add Doctor");
        }

        while (true)
        {
            Manager.WriteAt("ID(8 digit number) : ", 0, ++row);
            string input = Console.ReadLine();
            Match match = numRegEx.Match(input);
            if (!int.TryParse(input, out id) || id < 10000000 || id > 99999999 || match.Success)
            {
                Manager.WriteAt("Invalid Id, Please write a 8 digits number", 0, ++row);
            }else if (idList.Contains(id))
            {
                Manager.WriteAt("ID Duplicates. Please try again with another ID", 0, ++row);   
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
            Manager.WriteAt("Email (janedoe@example.com): ", 0, ++row);
            email = Console.ReadLine();
            if (!email.Contains('@'))
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
            if (match.Success)
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
            Manager.WriteAt("Street Name (only street name, exclude St, Ave, etc..) : ", 0, ++row);
            streetName = Console.ReadLine();
            Match match = regex.Match(streetName);
            if (match.Success)
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
            if (match.Success)
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
            Manager.WriteAt("Phone(Number only): ", 0, ++row);
            phone = Console.ReadLine();
            Match match = numRegEx.Match(phone);
            if (match.Success)
            {
                Manager.WriteAt("Invalid phone number. ", 0,
                    ++row);
            }
            else
            {
                break;
            }
        }
        
        //Password check
        while (true)
        {
            Manager.WriteAt("Password(8-16 digit, alphabets and numbers) : ", 0, ++row);
            password = Console.ReadLine();
            if (password.Length < 8 || password.Length > 16)
            {
                Manager.WriteAt("Password should between 8-16 digit, alphabets and numbers", 0, ++row);
                continue;
            }
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
        
        
        //TypeCheck(enum UserType)
        if (user == UserType.Doctor)
        {
            Doctor doctor = new Doctor(id, firstName + " " + lastName, streetNum + ", " + streetName + ", " + city,
                email, phone, password);
            FileMgr.UserList.Add(doctor);
            FileMgr.AddUserList.Add(doctor);
        }
        else if (user == UserType.Patient)
        {
            Patient patient = new Patient(id, firstName + " " + lastName, streetNum + " " + streetName + " " + city,
                email, phone, password);
            FileMgr.UserList.Add(patient);
            FileMgr.AddUserList.Add(patient);
        }

        Manager.WriteAt(firstName + " " + lastName + " added successfully.", 0, ++row);
        Console.ReadKey();
    }
}