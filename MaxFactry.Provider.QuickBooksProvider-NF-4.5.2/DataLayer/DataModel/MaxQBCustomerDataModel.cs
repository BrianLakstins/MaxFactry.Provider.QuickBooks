// <copyright file="MaxQBCustomerDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="11/5/2021" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBCustomerDataModel : MaxQBBaseDataModel
    {
        public readonly string AccountNumber = "AccountNumber";

        public readonly string AltContact = "AltContact";

        public readonly string AltPhone = "AltPhone";

        public readonly string Cc = "Cc";

        public readonly string CompanyName = "CompanyName";

        public readonly string Contact = "Contact";

        public readonly string EditSequence = "EditSequence";

        public readonly string Fax = "Fax";

        public readonly string FirstName = "FirstName";

        public readonly string FullName = "FullName";

        public readonly string JobDesc = "JobDesc";

        public readonly string JobTitle = "JobTitle";

        public readonly string LastName = "LastName";

        public readonly string MiddleName = "MiddleName";

        public readonly string Mobile = "Mobile";

        public readonly string Name = "Name";

        public readonly string Notes = "Notes";

        public readonly string Pager = "Pager";

        public readonly string Phone = "Phone";

        public readonly string Email = "Email";

        public readonly string PrintAs = "PrintAs";

        public readonly string ResaleNumber = "ResaleNumber";

        public readonly string Salutation = "Salutation";

        public readonly string Suffix = "Suffix";

        public readonly string TaxRegistrationNumber = "TaxRegistrationNumber";

        public readonly string Sublevel = "Sublevel";

        public readonly string JobEndDate = "JobEndDate";

        public readonly string JobProjectedEndDate = "JobProjectedEndDate";

        public readonly string JobStartDate = "JobStartDate";

        public readonly string TimeCreated = "TimeCreated";

        public readonly string TimeModified = "TimeModified";

        public readonly string ExternalGUID = "ExternalGUID";

        public readonly string ListID = "ListID";

        public readonly string AdditionalContactRefList = "AdditionalContactRefList";

        public readonly string AdditionalNotesRetList = "AdditionalNotesRetList";

        public readonly string Balance = "Balance";

        public readonly string BillAddress = "BillAddress";

        public readonly string BillAddressBlock = "BillAddressBlock";

        public readonly string ContactsRetList = "ContactsRetList";

        public readonly string CreditCardInfo = "CreditCardInfo";

        public readonly string CreditLimit = "CreditLimit";

        public readonly string ShipAddress = "ShipAddress";

        public readonly string ShipAddressBlock = "ShipAddressBlock";

        public readonly string IsStatementWithParent = "IsStatementWithParent";

        public readonly string TotalBalance = "TotalBalance";

        public readonly string ClassRef = "ClassRef";

        public readonly string ParentRef = "ParentRef";

        public readonly string ShipToAddressList = "ShipToAddressList";

        public readonly string CustomerTypeRef = "CustomerTypeRef";

        public readonly string TermsRef = "TermsRef";

        public readonly string SalesRepRef = "SalesRepRef";

        public readonly string OpenBalance = "OpenBalance";

        public readonly string OpenBalanceDate = "OpenBalanceDate";

        public readonly string SalesTaxCodeRef = "SalesTaxCodeRef";

        public readonly string ItemSalesTaxRef = "ItemSalesTaxRef";

        public readonly string SalesTaxCountry = "SalesTaxCountry";

        public readonly string PreferredPaymentMethodRef = "PreferredPaymentMethodRef";

        public readonly string JobStatus = "JobStatus";

        public readonly string DeliveryMethod = "DeliveryMethod";

        public readonly string PreferredDeliveryMethod = "PreferredDeliveryMethod";

        public readonly string PriceLevelRef = "PriceLevelRef";

        public readonly string CurrencyRef = "CurrencyRef";

        public readonly string DataExtRetList = "DataExtRetList";

        public readonly string IncludeRetElementList = "IncludeRetElementList";

        /// <summary>
        /// Initializes a new instance of the MaxQBCustomerDataModel class
        /// </summary>
        public MaxQBCustomerDataModel()
        {
            this.SetDataStorageName("MaxQuickbooksProviderCustomer");
            this.AddNullable(this.ListID, typeof(MaxShortString));
            this.AddNullable(this.TimeCreated, typeof(DateTime));
            this.AddNullable(this.TimeModified, typeof(DateTime));
            this.AddNullable(this.EditSequence, typeof(MaxShortString));
            this.AddNullable(this.Name, typeof(MaxShortString));
            this.AddNullable(this.FullName, typeof(MaxShortString));
            this.AddNullable(this.ClassRef, typeof(MaxShortString));
            this.AddNullable(this.ParentRef, typeof(MaxShortString));
            this.AddNullable(this.Sublevel, typeof(int));
            this.AddNullable(this.CompanyName, typeof(MaxShortString));
            this.AddNullable(this.Salutation, typeof(MaxShortString));
            this.AddNullable(this.FirstName, typeof(MaxShortString));
            this.AddNullable(this.MiddleName, typeof(MaxShortString));
            this.AddNullable(this.LastName, typeof(MaxShortString));
            this.AddNullable(this.Suffix, typeof(MaxShortString));
            this.AddNullable(this.JobTitle, typeof(MaxShortString));
            this.AddNullable(this.BillAddress, typeof(MaxLongString));
            this.AddNullable(this.BillAddressBlock, typeof(MaxLongString));
            this.AddNullable(this.ShipAddress, typeof(MaxLongString));
            this.AddNullable(this.ShipAddressBlock, typeof(MaxLongString));
            this.AddNullable(this.ShipToAddressList, typeof(MaxLongString));
            this.AddNullable(this.PrintAs, typeof(MaxShortString));
            this.AddNullable(this.Phone, typeof(MaxShortString));
            this.AddNullable(this.Mobile, typeof(MaxShortString));
            this.AddNullable(this.Pager, typeof(MaxShortString));
            this.AddNullable(this.AltPhone, typeof(MaxShortString));
            this.AddNullable(this.Fax, typeof(MaxShortString));
            this.AddNullable(this.Email, typeof(MaxShortString));
            this.AddNullable(this.Cc, typeof(MaxShortString));
            this.AddNullable(this.Contact, typeof(MaxShortString));
            this.AddNullable(this.AltContact, typeof(MaxShortString));
            this.AddNullable(this.AdditionalContactRefList, typeof(byte[]));
            this.AddNullable(this.ContactsRetList, typeof(MaxLongString));
            this.AddNullable(this.CustomerTypeRef, typeof(MaxShortString));
            this.AddNullable(this.TermsRef, typeof(MaxShortString));
            this.AddNullable(this.SalesRepRef, typeof(MaxShortString));
            this.AddNullable(this.OpenBalance, typeof(double));
            this.AddNullable(this.OpenBalanceDate, typeof(DateTime));
            this.AddNullable(this.Balance, typeof(double));
            this.AddNullable(this.TotalBalance, typeof(double));
            this.AddNullable(this.SalesTaxCodeRef, typeof(MaxShortString));
            this.AddNullable(this.ItemSalesTaxRef, typeof(MaxShortString));
            this.AddNullable(this.SalesTaxCountry, typeof(int));
            this.AddNullable(this.ResaleNumber, typeof(MaxShortString));
            this.AddNullable(this.AccountNumber, typeof(MaxShortString));
            this.AddNullable(this.CreditLimit, typeof(double));
            this.AddNullable(this.PreferredPaymentMethodRef, typeof(MaxShortString));
            this.AddNullable(this.CreditCardInfo, typeof(MaxLongString));
            this.AddNullable(this.JobStatus, typeof(int));
            this.AddNullable(this.JobStartDate, typeof(DateTime));
            this.AddNullable(this.JobProjectedEndDate, typeof(DateTime));
            this.AddNullable(this.JobEndDate, typeof(DateTime));
            this.AddNullable(this.JobDesc, typeof(MaxShortString));
            this.AddNullable(this.Notes, typeof(MaxLongString));
            this.AddNullable(this.AdditionalNotesRetList, typeof(MaxLongString));
            this.AddNullable(this.IsStatementWithParent, typeof(bool));
            this.AddNullable(this.DeliveryMethod, typeof(int));
            this.AddNullable(this.PreferredDeliveryMethod, typeof(int));
            this.AddNullable(this.PriceLevelRef, typeof(MaxShortString));
            this.AddNullable(this.ExternalGUID, typeof(Guid));
            this.AddNullable(this.TaxRegistrationNumber, typeof(MaxShortString));
            this.AddNullable(this.CurrencyRef, typeof(MaxShortString));
            this.AddNullable(this.DataExtRetList, typeof(MaxShortString));
            this.AddNullable(this.IncludeRetElementList, typeof(MaxLongString));
        }
    }
}

