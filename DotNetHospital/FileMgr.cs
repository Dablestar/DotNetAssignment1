namespace DotNetHospital;

public enum FileType
{
    USER,
    APPOINTMENT
}
public static class FileMgr
{
    private static List<User> userList;
    private static List<Appointment> appointmentList;

    private static List<User> addUserList;
    private static List<Appointment> addAppointmentList;

    static FileMgr()
    {
        userList = GetUserList();
        appointmentList = GetAppointmentList();

        addUserList = new List<User>();
        addAppointmentList = new List<Appointment>();
    }
    
    //Read user list from text file and returns
    public static List<string[]> ReadFile(string path)
    {
        int patientCount = 0;
        List<string[]> result = new List<string[]>();
        
        try
        {
            foreach (string line in File.ReadLines(path))
            {
                result.Add(line.Split(","));
            }
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine("File Not Found. Please check your path");
            Console.ReadKey();
        }
        
        return result;
    }
    public static List<User> GetUserList()
    {
        List<User> userList = new List<User>();
        foreach(string[] tempUser in ReadFile("../../../userinfo.txt"))
        {
            if (!tempUser[0].Equals("ID"))
            {
                if (tempUser[5].Equals("P"))
                {
                    Patient tempPatient = new Patient(Convert.ToInt32(tempUser[0]), tempUser[1], tempUser[2],
                        tempUser[3], tempUser[4], tempUser[6]);
                    userList.Add(tempPatient);
                }else if (tempUser[5].Equals("D"))
                {
                    Doctor tempPatient = new Doctor(Convert.ToInt32(tempUser[0]), tempUser[1], tempUser[2],
                        tempUser[3], tempUser[4], tempUser[6]);
                    userList.Add(tempPatient);
                }else if (tempUser[5].Equals("A"))
                {
                    Admin tempPatient = new Admin(Convert.ToInt32(tempUser[0]), tempUser[1], tempUser[2],
                        tempUser[3], tempUser[4], tempUser[6]);
                    userList.Add(tempPatient);
                }
            }
            else
            {
                continue;
            }
        }

        return userList;
    }
    public static List<Appointment> GetAppointmentList()
    {
        List<string[]> appointments = ReadFile("../../../appointment.txt");
        List<Appointment> results = new List<Appointment>();

        foreach (string[] tempAppointment in appointments)
        {
            if (!tempAppointment[0].Equals("DOCTORID"))
            {
                Appointment temp = new Appointment(Convert.ToInt32(tempAppointment[0]), Convert.ToInt32(tempAppointment[1]), tempAppointment[2]);
                results.Add(temp);
            }
        }
        
        
        return results;
    }
    public static List<Appointment> GetAppointmentList(int id, string type)
    {
        List<Appointment> result = new List<Appointment>();
        if (type.Equals("Doctor"))
        {
            foreach (Appointment temp in GetAppointmentList())
            {
                if (temp.DoctorId == id)
                {
                    result.Add(temp);
                }
            }    
        }
        if (type.Equals("Patient"))
        {
            foreach (Appointment temp in GetAppointmentList())
            {
                if (temp.PatientId == id)
                {
                    result.Add(temp);
                }
            }  
        }
        
        return result;
    }
    
    public static void WriteIntoFile(FileType type, string info)
    {
        string path = type.Equals(FileType.APPOINTMENT) ? "../../../appointment.txt" : "../../../userinfo.txt";
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(info);
    }

    public static List<User> UserList
    {
        get { return userList; }
    }
    public static List<Appointment> AppointmentList
    {
        get { return appointmentList; }
    }

    public static List<Appointment> AddAppointmentList
    {
        get { return addAppointmentList; }
    }
    
    public static List<User> AddUserList
    {
        get { return AddUserList; }
    }

    
}