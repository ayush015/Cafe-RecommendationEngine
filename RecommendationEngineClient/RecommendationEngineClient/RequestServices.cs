using RecommendationEngineClient.Common;
using System.Net.Sockets;
using System.Text;

namespace RecommendationEngineClient
{
    public class RequestServices
    {
        public async Task<string> SendRequestAsync(string request)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(ApplicationConstants.SocketAddress, ApplicationConstants.Port);
                NetworkStream stream = client.GetStream();

                byte[] data = Encoding.ASCII.GetBytes(request);
                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[2048];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string responseData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                return responseData;
            }
        }
    }
}
