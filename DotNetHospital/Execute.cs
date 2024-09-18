namespace DotNetHospital;

public class Execute
{
    public static void Main(string[] args)
    {
        Hospital? hospital = new Hospital();

        while (true)
        {
            User currentUser = hospital.Login();
            if (currentUser.Menu())
            {
                break;
            }
        }

        AppDomain.CurrentDomain.ProcessExit += Hospital.OnApplicationExit;
    }
}