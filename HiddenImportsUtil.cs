using System.Runtime.InteropServices;

namespace bytearray.Util.HiddenImports
{
    public class HiddenImportsUtil
    {
        public delegate int MyDelegate(params object[] parameters);

        public static object Call<TDelegate>(string dllname, string func, TDelegate delegateFunc, params object[] parameters)
            where TDelegate : Delegate
        {
            IntPtr moduleHandle = LoadLibrary(dllname);
            IntPtr functionPtr = GetProcAddress(moduleHandle, func);

            TDelegate function = Marshal.GetDelegateForFunctionPointer<TDelegate>(functionPtr);

            var result = function.DynamicInvoke(parameters);
            //Console.WriteLine("Result: " + Convert.ToString(result));

            FreeLibrary(moduleHandle);

            return result;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string dllname);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);
    }
}
