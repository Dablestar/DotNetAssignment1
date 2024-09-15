namespace DotNetHospital;

public enum UserType
{
    Patient,
    Doctor,
    Admin
}

public class User
{
    private int id;
    private string fullName, address, email, phone, password;
    public ConsoleMgr manager;

    public User()
    {
    }

    public User(int id, string fullName, string address, string email, string phone, string password)
    {
        this.id = id;
        this.fullName = fullName;
        this.address = address;
        this.email = email;
        this.phone = phone;
        this.password = password;
        manager = new ConsoleMgr();
    }

    public virtual void Menu()
    {
        Console.WriteLine("Debug");
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string FullName
    {
        get { return fullName; }
        set { fullName = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public ConsoleMgr Manager
    {
        get { return manager; }
    }
}

public class Patient : User
{
    private UserType type;

    public Patient(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
        this.type = UserType.Patient;
    }

    public override void Menu()
    {
        int input = 0;
        bool exit = false;
        Manager.DrawSquare("Patient Menu");
        Manager.WriteAt("\nWelcome to DOTNET Hospital \n" +
                        "Please Choose an option \n" +
                        "1. List patient details \n" +
                        "2. List my doctor details\n" +
                        "3. List all appointments\n" +
                        "4. Book appointments\n" +
                        "5. Exit to Login\n" +
                        "6. Exit System\n", 4, 6);

        while (!exit)
        {
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
                    Hospital.Login().Menu();
                    break;
                case 6:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Error, Please try again");
                    break;
            }
        }
    }

    private void AddAppointment()
    {
        int row = 6;
        int choice;
        List<User> doctorList = new List<User>();
        Manager.DrawSquare("Book Appointment");
        Manager.WriteAt("You are not registered with any doctor! Please choose which doctor you would like to register with : ", 0, row);
        foreach (User temp in FileMgr.UserList)
        {
            if (temp is Doctor)
            {
                Manager.WriteAt(temp.ToString(), 0, ++row);
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
                break;
            }
        }
        Manager.WriteAt("Description of the Appointment : ", 0, ++row);
        string description = Console.ReadLine();

        Appointment newAppointment = new Appointment(doctorList[choice].Id, Id, description);
        string fileInputString = doctorList[choice].Id + "," + Id + "," + description;
        FileMgr.WriteIntoFile(FileType.APPOINTMENT, fileInputString);
        FileMgr.AppointmentList.Add(newAppointment);
        FileMgr.AddAppointmentList.Add(newAppointment);
        
        Manager.WriteAt("The appointment has been booked successfully", 0, ++row);

    }


    private void PrintAppointmentList()
    {
        int column = 6;
        Manager.DrawSquare("My Appointments");
        Manager.WriteAt("Doctor  | Patient   | Description \n", 4, column);
        foreach (Appointment currentAppointment in FileMgr.GetAppointmentList(Id, "Patient"))
        {
            Manager.WriteAt(currentAppointment.ToString(), 4, ++column);
        }

        Console.ReadKey();
        Menu();
    }

    private void PrintDoctorDetails()
    {
        List<int> doctorIdList = new List<int>();
        List<User> userList = FileMgr.UserList;

        Manager.DrawSquare("My Doctor");
        foreach (Appointment appointment in FileMgr.GetAppointmentList(Id, "Patient"))
        {
            if (!doctorIdList.Contains(appointment.DoctorId))
            {
                doctorIdList.Add(appointment.DoctorId);
            }
        }

        foreach (User user in userList)
        {
            foreach (int id in doctorIdList)
            {
                if (id == user.Id)
                {
                    Manager.WriteAt(user.ToString(), 4, 6);
                }
            }
        }

        Console.ReadKey();
        Menu();
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
        Menu();
    }
}

public class Doctor : User
{
    private UserType type;

    public Doctor(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
        this.type = UserType.Doctor;
    }

    public override void Menu()
    {
    }

    public override string ToString()
    {
        string result = FullName + "    | " + Email + "   | " + Phone + "   | " + Address;
        return result;
    }
}

public class Admin : User
{
    private UserType type;

    public Admin(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
        this.type = UserType.Admin;
    }

    public override void Menu()
    {
    }

    public void AddUser()
    {
    }
}