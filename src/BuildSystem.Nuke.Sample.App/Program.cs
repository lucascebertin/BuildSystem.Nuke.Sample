using BuildSystem.Nuke.Sample.App.Model;
using System;

namespace BuildSystem.Nuke.Sample.App
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if(args == null)
                Console.WriteLine($"Hello World! {new Calc().Sum(0,1)}");


	    Console.WriteLine("Hello World!");
            return 0;
        }
    }
}
