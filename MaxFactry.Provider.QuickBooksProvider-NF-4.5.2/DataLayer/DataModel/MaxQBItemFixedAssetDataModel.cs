// <copyright file="MaxQBItemFixedAssetDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxQBItemFixedAssetDataModel : MaxQBBaseDataModel
    {
        public readonly string ListID = "ListID";

        public readonly string TimeCreated = "TimeCreated";

        public readonly string TimeModified = "TimeModified";

        public readonly string EditSequence = "EditSequence";

        public readonly string Name = "Name";

        public readonly string BarCodeValue = "BarCodeValue";

        public readonly string ClassRef = "ClassRef";

        public readonly string AcquiredAs = "AcquiredAs";

        public readonly string PurchaseDesc = "PurchaseDesc";

        public readonly string PurchaseDate = "PurchaseDate";

        public readonly string PurchaseCost = "PurchaseCost";

        public readonly string VendorOrPayeeName = "VendorOrPayeeName";

        public readonly string AssetAccountRef = "AssetAccountRef";

        public readonly string FixedAssetSalesInfo = "FixedAssetSalesInfo";

        public readonly string AssetDesc = "AssetDesc";

        public readonly string Location = "Location";

        public readonly string PONumber = "PONumber";

        public readonly string SerialNumber = "SerialNumber";

        public readonly string WarrantyExpDate = "WarrantyExpDate";

        public readonly string Notes = "Notes";

        public readonly string AssetNumber = "AssetNumber";

        public readonly string CostBasis = "CostBasis";

        public readonly string YearEndAccumulatedDepreciation = "YearEndAccumulatedDepreciation";

        public readonly string YearEndBookValue = "YearEndBookValue";

        public readonly string ExternalGUID = "ExternalGUID";

        public readonly string DataExtRetList = "DataExtRetList";

        /// <summary>
        /// Initializes a new instance of the MaxQBItemFixedAssetDataModel class
        /// </summary>
        public MaxQBItemFixedAssetDataModel()
        {
            this.SetDataStorageName("MaxQBItemFixedAsset");
            this.AddNullable(this.ListID, typeof(MaxShortString));
            this.AddNullable(this.TimeCreated, typeof(DateTime));
            this.AddNullable(this.TimeModified, typeof(DateTime));
            this.AddNullable(this.EditSequence, typeof(MaxShortString));
            this.AddNullable(this.Name, typeof(MaxShortString));
            this.AddNullable(this.BarCodeValue, typeof(MaxShortString));
            this.AddNullable(this.ClassRef, typeof(MaxShortString));
            this.AddNullable(this.AcquiredAs, typeof(int));
            this.AddNullable(this.PurchaseDesc, typeof(MaxShortString));
            this.AddNullable(this.PurchaseDate, typeof(DateTime));
            this.AddNullable(this.PurchaseCost, typeof(double));
            this.AddNullable(this.VendorOrPayeeName, typeof(MaxShortString));
            this.AddNullable(this.AssetAccountRef, typeof(MaxShortString));
            this.AddNullable(this.FixedAssetSalesInfo, typeof(MaxLongString));
            this.AddNullable(this.AssetDesc, typeof(MaxShortString));
            this.AddNullable(this.Location, typeof(MaxShortString));
            this.AddNullable(this.PONumber, typeof(MaxShortString));
            this.AddNullable(this.SerialNumber, typeof(MaxShortString));
            this.AddNullable(this.WarrantyExpDate, typeof(DateTime));
            this.AddNullable(this.Notes, typeof(MaxShortString));
            this.AddNullable(this.AssetNumber, typeof(MaxShortString));
            this.AddNullable(this.CostBasis, typeof(double));
            this.AddNullable(this.YearEndAccumulatedDepreciation, typeof(double));
            this.AddNullable(this.YearEndBookValue, typeof(double));
            this.AddNullable(this.ExternalGUID, typeof(MaxShortString));
            this.AddNullable(this.DataExtRetList, typeof(MaxLongString));
        }
    }
}

