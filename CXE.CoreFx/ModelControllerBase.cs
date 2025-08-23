using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CXE.CoreFx.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace CXE.CoreFx.Base
{
	public class ModelControllerBase<T> : IEntityDataSourceRetrieverService where T : EntityBase, new()
	{
		public ModelControllerBase(IOrganizationService service, T entity, LoggerBase logger)
		{
			EntityMetadata = null;
			Service = service ?? throw new ArgumentNullException(nameof(service), "Service cannot be null.");
			Entity = entity ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
			Logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");

		}
		protected virtual T Entity
		{
			get;
			set;
		}
		public LoggerBase Logger
		{
			get;
		}
		protected virtual IOrganizationService Service
		{
			get;
			set;
		}

		protected virtual string TableName => EntityMetadata?.SchemaName;
		protected virtual string TableId => EntityMetadata?.DataSourceId?.ToString() ?? Guid.Empty.ToString();
		public virtual string TableDisplayName => EntityMetadata?.DisplayName?.UserLocalizedLabel?.Label;
		public virtual string TablePluralDisplayName => EntityMetadata?.DisplayCollectionName?.UserLocalizedLabel?.Label;
		public virtual string TableDescription => EntityMetadata?.Description?.UserLocalizedLabel?.Label;
		public virtual string TablePluralDescription => $"Table for {TablePluralDisplayName} entities.";
		public virtual string TableLogicalName => Entity.LogicalName; // this.EntityMetadata?.LogicalName;
		public virtual EntityMetadata EntityMetadata
		{
			get;
			set;
		}

		Entity IEntityDataSourceRetrieverService.RetrieveEntityDataSource()
		{
			throw new NotImplementedException();
		}

		public virtual EntityMetadata RetrieveEntityMetadata()
		{
			Logger.Log($"Retrieving entity metadata for {TableLogicalName}");
			RetrieveEntityRequest req = new()
			{
				EntityFilters = EntityFilters.Entity,
				LogicalName = TableLogicalName
			};
			RetrieveEntityResponse response = (RetrieveEntityResponse) Service.Execute(req);
			Logger.Log($"Entity metadata retrieved for {TableLogicalName}");
			return response == null || response.EntityMetadata == null
				? throw new InvalidOperationException($"Entity metadata for {TableLogicalName} could not be retrieved.")
				: response.EntityMetadata;
		}



		private static ColumnSet GetColumnSet(string[] propertyNames)
		{
			ColumnSet columns = propertyNames.Length > 0
				? new ColumnSet(propertyNames)
				: new ColumnSet(true); // fallback to all columns if none found
			return columns;
		}

		private static string[] GetColumnNames()
		{
			string[] propertyNames = typeof(T)
									.GetProperties(BindingFlags.Public | BindingFlags.Instance)
									.Select(x => x.GetCustomAttribute<DataverseColumnAttribute>())
									.Where(x => x != null)
									.Select(x => x.LogicalName)
									.Distinct()
									.ToArray();
			return propertyNames;

		}

		public static string GetLogicalName(string propertyName)
		{
			PropertyInfo prop = typeof(T).GetProperty(propertyName);
			DataverseColumnAttribute attr = prop?.GetCustomAttribute<DataverseColumnAttribute>();
			return attr?.LogicalName ?? propertyName;
		}

		public virtual List<T> RetrieveMultiple(params ConditionExpression[] conditionExpression)
		{
			List<T> list = new();
			QueryExpression query = new(TableLogicalName)
			{
				ColumnSet = GetColumnSet(GetColumnNames())
			};
			if (conditionExpression?.Length is not null and > 0)
			{
				foreach (ConditionExpression exp in conditionExpression)
				{
					query.Criteria.AddCondition(exp);
				}
			}
			foreach (Entity item in Service.RetrieveMultiple(query).Entities)
			{
				T instance = new();
				instance.LoadFromEntity(item);
				list.Add(instance);
			}
			return list;
		}
		public virtual T Retrieve()
		{
			ColumnSet columns = GetColumnSet(GetColumnNames());

			T entity = (T) Service.Retrieve(TableLogicalName, Entity.Id, columns);
			return entity;
		}
	}
}
