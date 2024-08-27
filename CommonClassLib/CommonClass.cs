using System;

namespace CommonClassLib
{
    public class CommonClass
    {
        int helloCount = 1;
        string times => helloCount > 1 ? "times" : "time";
        public void SayHello()
        {
            Console.WriteLine($"Hello {helloCount} {times}.");
            helloCount++;
        }
    }
}
