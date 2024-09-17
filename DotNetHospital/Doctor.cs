namespace DotNetHospital;


public class Doctor : User
{

    public Doctor(int id, string fullName, string address, string email, string phone, string password) : base(id,
        fullName, address, email, phone, password)
    {
        
    }

    public override void Menu()
    {
        int input = 0;
        Manager.DrawSquare("Doctor Menu");
        Manager.WriteAt("\nWelcome to DOTNET Hospital" + FullName + "\n" +
                        "Please Choose an option \n" +
                        "1. List doctor details \n" +
                        "2. List patients\n" +
                        "3. List appointments\n" +
                        "4. Check particular patient\n" +
                        "5. List appointments with patient\n" +
                        "6. Exit to Login\n" +
                        "7. Exit System\n", 4, 6);

        while (true)
        {
            input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    PrintDoctorDetails();
                    break;
                case 2:
                    PrintPatients();
                    break;
                case 3:
                    PrintAppointments();
                    break;
                case 4:
                    PrintParticularPatient();
                    break;
                case 5:
                    PrintAppointmentWithPatient();
                    break;
                case 6:
                    Hospital.Login().Menu();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Error, Please try again");
                    break;
            }
        }
    }

    private void PrintAppointmentWithPatient()
    {
        int row = 6;
        int input;
        List<Appointment> appointmentList;
        
        Manager.DrawSquare("Appointments With");

        while (true)
        {
            Manager.WriteAt("Enter the ID of the patient you would like to view appointments for : ", 0, ++row);
            input = Convert.ToInt32(Console.ReadLine());

            appointmentList = FileMgr.GetAppointmentList(input, "Patient");
            if (appointmentList.Count == 0)
            {
                Manager.WriteAt("Appointment with ID #" + input +" does not exist. Please try again.", 0, ++row);
                Console.ReadKey();
            }
            else
            {
                Manager.WriteAt("Doctor                 | Patient                   | Description \n", 4, ++row);
                foreach (Appointment appointment in appointmentList)
                {
                    Manager.WriteAt(appointment.ToString(), 0, ++row);
                }
                break;
            }
        }
        Console.ReadKey();
        Menu();
    }

    private void PrintParticularPatient()
    {
        int row = 6;
        int input;
        bool patientExists = false;
        Patient searchPatient = null;
        
        Manager.DrawSquare("Check Patient Details");

        while (!patientExists)
        {
            Manager.WriteAt("Enter the ID of the patient to check : ", 0,++row);
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
                Manager.WriteAt("Patient with ID #" + input +" does not exist. Please try again.", 0, ++row);
            }
        }
        Manager.WriteAt("Name           | Doctor                | Email Address                  | Phone            | Address           ", 0, ++row);
        Manager.WriteAt(searchPatient.FullName + "            | " + FullName + "          | " + searchPatient,0, ++row);

        Console.ReadKey();
        Menu();
    }

    private void PrintAppointments()
    {
        int row = 6;
        Manager.DrawSquare("My Appointments");
        Manager.WriteAt("Doctor                 | Patient                   | Description \n", 4, ++row);
        foreach (Appointment currentAppointment in FileMgr.GetAppointmentList(Id, "Doctor"))
        {
            Manager.WriteAt(currentAppointment.ToString(), 4, ++row);
        }

        Console.ReadKey();
        Menu();
    }

    private void PrintPatients()
    {
        int row = 6;
        List<int> patientIdList = new List<int>();
        
        Manager.DrawSquare("My Patients");
        Manager.WriteAt("Name           | Doctor                | Email Address                  | Phone            | Address           ", 0, ++row);
        foreach (Appointment temp in FileMgr.GetAppointmentList(Id, "Doctor"))
        {
            patientIdList.Add(temp.PatientId);
        }

        foreach (int id in patientIdList)
        {
            foreach (User patient in FileMgr.UserList)
            {
                if (patient.Id == id)
                {
                    Manager.WriteAt(patient.FullName + "            | " + FullName + "          | " + patient,0, ++row);
                }
            }
        }

        Console.ReadKey();
        Menu();
    }

    private void PrintDoctorDetails()
    {
        int row = 6;
        Manager.DrawSquare("My Details");
        Manager.WriteAt("Name           | Email Address                  | Phone            | Address           ", 4, row);
        Manager.WriteAt("---------------------------------------------------------------------------------------", 4, ++row);
        Manager.WriteAt(ToString(), 4, ++row);
        
        Console.ReadKey();
        Menu();
    }
    public override string ToString()
    {
        return FullName + "            | " + Email + "         | " + Phone + "         | " + Address;
    }
    
    ~Doctor()
    {
        Console.WriteLine("Destructor Test - Doctor");
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
