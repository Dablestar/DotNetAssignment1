namespace DotNetHospital;

public struct Appointment
{
    

    private int doctorId, patientId;
    private string description;

    public Appointment()
    {
        
    }
    public Appointment(int doctorId, int patientId, string description)
    {
        this.doctorId = doctorId;
        this.patientId = patientId;
        this.description = description;
    }
    
    public int DoctorId
    {
        get { return doctorId;}
        set { doctorId = value; }
    }

    public int PatientId
    {
        get { return patientId; }
        set { patientId = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public override string ToString()
    {
        string doctorName = " ", patientName = " ";
        foreach (User temp in FileMgr.UserList)
        {
            if (doctorId.Equals(temp.Id))
            {
                doctorName = temp.FullName;
            }

            if (patientId.Equals(temp.Id))
            {
                patientName = temp.FullName;
            }
        }

        return doctorName + "   | " + patientName + "   | " + description;
    }
    
}