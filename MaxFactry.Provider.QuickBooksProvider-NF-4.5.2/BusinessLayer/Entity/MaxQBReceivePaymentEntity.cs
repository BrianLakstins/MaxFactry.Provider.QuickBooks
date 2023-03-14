// <copyright file="MaxQBReceivePaymentEntity.cs" company="Lakstins Family, LLC">
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
// <change date="2/14/2023" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBReceivePaymentEntity : MaxQBBaseEntity
    {
        private List<MaxQBAppliedToTxnEntity> _oAppliedToTxnList = null;

        private List<string> _oIncludeRetElementList = null;

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxQBReceivePaymentEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBReceivePaymentEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string CustomerFullName
        {
            get
            {
                return this.GetString(this.DataModel.CustomerFullName);
            }

            set
            {
                this.Set(this.DataModel.CustomerFullName, value);
            }
        }

        public string ARAccountRefFullName
        {
            get
            {
                return this.GetString(this.DataModel.ARAccountRefFullName);
            }

            set
            {
                this.Set(this.DataModel.ARAccountRefFullName, value);
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

        public double TotalAmount
        {
            get
            {
                return this.GetDouble(this.DataModel.TotalAmount);
            }

            set
            {
                this.Set(this.DataModel.TotalAmount, value);
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

        public string PaymentMethodRefFullName
        {
            get
            {
                return this.GetString(this.DataModel.PaymentMethodRefFullName);
            }

            set
            {
                this.Set(this.DataModel.PaymentMethodRefFullName, value);
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

        public string DepositToAccountRefFullName
        {
            get
            {
                return this.GetString(this.DataModel.DepositToAccountRefFullName);
            }

            set
            {
                this.Set(this.DataModel.DepositToAccountRefFullName, value);
            }
        }

        public Guid ExternalGUID
        {
            get
            {
                return this.GetGuid(this.DataModel.ExternalGUID);
            }

            set
            {
                this.Set(this.DataModel.ExternalGUID, value);
            }
        }

        public bool IsAutoApply
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsAutoApply);
            }

            set
            {
                this.Set(this.DataModel.IsAutoApply, value);
            }
        }

        public List<MaxQBAppliedToTxnEntity> AppliedToTxnList
        {
            get
            {
                if (null == this._oAppliedToTxnList)
                {
                    this._oAppliedToTxnList = new List<MaxQBAppliedToTxnEntity>();
                    MaxDataList loDataList = this.GetObject(this.DataModel.AppliedToTxnListText, typeof(MaxDataList)) as MaxDataList;
                    if (null != loDataList)
                    {
                        for (int lnD = 0; lnD < loDataList.Count; lnD++)
                        {
                            MaxQBAppliedToTxnEntity loEntity = MaxQBAppliedToTxnEntity.Create();
                            loEntity.Load(loDataList[lnD]);
                            this._oAppliedToTxnList.Add(loEntity);
                        }
                    }
                }

                return this._oAppliedToTxnList;
            }

            set
            {
                this._oAppliedToTxnList = value;
            }
        }

        public List<string> IncludeRetElementList
        {
            get
            {
                if (null == this._oIncludeRetElementList)
                {
                    this._oIncludeRetElementList = new List<string>();
                    string[] laIncludeRetElementList = this.GetObject(this.DataModel.IncludeRetElementListText, typeof(string[])) as string[];
                    if (null != laIncludeRetElementList)
                    {
                        this._oIncludeRetElementList = new List<string>(laIncludeRetElementList);
                    }
                }

                return this._oIncludeRetElementList;
            }

            set
            {
                this._oIncludeRetElementList = value;
            }
        }

        protected override void SetProperties()
        {
            if (null != this._oAppliedToTxnList)
            {
                MaxIndex loDataIndex = new MaxIndex();
                foreach (MaxQBAppliedToTxnEntity loEntity in this._oAppliedToTxnList)
                {
                    loDataIndex.Add(loEntity.GetDataIndex());
                }

                this.SetObject(this.DataModel.AppliedToTxnListText, loDataIndex);
            }

            if (null != this._oIncludeRetElementList)
            {
                this.SetObject(this.DataModel.IncludeRetElementListText, this._oIncludeRetElementList.ToArray());
            }

            base.SetProperties();
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBReceivePaymentDataModel DataModel
        {
            get
            {
                return (MaxQBReceivePaymentDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBReceivePaymentEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBReceivePaymentEntity),
                typeof(MaxQBReceivePaymentDataModel)) as MaxQBReceivePaymentEntity;
        }
    }
}
