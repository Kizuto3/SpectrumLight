using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Abstractions.Models
{
    public interface IArduinoCommunicator
    {
        public bool IsConnected { get; }
        public string ConnectionName { get; }
        int BaudRate { get; }
        BluetoothPort ComPort { get; set; }
        Task<bool> ConnectDevice();
        void DisconnectDevice();
        bool SendData(string data);
        bool SendData(byte[] data);
    }
}
