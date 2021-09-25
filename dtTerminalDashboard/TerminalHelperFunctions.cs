using System;

public static class TerminalHelperFunctions
{
	public static string AskUser(string msg, bool sameLine = true)
    {
        if (sameLine)
        {
            Console.Write(msg); 
        } 
        else
        {
            Console.WriteLine(msg);
        }
        return Console.ReadLine();
    }
    public static void WarnUser(string msg)
    {
        Console.WriteLine(msg);
    }
    public static void ShowUserInfo(string msg)
    {
        Console.WriteLine($"###########################\n{msg}\n###########################");
    }
}
