// <copyright file="MaxQBInvoiceDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxQBInvoiceDataModel : MaxQBBaseDataModel
    {
        public readonly string TxnID = "TxnID";

        public readonly string TimeCreated = "TimeCreated";

        public readonly string TimeModified = "TimeModified";

        public readonly string EditSequence = "EditSequence";

        public readonly string TxnNumber = "TxnNumber";

        public readonly string CustomerRef = "CustomerRef";

        public readonly string ClassRef = "ClassRef";

        public readonly string ARAccountRef = "ARAccountRef";

        public readonly string TemplateRef = "TemplateRef";

        public readonly string TxnDate = "TxnDate";

        public readonly string RefNumber = "RefNumber";

        public readonly string BillAddress = "BillAddress";

        public readonly string BillAddressBlock = "BillAddressBlock";

        public readonly string ShipAddress = "ShipAddress";

        public readonly string ShipAddressBlock = "ShipAddressBlock";

        public readonly string IsPending = "IsPending";

        public readonly string IsFinanceCharge = "IsFinanceCharge";

        public readonly string PONumber = "PONumber";

        public readonly string TermsRef = "TermsRef";

        public readonly string DueDate = "DueDate";

        public readonly string SalesRepRef = "SalesRepRef";

        public readonly string FOB = "FOB";

        public readonly string ShipDate = "ShipDate";

        public readonly string ShipMethodRef = "ShipMethodRef";

        public readonly string Subtotal = "Subtotal";

        public readonly string ItemSalesTaxRef = "ItemSalesTaxRef";

        public readonly string SalesTaxPercentage = "SalesTaxPercentage";

        public readonly string SalesTaxTotal = "SalesTaxTotal";

        public readonly string AppliedAmount = "AppliedAmount";

        public readonly string BalanceRemaining = "BalanceRemaining";

        public readonly string CurrencyRef = "CurrencyRef";

        public readonly string BalanceRemainingInHomeCurrency = "BalanceRemainingInHomeCurrency";

        public readonly string Memo = "Memo";

        public readonly string IsPaid = "IsPaid";

        public readonly string CustomerMsgRef = "CustomerMsgRef";

        public readonly string IsToBePrinted = "IsToBePrinted";

        public readonly string IsToBeEmailed = "IsToBeEmailed";

        public readonly string IsTaxIncluded = "IsTaxIncluded";

        public readonly string CustomerSalesTaxCodeRef = "CustomerSalesTaxCodeRef";

        public readonly string SuggestedDiscountAmount = "SuggestedDiscountAmount";

        public readonly string SuggestedDiscountDate = "SuggestedDiscountDate";

        public readonly string Other = "Other";

        public readonly string ExchangeRate = "ExchangeRate";

        public readonly string ExternalGUID = "ExternalGUID";

        public readonly string LinkToTxnIDList = "LinkToTxnIDList";

        public readonly string SetCreditList = "SetCreditList";

        public readonly string ORInvoiceLineAddList = "ORInvoiceLineAddList";

        public readonly string DataExtRetList = "DataExtRetList";

        public readonly string IncludeRetElementList = "IncludeRetElementList";

        public readonly string defMacro = "defMacro";


        /// <summary>
        /// Initializes a new instance of the MaxQBInvoiceDataModel class
        /// </summary>
        public MaxQBInvoiceDataModel()
        {
            this.SetDataStorageName("MaxQBInvoice");
            this.AddNullable(this.TxnID, typeof(MaxShortString));
            this.AddNullable(this.TimeCreated, typeof(DateTime));
            this.AddNullable(this.TimeModified, typeof(DateTime));
            this.AddNullable(this.EditSequence, typeof(MaxShortString));
            this.AddNullable(this.TxnNumber, typeof(int));
            this.AddNullable(this.CustomerRef, typeof(MaxShortString));
            this.AddNullable(this.ClassRef, typeof(MaxShortString));
            this.AddNullable(this.ARAccountRef, typeof(MaxShortString));
            this.AddNullable(this.TemplateRef, typeof(MaxShortString));
            this.AddNullable(this.TxnDate, typeof(DateTime));
            this.AddNullable(this.RefNumber, typeof(MaxShortString));
            this.AddNullable(this.BillAddress, typeof(MaxLongString));
            this.AddNullable(this.BillAddressBlock, typeof(MaxLongString));
            this.AddNullable(this.ShipAddress, typeof(MaxLongString));
            this.AddNullable(this.ShipAddressBlock, typeof(MaxLongString));
            this.AddNullable(this.IsPending, typeof(bool));
            this.AddNullable(this.IsFinanceCharge, typeof(bool));
            this.AddNullable(this.PONumber, typeof(MaxShortString));
            this.AddNullable(this.TermsRef, typeof(MaxShortString));
            this.AddNullable(this.DueDate, typeof(DateTime));
            this.AddNullable(this.SalesRepRef, typeof(MaxShortString));
            this.AddNullable(this.FOB, typeof(MaxShortString));
            this.AddNullable(this.ShipDate, typeof(DateTime));
            this.AddNullable(this.ShipMethodRef, typeof(MaxShortString));
            this.AddNullable(this.Subtotal, typeof(double));
            this.AddNullable(this.ItemSalesTaxRef, typeof(MaxShortString));
            this.AddNullable(this.SalesTaxPercentage, typeof(double));
            this.AddNullable(this.SalesTaxTotal, typeof(double));
            this.AddNullable(this.AppliedAmount, typeof(double));
            this.AddNullable(this.BalanceRemaining, typeof(double));
            this.AddNullable(this.CurrencyRef, typeof(MaxShortString));
            this.AddNullable(this.BalanceRemainingInHomeCurrency, typeof(double));
            this.AddNullable(this.Memo, typeof(MaxShortString));
            this.AddNullable(this.IsPaid, typeof(bool));
            this.AddNullable(this.CustomerMsgRef, typeof(MaxShortString));
            this.AddNullable(this.IsToBePrinted, typeof(bool));
            this.AddNullable(this.IsToBeEmailed, typeof(bool));
            this.AddNullable(this.IsTaxIncluded, typeof(bool));
            this.AddNullable(this.CustomerSalesTaxCodeRef, typeof(MaxShortString));
            this.AddNullable(this.SuggestedDiscountAmount, typeof(double));
            this.AddNullable(this.SuggestedDiscountDate, typeof(DateTime));
            this.AddNullable(this.Other, typeof(MaxShortString));
            this.AddNullable(this.ExchangeRate, typeof(double));
            this.AddNullable(this.ExternalGUID, typeof(MaxShortString));
            this.AddNullable(this.LinkToTxnIDList, typeof(MaxLongString));
            this.AddNullable(this.SetCreditList, typeof(MaxLongString));
            this.AddNullable(this.ORInvoiceLineAddList, typeof(MaxLongString));
            this.AddNullable(this.DataExtRetList, typeof(MaxLongString));
            this.AddNullable(this.IncludeRetElementList, typeof(MaxLongString));            
            this.AddNullable(this.defMacro, typeof(MaxShortString));
        }
    }
}

