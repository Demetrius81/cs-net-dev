using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Seminar.Server;
public class PromotedServer
{
    private TcpListener _listener;
    private List<ClientInfo> _clients;

    public PromotedServer()
    {
        this._listener = new TcpListener(IPAddress.Any, 5000);
        this._clients = new List<ClientInfo>();
    }

    public void Run()
    {
        TcpClient client = null;
        _listener.Start();
    }
}
