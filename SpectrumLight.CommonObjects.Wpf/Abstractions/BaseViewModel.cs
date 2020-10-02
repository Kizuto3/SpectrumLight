using SpectrumLight.CommonObjects.Abstractions.Models;
using System;

namespace SpectrumLight.CommonObjects.Wpf.Abstractions
{
    public abstract class BaseViewModel : BaseModel
    {
        public IApplicationModel ApplicationModel { get; }

        protected BaseViewModel(IApplicationModel applicationModel)
        {
            ApplicationModel = applicationModel ?? throw new ArgumentNullException(nameof(applicationModel));
        }
    }
}
