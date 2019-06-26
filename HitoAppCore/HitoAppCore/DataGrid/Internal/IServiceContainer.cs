using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public interface IServiceContainer : IServiceProvider
    {
        void AddService<ServiceType>(object serviceInstance);
        ServiceType GetService<ServiceType>();
        void RemoveService<ServiceType>();
    }
}
