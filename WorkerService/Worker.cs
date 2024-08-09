using System.Net.Sockets;
using System.Net;
using System.Text;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _port = 6669;
        private TcpListener _listener;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener = new TcpListener(IPAddress.Loopback, _port);
            _listener.Start();

            while (!stoppingToken.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _ = HandleClient(client, stoppingToken);
            }

            _listener.Stop();
        }

        private async Task HandleClient(TcpClient client, CancellationToken stoppingToken)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                var buffer = new byte[1024];
                int byteCount;
                while ((byteCount = await stream.ReadAsync(buffer, 0, buffer.Length, stoppingToken)) != 0)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    _logger.LogInformation($"Received: {message}");

                    // Process the message and prepare a response
                    var responseMessage = "Command executed by the service!";
                    var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length, stoppingToken);
                }
            }
        }
    }
}
