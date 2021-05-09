using SpectrumLight.CommonObjects.Abstractions.Models;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Implementations.Models
{
    public class ArduinoCommunicator : IArduinoCommunicator
    {
        public int BaudRate { get; } = 56700;
        public BluetoothPort ComPort { get; set; }

        public ArduinoCommunicator(){ }

        public async Task<bool> ConnectDevice()
        {
            if (ComPort == null)
            {
                return await Task.FromResult(false);
            }
            if (ComPort.IsOpen)
            {
                return await Task.FromResult(true);
            }

            ComPort.BaudRate = BaudRate;

            var isPortOpened = await Task.Run(() =>
            {
                try
                {
                    ComPort.Open();
                    return true;
                }
                catch (System.IO.IOException)
                {
                    return false;
                }
            });

            return isPortOpened;
        }

        public void DisconnectDevice()
        {
            if (ComPort != null)
            {
                ComPort.Close();
            }
        }

        public bool SendData(string data)
        {
            if (!ComPort.IsOpen)
            {
                return false;
            }

            ComPort.Write(data);
            return true;
        }

        public bool SendData(byte[] data)
        {
            if (!ComPort.IsOpen)
            {
                return false;
            }

            ComPort.Write(data, 0, data.Length);
            return true;
        }
    }
}
