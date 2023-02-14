// <copyright file="MaxQBReceivePaymentDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="2/7/2023" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBReceivePaymentDataModel : MaxQBBaseDataModel
    {
        public readonly string CustomerFullName = "CustomerFullName";

        public readonly string ARAccountRefFullName = "ARAccountRefFullName";

        public readonly string TxnDate = "TxnDate";

        public readonly string RefNumber = "RefNumber";

        public readonly string TotalAmount = "TotalAmount";

        public readonly string ExchangeRate = "ExchangeRate";

        public readonly string PaymentMethodRefFullName = "PaymentMethodRefFullName";

        public readonly string Memo = "Memo";

        public readonly string DepositToAccountRefFullName = "DepositToAccountRefFullName";

        public readonly string ExternalGUID = "ExternalGUID";

        public readonly string IsAutoApply = "IsAutoApply";

        public readonly string AppliedToTxnListText = "AppliedToTxnListText";

        public readonly string IncludeRetElementListText = "IncludeRetElementListText";

        /// <summary>
        /// Initializes a new instance of the MaxQBReceivePaymentDataModel class
        /// </summary>
        public MaxQBReceivePaymentDataModel()
        {
            this.SetDataStorageName("MaxQBReceivePayment");
            this.AddNullable(this.CustomerFullName, typeof(string));
            this.AddNullable(this.ARAccountRefFullName, typeof(string));
            this.AddNullable(this.TxnDate, typeof(DateTime));
            this.AddNullable(this.RefNumber, typeof(string));
            this.AddNullable(this.TotalAmount, typeof(double));
            this.AddNullable(this.ExchangeRate, typeof(double));
            this.AddNullable(this.PaymentMethodRefFullName, typeof(string));
            this.AddNullable(this.Memo, typeof(MaxLongString));
            this.AddNullable(this.DepositToAccountRefFullName, typeof(string));
            this.AddNullable(this.ExternalGUID, typeof(Guid));
            this.AddNullable(this.IsAutoApply, typeof(bool));
            this.AddNullable(this.AppliedToTxnListText, typeof(MaxLongString));
            this.AddNullable(this.IncludeRetElementListText, typeof(MaxLongString));
        }
    }
}

