using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
    {
        private Dictionary<Type, object> services;

        private Dictionary<Type, object> Services
        {
            get
            {
                if (this.services == null)
                {
                    this.services = new Dictionary<Type, object>();
                }
                return this.services;
            }
        }

        public ServiceContainer()
        {
        }

        public void AddService<ServiceType>(object serviceInstance)
        {
            if (serviceInstance == null)
            {
                throw new ArgumentNullException("serviceInstance");
            }
            Type key = typeof(ServiceType);
            if (!IntrospectionExtensions.GetTypeInfo(key).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(serviceInstance.GetType())))
            {
                throw new ArgumentException(key.FullName);
            }
            if (this.Services.ContainsKey(key))
            {
                throw new ArgumentException(key.FullName);
            }
            this.Services[key] = serviceInstance;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dictionary<Type, object> services = this.services;
                this.services = null;
                if (services != null)
                {
                    foreach (IDisposable disposable in services.Values)
                    {
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
            }
        }

        public ServiceType GetService<ServiceType>() => ((ServiceType)this.GetService((Type)typeof(ServiceType)));

        public object GetService(Type serviceType)
        {
            this.Services.TryGetValue(serviceType, out object obj2);
            return obj2;
        }

        public void RemoveService<ServiceType>()
        {
            this.Services.Remove(typeof(ServiceType));
        }
    }
}
