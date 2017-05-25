using System;

namespace test
{
    using System.Runtime.InteropServices;

    internal class Program
    {
        private static void Main(string[] args)
        {
            InteropCppTest();
            Console.Read();
        }

        private static void InteropCppTest()
        {
            NativeApi.PrintNumber(42);
            NativeApi.PrintNumber2(53);
            var ret = NativeApi.PrintEntry(new Entry
                                     {
                                         name = "answer",
                                         value = 111
                                     });

            Console.WriteLine(ret);

            var e = new Entry { name = "answer", value = 111111 };
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(e));
            Marshal.StructureToPtr(e, ptr, false);

            NativeApi.PrintEntry2(ptr);

            Marshal.DestroyStructure(ptr, typeof(Entry));

            var impl = new CallBackImpl();

            NativeApi.Register(new Callbacks
            {
                Executor = impl.Execute
            });

//            GC.Collect();
//            GC.WaitForPendingFinalizers();

            NativeApi.Start();

        }
    }
}