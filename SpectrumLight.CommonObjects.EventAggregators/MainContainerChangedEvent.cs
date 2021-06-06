using Prism.Events;
using SpectrumLight.CommonObjects.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumLight.CommonObjects.EventAggregators
{
    public class MainContainerChangedEvent : PubSubEvent<IHexagonsContainer>
    {
    }
}
