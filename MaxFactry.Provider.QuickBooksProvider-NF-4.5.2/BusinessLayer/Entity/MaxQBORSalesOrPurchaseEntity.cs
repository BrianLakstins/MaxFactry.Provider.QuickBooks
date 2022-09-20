// <copyright file="MaxQBORSalesOrPurchaseEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/16/2021" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Provider.QuickbooksProvider.DataLayer;
    using Interop.QBFC15;

    public class MaxQBORSalesOrPurchaseEntity : MaxQBBaseEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBORSalesOrPurchaseEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBORSalesOrPurchaseEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Desc
        {
            get
            {
                return this.GetString(this.DataModel.Desc);
            }

            set
            {
                this.Set(this.DataModel.Desc, value);
            }
        }

        public double ORPrice
        {
            get
            {
                return this.GetDouble(this.DataModel.ORPrice);
            }

            set
            {
                this.Set(this.DataModel.ORPrice, value);
            }
        }

        public MaxQBBaseRefEntity AccountRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.AccountRef);
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
                Set(this.DataModel.AccountRef, value.ExportToString());
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBORSalesOrPurchaseDataModel DataModel
        {
            get
            {
                return (MaxQBORSalesOrPurchaseDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBORSalesOrPurchaseEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBORSalesOrPurchaseEntity),
                typeof(MaxQBORSalesOrPurchaseDataModel)) as MaxQBORSalesOrPurchaseEntity;
        }
    }
}
