using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base
{
	[DataContract]
	public class EntityBase : Entity
	{

		public EntityBase(string entityName) : base(entityName)
		{
		}
		public EntityBase(string entityName, Guid id) : base(entityName, id)
		{

		}
		private string GetLogicalNameFromAttribute(string propertyName)
		{
			PropertyInfo prop = GetType().GetProperty(propertyName);
			DataverseColumnAttribute attr = prop?.GetCustomAttribute<DataverseColumnAttribute>();
			return attr?.LogicalName ?? propertyName;
		}

		protected T GetValue<T>([CallerMemberName] string propertyName = null)
		{
			string logicalName = GetLogicalNameFromAttribute(propertyName);
			_ = TryGetAttributeValue<T>(logicalName, out T result);
			return result;
		}

		protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
		{
			string logicalName = GetLogicalNameFromAttribute(propertyName);
			SetAttributeValue(logicalName, value);
		}


		protected T GetEnum<T>([CallerMemberName] string propertyName = null) where T : Enum
		{
			string logicalName = GetLogicalNameFromAttribute(propertyName);
			_ = TryGetAttributeValue<OptionSetValue>(logicalName, out OptionSetValue optionSetValue);
			return optionSetValue == null ? default : (T) Enum.ToObject(typeof(T), optionSetValue.Value);
		}

		protected void SetEnum<T>(T value, [CallerMemberName] string propertyName = null) where T : Enum
		{
			string logicalName = GetLogicalNameFromAttribute(propertyName);
			SetAttributeValue(logicalName, new OptionSetValue(Convert.ToInt32(value)));
		}

		public virtual void LoadFromEntity(Entity entity)
		{
			Id = entity.Id;
			LogicalName = entity.LogicalName;
			Attributes = entity.Attributes;
		}
	}
}
