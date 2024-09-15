﻿// See https://aka.ms/new-console-template for more information


using System.Drawing;
using System.Runtime.CompilerServices;
using DotNetHospital;

public class Hospital
{
    public static void Main(string[] args)
    {
        Login().Menu();
    }

    public static User Login()
    {
        bool accountExist = false, passCorrect = false;
        //call ReadFile() to get userList
        //initialize console
        ConsoleMgr manager = new ConsoleMgr();
        Console.SetBufferSize(Console.BufferWidth, 50);


        while (!accountExist || !passCorrect)
        {
            accountExist = false;
            passCorrect = false;
            Console.Clear();
            manager.DrawSquare("login");
            manager.WriteAt("ID : ", 0, 6);
                int id = Convert.ToInt32(Console.ReadLine());
            manager.WriteAt("Password : ", 0, 7);
            string pwd = Console.ReadLine();
            foreach (User user in FileMgr.GetUserList())
            {
                if (id.Equals(user.Id))
                {
                    accountExist = true;
                    if (pwd.Equals(user.Password))
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

            if (accountExist && !passCorrect)
            {
                Console.WriteLine("Invalid Credential. please try again.");
                Console.ReadKey();
            }

            if (!accountExist)
            {
                Console.WriteLine("Account does not exist, try again");
                Console.ReadKey();
            }
        }

        return null;
    }

    

    
}
