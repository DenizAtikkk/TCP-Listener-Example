using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string ip = "IP ADRESS";
        int port = "PORT NUMBER";
        string previousData = null;

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            socket.Connect(IPAddress.Parse(ip), port);
            if (socket.Connected)
            {
                Console.WriteLine($"Connected to({ip}:{port})");


                while (true)
                {

                    socket.Send(Encoding.ASCII.GetBytes("DATA REQUEST"));

                    byte[] buffer = new byte[1024 * 1024];
                    int bytesRead = socket.Receive(buffer);

                    if (bytesRead > 0)
                    {
                        string newData = Encoding.ASCII.GetString(buffer, 0, bytesRead);


                        if (newData != previousData)
                        {

                            Console.WriteLine($"Data: {newData}");
                            previousData = newData;
                        }
                    }


                    Thread.Sleep(1000);
                }
            }
            else
            {
                Console.WriteLine($"Bağlantı kurulamadı. ({ip}:{port})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }
        finally
        {
            socket.Close();
        }
    }
}
