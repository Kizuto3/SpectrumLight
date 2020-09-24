namespace SpectrumLight.CommonObjects.Abstractions.Models
{
    public interface IHexagon
    {
        double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        int Index { get; set; }
    }
}
