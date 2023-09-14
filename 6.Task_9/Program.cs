using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    static void Main(string[] args)
    {
        
    }
}

class UserUtils
{
    private static Random random = new Random();

    public static int GenerateRandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }
}
