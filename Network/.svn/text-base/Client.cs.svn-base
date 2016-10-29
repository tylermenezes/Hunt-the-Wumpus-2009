using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace HuntTheWumpus.Network {
	public class Client : SendRecieve {
		private Socket serverConnection;
		private IPEndPoint serverConnectionDetails;

		public Client( IPEndPoint toConnect ) {
			serverConnectionDetails = toConnect;
			serverConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serverConnection.Connect(serverConnectionDetails);
			serverConnection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(acceptData), null);
		}

		private void acceptData( IAsyncResult result ) {
			System.Console.WriteLine(System.Text.Encoding.ASCII.GetString(buffer));
			serverConnection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(acceptData), null);
		}

		protected override void dispatchMessage( byte[] message ) {
			serverConnection.Send(message);
		}

		public void send( ServerCommandTypes type, byte[] data ) {
			send(SendRecieve.concatArray(System.Text.Encoding.ASCII.GetBytes(getCommandString(type)), data));
		}

		public void send( ServerCommandTypes type, string data ) {
			send(System.Text.Encoding.ASCII.GetBytes(getCommandString(type) + data));
		}

		public static string getCommandString( ServerCommandTypes type ) {
			switch (type) {
				case ServerCommandTypes.CONTROL:
					return "C";
					break;
				case ServerCommandTypes.DEBUG:
					return "D";
					break;
				case ServerCommandTypes.MOVEMENT:
					return "M";
					break;
				case ServerCommandTypes.ORIENTATION:
					return "O";
					break;
				case ServerCommandTypes.SPEECH:
					return "S";
					break;
				case ServerCommandTypes.FIRE:
					return "F";
					break;
				case ServerCommandTypes.INVALID:
					throw new Exception("Why would you want to send an invalid command?");
					break;
				default:
					return "S";
					break;
			}
		}
	}
}
