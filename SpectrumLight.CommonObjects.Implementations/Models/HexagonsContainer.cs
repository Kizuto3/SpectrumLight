using Prism.Mvvm;
using SpectrumLight.CommonObjects.Abstractions.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpectrumLight.CommonObjects.Implementations.Models
{
    public class HexagonsContainer : BindableBase, IHexagonsContainer
    {
        public ObservableCollection<IHexagon> Hexagons { get; }

        public HexagonsContainer()
        {
            Hexagons = new ObservableCollection<IHexagon>();
        }

        public void AddHexagon(double x, double y, double width, double height, int index)
        {
            Hexagons.Add(new Hexagon { X = x, Y = y, Width = width, Height = height, Index = index });
        }

        public void CheckHexagonsPosition()
        {
            throw new NotImplementedException();
        }

        public void RemoveHexagon(int index)
        {
            var hexagon = Hexagons.First(h => h.Index == index);
            if (hexagon != null)
            {
                Hexagons.Remove(hexagon);
            }
        }
    }
}
