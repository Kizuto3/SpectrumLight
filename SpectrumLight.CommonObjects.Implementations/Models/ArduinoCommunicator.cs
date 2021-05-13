using Prism.Mvvm;
using SpectrumLight.CommonObjects.Abstractions.Models;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Implementations.Models
{
    public class ArduinoCommunicator : BindableBase, IArduinoCommunicator
    {
        public bool IsConnected
        {
            get => ComPort != null && ComPort.IsOpen;
        }

        public string ConnectionName
        {
            get => ComPort == null ? "Disconected" : ComPort.Name;
        }

        public int BaudRate { get; } = 56700;

        public BluetoothPort ComPort { get; set; }

        public ArduinoCommunicator() { }

        public async Task<bool> ConnectDevice()
        {
            if (ComPort == null)
            {
                RaisePropertyChanged(nameof(IsConnected));
                RaisePropertyChanged(nameof(ConnectionName));

                return await Task.FromResult(false);
            }
            if (ComPort.IsOpen)
            {
                RaisePropertyChanged(nameof(IsConnected));
                RaisePropertyChanged(nameof(ConnectionName));

                return await Task.FromResult(true);
            }

            ComPort.BaudRate = BaudRate;

            var isPortOpened = await Task.Run(() =>
            {
                try
                {
                    ComPort.Open();
                    RaisePropertyChanged(nameof(IsConnected));
                    RaisePropertyChanged(nameof(ConnectionName));

                    return true;
                }
                catch (System.IO.IOException)
                {
                    RaisePropertyChanged(nameof(IsConnected));
                    RaisePropertyChanged(nameof(ConnectionName));

                    return false;
                }
            });

            return isPortOpened;
        }

        public void DisconnectDevice()
        {
            if (ComPort != null)
            {
                RaisePropertyChanged(nameof(IsConnected));
                RaisePropertyChanged(nameof(ConnectionName));

                ComPort.Close();
            }
        }

        public bool SendData(string data)
        {
            RaisePropertyChanged(nameof(IsConnected));
            RaisePropertyChanged(nameof(ConnectionName));

            if (!ComPort.IsOpen)
            {
                return false;
            }

            ComPort.Write(data);
            return true;
        }

        public bool SendData(byte[] data)
        {
            RaisePropertyChanged(nameof(IsConnected));
            RaisePropertyChanged(nameof(ConnectionName));

            if (!ComPort.IsOpen)
            {
                return false;
            }

            ComPort.Write(data, 0, data.Length);
            return true;
        }
    }
}
