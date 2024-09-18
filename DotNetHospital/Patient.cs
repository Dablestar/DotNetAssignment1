namespace DotNetHospital;
public class Patient : User
{
    private int? mainDoctor;

    public Patient(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
        mainDoctor = null;
    }
    
    public Patient(int id, string fullName, string address, string email, string phone, string password, int doctorId) : base(id,
        fullName, address, email, phone, password)
    {
        mainDoctor = doctorId;
    }

    public override bool Menu()
    {
        while (true) 
        {
            int input = 0;
            bool isQuit = false;
            Manager.DrawSquare("Patient Menu");
            Manager.WriteAt("\nWelcome to DOTNET Hospital" + FullName + "\n" +
                            "Please Choose an option \n" +
                            "1. List patient details \n" +
                            "2. List my doctor details\n" +
                            "3. List all appointments\n" +
                            "4. Book appointments\n" +
                            "5. Exit to Login\n" +
                            "6. Exit System\n", 4, 6);
            input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    PrintPatientDetails();
                    break;
                case 2:
                    PrintDoctorDetails();
                    break;
                case 3:
                    PrintAppointmentList();
                    break;
                case 4:
                    AddAppointment();
                    break;
                case 5:
                    return false;
                case 6:
                    return true;
                default:
                    Console.WriteLine("Error, Please try again");
                    break;
            }

            if (isQuit)
            {
                break;
            }
        }

        return false;
    }

    private void AddAppointment()
    {
        int row = 6;
        int idx = 0;
        int choice;
        List<User> doctorList = new List<User>();
        Manager.DrawSquare("Book Appointment");

        if (mainDoctor == null)
        {
            Manager.WriteAt("You are not registered with any doctor! Please choose which doctor you would like to register with : ", 0, row);
            foreach (User temp in FileMgr.UserList)
            {
                if (temp is Doctor)
                {
                    Manager.WriteAt("#"+ ++idx + ": " + temp + "\n", 0, ++row);
                    doctorList.Add(temp);
                }
            }

            while (true)
            {
                choice = Convert.ToInt32(Console.Read());
                if (choice > doctorList.Count + 1)
                {
                    Manager.WriteAt("Invalid Number, Please try again", 0, ++row);
                }
                else
                {
                    mainDoctor = doctorList[choice - 1].Id;
                    FileMgr.AddDoctorToExistingPatient(this);
                    break;
                }
            }
        }
        else
        {
            Manager.WriteAt("Your Doctor Detail : ", 0, ++row);
            Manager.WriteAt(FileMgr.GetUserById(mainDoctor.Value).ToString(), 0, ++row);
        }
        
        Manager.WriteAt("Description of the Appointment : ", 0, ++row);
        string description = Console.ReadLine();

        Appointment newAppointment = new Appointment(mainDoctor.Value, Id, description);
        FileMgr.AppointmentList.Add(newAppointment);
        FileMgr.AddAppointmentList.Add(newAppointment);
        
        
        
        Manager.WriteAt("The appointment has been booked successfully", 0, ++row);
        Console.ReadKey();
    }


    private void PrintAppointmentList()
    {
        int row = 6;
        Manager.DrawSquare("My Appointments");
        Manager.WriteAt("Doctor                 | Patient                   | Description \n", 4, row);
        foreach (Appointment currentAppointment in FileMgr.GetAppointmentList(Id, "Patient"))
        {
            Manager.WriteAt(currentAppointment.ToString(), 4, ++row);
        }

        Console.ReadKey();
    }

    private void PrintDoctorDetails()
    {
        int row = 6;
        List<int> doctorIdList = new List<int>();
        List<User> userList = FileMgr.UserList;

        Manager.DrawSquare("My Doctor");
        if (mainDoctor != null)
        {
            Manager.WriteAt("Your doctor : ", 4, row);
            Manager.WriteAt(" Name                  | Email Address                             | Phone               | Address       ", 4, ++row);
            Manager.WriteAt(FileMgr.GetUserById(mainDoctor.Value).ToString(), 4, ++row);
        }
        else
        {
            Manager.WriteAt("You are not registered by any doctor. Please try again later.", 4, ++row);
        }
        Console.ReadKey();
    }

    private void PrintPatientDetails()
    {
        Manager.DrawSquare("My Details");
        Console.WriteLine("\n");
        Manager.WriteAt("Patient ID : " + Id, 4, 6);
        Manager.WriteAt("Full Name : " + FullName, 4, 7);
        Manager.WriteAt("Address : " + Address, 4, 8);
        Manager.WriteAt("Email : " + Email, 4, 9);
        Manager.WriteAt("Phone : " + Phone, 4, 10);
        Console.ReadKey();
    }

    public override string ToString()
    {
        if (mainDoctor == null)
        {
            return FullName + "         |           | " + Email + "         | " + Phone + "             | " + Address;    
        }

        return FullName + "         | " + FileMgr.GetFullNameById(mainDoctor.Value) + "           | " + Email + "         | " + Phone +
               "         | " + Address;
    }

    public int MainDoctor
    {
        get { return mainDoctor.Value; }
        set { mainDoctor = value; }
    }

    ~Patient()
    {
        Console.WriteLine("Destructor Test - Patient");
        Console.ReadKey();

        if (FileMgr.AddAppointmentList.Count != 0)
        {
            foreach (Appointment temp in FileMgr.AddAppointmentList)
            {
                string addString = temp.DoctorId + "," + temp.PatientId + "," + temp.Description;
                FileMgr.WriteIntoFile(FileType.APPOINTMENT, addString);
            }
        }
    }

}