using System;

public static class TerminalHelperFuncions
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
        Console.ReadLine();
    }
}
