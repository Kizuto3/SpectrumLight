using System.Collections.ObjectModel;

namespace SpectrumLight.CommonObjects.Abstractions.Models
{
    public interface IHexagonsContainer
    {
        ObservableCollection<IHexagon> Hexagons { get; }
        void AddHexagon(double x, double y, double width, double height, int index, byte[] argb);
        void RemoveHexagon(int index);
        void CheckHexagonsPosition();
    }
}
