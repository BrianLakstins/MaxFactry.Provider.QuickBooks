// <copyright file="MaxQBORSalesAndPurchaseEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/12/2021" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBORSalesAndPurchaseEntity : MaxQBBaseEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBORSalesAndPurchaseEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBORSalesAndPurchaseEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string SalesDesc
        {
            get
            {
                return this.GetString(this.DataModel.SalesDesc);
            }

            set
            {
                this.Set(this.DataModel.SalesDesc, value);
            }
        }

        public double SalesPrice
        {
            get
            {
                return this.GetDouble(this.DataModel.SalesPrice);
            }

            set
            {
                this.Set(this.DataModel.SalesPrice, value);
            }
        }

        public MaxQBBaseRefEntity IncomeAccountRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.IncomeAccountRef);
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
                Set(this.DataModel.IncomeAccountRef, value.ExportToString());
            }
        }

        public string PurchaseDesc
        {
            get
            {
                return this.GetString(this.DataModel.PurchaseDesc);
            }

            set
            {
                this.Set(this.DataModel.PurchaseDesc, value);
            }
        }

        public double PurchaseCost
        {
            get
            {
                return this.GetDouble(this.DataModel.PurchaseCost);
            }

            set
            {
                this.Set(this.DataModel.PurchaseCost, value);
            }
        }

        public MaxQBBaseRefEntity PurchaseTaxCodeRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.PurchaseTaxCodeRef);
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
                Set(this.DataModel.PurchaseTaxCodeRef, value.ExportToString());
            }
        }

        public MaxQBBaseRefEntity ExpenseAccountRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.ExpenseAccountRef);
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
                Set(this.DataModel.ExpenseAccountRef, value.ExportToString());
            }
        }

        public MaxQBBaseRefEntity PrefVendorRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.PrefVendorRef);
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
                Set(this.DataModel.PrefVendorRef, value.ExportToString());
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBORSalesAndPurchaseDataModel DataModel
        {
            get
            {
                return (MaxQBORSalesAndPurchaseDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBORSalesAndPurchaseEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBORSalesAndPurchaseEntity),
                typeof(MaxQBORSalesAndPurchaseDataModel)) as MaxQBORSalesAndPurchaseEntity;
        }
    }
}
