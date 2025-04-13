using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class UDPServer
{
    private UdpClient udpListener;
    private Thread listenerThread;
    private int port = 8888; 

    public event Action<string> OnMessageReceived;

    public void Start()
    {
        udpListener = new UdpClient(port);
        listenerThread = new Thread(ListenForMessages);
        listenerThread.IsBackground = true;
        listenerThread.Start();
        OnMessageReceived?.Invoke("Server Started, port: " + port);
    }

    private void ListenForMessages()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        while (true)
        {
            try
            {
                byte[] receivedBytes = udpListener.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(receivedBytes);
             // OnMessageReceived?.Invoke($"[GELEN] {remoteEndPoint}: {receivedMessage}");
            }
            catch (Exception ex)
            {
                OnMessageReceived?.Invoke("Error: " + ex.Message);
                break;
            }
        }
    }

    public void Stop()
    {
        udpListener?.Close();
        listenerThread?.Abort();
        OnMessageReceived?.Invoke("Server Stopped.");
    }
}
