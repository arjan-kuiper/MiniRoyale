using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Networking;
using System.Net;
using System.IO;

namespace MINI_ROYALE
{
    class NetworkManager
    {
        public static NetworkManager instance;
        private string port;
        private HostName address;
     
        public NetworkManager(string port = "1997", string address = "") {
            instance = this;
            this.port = port;
            // Set the hostname variable. if no address was given, enter the local IP address.
            this.address = address == "" ? new HostName(Convert.ToString(IPAddress.Loopback)) : new HostName(address);
        }  

        public static string callSendServerSocket(string request) {
                Debug.WriteLine("[NetworkManager] Attempting to connect to: " + NetworkManager.instance.address.ToString());
                NetworkManager.instance.sendServerSocket();


            return "";
        }

        public async void sendServerSocket() {
            using (StreamSocket streamSocket = new StreamSocket()) {
                try {
                    Debug.WriteLine("[NetworkManager] Initiating connection!");
                    // Connect to the server on the designated address and port.
                    await streamSocket.ConnectAsync(NetworkManager.instance.address, NetworkManager.instance.port);
                    Debug.WriteLine("[NetworkManager] Connected!");

                    string request = "Hello, World!";
                    using (Stream outputStream = streamSocket.OutputStream.AsStreamForWrite()) {
                        using (var streamWriter = new StreamWriter(outputStream)) {
                            await streamWriter.WriteLineAsync(request);
                            await streamWriter.FlushAsync();
                        }
                    }
                } catch (Exception e) {
                    Debug.WriteLine("[NetworkManager] Oops! Something went wrong! {" + e.Message + '}');
                }
            }
        }
    }
}
