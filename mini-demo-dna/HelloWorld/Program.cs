using System;

namespace HelloWorld
{
    class Program
    {
        // This method produces the IL:
        //   LDC.I4.S 84
        //   RET
        static int GetNumber() => 84;

        static int Main(string[] args)
        {
            // Console.WriteLine(...) is much too complex for this
            // demo runtime, so comment it out.

            //Console.WriteLine("Hello World!");

            // But it's fairly simple to interpret a return instruction with a value.
            // This will cause the exit-code of the program to be the returned value.

            // A release build of this code will produce just two IL instructions:
            //   CALL GetNumber()
            //   LDC.I4.S 42
            //   ADD
            //   RET
            // The demo runtime is only able to execute these two instructions.

            return GetNumber() + 42;
        }
    }
}
