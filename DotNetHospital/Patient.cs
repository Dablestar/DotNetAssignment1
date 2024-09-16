namespace DotNetHospital;
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
        int idx = 0;
        int choice;
        int doctorId = 0;
        bool appointmentExists = false;
        List<User> doctorList = new List<User>();
        Manager.DrawSquare("Book Appointment");

        foreach (Appointment temp in FileMgr.AppointmentList)
        {
            if (temp.PatientId == Id)
            {
                appointmentExists = true;
                doctorId = temp.DoctorId;
                break;
            }
        }

        if (!appointmentExists)
        {
            Manager.WriteAt("You are not registered with any doctor! Please choose which doctor you would like to register with : ", 0, row);
            foreach (User temp in FileMgr.UserList)
            {
                if (temp is Doctor)
                {
                    Manager.WriteAt("#"+ ++idx + ": " + temp.ToString() + "\n", 0, ++row);
                    doctorList.Add(temp);
                }
            }

            while (true)
            {
                choice = Convert.ToInt32(Console.ReadLine());
                if (choice > doctorList.Count + 1)
                {
                    Manager.WriteAt("Invalid Number, Please try again", 0, ++row);
                }
                else
                {
                    doctorId = doctorList[choice - 1].Id;
                    break;
                }
            }
        }
        else
        {
            Manager.WriteAt("Your Doctor Detail : ", 0, ++row);
            foreach (User temp in FileMgr.UserList)
            {
                if (temp.Id == doctorId)
                {
                    Manager.WriteAt(temp.ToString(), 0, ++row);
                    break;
                }
            }
        }
        
        Manager.WriteAt("Description of the Appointment : ", 0, ++row);
        string description = Console.ReadLine();

        Appointment newAppointment = new Appointment(doctorId, Id, description);
        string fileInputString = doctorId + "," + Id + "," + description;
        FileMgr.WriteIntoFile(FileType.APPOINTMENT, fileInputString);
        FileMgr.AppointmentList.Add(newAppointment);
        FileMgr.AddAppointmentList.Add(newAppointment);
        
        Manager.WriteAt("The appointment has been booked successfully", 0, ++row);
        Menu();
    }


    private void PrintAppointmentList()
    {
        int column = 6;
        Manager.DrawSquare("My Appointments");
        Manager.WriteAt("Doctor                 | Patient                   | Description \n", 4, column);
        foreach (Appointment currentAppointment in FileMgr.GetAppointmentList(Id, "Patient"))
        {
            Manager.WriteAt(currentAppointment.ToString(), 4, ++column);
        }

        Console.ReadKey();
        Menu();
    }

    private void PrintDoctorDetails()
    {
        int row = 6;
        List<int> doctorIdList = new List<int>();
        List<User> userList = FileMgr.UserList;

        Manager.DrawSquare("My Doctor");
        
        Manager.WriteAt("Your doctor : ", 4, row);
        Manager.WriteAt(" Name                  | Email Address                             | Phone               | Address       ", 4, ++row);
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
                    Manager.WriteAt(user.ToString(), 4,  ++row);
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

    public override string ToString()
    {
        return Email + "         | " + Phone + "             | " + Address;
    }
}