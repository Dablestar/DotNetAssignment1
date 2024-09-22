// See https://aka.ms/new-console-template for more information


using System.Security;
using DotNetHospital;

public class Hospital
{
    //Receive id and password
    public User Login()
    {
        bool accountExist = false, passCorrect = false;
        //call ReadFile() to get userList
        //initialize console
        ConsoleMgr manager = new ConsoleMgr();
        Console.SetBufferSize(Console.BufferWidth, 50);

        //repeat until both correct
        while (!accountExist || !passCorrect)
        {
            accountExist = false;
            passCorrect = false;
            Console.Clear();
            manager.DrawSquare("login");
            manager.WriteAt("ID : ", 0, 6);
            int id = Convert.ToInt32(Console.ReadLine());
            manager.WriteAt("Password : ", 0, 7);
            SecureString pwd = new SecureString();
            ConsoleKeyInfo keyInfo;
            //password masking
            //char input (ConsoleKeyInfo) => SecureString => string
            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pwd.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pwd.Length > 0)
                {
                    pwd.RemoveAt(pwd.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            foreach (User user in FileMgr.UserList)
            {
                if (id.Equals(user.Id))
                {
                    accountExist = true;
                    if (new System.Net.NetworkCredential("", pwd).Password.Equals(user.Password))
                    {
                        if (user is Patient)
                        {
                            return user;
                        }

                        if (user is Doctor)
                        {
                            return user;
                        }

                        if (user is Admin)
                        {
                            return user;
                        }
                    }
                    else
                    {
                        passCorrect = false;
                        break;
                    }
                }
            }
            
            //Id exists but password not match
            if (accountExist && !passCorrect)
            {
                manager.WriteAt("Invalid Credential. please try again.", 0, 8);
                Console.ReadKey();
            }
            
            //Id not exists
            if (!accountExist)
            {
                manager.WriteAt("Account does not exist, try again", 0, 8);
                Console.ReadKey();
            }
        }

        return null;
    }
    
    //Execute when application exit
    //Write all addUserList and addAppointmentList contents into files
    public static void OnApplicationExit(object sender, EventArgs e)
    {
        if (FileMgr.AddAppointmentList.Count != 0)
        {
            foreach (Appointment temp in FileMgr.AddAppointmentList)
            {
                string addString = temp.DoctorId + "," + temp.PatientId + "," + temp.Description;
                FileMgr.WriteIntoFile(FileType.APPOINTMENT, addString);
            }
        }

        if (FileMgr.AddUserList.Count != 0)
        {
            foreach (User temp in FileMgr.AddUserList)
            {
                string addString;
                char role = ' ';
                if (temp is Doctor) role = 'D';
                else if (temp is Patient) role = 'P';

                if (temp is Patient)
                {
                    //Empty last column if doctor is not registered
                    Patient tempPatient = (Patient)temp;
                    if (tempPatient.MainDoctor != null)
                    {
                        addString = temp.Id + "," + temp.FullName + "," + temp.Address + "," + temp.Email + "," +
                                           temp.Phone + "," + role + "," + temp.Password + "," + tempPatient.MainDoctor;        
                    }
                    else
                    {
                        addString = temp.Id + "," + temp.FullName + "," + temp.Address + "," + temp.Email + "," +
                                           temp.Phone + "," + role + "," + temp.Password + "," + " ";   
                    }
                }
                else
                {
                    addString = temp.Id + "," + temp.FullName + "," + temp.Address + "," + temp.Email + "," +
                                       temp.Phone + "," + role + "," + temp.Password;
                }
                
                FileMgr.WriteIntoFile(FileType.USER, addString);
            }
        }
    }
}