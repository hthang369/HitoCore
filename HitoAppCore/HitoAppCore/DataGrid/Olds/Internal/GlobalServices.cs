using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public class GlobalServices
    {
        // Fields
        private static IServiceContainer instance;

        // Methods
        private GlobalServices()
        {
        }

        // Properties
        public static IServiceContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceContainer();
                    instance.AddService<IServiceContainer>(instance);
                }
                return instance;
            }
        }
    }
}
