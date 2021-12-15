using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WSServer.Models
{
    public class BimBaseWebSocketMessage
    {
        /// <summary>
        /// cmd:命令名
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// 命令内容
        /// </summary>
        public string CommandContext { get; set; }
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ScreenRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }


}
