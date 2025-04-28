// <copyright file="MaxQBInvoiceEntity.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Base.DataLayer.Library;
    using MaxFactry.Provider.QuickbooksProvider.DataLayer;
    using Interop.QBFC15;

    public class MaxQBInvoiceEntity : MaxQBBaseEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBInvoiceEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBInvoiceEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string TxnID
        {
            get
            {
                return this.GetString(this.DataModel.TxnID);
            }

            set
            {
                this.Set(this.DataModel.TxnID, value);
            }
        }

        public DateTime TimeCreated
        {
            get
            {
                return this.GetDateTime(this.DataModel.TimeCreated);
            }

            set
            {
                this.Set(this.DataModel.TimeCreated, value);
            }
        }

        public DateTime TimeModified
        {
            get
            {
                return this.GetDateTime(this.DataModel.TimeModified);
            }

            set
            {
                this.Set(this.DataModel.TimeModified, value);
            }
        }

        public string EditSequence
        {
            get
            {
                return this.GetString(this.DataModel.EditSequence);
            }

            set
            {
                this.Set(this.DataModel.EditSequence, value);
            }
        }

        public int TxnNumber
        {
            get
            {
                return this.GetInt(this.DataModel.TxnNumber);
            }

            set
            {
                this.Set(this.DataModel.TxnNumber, value);
            }
        }

        public string CustomerRef
        {
            get
            {
                return this.GetString(this.DataModel.CustomerRef);
            }

            set
            {
                this.Set(this.DataModel.CustomerRef, value);
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

        public string ARAccountRef
        {
            get
            {
                return this.GetString(this.DataModel.ARAccountRef);
            }

            set
            {
                this.Set(this.DataModel.ARAccountRef, value);
            }
        }

        public string TemplateRef
        {
            get
            {
                return this.GetString(this.DataModel.TemplateRef);
            }

            set
            {
                this.Set(this.DataModel.TemplateRef, value);
            }
        }

        public DateTime TxnDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.TxnDate);
            }

            set
            {
                this.Set(this.DataModel.TxnDate, value);
            }
        }

        public string RefNumber
        {
            get
            {
                return this.GetString(this.DataModel.RefNumber);
            }

            set
            {
                this.Set(this.DataModel.RefNumber, value);
            }
        }

        public MaxQBAddressEntity BillAddress
        {
            get
            {
                MaxQBAddressEntity loR = MaxQBAddressEntity.Create();
                object loData = this.Get(this.DataModel.BillAddress);
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

                Set(this.DataModel.BillAddress, value.ExportToString());
            }
        }

        public string BillAddressBlock
        {
            get
            {
                return this.GetString(this.DataModel.BillAddressBlock);
            }

            set
            {
                this.Set(this.DataModel.BillAddressBlock, value);
            }
        }

        public MaxQBAddressEntity ShipAddress
        {
            get
            {
                MaxQBAddressEntity loR = MaxQBAddressEntity.Create();
                object loData = this.Get(this.DataModel.ShipAddress);
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

                Set(this.DataModel.ShipAddress, value.ExportToString());
            }
        }

        public string ShipAddressBlock
        {
            get
            {
                return this.GetString(this.DataModel.ShipAddressBlock);
            }

            set
            {
                this.Set(this.DataModel.ShipAddressBlock, value);
            }
        }

        public bool IsPending
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsPending);
            }

            set
            {
                this.Set(this.DataModel.IsPending, value);
            }
        }

        public bool IsFinanceCharge
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsFinanceCharge);
            }

            set
            {
                this.Set(this.DataModel.IsFinanceCharge, value);
            }
        }

        public string PONumber
        {
            get
            {
                return this.GetString(this.DataModel.PONumber);
            }

            set
            {
                this.Set(this.DataModel.PONumber, value);
                //// Should we truncate it, or let it cause an error when too long?
                //this.Set(this.DataModel.PONumber, value.Substring(0, Math.Min(value.Length, 25)));
            }
        }

        public MaxQBBaseRefEntity TermsRef
        {
            get
            {
                MaxQBBaseRefEntity loR = MaxQBBaseRefEntity.Create();
                object loData = this.Get(this.DataModel.TermsRef);
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
                Set(this.DataModel.TermsRef, value.ExportToString());
            }
        }

        public DateTime DateTime
        {
            get
            {
                return this.GetDateTime(this.DataModel.DueDate);
            }

            set
            {
                this.Set(this.DataModel.DueDate, value);
            }
        }

        public string SalesRepRef
        {
            get
            {
                return this.GetString(this.DataModel.SalesRepRef);
            }

            set
            {
                this.Set(this.DataModel.SalesRepRef, value);
            }
        }

        public string FOB
        {
            get
            {
                return this.GetString(this.DataModel.FOB);
            }

            set
            {
                this.Set(this.DataModel.FOB, value);
            }
        }
        public DateTime ShipDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.ShipDate);
            }

            set
            {
                this.Set(this.DataModel.ShipDate, value);
            }
        }

        public string ShipMethodRef
        {
            get
            {
                return this.GetString(this.DataModel.ShipMethodRef);
            }

            set
            {
                this.Set(this.DataModel.ShipMethodRef, value);
            }
        }

        public double Subtotal
        {
            get
            {
                return this.GetDouble(this.DataModel.Subtotal);
            }

            set
            {
                this.Set(this.DataModel.Subtotal, value);
            }
        }

        public string ItemSalesTaxRef
        {
            get
            {
                return this.GetString(this.DataModel.ItemSalesTaxRef);
            }

            set
            {
                this.Set(this.DataModel.ItemSalesTaxRef, value);
            }
        }

        public double SalesTaxPercentage
        {
            get
            {
                return this.GetDouble(this.DataModel.SalesTaxPercentage);
            }

            set
            {
                this.Set(this.DataModel.SalesTaxPercentage, value);
            }
        }

        public double SalesTaxTotal
        {
            get
            {
                return this.GetDouble(this.DataModel.SalesTaxTotal);
            }

            set
            {
                this.Set(this.DataModel.SalesTaxTotal, value);
            }
        }

        public double AppliedAmount
        {
            get
            {
                return this.GetDouble(this.DataModel.AppliedAmount);
            }

            set
            {
                this.Set(this.DataModel.AppliedAmount, value);
            }
        }

        public double BalanceRemaining
        {
            get
            {
                return this.GetDouble(this.DataModel.BalanceRemaining);
            }

            set
            {
                this.Set(this.DataModel.BalanceRemaining, value);
            }
        }

        public string CurrencyRef
        {
            get
            {
                return this.GetString(this.DataModel.CurrencyRef);
            }

            set
            {
                this.Set(this.DataModel.CurrencyRef, value);
            }
        }

        public double BalanceRemainingInHomeCurrency
        {
            get
            {
                return this.GetDouble(this.DataModel.BalanceRemainingInHomeCurrency);
            }

            set
            {
                this.Set(this.DataModel.BalanceRemainingInHomeCurrency, value);
            }
        }

        public string Memo
        {
            get
            {
                return this.GetString(this.DataModel.Memo);
            }

            set
            {
                this.Set(this.DataModel.Memo, value);
            }
        }

        public bool IsPaid
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsPaid);
            }

            set
            {
                this.Set(this.DataModel.IsPaid, value);
            }
        }

        public string CustomerMsgRef
        {
            get
            {
                return this.GetString(this.DataModel.CustomerMsgRef);
            }

            set
            {
                this.Set(this.DataModel.CustomerMsgRef, value);
            }
        }

        public bool IsToBePrinted
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsToBePrinted);
            }

            set
            {
                this.Set(this.DataModel.IsToBePrinted, value);
            }
        }

        public bool IsToBeEmailed
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsToBeEmailed);
            }

            set
            {
                this.Set(this.DataModel.IsToBeEmailed, value);
            }
        }

        public bool IsTaxIncluded
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsTaxIncluded);
            }

            set
            {
                this.Set(this.DataModel.IsTaxIncluded, value);
            }
        }

        public String CustomerSalesTaxCodeRef
        {
            get
            {
                return this.GetString(this.DataModel.CustomerSalesTaxCodeRef);
            }

            set
            {
                this.Set(this.DataModel.CustomerSalesTaxCodeRef, value);
            }
        }

        public double SuggestedDiscountAmount
        {
            get
            {
                return this.GetDouble(this.DataModel.SuggestedDiscountAmount);
            }

            set
            {
                this.Set(this.DataModel.SuggestedDiscountAmount, value);
            }
        }

        public DateTime SuggestedDiscountDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.SuggestedDiscountDate);
            }

            set
            {
                this.Set(this.DataModel.SuggestedDiscountDate, value);
            }
        }

        public String Other
        {
            get
            {
                return this.GetString(this.DataModel.Other);
            }

            set
            {
                this.Set(this.DataModel.Other, value);
            }
        }

        public double ExchangeRate
        {
            get
            {
                return this.GetDouble(this.DataModel.ExchangeRate);
            }

            set
            {
                this.Set(this.DataModel.ExchangeRate, value);
            }
        }

        public String ExternalGUID
        {
            get
            {
                return this.GetString(this.DataModel.ExternalGUID);
            }

            set
            {
                this.Set(this.DataModel.ExternalGUID, value);
            }
        }

        public String LinkToTxnIDList
        {
            get
            {
                return this.GetString(this.DataModel.LinkToTxnIDList);
            }

            set
            {
                this.Set(this.DataModel.LinkToTxnIDList, value);
            }
        }

        public String SetCreditList
        {
            get
            {
                return this.GetString(this.DataModel.SetCreditList);
            }

            set
            {
                this.Set(this.DataModel.SetCreditList, value);
            }
        }

        public List<MaxQBInvoiceLineEntity> ORInvoiceLineAddList
        {
            get
            {
                List<MaxQBInvoiceLineEntity> loR = new List<MaxQBInvoiceLineEntity>();
                string[] laData = this.GetObject(this.DataModel.ORInvoiceLineAddList, typeof(string[])) as string[];
                foreach (string lsData in laData)
                {
                    MaxQBInvoiceLineEntity loEntity = MaxQBInvoiceLineEntity.Create();
                    loEntity.Load(lsData);
                    loR.Add(loEntity);
                }

                return loR;
            }

            set
            {
                List<string> loList = new List<string>();
                foreach (MaxQBInvoiceLineEntity loEntity in value)
                {
                    loList.Add(loEntity.ExportToString());
                }

                this.SetObject(this.DataModel.ORInvoiceLineAddList, loList.ToArray());
            }
        }

        public String DataExtRetList
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

        public String IncludeRetElementList
        {
            get
            {
                return this.GetString(this.DataModel.IncludeRetElementList);
            }

            set
            {
                this.Set(this.DataModel.IncludeRetElementList, value);
            }
        }

        public String defMacro
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

        public string Error
        {
            get; set;
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBInvoiceDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(this.DataModelType) as MaxQBInvoiceDataModel;
            }
        }

        public static MaxQBInvoiceEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBInvoiceEntity),
                typeof(MaxQBInvoiceDataModel)) as MaxQBInvoiceEntity;
        }

        public MaxEntityList LoadAllQBDesktopByRefNumber(string lsRefNumber)
        {
            this.Set(this.DataModel.RefNumber, lsRefNumber);
            MaxData loDataFilter = new MaxData(this.Data);
            //// Add a Query 
            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(this.DataModel.RefNumber, "=", lsRefNumber);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(this.QBBaseDataModel.AlternateId, "=", "QBDesktop");
            loDataQuery.EndGroup();

            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            int lnTotal = int.MinValue;
            MaxDataList loDataList = MaxBaseIdRepository.Select(this.Data, loDataQuery, 0, 0, string.Empty);
            loR = MaxEntityList.Create(this.GetType(), loDataList);
            loR.Total = lnTotal;
            return loR;
        }

        public MaxEntityList LoadAllQBDesktopByPaidStatus(bool lbIsPaid)
        {
            this.Set(this.DataModel.IsPaid, lbIsPaid);
            MaxData loDataFilter = new MaxData(this.Data);
            //// Add a Query 
            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(this.DataModel.IsPaid, "=", lbIsPaid.ToString());
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(this.QBBaseDataModel.AlternateId, "=", "QBDesktop");
            loDataQuery.EndGroup();

            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            int lnTotal = int.MinValue;
            MaxDataList loDataList = MaxBaseIdRepository.Select(this.Data, loDataQuery, 0, 0, string.Empty);
            loR = MaxEntityList.Create(this.GetType(), loDataList);
            loR.Total = lnTotal;
            return loR;
        }

        public MaxEntityList LoadAllQBDesktopByTxnDatePaidStatus(DateTime ldTxnDate, bool lbIsPaid)
        {
            this.Set(this.DataModel.IsPaid, lbIsPaid);
            MaxData loDataFilter = new MaxData(this.Data);
            //// Add a Query 
            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(this.DataModel.TxnDate, "=", ldTxnDate.ToString());
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(this.DataModel.IsPaid, "=", lbIsPaid.ToString());
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(this.QBBaseDataModel.AlternateId, "=", "QBDesktop");
            loDataQuery.EndGroup();

            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            int lnTotal = int.MinValue;
            MaxDataList loDataList = MaxBaseIdRepository.Select(this.Data, loDataQuery, 0, 0, string.Empty);
            MaxEntityList loList = MaxEntityList.Create(this.GetType(), loDataList);
            loR.Total = lnTotal;

            SortedList<string, MaxQBInvoiceEntity> loSortedList = new SortedList<string, MaxQBInvoiceEntity>();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxQBInvoiceEntity loEntity = loList[lnE] as MaxQBInvoiceEntity;
                loSortedList.Add(MaxConvertLibrary.ConvertToSortString(typeof(object), loEntity.TxnDate) + loEntity.GetDefaultSortString(), loEntity);
            }

            foreach (MaxQBInvoiceEntity loEntity in loSortedList.Values)
            {
                loR.Add(loEntity);
            }

            return loR;
        }
    }
}
