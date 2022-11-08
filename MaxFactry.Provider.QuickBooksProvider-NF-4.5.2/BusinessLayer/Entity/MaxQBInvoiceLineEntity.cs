// <copyright file="MaxQBInvoiceLineEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/17/2021" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBInvoiceLineEntity : MaxQBBaseEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBInvoiceLineEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBInvoiceLineEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string TxnLineID
        {
            get
            {
                return this.GetString(this.DataModel.TxnLineID);
            }

            set
            {
                this.Set(this.DataModel.TxnLineID, value);
            }
        }

        public string ItemRef
        {
            get
            {
                string lsR = this.GetString(this.DataModel.ItemRef);
                int lnMaxLength = 31;
                if (lsR.Length > lnMaxLength)
                {
                    lsR = lsR.Substring(0, lnMaxLength);
                }

                return lsR;
            }

            set
            {
                this.Set(this.DataModel.ItemRef, value);
            }
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

        public double Quantity
        {
            get
            {
                return this.GetDouble(this.DataModel.Quantity);
            }

            set
            {
                this.Set(this.DataModel.Quantity, value);
            }
        }

        public string UnitOfMeasure
        {
            get
            {
                return this.GetString(this.DataModel.UnitOfMeasure);
            }

            set
            {
                this.Set(this.DataModel.UnitOfMeasure, value);
            }
        }

        public string OverrideUOMSetRef
        {
            get
            {
                return this.GetString(this.DataModel.OverrideUOMSetRef);
            }

            set
            {
                this.Set(this.DataModel.OverrideUOMSetRef, value);
            }
        }

        public double ORRatePriceLevel
        {
            get
            {
                return this.GetDouble(this.DataModel.ORRatePriceLevel);
            }

            set
            {
                this.Set(this.DataModel.ORRatePriceLevel, value);
            }
        }

        public double ORRate
        {
            get
            {
                return this.GetDouble(this.DataModel.ORRate);
            }

            set
            {
                this.Set(this.DataModel.ORRate, value);
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

        public double Amount
        {
            get
            {
                return this.GetDouble(this.DataModel.Amount);
            }

            set
            {
                this.Set(this.DataModel.Amount, value);
            }
        }

        public double TaxAmount
        {
            get
            {
                return this.GetDouble(this.DataModel.TaxAmount);
            }

            set
            {
                this.Set(this.DataModel.TaxAmount, value);
            }
        }

        public int OptionForPriceRuleConflict
        {
            get
            {
                return this.GetInt(this.DataModel.OptionForPriceRuleConflict);
            }

            set
            {
                this.Set(this.DataModel.OptionForPriceRuleConflict, value);
            }
        }

        public string InventorySiteRef
        {
            get
            {
                return this.GetString(this.DataModel.InventorySiteRef);
            }

            set
            {
                this.Set(this.DataModel.InventorySiteRef, value);
            }
        }

        public string InventorySiteLocationRef
        {
            get
            {
                return this.GetString(this.DataModel.InventorySiteLocationRef);
            }

            set
            {
                this.Set(this.DataModel.InventorySiteLocationRef, value);
            }
        }

        public string ORSerialLotNumber
        {
            get
            {
                return this.GetString(this.DataModel.ORSerialLotNumber);
            }

            set
            {
                this.Set(this.DataModel.ORSerialLotNumber, value);
            }
        }

        public DateTime ServiceDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.ServiceDate);
            }

            set
            {
                this.Set(this.DataModel.ServiceDate, value);
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

        public bool IsTaxable
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsTaxable);
            }

            set
            {
                this.Set(this.DataModel.IsTaxable, value);
            }
        }

        public string OverrideItemAccountRef
        {
            get
            {
                return this.GetString(this.DataModel.OverrideItemAccountRef);
            }

            set
            {
                this.Set(this.DataModel.OverrideItemAccountRef, value);
            }
        }

        public string Other1
        {
            get
            {
                return this.GetString(this.DataModel.Other1);
            }

            set
            {
                this.Set(this.DataModel.Other1, value);
            }
        }

        public string Other2
        {
            get
            {
                return this.GetString(this.DataModel.Other2);
            }

            set
            {
                this.Set(this.DataModel.Other2, value);
            }
        }

        public string LinkToTxn
        {
            get
            {
                return this.GetString(this.DataModel.LinkToTxn);
            }

            set
            {
                this.Set(this.DataModel.LinkToTxn, value);
            }
        }

        public string DataExtList
        {
            get
            {
                return this.GetString(this.DataModel.DataExtList);
            }

            set
            {
                this.Set(this.DataModel.DataExtList, value);
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

        public string defMacro
        {
            get
            {
                return this.GetString(this.DataModel.defMacro);
            }

            set
            {
                this.Set(this.DataModel.defMacro, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBInvoiceLineDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(this.DataModelType) as MaxQBInvoiceLineDataModel;
            }
        }

        public static MaxQBInvoiceLineEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBInvoiceLineEntity),
                typeof(MaxQBInvoiceLineDataModel)) as MaxQBInvoiceLineEntity;
        }

        public override string GetDefaultSortString()
        {
            return this.Desc + base.GetDefaultSortString();
        }
    }
}
