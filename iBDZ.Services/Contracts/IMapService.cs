using iBDZ.Data.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace iBDZ.Services.Contracts
{
    public interface IMapService
    {
		MapRenderData GetRenderingData();
    }
}
