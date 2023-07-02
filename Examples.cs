using bytearray.Util.HiddenImports;
using System.Runtime.InteropServices;

namespace HiddenIAT
{
    public delegate int MyMessageBoxDelegate(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    public delegate uint GetTickCountDelegate();

    public delegate uint GetLastErrorDelegate();

    public delegate nint GetForegroundWindowDelegate();

    internal class Program
    {
        private static void Main()
        {
            var MessageBoxAv = HiddenImportsUtil.Call("user32.dll", "MessageBoxA",
                new MyMessageBoxDelegate(MessageBoxW),
                IntPtr.Zero, "Hello", "Message", 0u);
            Console.WriteLine("MessageBoxA result: {0}", MessageBoxAv);

            var GetTickCountv = HiddenImportsUtil.Call("kernel32.dll", "GetTickCount", new GetTickCountDelegate(GetTickCount));

            Console.WriteLine("GetTickCount result: {0}", GetTickCountv);

            var GetTickCount64v = HiddenImportsUtil.Call("kernel32.dll", "GetTickCount64",
                new GetTickCount64Delegate(GetTickCount64));

            Console.WriteLine("GetTickCount result: {0}", GetTickCount64v);

            var GetLastErrorv = HiddenImportsUtil.Call("kernel32.dll", "GetLastError",
                new GetTickCount64Delegate(GetTickCount64));

            Console.WriteLine("GetLastError result: {0}", GetLastErrorv);

            var GetForegroundWindowv = HiddenImportsUtil.Call("user32.dll", "GetForegroundWindow",
                new GetForegroundWindowDelegate(GetForegroundWindow));

            Console.WriteLine("GetTickCount result: {0}", GetForegroundWindowv);

            Console.ReadKey();
        }

        public delegate ulong GetTickCount64Delegate();

        public static ulong GetTickCount64()
        {
            ulong tickCount = GetTickCount64();

            Console.WriteLine("TickCount64: " + tickCount);

            return tickCount;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        [DllImport("kernel32.dll")]
        private static extern uint GetTickCount();

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
