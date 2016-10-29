using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HuntTheWumpus.Network {
	public class Listener {
		public enum Proto {
			TCP = 1,
			UDP
		}
		public Proto proto {
			get;
			private set;
		}
		public int port {
			get;
			private set;
		}

		/// <summary>
		/// Intilizes the nework manager.
		/// </summary>
		/// <param name="protocol">The protocol to use.</param>
		/// <param name="port">The port on which to listen.</param>
		public Listener(Proto protocol, int port) {
			this.proto = protocol;
			this.port = port;
		}

		public void bind() {

		}

		public void unbind() {

		}

		public void send( byte[] data ) {

		}
		public void send( string data ) {
			Console.WriteLine(data);
		}

		public void listen() {
		}

		public void incomingConnection( IPEndPoint connector ) {

		}
		public void incomingData( byte[] data ) {

		}
	}
}
