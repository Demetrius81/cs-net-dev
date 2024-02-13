using System.Net.Sockets;
using System.Net;

namespace Lesson1.Seminar.Server;

internal partial class Program
{
    public class ChatServer
    {
        private readonly TcpListener _listener;

        public ChatServer()
        {
            _listener = new TcpListener(IPAddress.Any, 5000);
        }

        public async Task Run(string[] args)
        {
            try
            {
                _listener.Start();
                await Console.Out.WriteLineAsync("Server is started.");

                while (true)
                {
                    var tcpClient = await _listener.AcceptTcpClientAsync();
                    await Console.Out.WriteLineAsync("Sussesfully connected");
                    Task.Run(() => ProcessClient(tcpClient));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public async Task ProcessClient(TcpClient client)
        {
            var reader = new StreamReader(client.GetStream());
            var message = await reader.ReadLineAsync();
            await Console.Out.WriteLineAsync("www");
        }
    }
}
