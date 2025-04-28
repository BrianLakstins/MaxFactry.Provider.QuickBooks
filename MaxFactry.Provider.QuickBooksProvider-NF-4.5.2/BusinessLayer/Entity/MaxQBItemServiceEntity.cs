// <copyright file="MaxQBItemServiceEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/4/2021" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBItemServiceEntity : MaxQBBaseEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBItemServiceEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBItemServiceEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string ListID
        {
            get
            {
                return this.GetString(this.DataModel.ListID);
            }

            set
            {
                this.Set(this.DataModel.ListID, value);
            }
        }

        public DateTime TimeCreated
        {
            get
            {
                return this.GetDateTime(this.DataModel.TimeCreated);
            }

            set
            {
                this.Set(this.DataModel.TimeCreated, value);
            }
        }

        public DateTime TimeModified
        {
            get
            {
                return this.GetDateTime(this.DataModel.TimeModified);
            }

            set
            {
                this.Set(this.DataModel.TimeModified, value);
            }
        }

        public string EditSequence
        {
            get
            {
                return this.GetString(this.DataModel.EditSequence);
            }

            set
            {
                this.Set(this.DataModel.EditSequence, value);
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

        public string FullName
        {
            get
            {
                return this.GetString(this.DataModel.FullName);
            }

            set
            {
                this.Set(this.DataModel.FullName, value);
            }
        }

        public string BarCodeValue
        {
            get
            {
                return this.GetString(this.DataModel.BarCodeValue);
            }

            set
            {
                this.Set(this.DataModel.BarCodeValue, value);
            }
        }

        public string ClassRef
        {
            get
            {
                return this.GetString(this.DataModel.ClassRef);
            }

            set
            {
                this.Set(this.DataModel.ClassRef, value);
            }
        }

        public string ParentRef
        {
            get
            {
                return this.GetString(this.DataModel.ParentRef);
            }

            set
            {
                this.Set(this.DataModel.ParentRef, value);
            }
        }

        public int Sublevel
        {
            get
            {
                return this.GetInt(this.DataModel.Sublevel);
            }

            set
            {
                this.Set(this.DataModel.Sublevel, value);
            }
        }

        public string UnitOfMeasureSetRef
        {
            get
            {
                return this.GetString(this.DataModel.UnitOfMeasureSetRef);
            }

            set
            {
                this.Set(this.DataModel.UnitOfMeasureSetRef, value);
            }
        }

        public bool IsTaxIncluded
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsTaxIncluded);
            }

            set
            {
                this.Set(this.DataModel.IsTaxIncluded, value);
            }
        }

        public string SalesTaxCodeRef
        {
            get
            {
                return this.GetString(this.DataModel.SalesTaxCodeRef);
            }

            set
            {
                this.Set(this.DataModel.SalesTaxCodeRef, value);
            }
        }

        public string ORSalesPurchase
        {
            get
            {
                return this.GetString(this.DataModel.ORSalesPurchase);
            }

            set
            {
                this.Set(this.DataModel.ORSalesPurchase, value);
            }
        }

        public string ExternalGUID
        {
            get
            {
                return this.GetString(this.DataModel.ExternalGUID);
            }

            set
            {
                this.Set(this.DataModel.ExternalGUID, value);
            }
        }

        public string DataExtRetList
        {
            get
            {
                return this.GetString(this.DataModel.DataExtRetList);
            }

            set
            {
                this.Set(this.DataModel.DataExtRetList, value);
            }
        }

        public string IncludeRetElementList
        {
            get
            {
                return this.GetString(this.DataModel.IncludeRetElementList);
            }

            set
            {
                this.Set(this.DataModel.IncludeRetElementList, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBItemServiceDataModel DataModel
        {
            get
            {
                return (MaxQBItemServiceDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBItemServiceEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBItemServiceEntity),
                typeof(MaxQBItemServiceDataModel)) as MaxQBItemServiceEntity;
        }
    }
}
