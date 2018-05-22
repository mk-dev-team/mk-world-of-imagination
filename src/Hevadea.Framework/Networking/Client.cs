﻿using System;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public sealed class Client : Peer
    {
        private const int CONNECTION_TIMEOUT = 2000;

        public bool Connected => Socket.IsConnected();

        public delegate void ConnectionChangeHandler();

        public ConnectionChangeHandler ConnectionLost;

        public Client(bool noDelay = false) : base(noDelay)
        {
        }

        public bool Connect(string ip, int port, byte attemptCount)
        {
            for (var i = 0; i < attemptCount; i++)
            {
                try
                {
                    Socket = new Socket(Socket.AddressFamily, Socket.SocketType, Socket.ProtocolType)
                    {
                        NoDelay = this.NoDelay
                    };

                    var result = Socket.BeginConnect(ip, port, null, null);

                    if (result.AsyncWaitHandle.WaitOne(CONNECTION_TIMEOUT, true) && Socket.Connected)
                    {
                        new Thread(x => BeginReceiving(Socket, 0)).Start();
                        Console.WriteLine("[NetClient] Connection established with " + ip + ":" + port);
                        Socket.EndConnect(result);
                        return true;
                    }
                }
                catch (SocketException) { }
                catch (InvalidOperationException) { }

                Socket.Close();
            }
            return false;
        }

        public void Disconnect()
        {
            Socket.Dispose();
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = NoDelay
            };
        }

        public void SendData(PacketBuilder packet) => SendData(Socket, packet);
        public void SendData(byte[] packet) => SendData(Socket, packet);

        public override void HandleDisconnectedSocket(Socket socket)
        {
            ConnectionLost?.Invoke();

            Socket.Dispose();
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = base.NoDelay
            };
        }
    }
}