﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tracking
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="WorkflowInstanceStore")]
	public partial class WorkFlowInstanceDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public WorkFlowInstanceDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WorkflowInstanceStoreConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public WorkFlowInstanceDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WorkFlowInstanceDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WorkFlowInstanceDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WorkFlowInstanceDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<InstancesTable> InstancesTables
		{
			get
			{
				return this.GetTable<InstancesTable>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[System.Activities.DurableInstancing].InstancesTable")]
	public partial class InstancesTable
	{
		
		private System.Guid _Id;
		
		private long _SurrogateInstanceId;
		
		private System.Nullable<long> _SurrogateLockOwnerId;
		
		private System.Data.Linq.Binary _PrimitiveDataProperties;
		
		private System.Data.Linq.Binary _ComplexDataProperties;
		
		private System.Data.Linq.Binary _WriteOnlyPrimitiveDataProperties;
		
		private System.Data.Linq.Binary _WriteOnlyComplexDataProperties;
		
		private System.Data.Linq.Binary _MetadataProperties;
		
		private System.Nullable<byte> _DataEncodingOption;
		
		private System.Nullable<byte> _MetadataEncodingOption;
		
		private long _Version;
		
		private System.Nullable<System.DateTime> _PendingTimer;
		
		private System.DateTime _CreationTime;
		
		private System.Nullable<System.DateTime> _LastUpdated;
		
		private System.Nullable<System.Guid> _WorkflowHostType;
		
		private System.Nullable<long> _ServiceDeploymentId;
		
		private string _SuspensionExceptionName;
		
		private string _SuspensionReason;
		
		private string _BlockingBookmarks;
		
		private string _LastMachineRunOn;
		
		private string _ExecutionStatus;
		
		private System.Nullable<bool> _IsInitialized;
		
		private System.Nullable<bool> _IsSuspended;
		
		private System.Nullable<bool> _IsReadyToRun;
		
		private System.Nullable<bool> _IsCompleted;
		
		public InstancesTable()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SurrogateInstanceId", AutoSync=AutoSync.Always, DbType="BigInt NOT NULL IDENTITY", IsDbGenerated=true)]
		public long SurrogateInstanceId
		{
			get
			{
				return this._SurrogateInstanceId;
			}
			set
			{
				if ((this._SurrogateInstanceId != value))
				{
					this._SurrogateInstanceId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SurrogateLockOwnerId", DbType="BigInt")]
		public System.Nullable<long> SurrogateLockOwnerId
		{
			get
			{
				return this._SurrogateLockOwnerId;
			}
			set
			{
				if ((this._SurrogateLockOwnerId != value))
				{
					this._SurrogateLockOwnerId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrimitiveDataProperties", DbType="VarBinary(MAX)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary PrimitiveDataProperties
		{
			get
			{
				return this._PrimitiveDataProperties;
			}
			set
			{
				if ((this._PrimitiveDataProperties != value))
				{
					this._PrimitiveDataProperties = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ComplexDataProperties", DbType="VarBinary(MAX)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ComplexDataProperties
		{
			get
			{
				return this._ComplexDataProperties;
			}
			set
			{
				if ((this._ComplexDataProperties != value))
				{
					this._ComplexDataProperties = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WriteOnlyPrimitiveDataProperties", DbType="VarBinary(MAX)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary WriteOnlyPrimitiveDataProperties
		{
			get
			{
				return this._WriteOnlyPrimitiveDataProperties;
			}
			set
			{
				if ((this._WriteOnlyPrimitiveDataProperties != value))
				{
					this._WriteOnlyPrimitiveDataProperties = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WriteOnlyComplexDataProperties", DbType="VarBinary(MAX)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary WriteOnlyComplexDataProperties
		{
			get
			{
				return this._WriteOnlyComplexDataProperties;
			}
			set
			{
				if ((this._WriteOnlyComplexDataProperties != value))
				{
					this._WriteOnlyComplexDataProperties = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetadataProperties", DbType="VarBinary(MAX)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary MetadataProperties
		{
			get
			{
				return this._MetadataProperties;
			}
			set
			{
				if ((this._MetadataProperties != value))
				{
					this._MetadataProperties = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataEncodingOption", DbType="TinyInt")]
		public System.Nullable<byte> DataEncodingOption
		{
			get
			{
				return this._DataEncodingOption;
			}
			set
			{
				if ((this._DataEncodingOption != value))
				{
					this._DataEncodingOption = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MetadataEncodingOption", DbType="TinyInt")]
		public System.Nullable<byte> MetadataEncodingOption
		{
			get
			{
				return this._MetadataEncodingOption;
			}
			set
			{
				if ((this._MetadataEncodingOption != value))
				{
					this._MetadataEncodingOption = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", DbType="BigInt NOT NULL")]
		public long Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this._Version = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PendingTimer", DbType="DateTime")]
		public System.Nullable<System.DateTime> PendingTimer
		{
			get
			{
				return this._PendingTimer;
			}
			set
			{
				if ((this._PendingTimer != value))
				{
					this._PendingTimer = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreationTime", DbType="DateTime NOT NULL")]
		public System.DateTime CreationTime
		{
			get
			{
				return this._CreationTime;
			}
			set
			{
				if ((this._CreationTime != value))
				{
					this._CreationTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastUpdated", DbType="DateTime")]
		public System.Nullable<System.DateTime> LastUpdated
		{
			get
			{
				return this._LastUpdated;
			}
			set
			{
				if ((this._LastUpdated != value))
				{
					this._LastUpdated = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WorkflowHostType", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> WorkflowHostType
		{
			get
			{
				return this._WorkflowHostType;
			}
			set
			{
				if ((this._WorkflowHostType != value))
				{
					this._WorkflowHostType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServiceDeploymentId", DbType="BigInt")]
		public System.Nullable<long> ServiceDeploymentId
		{
			get
			{
				return this._ServiceDeploymentId;
			}
			set
			{
				if ((this._ServiceDeploymentId != value))
				{
					this._ServiceDeploymentId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SuspensionExceptionName", DbType="NVarChar(450)")]
		public string SuspensionExceptionName
		{
			get
			{
				return this._SuspensionExceptionName;
			}
			set
			{
				if ((this._SuspensionExceptionName != value))
				{
					this._SuspensionExceptionName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SuspensionReason", DbType="NVarChar(MAX)")]
		public string SuspensionReason
		{
			get
			{
				return this._SuspensionReason;
			}
			set
			{
				if ((this._SuspensionReason != value))
				{
					this._SuspensionReason = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BlockingBookmarks", DbType="NVarChar(MAX)")]
		public string BlockingBookmarks
		{
			get
			{
				return this._BlockingBookmarks;
			}
			set
			{
				if ((this._BlockingBookmarks != value))
				{
					this._BlockingBookmarks = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastMachineRunOn", DbType="NVarChar(450)")]
		public string LastMachineRunOn
		{
			get
			{
				return this._LastMachineRunOn;
			}
			set
			{
				if ((this._LastMachineRunOn != value))
				{
					this._LastMachineRunOn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExecutionStatus", DbType="NVarChar(450)")]
		public string ExecutionStatus
		{
			get
			{
				return this._ExecutionStatus;
			}
			set
			{
				if ((this._ExecutionStatus != value))
				{
					this._ExecutionStatus = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsInitialized", DbType="Bit")]
		public System.Nullable<bool> IsInitialized
		{
			get
			{
				return this._IsInitialized;
			}
			set
			{
				if ((this._IsInitialized != value))
				{
					this._IsInitialized = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsSuspended", DbType="Bit")]
		public System.Nullable<bool> IsSuspended
		{
			get
			{
				return this._IsSuspended;
			}
			set
			{
				if ((this._IsSuspended != value))
				{
					this._IsSuspended = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsReadyToRun", DbType="Bit")]
		public System.Nullable<bool> IsReadyToRun
		{
			get
			{
				return this._IsReadyToRun;
			}
			set
			{
				if ((this._IsReadyToRun != value))
				{
					this._IsReadyToRun = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsCompleted", DbType="Bit")]
		public System.Nullable<bool> IsCompleted
		{
			get
			{
				return this._IsCompleted;
			}
			set
			{
				if ((this._IsCompleted != value))
				{
					this._IsCompleted = value;
				}
			}
		}
	}
}
#pragma warning restore 1591