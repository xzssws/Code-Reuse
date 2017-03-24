using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace OSLibrary.网络
{
    /// <summary>
    /// 获取网络某些信息的类
    /// </summary>
    public class Network
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="hostname">主机名</param>
        /// <returns></returns>
        public static string GetIpAdrress(string hostname)
        {
            string ip;
            try
            {
                IPHostEntry iphe = Dns.GetHostEntry(hostname);
                foreach (IPAddress address in iphe.AddressList)
                {
                    ip = address.ToString();
                    if (ip != "") return ip;
                }
            }
            catch
            {
            }
            return "";
        }

        public static string GetIpV4Adress(string hostname)
        {
            string ip;
            try
            {
                IPHostEntry iphe = Dns.GetHostEntry(hostname);
                foreach (IPAddress address in iphe.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        continue;
                    }
                    ip = address.ToString();
                    if (ip != "")
                    {
                        if (Ping(ip))
                        {
                            return ip;
                        }
                    }
                }
            }
            catch
            {
            }
            return "";
        }


        /// <summary>
        /// 是否能 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public static bool Ping(string ip)
        {
            int timeout = 120;
            string data = "Test Data!";
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true; byte[] buffer = Encoding.ASCII.GetBytes(data);
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success) return true; else return false;
        }

        /// <summary>
        /// 获取本地计算机名
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// 字符形式的IP地址转换为IPAddress实例
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static IPAddress ToIPAddress(string IP)
        {
            return IPAddress.Parse(IP);
        }

        /// <summary>
        /// byte数组形式的IP地址转换为IPAddress实例
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static IPAddress ToIPAddress(byte[] IP)
        {
            return new IPAddress(IP);
        }

        /// <summary>
        /// 分开IP地址为byte数组形式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static byte[] SplitIP(string ip)
        {
            byte[] IP = new byte[4];
            string[] splitIp = ip.Split(new char[] { '.' });
            if (splitIp.Length != 4) return null;
            try
            {
                for (int i = 0; i < 4; i++)
                    IP[i] = byte.Parse(splitIp[i]);
            }
            catch
            {
                return null;
            }
            return IP;
        }
    }

}
