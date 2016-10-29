using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;

namespace HuntTheWumpus.Network {
	public class Server : SendRecieve {
		private Queue<ServersClient> clients = new Queue<ServersClient>();
		private TcpListener server;
		public event GenericIncomingDataHandler clientSendData;

		public Server( IPEndPoint ipInfo ) {
			server = new TcpListener(ipInfo.Address, ipInfo.Port);
			server.Start();
			listen();
			bufferSpace = 1024 * 1024;
		}

		protected override void dispatchMessage( byte[] data ) {
			foreach (ServersClient client in clients) {
				client.clientStream.Write(data, 0, data.Length);
			}
		}

		public void send( byte[] data ) {
			base.send(data);
		}

		public void send( string data ) {
			base.send(data);
		}

		public int bufferSpace {
			get {
				return buffer.Length;
			}
			set {
				buffer = new Byte[value];
			}
		}

		private void listen() {
			server.BeginAcceptTcpClient(new AsyncCallback(doAcceptClient), server);
		}

		private void doAcceptClient( IAsyncResult tcpResult ) {
			TcpClient client = server.EndAcceptTcpClient(tcpResult);
			ServersClient _clientClass = new ServersClient(client.GetStream(), client.Client.RemoteEndPoint);
			_clientClass.incomingData += new ClientIncomingDataHandler(_clientClass_incomingData);
			clients.Enqueue(_clientClass);
			this.listen();
		}

		private void _clientClass_incomingData( byte[] data, object sender ) {
			try {
				clientSendData(data, ((ServersClient)sender).clientInfo);
			} catch {
			}
		}

		public static ServerCommandTypes getCommandType( string commandLine ) {
			switch (commandLine.Substring(0, 1).ToUpper()) {
				case "M":
					return ServerCommandTypes.MOVEMENT;
					break;
				case "A": // Depreciated
				case "O":
					return ServerCommandTypes.ORIENTATION;
					break;
				case "S": // Yell
				case "W": // Whisper (Doesn't make any difference at the moment.)
					return ServerCommandTypes.SPEECH;
					break;
				case "C":
					return ServerCommandTypes.CONTROL;
					break;
				case "D":
					return ServerCommandTypes.DEBUG;
					break;
				case "F":
					return ServerCommandTypes.FIRE;
					break;
				default:
					return ServerCommandTypes.INVALID;
					break;
			}
		}

		public static ServerCommandTypes getCommandType( byte[] commandBytes ) {
			string commandLine = System.Text.Encoding.ASCII.GetString(commandBytes);
			switch (commandLine.Substring(0, 1).ToUpper()) {
				case "M":
					return ServerCommandTypes.MOVEMENT;
					break;
				case "A": // Depreciated
				case "O":
					return ServerCommandTypes.ORIENTATION;
					break;
				case "S": // Yell
				case "W": // Whisper (Doesn't make any difference at the moment.)
					return ServerCommandTypes.SPEECH;
					break;
				case "C":
					return ServerCommandTypes.CONTROL;
					break;
				case "F":
					return ServerCommandTypes.FIRE;
					break;
				case "D":
					return ServerCommandTypes.DEBUG;
					break;
				default:
					return ServerCommandTypes.INVALID;
					break;
			}
		}

		public static string getCommand( string commandLine ) {
			return commandLine.Substring(1);
		}

		public static string getCommand( byte[] commandBytes ) {
			return System.Text.Encoding.ASCII.GetString(commandBytes).Substring(1);
		}
	}

	public class ServersClient : SendRecieve {
		public EndPoint clientInfo {get; private set;}
		public NetworkStream clientStream;
		public event ClientIncomingDataHandler incomingData;


		public ServersClient( NetworkStream client, EndPoint clientI ) {
			clientInfo = clientI;
			clientStream = client;
			clientStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(incomingAsyncData), null);
		}

		public void incomingAsyncData( IAsyncResult result ) {
			byte[] writtenData = readAsyncStreamWrite(clientStream, buffer, result);
			incomingData(writtenData, this);
			buffer = new byte[buffer.Length];
			clientStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(incomingAsyncData), null);
		}

		protected override void dispatchMessage( byte[] message ) {
			clientStream.Write(message, 0, message.Length);
		}
	}

	public delegate void ClientIncomingDataHandler(byte[] data, object sender);
	public delegate void GenericIncomingDataHandler(byte[] data, EndPoint client);
}
