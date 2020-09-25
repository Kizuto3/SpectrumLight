using SpectrumLight.CommonObjects.Abstractions.Enums;
using SpectrumLight.CommonObjects.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Implementations.Helpers
{
    class BluetoothComPortsSeeker
    {
        public static async Task<ICollection<BluetoothPort>> GetBluetoothCOMPort()
        {
            ICollection<BluetoothPort> Ports = await Task.Run(() => {
                Collection<BluetoothPort> COMPorts = new Collection<BluetoothPort>();
                ManagementObjectCollection results = new ManagementObjectSearcher(@"SELECT PNPClass, PNPDeviceID, Name, HardwareID FROM Win32_PnPEntity WHERE (Name LIKE '%COM%' AND PNPDeviceID LIKE '%BTHENUM%' AND PNPClass = 'Ports') OR (PNPClass = 'Bluetooth' AND PNPDeviceID LIKE '%BTHENUM\\DEV%')").Get();

                List<ManagementObject> bluetoothCOMPorts = new List<ManagementObject>();
                List<ManagementObject> bluetoothDevices = new List<ManagementObject>();

                foreach (ManagementObject queryObj in results)
                {
                    if (queryObj["PNPClass"].ToString() == "Bluetooth")
                    {
                        bluetoothDevices.Add(queryObj);
                    }
                    else if (queryObj["PNPClass"].ToString() == "Ports")
                    {
                        bluetoothCOMPorts.Add(queryObj);
                    }
                }

                foreach (ManagementObject bluetoothDevice in bluetoothDevices)
                {
                    foreach (ManagementObject bluetoothCOMPort in bluetoothCOMPorts)
                    {
                        string comPortPNPDeviceID = bluetoothCOMPort["PNPDeviceID"].ToString();
                        if (ExtractBluetoothDevice(bluetoothDevice["PNPDeviceID"].ToString()) == ExtractDevice(comPortPNPDeviceID))
                        {
                            BluetoothPort outgoingPort = new BluetoothPort(ExtractCOMPortFromName(bluetoothCOMPort["Name"].ToString()), $"{bluetoothDevice["Name"]} \'{GetDataBusName(comPortPNPDeviceID)}\'", DirectionType.Outgoing);
                            COMPorts.Add(outgoingPort);

                            if (TryFindPair(bluetoothDevice["Name"].ToString(), ((string[])bluetoothCOMPort["HardwareID"])[0], bluetoothCOMPorts, out BluetoothPort incomingPort))
                            {
                                COMPorts.Add(incomingPort);
                            }
                        }
                    }
                }
                return COMPorts;
            });
            return Ports;
        }

        private static string ExtractBluetoothDevice(string pnpDeviceID)
        {
            int startPos = pnpDeviceID.LastIndexOf('_') + 1;
            return pnpDeviceID.Substring(startPos);
        }
        private static string ExtractCOMPortFromName(string name)
        {
            int openBracket = name.IndexOf('(');
            int closeBracket = name.IndexOf(')');
            return name.Substring(openBracket + 1, closeBracket - openBracket - 1);
        }

        private static string ExtractDevice(string pnpDeviceID)
        {
            int startPos = pnpDeviceID.LastIndexOf('&') + 1;
            int length = pnpDeviceID.LastIndexOf('_') - startPos;
            return pnpDeviceID.Substring(startPos, length);
        }

        private static bool TryFindPair(string pairsName, string hardwareID, List<ManagementObject> bluetoothCOMPorts, out BluetoothPort comPort)
        {
            foreach (ManagementObject bluetoothCOMPort in bluetoothCOMPorts)
            {
                string itemHardwareID = ((string[])bluetoothCOMPort["HardwareID"])[0];
                if (hardwareID != itemHardwareID && ExtractHardwareID(hardwareID) == ExtractHardwareID(itemHardwareID))
                {
                    comPort = new BluetoothPort(ExtractCOMPortFromName(bluetoothCOMPort["Name"].ToString()), pairsName, DirectionType.Incoming);
                    return true;
                }
            }
            comPort = null;
            return false;
        }

        private static string ExtractHardwareID(string fullHardwareID)
        {
            int length = fullHardwareID.LastIndexOf('_');
            return fullHardwareID.Substring(0, length);
        }
        private static string GetDataBusName(string pnpDeviceID)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript($@"Get-PnpDeviceProperty -InstanceId '{pnpDeviceID}' -KeyName 'DEVPKEY_Device_BusReportedDeviceDesc' | select-object Data");

                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                foreach (PSObject outputItem in PSOutput)
                {
                    if (outputItem != null)
                    {
                        Console.WriteLine(outputItem.BaseObject.GetType().FullName);
                        foreach (var p in outputItem.Properties)
                        {
                            if (p.Name == "Data")
                            {
                                return p.Value?.ToString();
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
