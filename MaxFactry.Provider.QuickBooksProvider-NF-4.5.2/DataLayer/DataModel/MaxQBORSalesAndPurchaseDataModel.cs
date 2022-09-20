// <copyright file="MaxQBORSalesAndPurchaseDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBORSalesAndPurchaseDataModel : MaxQBBaseDataModel
    {
        public readonly string SalesDesc = "SalesDesc";

        public readonly string SalesPrice = "SalesPrice";

        public readonly string IncomeAccountRef = "IncomeAccountRef";

        public readonly string PurchaseDesc = "PurchaseDesc";

        public readonly string PurchaseCost = "PurchaseCost";

        public readonly string PurchaseTaxCodeRef = "PurchaseTaxCodeRef";

        public readonly string ExpenseAccountRef = "ExpenseAccountRef";

        public readonly string PrefVendorRef = "PrefVendorRef";

        /// <summary>
        /// Initializes a new instance of the MaxQBBaseDataModel class
        /// </summary>
        public MaxQBORSalesAndPurchaseDataModel()
        {
            this.AddNullable(this.SalesDesc, typeof(MaxLongString));
            this.AddNullable(this.SalesPrice, typeof(double));
            this.AddNullable(this.IncomeAccountRef, typeof(MaxLongString));
            this.AddNullable(this.PurchaseDesc, typeof(MaxLongString));
            this.AddNullable(this.PurchaseCost, typeof(double));
            this.AddNullable(this.PurchaseTaxCodeRef, typeof(MaxLongString));
            this.AddNullable(this.ExpenseAccountRef, typeof(MaxLongString));
            this.AddNullable(this.PrefVendorRef, typeof(MaxLongString));
        }
    }
}
