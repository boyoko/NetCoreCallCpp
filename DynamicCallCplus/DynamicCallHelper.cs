
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCallCplus
{
    public class DynamicCallHelper
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary", SetLastError = true)]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule,[MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);

        ///
        /// 通过非托管函数名转换为对应的委托 , by jingzhongrong
        ///
        /// 通过 LoadLibrary 获得的 DLL 句柄
        /// 非托管函数名
        /// 对应的委托类型
        /// 委托实例，可强制转换为适当的委托类型
        //public static Delegate GetFunctionAddress(int dllModule, String functionName, Type t)
        //{
        //    int address = GetProcAddress(dllModule, functionName);
        //    if (address == 0)
        //        return null;
        //    else
        //        return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        //}

        ///
        /// 将表示函数地址的 intPtr 实例转换成对应的委托 , by jingzhongrong
        ///
        public static Delegate GetDelegateFromintPtr(IntPtr address, Type t)
        {
            if (address == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(address, t);
        }

        ///
        /// 将表示函数地址的 int  转换成对应的委托，by jingzhongrong
        ///
        public static Delegate GetDelegateFromintPtr(int address, Type t)
        {
            if (address == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }
    }
}
