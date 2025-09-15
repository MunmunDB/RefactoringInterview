namespace TCPClientAppPlugin
{
    using RefactoringInterview.Core.Application;
    using RefactoringInterview.Core.Domain;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class TcpUserInterface : IClientApplication, IDisposable
    {
        private TcpListener _listener;
        private TcpClient _client;
        private NetworkStream _stream;

        public TcpUserInterface(int port = 5000)
        {
            _listener = new TcpListener(IPAddress.Loopback, port);
            _listener.Start();
            Console.WriteLine($"TCP Server started on port {port}. Waiting for client...");
            _client = _listener.AcceptTcpClient();
            _stream = _client.GetStream();
            Console.WriteLine("Client connected!");
        }

        public string GetInput(string prompt)
        {
            // Send prompt to client
            SendMessage(prompt);

            // Receive response from client
            var buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        }

        public void Response(string message)
        {
            SendMessage(message);
        }

        private void SendMessage(string message)
        {
            if (_stream == null) return;
            var data = Encoding.UTF8.GetBytes(message + "\n");
            _stream.Write(data, 0, data.Length);
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Close();
            _listener?.Stop();
        }

        public User GetUserInput()
        {
            throw new NotImplementedException();
        }
    }

}
