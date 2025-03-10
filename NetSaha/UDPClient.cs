using NAudio.Wave;
using System.Net.Sockets;
using System.Net;

public class UDPClient
{
    private UdpClient udpClient;
    private int port = 8888;

    public void SendMessage(string serverIP, byte[] message)
    {
        try
        {
            udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
            udpClient.Send(message, message.Length, serverEndPoint);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }
}

public class VoiceRecorder
{
    private WaveInEvent waveIn;
    private UDPClient client;
    private string hedefIP;

    public VoiceRecorder(string hedefIP)
    {
        this.hedefIP = hedefIP;
        client = new UDPClient();

        
        waveIn = new WaveInEvent();
        waveIn.DeviceNumber = 0; 
        waveIn.WaveFormat = new WaveFormat(16000, 1); 
        waveIn.DataAvailable += OnDataAvailable;
    }

    private void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        
        byte[] byteBuffer = e.Buffer;
        client.SendMessage(hedefIP, byteBuffer);
    }

    public void StartRecording()
    {
        waveIn.StartRecording();
    }

    public void StopRecording()
    {
        waveIn.StopRecording();
    }
}
