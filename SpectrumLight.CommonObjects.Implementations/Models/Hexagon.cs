using Prism.Mvvm;
using SpectrumLight.CommonObjects.Abstractions.Models;

namespace SpectrumLight.CommonObjects.Implementations.Models
{
    public class Hexagon : BindableBase, IHexagon
    {
        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private byte[] _argb;
        private int _index;

        public Hexagon() {}

        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }
        public byte[] ARGB
        {
            get => _argb;
            set => SetProperty(ref _argb, value);
        }
        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }
    }
}
