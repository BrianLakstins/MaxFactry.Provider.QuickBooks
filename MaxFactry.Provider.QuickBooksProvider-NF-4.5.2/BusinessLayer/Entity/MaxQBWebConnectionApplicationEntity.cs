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

    public class MaxQBWebConnectionApplicationEntity : MaxQBBaseEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBWebConnectionApplicationEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBWebConnectionApplicationEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string AppDescription
        {
            get
            {
                return this.GetString(this.DataModel.AppDescription);
            }

            set
            {
                this.Set(this.DataModel.AppDescription, value);
            }
        }

        public string AppDisplayName
        {
            get
            {
                return this.GetString(this.DataModel.AppDisplayName);
            }

            set
            {
                this.Set(this.DataModel.AppDisplayName, value);
            }
        }

        public string AppID
        {
            get
            {
                return this.GetString(this.DataModel.AppID);
            }

            set
            {
                this.Set(this.DataModel.AppID, value);
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

        public string AppSupport
        {
            get
            {
                return this.GetString(this.DataModel.AppSupport);
            }

            set
            {
                this.Set(this.DataModel.AppSupport, value);
            }
        }

        public string AppUniqueName
        {
            get
            {
                return this.GetString(this.DataModel.AppUniqueName);
            }

            set
            {
                this.Set(this.DataModel.AppUniqueName, value);
            }
        }

        public string AppURL
        {
            get
            {
                return this.GetString(this.DataModel.AppURL);
            }

            set
            {
                this.Set(this.DataModel.AppURL, value);
            }
        }

        public string AuthFlags
        {
            get
            {
                return this.GetString(this.DataModel.AuthFlags);
            }

            set
            {
                this.Set(this.DataModel.AuthFlags, value);
            }
        }

        public string FileID
        {
            get
            {
                return this.GetString(this.DataModel.FileID);
            }

            set
            {
                this.Set(this.DataModel.FileID, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsReadOnly);
            }

            set
            {
                this.Set(this.DataModel.IsReadOnly, value);
            }
        }

        public bool Notify
        {
            get
            {
                return this.GetBoolean(this.DataModel.Notify);
            }

            set
            {
                this.Set(this.DataModel.Notify, value);
            }
        }

        public Guid OwnerID
        {
            get
            {
                return this.GetGuid(this.DataModel.OwnerID);
            }

            set
            {
                this.Set(this.DataModel.OwnerID, value);
            }
        }

        public string PersonalDataPref
        {
            get
            {
                return this.GetString(this.DataModel.PersonalDataPref);
            }

            set
            {
                this.Set(this.DataModel.PersonalDataPref, value);
            }
        }

        public string QBType
        {
            get
            {
                return this.GetString(this.DataModel.QBType);
            }

            set
            {
                this.Set(this.DataModel.QBType, value);
            }
        }

        public string Scheduler
        {
            get
            {
                return this.GetString(this.DataModel.Scheduler);
            }

            set
            {
                this.Set(this.DataModel.Scheduler, value);
            }
        }

        public string UserName
        {
            get
            {
                return this.GetString(this.DataModel.UserName);
            }

            set
            {
                this.Set(this.DataModel.UserName, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBWebConnectApplicationDataModel DataModel
        {
            get
            {
                return (MaxQBWebConnectApplicationDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBWebConnectionApplicationEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBWebConnectionApplicationEntity),
                typeof(MaxQBWebConnectApplicationDataModel)) as MaxQBWebConnectionApplicationEntity;
        }
    }
}
