using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Abstractions.Models
{
    public interface IConnectionManager
    {
        bool IsConnected { get; set; }
        string Connection { get; set; }
        string ErrorMessage { get; set; }
    }
}
