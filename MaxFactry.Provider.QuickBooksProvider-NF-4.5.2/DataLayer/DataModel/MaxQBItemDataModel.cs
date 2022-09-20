// <copyright file="MaxQBItemDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="11/3/2021" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBItemDataModel : MaxQBBaseDataModel
    {
        public readonly string ItemServiceRet = "ItemServiceRet";

        public readonly string ItemNonInventoryRet = "ItemNonInventoryRet";

        public readonly string ItemOtherChargeRet = "ItemOtherChargeRet";

        public readonly string ItemInventoryRet = "ItemInventoryRet";

        public readonly string ItemInventoryAssemblyRet = "ItemInventoryAssemblyRet";

        public readonly string ItemFixedAssetRet = "ItemFixedAssetRet";

        public readonly string ItemDiscountRet = "ItemDiscountRet";

        public readonly string ItemPaymentRet = "ItemPaymentRet";

        public readonly string ItemSalesTaxRet = "ItemSalesTaxRet";

        public readonly string ItemSalesTaxGroupRet = "ItemSalesTaxGroupRet";

        public readonly string ItemGroupRet = "ItemGroupRet";

        public readonly string ItemSubtotalRet = "ItemSubtotalRet";
        

        /// <summary>
        /// Initializes a new instance of the MaxQBItemDataModel class
        /// </summary>
        public MaxQBItemDataModel()
        {
            this.SetDataStorageName("MaxQBItem");
            this.AddNullable(this.ItemServiceRet, typeof(MaxLongString));
            this.AddNullable(this.ItemNonInventoryRet, typeof(MaxLongString));
            this.AddNullable(this.ItemOtherChargeRet, typeof(MaxLongString));
            this.AddNullable(this.ItemInventoryRet, typeof(MaxLongString));
            this.AddNullable(this.ItemInventoryAssemblyRet, typeof(MaxLongString));
            this.AddNullable(this.ItemFixedAssetRet, typeof(MaxLongString));
            this.AddNullable(this.ItemDiscountRet, typeof(MaxLongString));
            this.AddNullable(this.ItemPaymentRet, typeof(MaxLongString));
            this.AddNullable(this.ItemSalesTaxRet, typeof(MaxLongString));
            this.AddNullable(this.ItemSalesTaxGroupRet, typeof(MaxLongString));
            this.AddNullable(this.ItemGroupRet, typeof(MaxLongString));
            this.AddNullable(this.ItemSubtotalRet, typeof(MaxLongString));
        }
    }
}

