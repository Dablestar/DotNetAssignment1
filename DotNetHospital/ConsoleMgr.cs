namespace DotNetHospital;

public class ConsoleMgr
{
    private const int originX = 4, originY = 2;

    public ConsoleMgr()
    {
        Console.Clear();
    }
    public void WriteAt(string str, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(originX + x, originY + y);
            Console.Write(str);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public void DrawSquare(string menuName)
    {
        Console.Clear();
        WriteAt("┌", 0, 0);
        for (int i = 1; i < 40; i++)
        {
            WriteAt("─", i, 0);
        }
        WriteAt("┐", 40, 0);
        for (int i = 1; i < 4; i++)
        {
            WriteAt("│", 0, i);
            WriteAt("│", 40, i);
        }
        WriteAt("└", 0, 4);
        for (int i = 1; i < 40; i++)
        {
            WriteAt("─", i, 4);
        }
        WriteAt("┘", 40, 4);
        
        WriteAt("DOTNET Hospital Management System", 4, 1);
        WriteAt("-------------------------------------", 2, 2);
        WriteAt(menuName, 20 - (menuName.Length / 2), 3);
    }
    
    
    
}