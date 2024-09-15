namespace DotNetHospital;

public static class FileMgr
{
    
    //Read user list from text file and returns
    public static List<string[]> ReadFile(string path)
    {
        int patientCount = 0;
        List<string[]> text = new List<string[]>();
        
        try
        {
            foreach (string line in File.ReadLines(path))
            {
                text.Add(line.Split());
            }
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine("File Not Found. Please check your path");
            Console.ReadKey();
        }
        
        return text;
    }
    public static List<User> GetUserList()
    {
        List<User> userList = new List<User>();
        foreach(string[] tempUser in FileMgr.ReadFile("../../../userinfo.txt"))
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
    public static List<string> GetAppointmentList(int Id)
    {
        List<User> users = GetUserList();
        List<string[]> appointments = FileMgr.ReadFile("../../../appointment.txt");

        List<string> results = new List<string>();
        try
        {
            foreach (string[] tempAppointment in appointments)
            {
                bool idExist = true;
                foreach (User tempUser in users)
                {
                    if (Id == Convert.ToInt32(tempAppointment[0]))
                    {
                        if (Convert.ToInt32(tempAppointment[0]) == tempUser.Id && tempUser is Patient)
                        {
                            tempAppointment[0] = tempUser.Id.ToString();
                        }
                        else if (Convert.ToInt32(tempAppointment[1]) == tempUser.Id && tempUser is Doctor)
                        {
                            tempAppointment[1] = tempUser.Id.ToString();
                        }
                        else
                        {
                            idExist = false;
                        }
                    }
                }

                if (idExist)
                {
                    results.Add(tempAppointment[0] + "  | " + tempAppointment[1] + "    | " + tempAppointment[2]);
                }
                else
                {
                    throw new Exception("Data Not Found");
                }

            }
        }
        catch (Exception e)
        {
            Console.Write("Invalid appointment data detected.");
        }
        
        return results;
    }

    public static void WriteIntoFile(User user)
    {
        
    }
}