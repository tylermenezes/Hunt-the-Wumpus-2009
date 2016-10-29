using System;

namespace HuntTheWumpus 
{
	static class Program 
    {
		public static Network.Server Server {
			get;
			private set;
		}
		public static Network.Client Client {
			get;
			private set;
		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
        {
            Server = new Network.Server(new System.Net.IPEndPoint(16777343L, 9867));
            if (args.Length > 0)
                if (args[0] == "server")
                    return;

            Client = new Network.Client(new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 9867));
			
            // TODO: Splash screen?
			//new HuntTheWumpus.Screens.AxERx0().ShowDialog();

			GameAndStuff.HTW.Instance.Run();
		}
	}
}

