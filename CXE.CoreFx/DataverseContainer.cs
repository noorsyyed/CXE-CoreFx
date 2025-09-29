using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using CXE.CoreFx.Enums;

namespace CXE.CoreFx
{
	public class DataverseContainer : IDataverseContainer
	{
		private readonly Dictionary<Type, Registration> _registrations = new Dictionary<Type, Registration>();
		private readonly ConcurrentDictionary<Type, object> _instances = new ConcurrentDictionary<Type, object>();

		private IDataverseContainer AddRegistration(Type serviceType, Registration registration)
		{
			_registrations[serviceType] = registration;
			return this;
		}

		public IDataverseContainer AddScoped<TService>()
		{
			return AddScoped(typeof(TService));
		}
		public IDataverseContainer AddSingleton<TService>()
		{
			return AddSingleton(typeof(TService));
		}
		public IDataverseContainer AddSingleton(Type serviceType)
		{
			AddRegistration(serviceType, new Registration(serviceType, Lifetime.Singleton));
			ClearSingletonInstance(serviceType);
			return this;
		}
		public IDataverseContainer AddTransient<TService>()
		{
			return AddTransient(typeof(TService));
		}
		public IDataverseContainer AddTransient(Type serviceType)
		{
			return AddRegistration(serviceType, new Registration(serviceType, Lifetime.Transient));
		}

		public IDataverseContainer Remove<TService>()
		{
			_registrations.Remove(typeof(TService));
			return this;
		}
		private void ClearSingletonInstance(Type serviceType)
		{
			// Registration has been updated, so remove any existing instance
			_instances.TryRemove(serviceType, out _);
		}
	}

}
