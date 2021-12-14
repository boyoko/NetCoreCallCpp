using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DynamicCallCplus
{
    class Program
    {
        const string NativeLib = @"D:\codes\DynamicCallCplus\DynamicCallCplus\x64\Debug\CppDll.dll";

        delegate int Add(int a, int b);

        static void Main(string[] args)
        {
            if (NativeLibrary.TryLoad(NativeLib, out var handle))
            {
                if (NativeLibrary.TryGetExport(handle, "Add", out var address))
                {
                    Add add = (Add)Marshal.GetDelegateForFunctionPointer(address, typeof(Add));
                    Console.WriteLine(add(2, 3));
                }

            }
            Console.ReadKey();
        }



        private static void DllImportCall()
        {
            int hModule = DynamicCallHelper.LoadLibrary(NativeLib);

            IntPtr intPtr = DynamicCallHelper.GetProcAddress(hModule, "Add");

            Add add1 = (Add)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(Add));
            var ret = add1(2, 3);
            Console.WriteLine(ret);
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            IntPtr libHandle = IntPtr.Zero;
            //you can add here different loading logic
            if (libraryName == NativeLib && RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitProcess)
            {
                NativeLibrary.TryLoad(NativeLib, out libHandle);
            }


            //if (libraryName == NativeLib)
            //{
            //    NativeLibrary.TryLoad("libsomelibrary.so", assembly, DllImportSearchPath.ApplicationDirectory, out libHandle);
            //}
            return libHandle;
        }


    }
}
