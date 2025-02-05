using System.Net;
using System.Net.Sockets;

namespace CloudHRMS.Utlitity {
    public static class NetworkHelper {
        public static string GetIpAddress() {
            string ip = "127.0.0.1";
            try {
                ip = new WebClient().DownloadString("http://icanhazip.com");//49.20.11.23
            }
            catch (Exception ex) {
                ip = GetLocalIp();//192.168.xx.xx
            }
            return ip;
        }

        private static string GetLocalIp() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            return "we can't find local ip address.";
        }
    }
}
