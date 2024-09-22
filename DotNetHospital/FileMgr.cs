using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

namespace DotNetHospital;

public enum FileType
{
    USER,
    APPOINTMENT
}
public static class FileMgr
{
    
    //Contains all users and appointments
    private static List<User> userList;
    private static List<Appointment> appointmentList;
    
    //Contains new users and appointments
    //referenced by FileMgr on exit status
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
    
    //Read userinfo.txt and get info
    //Filter type by ROLE column (P: patient D: doctor A: admin)
    public static List<User> GetUserList()
    {
        List<User> userList = new List<User>();
        foreach(string[] tempUser in ReadFile("../../../userinfo.txt"))
        {
            //ignore index row
            if (!tempUser[0].Equals("ID"))
            {
                if (tempUser[5].Equals("P"))
                {
                    if (tempUser.Length == 8)
                    {
                        //doctor is not registered right after patient added
                        //empty column == null
                        if (tempUser[7].Equals(" "))
                        {
                            Patient tempPatient = new Patient(Convert.ToInt32(tempUser[0]), tempUser[1], tempUser[2],
                                tempUser[3], tempUser[4], tempUser[6], null);
                            userList.Add(tempPatient);
                        }
                        else
                        {
                            Patient tempPatient = new Patient(Convert.ToInt32(tempUser[0]), tempUser[1], tempUser[2],
                                tempUser[3], tempUser[4], tempUser[6], Convert.ToInt32(tempUser[7]));
                            userList.Add(tempPatient);
                        }
                    }
                    else
                    {
                        Patient tempPatient = new Patient(Convert.ToInt32(tempUser[0]), tempUser[1], tempUser[2],
                            tempUser[3], tempUser[4], tempUser[6]);    
                        userList.Add(tempPatient);
                    }
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
        }

        return userList;
    }
    
    //read appointment.txt and get appointment list
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
    
    //based on appointmentList, return list only contains id
    // if type is doctor, search on doctorId column
    // if type is patient, search on patientId column
    public static List<Appointment> GetAppointmentList(int id, string type)
    {
        List<Appointment> result = new List<Appointment>();
        if (type.Equals("Doctor"))
        {
            foreach (Appointment temp in appointmentList)
            {
                if (temp.DoctorId == id)
                {
                    result.Add(temp);
                }
            }    
        }
        if (type.Equals("Patient"))
        {
            foreach (Appointment temp in appointmentList)
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
        try
        {
            List<string> contents = new List<string>();
            foreach (string line in File.ReadAllLines(path))
            {
                contents.Add(line);    
            }
            contents.Add(info);
            string[] result = contents.ToArray();
            File.WriteAllLines(path, result);

        }
        catch (FileNotFoundException e)
        {
            Console.Clear();
            Console.WriteLine("File Not Found");
        }
        
    }
    
    // if userinfo already exists in appointment.txt without doctorId, and received new appointment
    // register doctorId in file.
    public static void AddDoctorToExistingPatient(Patient patient)
    {
        string path = "../../../userinfo.txt";
        string[] strList = File.ReadAllLines(path);
        
        for(int i=0; i<strList.Length; i++)
        {
            if (strList[i].Contains(patient.Id.ToString()))
            {
                strList[i] = patient.Id + "," + patient.FullName + "," + patient.Address + "," + patient.Email + "," +
                             patient.Phone + "," + "P," + patient.Password + "," + patient.MainDoctor;
                break;
            }
        }
        
        File.WriteAllLines(path, strList);
    }
    
    //return fullName by userId
    public static string GetFullNameById(int id)
    {
        try
        {
            foreach (User user in UserList)
            {
                if (user.Id == id)
                {
                    return user.FullName;
                }
            }

            throw new Exception("User does not exists");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    //return user information by userId
    public static User GetUserById(int id)
    {
        try
        {
            foreach (User user in UserList)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }

            throw new Exception("User does not exists");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    //Accessors

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
        get { return addUserList; }
    }

    
}