using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.Abstractions
{
    public interface IBaseInfrastructure
    {
        ISpectrumLightFactory SpectrumLightFactory { get; }

        T Resolve<T>();
    }
}
