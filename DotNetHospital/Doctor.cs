namespace DotNetHospital;


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
        int input = 0;
        bool exit = false;
        Manager.DrawSquare("Doctor Menu");
        Manager.WriteAt("\nWelcome to DOTNET Hospital \n" +
                        "Please Choose an option \n" +
                        "1. List doctor details \n" +
                        "2. List patients\n" +
                        "3. List appointments\n" +
                        "4. Check particular patient\n" +
                        "5. List appointments with patient\n" +
                        "6. Exit to Login\n" +
                        "7. Exit System\n", 4, 6);

        while (!exit)
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
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Error, Please try again");
                    break;
            }
        }
    }

    private void PrintAppointmentWithPatient()
    {
        
    }

    private void PrintParticularPatient()
    {
        throw new NotImplementedException();
    }

    private void PrintAppointments()
    {
        throw new NotImplementedException();
    }

    private void PrintPatients()
    {
        throw new NotImplementedException();
    }

    private void PrintDoctorDetails()
    {
        int row = 6;
        Manager.DrawSquare("My Details");
        Manager.WriteAt("Name           | Email Address                  | Phone            | Address           ", 4, row);
    }

    public override string ToString()
    {
        string result = FullName + "            | " + Email + "         | " + Phone + "         | " + Address;
        return result;
    }
}
