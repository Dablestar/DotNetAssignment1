namespace DotNetHospital;
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