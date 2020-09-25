using SpectrumLight.CommonObjects.Abstractions.Enums;
using System.IO.Ports;

namespace SpectrumLight.CommonObjects.Abstractions.Models
{
    public sealed class BluetoothPort : SerialPort
    {
        public string Name { get; set; }
        public string Port { get; set; }
        public DirectionType DirectionType { get; set; }

        public BluetoothPort(string name, string port, DirectionType directionType) =>
            (Name, Port, DirectionType, PortName) = (name, port, directionType, port);
    }
}
