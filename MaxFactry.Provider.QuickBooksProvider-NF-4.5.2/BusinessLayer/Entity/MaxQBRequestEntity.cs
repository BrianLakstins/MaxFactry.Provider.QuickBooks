// <copyright file="MaxQWCAppEntity.cs" company="Lakstins Family, LLC">
// Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// </copyright>

#region License
// <license>
// This software is provided 'as-is', without any express or implied warranty. In no 
// event will the author be held liable for any damages arising from the use of this 
// software.
//  
// Permission is granted to anyone to use this software for any purpose, including 
// commercial applications, and to alter it and redistribute it freely, subject to the 
// following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not claim that 
// you wrote the original software. If you use this software in a product, an 
// acknowledgment (see the following) in the product documentation is required.
// 
// Portions Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// 
// 2. Altered source versions must be plainly marked as such, and must not be 
// misrepresented as being the original software.
// 
// 3. This notice may not be removed or altered from any source distribution.
// </license>
#endregion

#region Change Log
// <changelog>
// <change date="10/22/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Base.DataLayer.Library;
    using MaxFactry.Provider.QuickbooksProvider.DataLayer;
    using Interop.QBFC15;

    public class MaxQBRequestEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBRequestEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBRequestEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string AppId
        {
            get
            {
                return this.GetString(this.DataModel.AppId);
            }

            set
            {
                this.Set(this.DataModel.AppId, value);
            }
        }

        public string AppName
        {
            get
            {
                return this.GetString(this.DataModel.AppName);
            }

            set
            {
                this.Set(this.DataModel.AppName, value);
            }
        }

        public string Request
        {
            get
            {
                return this.GetString(this.DataModel.Request);
            }

            set
            {
                this.Set(this.DataModel.Request, value);
            }
        }

        public Guid ResponseId
        {
            get
            {
                return this.GetGuid(this.DataModel.ResponseId);
            }

            set
            {
                this.Set(this.DataModel.ResponseId, value);
            }
        }

        public Guid SessionId
        {
            get
            {
                return this.GetGuid(this.DataModel.SessionId);
            }

            set
            {
                this.Set(this.DataModel.SessionId, value);
            }
        }

        public DateTime SentDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.SentDate);
            }

            set
            {
                this.Set(this.DataModel.SentDate, value);
            }
        }

        public string Username
        {
            get
            {
                return this.GetString(this.DataModel.Username);
            }

            set
            {
                this.Set(this.DataModel.Username, value);
            }
        }

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }

        public string Description
        {
            get
            {
                return this.GetString(this.DataModel.Description);
            }

            set
            {
                this.Set(this.DataModel.Description, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBRequestDataModel DataModel
        {
            get
            {
                return (MaxQBRequestDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBRequestEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBRequestEntity),
                typeof(MaxQBRequestDataModel)) as MaxQBRequestEntity;
        }

        public MaxEntityList LoadAllActiveByUser(string lsUsername)
        {
            return this.LoadAllActiveByProperty(DataModel.Username, lsUsername);
        }

        public MaxEntityList LoadAllActiveBySessionId(Guid loSessionId)
        {
            return this.LoadAllActiveByProperty(DataModel.SessionId, loSessionId);
        }

        /// <summary>
        /// Get all active customers modified in the last 10 days
        /// </summary>
        public static string GetQueryCustomerList()
        {
            MaxQBSessionEntity loSession = MaxQBSessionEntity.Create();
            IMsgSetRequest loRequestList = loSession.GetRequestList();
            ICustomerQuery loQuery = loRequestList.AppendCustomerQueryRq();
            loQuery.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly);
            loQuery.ORCustomerListQuery.CustomerListFilter.FromModifiedDate.SetValue(DateTime.UtcNow.AddDays(-10), true);
            return loRequestList.ToXMLString();
        }
    }
}
