using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace lab_2
{
    public class WebSocketHandler : IHttpHandler
    {

        private WebSocket _socket;
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(webSocketHandler);
            } else
            {
                context.Response.WriteFile("./index.html");
            }
        }

        public async Task webSocketHandler(AspNetWebSocketContext context)
        {
            this._socket = context.WebSocket;

            while (this._socket.State == WebSocketState.Open)
            {
                Thread.Sleep(2000);
                await send(DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        private async Task<String> receive()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>();
            WebSocketReceiveResult reciveResult =
                await this._socket.ReceiveAsync(buffer, CancellationToken.None);

            return Encoding.UTF8.GetString(buffer.Array, 0, reciveResult.Count);
        }

        private async Task send(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);

            await this._socket.SendAsync(
                buffer,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );
        }

        #endregion
    }
}
