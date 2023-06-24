using System;
using System.Threading;

namespace MonoBrick.FiveOne; 

public static class tester {
    public static void main(/* String[] args */) { 
        Console.WriteLine("Five one test!");
        Brick a = new Brick("COM5", debug:true);
        a.MotorA.On(100);
        a.MotorB.On(100);
        Console.WriteLine("sleeping!");
        Thread.Sleep(1000);
        a.logRepl();
        Thread.Sleep(9000);
    
    }
}