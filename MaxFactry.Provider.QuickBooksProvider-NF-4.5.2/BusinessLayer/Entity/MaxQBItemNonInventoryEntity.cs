// <copyright file="MaxQBItemNonInventoryEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/10/2021" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBItemNonInventoryEntity : MaxQBBaseEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBItemNonInventoryEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBItemNonInventoryEntity(Type loDataModelType)
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
                this.Set(this.DataModel.Name, value.Substring(0, Math.Min(value.Length, 31)));
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
                this.Set(this.DataModel.FullName, value.Substring(0, Math.Min(value.Length, 159)));
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
                this.Set(this.DataModel.BarCodeValue, value.Substring(0, Math.Min(value.Length, 50)));
            }
        }

        public string ManufacturerPartNumber
        {
            get
            {
                return this.GetString(this.DataModel.ManufacturerPartNumber);
            }

            set
            {
                this.Set(this.DataModel.ManufacturerPartNumber, value.Substring(0, Math.Min(value.Length, 31)));
            }
        }

        public MaxQBBaseRefEntity ParentRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.ParentRef);
                if (loData is MaxData)
                {
                    loR.Load(loData as MaxData);
                }
                else if (loData is string)
                {
                    loR.Load(loData as string);
                }

                return loR;
            }

            set
            {
                Set(this.DataModel.ParentRef, value.ExportToString());
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

        public MaxQBORSalesAndPurchaseEntity ORSalesAndPurchase
        {
            get
            {
                MaxQBORSalesAndPurchaseEntity loR = MaxQBORSalesAndPurchaseEntity.Create();
                object loData = this.Get(this.DataModel.ORSalesAndPurchase);
                if (loData is MaxData)
                {
                    loR.Load(loData as MaxData);
                }
                else if (loData is string)
                {
                    loR.Load(loData as string);
                }

                return loR;
            }

            set
            {
                Set(this.DataModel.ORSalesAndPurchase, value.ExportToString());
            }
        }

        public MaxQBORSalesOrPurchaseEntity ORSalesOrPurchase
        {
            get
            {
                MaxQBORSalesOrPurchaseEntity loR = MaxQBORSalesOrPurchaseEntity.Create();
                object loData = this.Get(this.DataModel.ORSalesOrPurchase);
                if (loData is MaxData)
                {
                    loR.Load(loData as MaxData);
                }
                else if (loData is string)
                {
                    loR.Load(loData as string);
                }

                return loR;
            }

            set
            {
                Set(this.DataModel.ORSalesOrPurchase, value.ExportToString());
            }
        }

        public Guid ExternalGUID
        {
            get
            {
                return this.GetGuid(this.DataModel.ExternalGUID);
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
        protected MaxQBItemNonInventoryDataModel DataModel
        {
            get
            {
                return (MaxQBItemNonInventoryDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBItemNonInventoryEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBItemNonInventoryEntity),
                typeof(MaxQBItemNonInventoryDataModel)) as MaxQBItemNonInventoryEntity;
        }

        public MaxEntityList LoadAllQBDesktopByFullName(string lsFullName)
        {
            this.Set(this.DataModel.FullName, lsFullName);
            MaxData loDataFilter = new MaxData(this.Data);
            //// Add a Query 
            MaxDataQuery loDataQuery = this.GetDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(this.DataModel.FullName, "=", lsFullName);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(this.QBBaseDataModel.AlternateId, "=", "QBDesktop");
            loDataQuery.EndGroup();

            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            int lnTotal = int.MinValue;
            MaxDataList loDataList = MaxBaseRepository.Select(this.Data, loDataQuery, 0, 0, string.Empty);
            loR = MaxEntityList.Create(this.GetType(), loDataList);
            loR.Total = lnTotal;
            return loR;
        }
    }
}
