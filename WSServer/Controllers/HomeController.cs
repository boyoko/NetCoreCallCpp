using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WSServer.Models;

namespace WSServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptionsMonitor<CppDllOptions> _settings;
        private const string NativeLib = @"D:\codes\DynamicCallCplus\DynamicCallCplus\x64\Debug\CppDll.dll";

        delegate int Add(int a, int b);

        //参数为结构体
        delegate int StructParamterTest(ref ScreenRect screenRect);

        public HomeController(ILogger<HomeController> logger, IOptionsMonitor<CppDllOptions> settings)
        {
            _logger = logger;
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }


        private async Task Echo(HttpContext httpContext, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                //TODO： 可使用 System.IO.Pipelines 进行优化
                var message = await JsonSerializer.DeserializeAsync<BimBaseWebSocketMessage>(new MemoryStream(buffer, 0, result.Count));

                string serverReturnValue = string.Empty;
                switch (message.CommandName)
                {
                    case "sendCommand":
                        //todo call different c++ method
                        serverReturnValue = GetResult().ToString();
                        break;
                    default:
                        break;
                }

                var sendBytes = Encoding.UTF8.GetBytes("啦啦啦啦啦，我处理完"+ serverReturnValue);//发送的数据
                var bsend = new ArraySegment<byte>(sendBytes);

                //发送二进制数据
                //await webSocket.SendAsync(bsend, WebSocketMessageType.Binary, true, CancellationToken.None);

                //发送文本数据
                await webSocket.SendAsync(bsend, WebSocketMessageType.Text, true, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }


        private int GetResult()
        {
            if (NativeLibrary.TryLoad(NativeLib, out var handle))
            {
                if (NativeLibrary.TryGetExport(handle, "StructParamterTest", out var address))
                {
                    StructParamterTest StructParamterTest = (StructParamterTest)Marshal.GetDelegateForFunctionPointer(address, typeof(StructParamterTest));

                    ScreenRect sr = new ScreenRect();

                    sr.bottom = 3;
                    sr.left = 4;
                    sr.right = 5;
                    sr.top = 6;

                    var result = StructParamterTest(ref sr);

                    return result;
                }

            }

            return 0;
        }

    }
}
