using Microsoft.WindowsAPICodePack.Net;
using SpectrumLight.CommonObjects.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Implementations.Models
{
    public class WiFiConnectionManager : IWiFiConnectionManager
    {
        public bool IsConnected
        {
            get
            {
                return NetworkListManager.IsConnectedToInternet;
            }
            set { }
        }

        public string Connection { get; set; }
        public string ErrorMessage { get; set; }

        public WiFiConnectionManager()
        {
            var connectedNetworks = NetworkListManager.GetNetworkConnections();

            StringBuilder connection = new StringBuilder();

            foreach(var network in connectedNetworks)
            {
                connection.Append($"{network.Network.Description}/");
            }

            if(connection.Length > 0)
            {
                connection.Remove(connection.Length - 1, 1);
                Connection = connection.ToString();
            }
        }
    }
}
