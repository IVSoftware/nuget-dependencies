using ClassLibA;
using System.Runtime.InteropServices;

[DllImport("user32.dll")]
static extern bool SetForegroundWindow(IntPtr hWnd);
[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();


ClassA classA = new();
classA.Common.SayHello();
classA.Common.SayHello();
classA.Common.SayHello();

// Make sure Console is on top of VS.
SetForegroundWindow(GetConsoleWindow());
Console.ReadKey();