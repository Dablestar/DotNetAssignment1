namespace DotNetHospital;

public class Execute
{
    public static void Main(string[] args)
    {
        Hospital? hospital = new Hospital();

        while (true)
        {
            //initialize with Login();
            User currentUser = hospital.Login();
            
            // Menu() returns boolean
            // if user return to login, return false, run login again
            // if user exit, return true, terminate loop and goto exit process
            if (currentUser.Menu())
            {
                break;
            }
        }
        
        //add OnApplicationExit method on exit process
        AppDomain.CurrentDomain.ProcessExit += Hospital.OnApplicationExit;
    }
}