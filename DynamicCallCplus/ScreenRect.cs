using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCallCplus
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ScreenRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    //图片颜色通道
    public enum BPMChannel
    {
        enRGBA,
        enABGR,
        enBGRA,
        enARGB,
        enRGB,
        enBGR
    };

}
