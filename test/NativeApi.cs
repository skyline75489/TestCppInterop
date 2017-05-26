namespace test
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Entry
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string name;

        public int value;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class AEntry : Entry
    {
        public int a;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class BEntry : Entry
    {
        public int b;
    }

    public delegate void Execute(ExecutorCallback callback, OnInternalSuccess internalSuccess);

    public delegate void OnSuccess(Entry a, OnInternalSuccess internalSuccess);

    public delegate void OnInternalSuccess(Entry a);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class ExecutorCallback
    {
        public OnSuccess OnSuccess;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class Callbacks
    {
        public Execute Executor;
    }

    public class NativeApi
    {
        private const string DllName = ".\\testcpp.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PrintNumber")]
        internal static extern void PrintNumber(int number);

        [DllImport(DllName)]
        internal static extern void PrintNumber2(int number);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PrintEntry(Entry entry);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PrintEntry2(IntPtr entry);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Register")]
        internal static extern void Register(Callbacks config);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Start")]
        internal static extern void Start();
    }

    public abstract class CallbackBase
    {
        public abstract void Execute(ExecutorCallback executeCallback, OnInternalSuccess internalSuccess);
    }

    public class CallBackImpl : CallbackBase
    {
        public override void Execute(ExecutorCallback executeCallback, OnInternalSuccess internalSuccess)
        {
            Console.WriteLine("Executing");
            Console.WriteLine("OK");
            Console.WriteLine("Now I'm going to tell cpp");
            executeCallback.OnSuccess(new BEntry { value = 123213, b = 1231233 }, internalSuccess);
        }
    }
}