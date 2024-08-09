using System.Net.Sockets;
using System.Text;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectToServer()
        {
            _client = new TcpClient("127.0.0.1", 6669);
            _stream = _client.GetStream();
        }

        private void SendMessage(string message)
        {
            if (_stream != null)
            {
                var data = Encoding.UTF8.GetBytes(message);
                _stream.Write(data, 0, data.Length);

                // Read response
                var buffer = new byte[1024];
                int byteCount = _stream.Read(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, byteCount);
                MessageBox.Show($"Response: {response}");
            }
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            if (_client == null || !_client.Connected)
            {
                ConnectToServer();
            }
            SendMessage("ExecuteCommand");
        }
    }
}
