// <copyright file="MaxQBItemInventoryDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxQBItemInventoryDataModel : MaxQBBaseDataModel
    {
        public readonly string ListID = "ListID";

        public readonly string TimeCreated = "TimeCreated";

        public readonly string TimeModified = "TimeModified";

        public readonly string EditSequence = "EditSequence";

        public readonly string Name = "Name";

        public readonly string FullName = "FullName";

        public readonly string BarCodeValue = "BarCodeValue";

        public readonly string ClassRef = "ClassRef";

        public readonly string ParentRef = "ParentRef";

        public readonly string Sublevel = "Sublevel";

        public readonly string ManufacturerPartNumber = "ManufacturerPartNumber";

        public readonly string UnitOfMeasureSetRef = "UnitOfMeasureSetRef";

        public readonly string IsTaxIncluded = "IsTaxIncluded";

        public readonly string SalesTaxCodeRef = "SalesTaxCodeRef";

        public readonly string SalesDesc = "SalesDesc";

        public readonly string SalesPrice = "SalesPrice";

        public readonly string IncomeAccountRef = "IncomeAccountRef";

        public readonly string PurchaseDesc = "PurchaseDesc";

        public readonly string PurchaseCost = "PurchaseCost";

        public readonly string PurchaseTaxCodeRef = "PurchaseTaxCodeRef";

        public readonly string COGSAccountRef = "COGSAccountRef";

        public readonly string PrefVendorRef = "PrefVendorRef";

        public readonly string AssetAccountRef = "AssetAccountRef";

        public readonly string ReorderPoint = "ReorderPoint";

        public readonly string Max = "Max";

        public readonly string QuantityOnHand = "QuantityOnHand";

        public readonly string AverageCost = "AverageCost";

        public readonly string QuantityOnOrder = "QuantityOnOrder";

        public readonly string QuantityOnSalesOrder = "QuantityOnSalesOrder";

        public readonly string ExternalGUID = "ExternalGUID";

        public readonly string DataExtRetList = "DataExtRetList";

        /// <summary>
        /// Initializes a new instance of the MaxQBItemInventoryDataModel class
        /// </summary>
        public MaxQBItemInventoryDataModel()
        {
            this.SetDataStorageName("MaxQBItemInventory");
            this.AddNullable(this.ListID, typeof(MaxShortString));
            this.AddNullable(this.TimeCreated, typeof(DateTime));
            this.AddNullable(this.TimeModified, typeof(DateTime));
            this.AddNullable(this.EditSequence, typeof(MaxShortString));
            this.AddNullable(this.Name, typeof(MaxShortString));
            this.AddNullable(this.FullName, typeof(MaxShortString));
            this.AddNullable(this.BarCodeValue, typeof(MaxShortString));
            this.AddNullable(this.ClassRef, typeof(MaxShortString));
            this.AddNullable(this.ParentRef, typeof(MaxShortString));
            this.AddNullable(this.Sublevel, typeof(int));
            this.AddNullable(this.UnitOfMeasureSetRef, typeof(MaxShortString));
            this.AddNullable(this.IsTaxIncluded, typeof(bool));
            this.AddNullable(this.SalesTaxCodeRef, typeof(MaxShortString));
            this.AddNullable(this.SalesDesc, typeof(MaxShortString));
            this.AddNullable(this.SalesPrice, typeof(double));
            this.AddNullable(this.IncomeAccountRef, typeof(MaxShortString));
            this.AddNullable(this.PurchaseDesc, typeof(MaxShortString));
            this.AddNullable(this.PurchaseCost, typeof(double));
            this.AddNullable(this.PurchaseTaxCodeRef, typeof(MaxShortString));
            this.AddNullable(this.COGSAccountRef, typeof(MaxShortString));
            this.AddNullable(this.PrefVendorRef, typeof(MaxShortString));
            this.AddNullable(this.AssetAccountRef, typeof(MaxShortString));
            this.AddNullable(this.ReorderPoint, typeof(double));
            this.AddNullable(this.Max, typeof(double));
            this.AddNullable(this.QuantityOnHand, typeof(double));
            this.AddNullable(this.AverageCost, typeof(double));
            this.AddNullable(this.QuantityOnOrder, typeof(double));
            this.AddNullable(this.QuantityOnSalesOrder, typeof(double));
            this.AddNullable(this.ExternalGUID, typeof(MaxShortString));
            this.AddNullable(this.DataExtRetList, typeof(MaxLongString));
        }
    }
}

