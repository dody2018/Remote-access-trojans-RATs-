/*
 * Created by SharpDevelop.
 * User: Dell
 * Date: 3/13/2023
 * Time: 9:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace client
{
	internal class Program
	{
		private static Socket s;
		public static void Main(string[] args)
		{
			s = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
			s.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
			s.Bind(new IPEndPoint(IPAddress.IPv6Any, 14568));
			
			Task t = Task.Factory.StartNew(listen);
			
			Console.WriteLine("-----");
			Console.ReadKey();
		}
		public static void listen() {
			
			IPEndPoint ipep = new IPEndPoint(IPAddress.IPv6Any, 0);
			EndPoint ep = ipep as EndPoint;
			Byte[] data = new byte[65535];
			s.ReceiveFrom(data, ref ep);
			List<Byte> bytes = new List<Byte>(data);
			bytes.RemoveAll(b=> b == 0);
			Console.WriteLine(Encoding.ASCII.GetString(bytes.ToArray()));
			listen();
			
		    //IPV4: 127.0.0.1
		    //IPV6: 46326:235623:346346:43254
		}
	}
}