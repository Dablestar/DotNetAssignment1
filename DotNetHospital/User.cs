namespace DotNetHospital;

public enum UserType
{
    Patient,
    Doctor,
    Admin
}

public class User
{
    private int id;
    private string fullName, address, email, phone, password;
    public ConsoleMgr manager;

    public User()
    {
    }

    public User(int id, string fullName, string address, string email, string phone, string password)
    {
        this.id = id;
        this.fullName = fullName;
        this.address = address;
        this.email = email;
        this.phone = phone;
        this.password = password;
        manager = new ConsoleMgr();
    }

    public virtual void Menu()
    {
        Console.WriteLine("Debug");
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string FullName
    {
        get { return fullName; }
        set { fullName = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public ConsoleMgr Manager
    {
        get { return manager; }
    }
}
