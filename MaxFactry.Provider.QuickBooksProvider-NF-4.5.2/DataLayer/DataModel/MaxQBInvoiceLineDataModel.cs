// <copyright file="MaxQBInvoiceLineDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBInvoiceLineDataModel : MaxQBBaseDataModel
    {
        public readonly string TxnLineID = "TxnLineID";

        public readonly string ItemRef = "ItemRef";

        public readonly string Desc = "Desc";

        public readonly string Quantity = "Quantity";

        public readonly string UnitOfMeasure = "UnitOfMeasure";

        public readonly string OverrideUOMSetRef = "OverrideUOMSetRef";

        public readonly string ORRatePriceLevel = "ORRatePriceLevel";

        public readonly string ORRate = "ORRate";

        public readonly string ClassRef = "ClassRef";

        public readonly string Amount = "Amount";

        public readonly string TaxAmount = "TaxAmount";

        public readonly string OptionForPriceRuleConflict = "OptionForPriceRuleConflict";

        public readonly string InventorySiteRef = "InventorySiteRef";

        public readonly string InventorySiteLocationRef = "InventorySiteLocationRef";

        public readonly string ORSerialLotNumber = "ORSerialLotNumber";

        public readonly string ServiceDate = "ServiceDate";

        public readonly string SalesTaxCodeRef = "SalesTaxCodeRef";

        public readonly string IsTaxable = "IsTaxable";

        public readonly string OverrideItemAccountRef = "OverrideItemAccountRef";

        public readonly string Other1 = "Other1";

        public readonly string Other2 = "Other2";

        public readonly string LinkToTxn = "LinkToTxn";

        public readonly string DataExtList = "DataExtList";

        public readonly string DataExtRetList = "DataExtRetList";

        public readonly string defMacro = "defMacro";

        /// <summary>
        /// Initializes a new instance of the MaxQBInvoiceLineDataModel class
        /// </summary>
        public MaxQBInvoiceLineDataModel()
        {
            this.SetDataStorageName("MaxQBInvoiceLine");
            this.AddNullable(this.TxnLineID, typeof(MaxShortString));
            this.AddNullable(this.ItemRef, typeof(MaxShortString));
            this.AddNullable(this.Desc, typeof(MaxShortString));
            this.AddNullable(this.Quantity, typeof(double));
            this.AddNullable(this.UnitOfMeasure, typeof(MaxShortString));
            this.AddNullable(this.OverrideUOMSetRef, typeof(MaxShortString));
            this.AddNullable(this.ORRatePriceLevel, typeof(double));
            this.AddNullable(this.ORRate, typeof(double));
            this.AddNullable(this.ClassRef, typeof(MaxShortString));
            this.AddNullable(this.Amount, typeof(double));
            this.AddNullable(this.TaxAmount, typeof(double));
            this.AddNullable(this.OptionForPriceRuleConflict, typeof(int));
            this.AddNullable(this.InventorySiteRef, typeof(MaxShortString));
            this.AddNullable(this.InventorySiteLocationRef, typeof(MaxShortString));
            this.AddNullable(this.ORSerialLotNumber, typeof(MaxShortString));
            this.AddNullable(this.ServiceDate, typeof(DateTime));
            this.AddNullable(this.SalesTaxCodeRef, typeof(MaxShortString));
            this.AddNullable(this.IsTaxable, typeof(bool));
            this.AddNullable(this.OverrideItemAccountRef, typeof(MaxShortString));
            this.AddNullable(this.Other1, typeof(MaxShortString));
            this.AddNullable(this.Other2, typeof(MaxShortString));
            this.AddNullable(this.LinkToTxn, typeof(MaxShortString));
            this.AddNullable(this.DataExtList, typeof(MaxLongString));
            this.AddNullable(this.DataExtRetList, typeof(MaxLongString));
            this.AddNullable(this.defMacro, typeof(MaxShortString));
        }
    }
}

