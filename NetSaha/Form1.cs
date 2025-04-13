using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using NAudio.Wave;

namespace NetSaha
{
    public partial class Form1 : Form
    {
        private string kullaniciIsmi = "";
        private UdpClient udpClient;
        private Thread soundThread;
        private IWaveIn waveIn;
        private WaveFormat waveFormat = new WaveFormat(44100, 1);
        private string Message;
        public Form1()
        {
            InitializeComponent();
        }

        UDPServer server;
        UDPClient client = new UDPClient();

        private void CreateSahaButton_Click(object sender, EventArgs e)
        {
            server = new UDPServer();
            server.OnMessageReceived += LogYaz;

            // Sunucu IP'sini al ve log'a yaz
            string sunucuIP = GetLocalIPAddress();
            LogYaz("Server Started, IP: " + sunucuIP);

            server.Start();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kullaniciIsmi))
            {
                MessageBox.Show("Please Enter Name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hedefIP = "127.0.0.1";
            string sunucuIP = GetLocalIPAddress();
            hedefIP = sunucuIP;


            string mesaj = $"{kullaniciIsmi} connected.";
            byte[] byteMesaj = Encoding.UTF8.GetBytes(mesaj);

            client.SendMessage(hedefIP, byteMesaj);
            LogYaz($"[{kullaniciIsmi}] connected.");
        }


        private void NameButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Name cannot be left blank!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            kullaniciIsmi = NameTextBox.Text.Trim();
            MessageBox.Show("Name is Saved!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LogYaz(string mesaj)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogYaz), mesaj);
            }
            else
            {
                LogListBox.Items.Add(mesaj);
            }
        }

        private string GetLocalIPAddress()
        {
            string localIP = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        // Ses kaydýný baþlatýyoruz
        private void StartVoiceRecording()
        {
            waveIn = new WaveInEvent();


            for (int deviceIndex = 0; deviceIndex < WaveIn.DeviceCount; deviceIndex++)
            {
                var deviceInfo = WaveIn.GetCapabilities(deviceIndex);
                Console.WriteLine($"Device {deviceIndex}: {deviceInfo.ProductName}");
            }

            // waveIn.DeviceNumber = 0; 
            waveIn.WaveFormat = waveFormat;
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.StartRecording();
        }



        // Ses verisi alýndýðýnda çalýþacak metot
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] byteBuffer = e.Buffer;
            string hedefIP = GetLocalIPAddress();
            client.SendMessage(hedefIP, byteBuffer);
        }



        private void StartListeningForVoice()
        {
            // Gelen ses verisini alacak ve çalacak
            UdpClient udpListener = new UdpClient(8888);
            Thread receiveThread = new Thread(() =>
            {
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 8888);
                    byte[] receivedBytes = udpListener.Receive(ref remoteEndPoint);
                    // Alýnan ses verisini çal
                    PlayReceivedVoice(receivedBytes);
                }
            });
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        // Ses verisini çal
        private void PlayReceivedVoice(byte[] byteBuffer)
        {
            using (var ms = new System.IO.MemoryStream(byteBuffer))
            using (var waveOut = new WaveOutEvent())
            {
                var waveFileReader = new WaveFileReader(ms);
                waveOut.Init(waveFileReader);
                waveOut.Play();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            waveIn?.StopRecording();
            waveIn?.Dispose();
        }

        private void StartVoiceChatButton_Click(object sender, EventArgs e)
        {

            StartVoiceRecording();
            StartListeningForVoice();
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            Message = MessageTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(Message))
            {
                MessageBox.Show("Message cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                string hedefIP = GetLocalIPAddress();
                string mesaj = $"{kullaniciIsmi}: {Message}";
                byte[] byteMesaj = Encoding.UTF8.GetBytes(mesaj);
                client.SendMessage(hedefIP, byteMesaj);
                LogYaz($"[{kullaniciIsmi}]: {Message}");
            }
        }

        private void closeSahaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
