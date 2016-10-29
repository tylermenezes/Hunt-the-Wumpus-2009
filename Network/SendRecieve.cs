using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using HuntTheWumpus.GraphicsAndStuff;

public enum ServerCommandTypes {
	MOVEMENT,
	ORIENTATION,
	FIRE,
	SPEECH,
	CONTROL,
	DEBUG,
	INVALID
}

namespace HuntTheWumpus.Network {
	public abstract class SendRecieve {

		protected byte[] buffer = new byte[4096];
		protected Queue<byte[]> updates = new Queue<byte[]>();

		private int currentReadLocation = 0;
		private Queue<byte[]> recievedFrames = new Queue<byte[]>();

		abstract protected void dispatchMessage(byte[] message);

		public void dispatchMessages() {
			foreach (byte[] update in updates) {
				dispatchMessage(update);
			}
			updates = new Queue<byte[]>();
		}
		protected void send( byte[] data ) {
			if (data.Length > buffer.Length)
				throw new Exception(data.Length + " is greater than " + buffer.Length + "!");
			if (data.Length < buffer.Length) {
				byte[] temp = new byte[buffer.Length - data.Length];
				data = concatArray(data, temp);
			}
			updates.Enqueue(data);
		}

		protected void send( string data ) {
			#warning Puns ahead
			byte[] dataBytes = System.Text.Encoding.ASCII.GetBytes(data);
			send(dataBytes);

		}

		/// <summary>
		/// Removes \0 butes off the end of a buffer
		/// </summary>
		/// <param name="toClean">The buffer to clean.</param>
		/// <returns></returns>
		public byte[] cleanBuffer(byte[] toClean){
			return Encoding.ASCII.GetBytes(System.Text.Encoding.ASCII.GetString(buffer).TrimEnd('\0'));
		}

		/// <summary>
		/// Reads the result of a stream write (stored in the buffer), and, if necesary,
		/// keeps reading until the end.
		/// </summary>
		/// <param name="recieveStream">Stream to read from.</param>
		/// <param name="buffer">The buffer with the start of the read</param>
		/// <param name="result">The result of an async. read</param>
		/// <returns></returns>
		public byte[] readAsyncStreamWrite( NetworkStream recieveStream, byte[] buffer, IAsyncResult result ) {
			recievedFrames.Enqueue(this.cleanBuffer(buffer));

			if (!result.IsCompleted) { // If we're not done, keep reading until we are.
				int i = buffer.Length; // Starting from where we left off...
				while(recieveStream.Read(buffer, i, buffer.Length) != -1){
					recievedFrames.Enqueue(this.cleanBuffer(buffer));
					i += buffer.Length;
				}
			}
			// Done reading.
			recieveStream.EndRead(result);

			// Contents were pushed onto a Queue, retreive them now.
			byte[] toReturn = concatArrayQueue(recievedFrames);

			// Clear the queue.
			recievedFrames = new Queue<byte[]>();

			//Done.
			return toReturn;
		}
		/// <summary>
		/// Concentrates two arrays.
		/// </summary>
		/// <param name="arrayOne">The first array</param>
		/// <param name="arrayTwo">The second array</param>
		/// <returns>array[0] & array[1]</returns>
		public static byte[] concatArray( byte[] arrayOne, byte[] arrayTwo ) {
			byte[] toReturn = new byte[arrayOne.Length + arrayTwo.Length];
			int i = 0;
			foreach (byte o in arrayOne) {
				toReturn[i] = o;
				i++;
			}
			foreach (byte o in arrayTwo) {
				toReturn[i] = o;
				i++;
			}
			return toReturn;
		}

		/// <summary>
		/// Concentrates a queue of binary arrays into a single array.
		/// </summary>
		/// <param name="toConcat"><![CDATA[The queue<byte[]> to concentrate.]]></param>
		/// <returns>Concentrated Queue.</returns>
		public static byte[] concatArrayQueue( Queue<byte[]> toConcat ) {
			byte[] toReturn = new byte[0];
			foreach (byte[] o in toConcat) {
				toReturn = concatArray(toReturn, o);
			}
			return toReturn;
		}

		}
}
