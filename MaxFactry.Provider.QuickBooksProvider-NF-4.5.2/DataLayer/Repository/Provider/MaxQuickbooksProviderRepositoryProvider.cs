// <copyright file="MaxQuickbooksProviderRepositoryProvider.cs" company="Lakstins Family, LLC">
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
// <change date="10/22/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer.Provider
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.DataLayer;
    using Interop.QBFC15;

    /// <summary>
    /// Default Provider for MaxQuickbooksProviderRepository
    /// </summary>
    public class MaxQuickbooksProviderRepositoryProvider : MaxFactry.Base.DataLayer.Provider.MaxBaseIdRepositoryDefaultProvider, IMaxQuickbooksProviderRepositoryProvider
    {
        private QBSessionManager _oQBSessionManager = null;

        private bool _bIsConnectionOpen = false;

        private bool _bIsSessionBegin = false;

        /// <summary>
        /// The Application ID, obtained from Intuit for you specific application, used
        /// when opening a connection with QuickBooks
        /// </summary>
        private string _sAppId = string.Empty;

        /// <summary>
        /// The application name used when opening a connection with QuickBooks
        /// </summary>
        private string _sAppName = "MaxFactry Quickbooks Provider";

        /// <summary>
        /// The connection type used when creating a connection with QuickBooks.
        /// This may be manually overridden within the initalize(...) method's parameters.
        /// </summary>
        private ENConnectionType _oConnectionType = ENConnectionType.ctLocalQBD;

        private static object _oLock = new object();

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="lsName">Name of the provider.</param>
        /// <param name="loConfig">Configuration information.</param>
        public override void Initialize(string lsName, MaxIndex loConfig)
        {
            base.Initialize(lsName, loConfig);
            string lsAppId = MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxQBAppId") as string;
            if (!string.IsNullOrEmpty(lsAppId))
            {
                this._sAppId = lsAppId;
            }

            string lsAppName = MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxQBAppName") as string;
            if (!string.IsNullOrEmpty(lsAppName))
            {
                this._sAppName = lsAppName;
            }

            _oQBSessionManager = new QBSessionManager();
        }

        public bool OpenConnection()
        {
            if (this._bIsConnectionOpen)
            {
                this._oQBSessionManager.CloseConnection();
            }

            try
            {
                this._oQBSessionManager.OpenConnection2(this._sAppId, this._sAppName, this._oConnectionType);
                this._bIsConnectionOpen = true;
                return true;
            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "OpenConnection", MaxEnumGroup.LogCritical, "Exception", loE));
            }

            return false;
        }

        public bool CloseConnection()
        {
            try
            {
                if (null != this._oQBSessionManager)
                {
                    this._oQBSessionManager.CloseConnection();
                    this._bIsConnectionOpen = false;
                    return true;
                }
            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "OpenConnection", MaxEnumGroup.LogCritical, "Exception", loE));
            }

            return false;
        }

        public bool BeginSession()
        {
            this.EndSession();

            try
            {
                this._oQBSessionManager.BeginSession(string.Empty, ENOpenMode.omDontCare);
                this._bIsSessionBegin = true;
                return true;
            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "BeginSession", MaxEnumGroup.LogCritical, "Exception", loE));
            }

            return false;
        }

        public bool EndSession()
        {
            try
            {
                if (null != this._oQBSessionManager && this._bIsSessionBegin)
                {
                    this._oQBSessionManager.EndSession();
                    this._bIsSessionBegin = false;
                    return true;
                }
            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "EndSession", MaxEnumGroup.LogCritical, "Exception", loE));
            }

            return false;
        }

        public override MaxDataList Select(MaxData loData, MaxDataQuery loDataQuery, int lnPageIndex, int lnPageSize, string lsSort, out int lnTotal, params string[] laFields)
        {
            string lsAlternateId = GetValue(loDataQuery, ((MaxBaseIdDataModel)loData.DataModel).AlternateId) as string;
            if (!string.IsNullOrEmpty(lsAlternateId) && lsAlternateId == "QBDesktop")
            {
                if (loData.DataModel is MaxQBHostDataModel)
                {
                    MaxQBHostDataModel loDataModel = loData.DataModel as MaxQBHostDataModel;
                    MaxDataList loR = new MaxDataList(loDataModel);
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    loRequest.AppendHostQueryRq();
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            lnTotal = 1;
                            MaxData loDataReturn = new MaxData(loDataModel);
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            IHostRet loDetail = loResponse.Detail as IHostRet;

                            loDataReturn.Set(loDataModel.SupportedQBXMLVersionList, GetAsStringArray(loDetail.SupportedQBXMLVersionList));
                            loDataReturn.Set(loDataModel.Country, GetAsString(loDetail.Country));
                            loDataReturn.Set(loDataModel.IsAutomaticLogin, GetAsBool(loDetail.IsAutomaticLogin));

                            //loDataReturn.Set(loDataModel.ListMetaData, loHostResponse.ListMetaData);
                            loDataReturn.Set(loDataModel.MajorVersion, GetAsString(loDetail.MajorVersion));
                            loDataReturn.Set(loDataModel.MinorVersion, GetAsString(loDetail.MinorVersion));
                            loDataReturn.Set(loDataModel.ProductName, GetAsString(loDetail.ProductName));
                            loDataReturn.Set(loDataModel.QBFileMode, GetAsInt(loDetail.QBFileMode));

                            loR.Add(loDataReturn);
                            return loR;
                        }
                        else
                        {
                            MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogCritical, "Response Count for Host Request is not 1 {loSetResponse.ResponseList.Count}", loSetResponse.ResponseList.Count));
                        }
                    }
                }
                else if (loData.DataModel is MaxQBCustomerMessageDataModel)
                {
                    MaxQBCustomerMessageDataModel loDataModel = loData.DataModel as MaxQBCustomerMessageDataModel;
                    MaxDataList loR = new MaxDataList(loDataModel);
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    loRequest.AppendCustomerMsgQueryRq();
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            ICustomerMsgRetList loList = loResponse.Detail as ICustomerMsgRetList;
                            if (null != loList && loList.Count > 0)
                            {
                                for (int lnD = 0; lnD < loList.Count; lnD++)
                                {
                                    ICustomerMsgRet loDetail = loList.GetAt(lnD);
                                    MaxData loDataReturn = new MaxData(loDataModel);
                                    loDataReturn.Set(loDataModel.Name, GetAsString(loDetail.Name));
                                    loDataReturn.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
                                    loDataReturn.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));
                                    loDataReturn.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
                                    loDataReturn.Set(loDataModel.TimeModified, GetAsDateTime(loDetail.TimeModified));
                                    loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));
                                    loR.Add(loDataReturn);
                                }

                                lnTotal = loR.Count;
                                return loR;
                            }
                        }
                    }

                }
                else if (loData.DataModel is MaxQBCustomerDataModel)
                {
                    MaxQBCustomerDataModel loDataModel = loData.DataModel as MaxQBCustomerDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    ICustomerQuery loQuery = loRequest.AppendCustomerQueryRq();

                    string lsFullName = GetValue(loDataQuery, loDataModel.FullName) as string;
                    if (!string.IsNullOrEmpty(lsFullName))
                    {
                        loQuery.ORCustomerListQuery.FullNameList.Add(lsFullName);
                    }

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            ICustomerRetList loList = loResponse.Detail as ICustomerRetList;
                            if (loResponse.StatusCode == 0)
                            {
                                if (null != loList)
                                {
                                    if (null != loList && loList.Count > 0)
                                    {
                                        MaxDataList loR = new MaxDataList(loDataModel);
                                        for (int lnL = 0; lnL < loList.Count; lnL++)
                                        {
                                            loR.Add(MapCustomerContent(loList.GetAt(lnL)));
                                        }

                                        lnTotal = loR.Count;
                                        return loR;
                                    }
                                }
                            }
                            else if (string.IsNullOrEmpty(lsFullName))
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogError, "Error in response from QB {Code} {Severity} {Message} {Detail}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage, loResponse.Detail));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemDataModel)
                {
                    MaxQBItemDataModel loDataModel = loData.DataModel as MaxQBItemDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemQuery loQuery = loRequest.AppendItemQueryRq();

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            IORItemRetList loList = loResponse.Detail as IORItemRetList;
                            if (null != loList && loList.Count > 0)
                            {
                                MaxDataList loR = new MaxDataList(loDataModel);
                                for (int lnL = 0; lnL < loList.Count; lnL++)
                                {
                                    loR.Add(MapItemContent(loList.GetAt(lnL)));
                                }

                                lnTotal = loR.Count;
                                return loR;
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemNonInventoryDataModel)
                {
                    MaxQBItemNonInventoryDataModel loDataModel = loData.DataModel as MaxQBItemNonInventoryDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemNonInventoryQuery loQuery = loRequest.AppendItemNonInventoryQueryRq();

                    string lsFullName = GetValue(loDataQuery, loDataModel.FullName) as string;
                    if (!string.IsNullOrEmpty(lsFullName))
                    {
                        loQuery.ORListQueryWithOwnerIDAndClass.FullNameList.Add(lsFullName);
                    }

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                IItemNonInventoryRetList loList = loResponse.Detail as IItemNonInventoryRetList;
                                if (null != loList && loList.Count > 0)
                                {
                                    MaxDataList loR = new MaxDataList(loDataModel);
                                    for (int lnL = 0; lnL < loList.Count; lnL++)
                                    {
                                        loR.Add(MapItemNonInventoryContent(loList.GetAt(lnL)));
                                    }

                                    lnTotal = loR.Count;
                                    return loR;
                                }
                            }
                            else if (string.IsNullOrEmpty(lsFullName))
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogError, "Error in response from QB {Code} {Severity} {Message} {Detail}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage, loResponse.Detail));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemOtherChargeDataModel)
                {
                    MaxQBItemOtherChargeDataModel loDataModel = loData.DataModel as MaxQBItemOtherChargeDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemOtherChargeQuery loQuery = loRequest.AppendItemOtherChargeQueryRq();

                    string lsFullName = GetValue(loDataQuery, loDataModel.FullName) as string;
                    if (!string.IsNullOrEmpty(lsFullName))
                    {
                        loQuery.ORListQueryWithOwnerIDAndClass.FullNameList.Add(lsFullName);
                    }

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                IItemOtherChargeRetList loList = loResponse.Detail as IItemOtherChargeRetList;
                                if (null != loList && loList.Count > 0)
                                {
                                    MaxDataList loR = new MaxDataList(loDataModel);
                                    for (int lnL = 0; lnL < loList.Count; lnL++)
                                    {
                                        loR.Add(MapItemOtherChargeContent(loList.GetAt(lnL)));
                                    }

                                    lnTotal = loR.Count;
                                    return loR;
                                }
                            }
                            else if (string.IsNullOrEmpty(lsFullName))
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogError, "Error in response from QB {Code} {Severity} {Message} {Detail}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage, loResponse.Detail));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemServiceDataModel)
                {
                    MaxQBItemServiceDataModel loDataModel = loData.DataModel as MaxQBItemServiceDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemServiceQuery loQuery = loRequest.AppendItemServiceQueryRq();

                    string lsFullName = GetValue(loDataQuery, loDataModel.FullName) as string;
                    if (!string.IsNullOrEmpty(lsFullName))
                    {
                        loQuery.ORListQueryWithOwnerIDAndClass.FullNameList.Add(lsFullName);
                    }

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                IItemServiceRetList loList = loResponse.Detail as IItemServiceRetList;
                                if (null != loList && loList.Count > 0)
                                {
                                    MaxDataList loR = new MaxDataList(loDataModel);
                                    for (int lnL = 0; lnL < loList.Count; lnL++)
                                    {
                                        loR.Add(MapItemServiceContent(loList.GetAt(lnL)));
                                    }

                                    lnTotal = loR.Count;
                                    return loR;
                                }
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogError, "Error in response from QB {Code} {Severity} {Message} {Detail}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage, loResponse.Detail));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBStandardTermsDataModel)
                {
                    MaxQBStandardTermsDataModel loDataModel = loData.DataModel as MaxQBStandardTermsDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    loRequest.AppendTermsQueryRq();
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            IORTermsRetList loList = loResponse.Detail as IORTermsRetList;
                            if (null != loList && loList.Count > 0)
                            {
                                MaxDataList loR = new MaxDataList(loDataModel);
                                for (int lnL = 0; lnL < loList.Count; lnL++)
                                {
                                    loR.Add(MapStandardTermsContent(loList.GetAt(lnL).StandardTermsRet));
                                }

                                lnTotal = loR.Count;
                                return loR;
                            }
                        }
                    }

                }
                else if (loData.DataModel is MaxQBInvoiceDataModel)
                {
                    MaxQBInvoiceDataModel loDataModel = loData.DataModel as MaxQBInvoiceDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IInvoiceQuery loQuery = loRequest.AppendInvoiceQueryRq();
                    loQuery.IncludeLineItems.SetValue(true);

                    string lsRefNumber = GetValue(loDataQuery, loDataModel.RefNumber) as string;
                    string lsIsPaid = GetValue(loDataQuery, loDataModel.IsPaid) as string;
                    string lsTxnDate = GetValue(loDataQuery, loDataModel.TxnDate) as string;
                    if (!string.IsNullOrEmpty(lsRefNumber))
                    {
                        loQuery.ORInvoiceQuery.RefNumberList.Add(lsRefNumber);
                    }
                    else if (!string.IsNullOrEmpty(lsIsPaid))
                    {
                        bool lbIsPaid = MaxConvertLibrary.ConvertToBoolean(typeof(object), lsIsPaid);
                        if (!lbIsPaid)
                        {
                            loQuery.ORInvoiceQuery.InvoiceFilter.PaidStatus.SetValue(ENPaidStatus.psNotPaidOnly);
                        }
                        else
                        {
                            loQuery.ORInvoiceQuery.InvoiceFilter.PaidStatus.SetValue(ENPaidStatus.psPaidOnly);
                        }

                        if (!string.IsNullOrEmpty(lsTxnDate))
                        {
                            DateTime ldTxnDate = MaxConvertLibrary.ConvertToDateTimeUtc(typeof(object), lsTxnDate);
                            loQuery.ORInvoiceQuery.InvoiceFilter.ORDateRangeFilter.TxnDateRangeFilter.ORTxnDateRangeFilter.TxnDateFilter.FromTxnDate.SetValue(ldTxnDate);
                        }
                    }

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            IInvoiceRetList loList = loResponse.Detail as IInvoiceRetList;
                            if (loResponse.StatusCode == 0)
                            {
                                if (null != loList)
                                {
                                    if (null != loList && loList.Count > 0)
                                    {
                                        MaxDataList loR = new MaxDataList(loDataModel);
                                        for (int lnL = 0; lnL < loList.Count; lnL++)
                                        {
                                            loR.Add(MapInvoiceContent(loList.GetAt(lnL)));
                                        }

                                        lnTotal = loR.Count;
                                        return loR;
                                    }
                                }
                            }
                            else if (string.IsNullOrEmpty(lsRefNumber))
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogError, "Error in response from QB {Code} {Severity} {Message} {Detail}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage, loResponse.Detail));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBReceivePaymentDataModel)
                {
                    MaxQBReceivePaymentDataModel loDataModel = loData.DataModel as MaxQBReceivePaymentDataModel;
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IReceivePaymentQuery loQuery = loRequest.AppendReceivePaymentQueryRq();
                    loQuery.IncludeLineItems.SetValue(true);

                    string lsRefNumber = GetValue(loDataQuery, loDataModel.RefNumber) as string;
                    if (!string.IsNullOrEmpty(lsRefNumber))
                    {
                        loQuery.ORTxnQuery.RefNumberList.Add(lsRefNumber);
                    }

                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            IReceivePaymentRetList loList = loResponse.Detail as IReceivePaymentRetList;
                            if (loResponse.StatusCode == 0)
                            {
                                if (null != loList)
                                {
                                    if (null != loList && loList.Count > 0)
                                    {
                                        MaxDataList loR = new MaxDataList(loDataModel);
                                        for (int lnL = 0; lnL < loList.Count; lnL++)
                                        {
                                            loR.Add(MapReceivePaymentContent(loList.GetAt(lnL)));
                                        }

                                        lnTotal = loR.Count;
                                        return loR;
                                    }
                                }
                            }
                            else if (string.IsNullOrEmpty(lsRefNumber))
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Select", MaxEnumGroup.LogError, "Error in response from QB {Code} {Severity} {Message} {Detail}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage, loResponse.Detail));
                            }
                        }
                    }
                }
            }

            return base.Select(loData, loDataQuery, lnPageIndex, lnPageSize, lsSort, out lnTotal, laFields);
        }

        public override bool Insert(MaxData loData)
        {
            string lsAlternateId = loData.Get(((MaxBaseIdDataModel)loData.DataModel).AlternateId) as string;
            if (!string.IsNullOrEmpty(lsAlternateId) && lsAlternateId == "QBDesktop")
            {
                if (loData.DataModel is MaxQBInvoiceDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IInvoiceAdd loQBData = loRequest.AppendInvoiceAddRq();
                    MapInvoiceContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Insert", MaxEnumGroup.LogError, "Error inserting into QB: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBCustomerDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    ICustomerAdd loQBData = loRequest.AppendCustomerAddRq();
                    MapCustomerContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Insert", MaxEnumGroup.LogError, "Error inserting into QB: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemServiceDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemServiceAdd loQBData = loRequest.AppendItemServiceAddRq();
                    MapItemServiceContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Insert", MaxEnumGroup.LogError, "Error inserting MaxQBItemServiceDataModel into QB: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemNonInventoryDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemNonInventoryAdd loQBData = loRequest.AppendItemNonInventoryAddRq();
                    MapItemNonInventoryContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Insert", MaxEnumGroup.LogError, "Error inserting MaxQBItemNonInventoryDataModel into QB: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBReceivePaymentDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IReceivePaymentAdd loQBData = loRequest.AppendReceivePaymentAddRq();
                    MapReceivePaymentContent(loQBData, loData, loRequest);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Insert", MaxEnumGroup.LogError, "Error inserting MaxQBReceivePaymentDataModel into QB: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }

                return false;
            }

            return base.Insert(loData);
        }

        public override bool Update(MaxData loData)
        {
            string lsAlternateId = loData.Get(((MaxBaseIdDataModel)loData.DataModel).AlternateId) as string;
            if (!string.IsNullOrEmpty(lsAlternateId) && lsAlternateId == "QBDesktop")
            {
                if (loData.DataModel is MaxQBInvoiceDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IInvoiceMod loQBData = loRequest.AppendInvoiceModRq();
                    MapInvoiceContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Update", MaxEnumGroup.LogError, "Error updating QB for MaxQBInvoiceDataModel: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBCustomerDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    ICustomerMod loQBData = loRequest.AppendCustomerModRq();
                    MapCustomerContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Update", MaxEnumGroup.LogError, "Error updating QB for MaxQBCustomerDataModel: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemServiceDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemServiceAdd loQBData = loRequest.AppendItemServiceAddRq();
                    MapItemServiceContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Update", MaxEnumGroup.LogError, "Error updating QB for MaxQBItemServiceDataModel: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }
                else if (loData.DataModel is MaxQBItemNonInventoryDataModel)
                {
                    IMsgSetRequest loRequest = this._oQBSessionManager.CreateMsgSetRequest("US", 14, 0);
                    IItemNonInventoryAdd loQBData = loRequest.AppendItemNonInventoryAddRq();
                    MapItemNonInventoryContent(loQBData, loData);
                    IMsgSetResponse loSetResponse = this.GetSetResponse(loRequest);
                    if (null != loSetResponse)
                    {
                        if (loSetResponse.ResponseList.Count == 1)
                        {
                            IResponse loResponse = loSetResponse.ResponseList.GetAt(0);
                            if (loResponse.StatusCode == 0)
                            {
                                return true;
                            }
                            else
                            {
                                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "Update", MaxEnumGroup.LogError, "Error updating QB for MaxQBItemNonInventoryDataModel: {StatusCode} {StatusSeverity} {StatusMessage}", loResponse.StatusCode, loResponse.StatusSeverity, loResponse.StatusMessage));
                            }
                        }
                    }
                }

                return false;
            }

            return base.Insert(loData);
        }


        private static MaxData MapCustomerContent(ICustomerRet loDetail)
        {
            MaxQBCustomerDataModel loDataModel = new MaxQBCustomerDataModel();
            MaxData loDataReturn = new MaxData(loDataModel);
            loDataReturn.Set(loDataModel.Name, GetAsString(loDetail.Name));
            loDataReturn.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
            loDataReturn.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));
            loDataReturn.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
            loDataReturn.Set(loDataModel.TimeModified, GetAsDateTime(loDetail.TimeModified));
            loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));

            loDataReturn.Set(loDataModel.FullName, GetAsString(loDetail.FullName));
            loDataReturn.Set(loDataModel.AccountNumber, GetAsString(loDetail.AccountNumber));
            loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));

            if (null != loDetail.AdditionalNotesRetList)
            {
                MaxIndex[] laAdditionalNotesRetIndex = new MaxIndex[loDetail.AdditionalNotesRetList.Count];
                for (int lnN = 0; lnN < loDetail.AdditionalNotesRetList.Count; lnN++)
                {
                    IAdditionalNotesRet loAdditionalNotesRet = loDetail.AdditionalNotesRetList.GetAt(lnN);
                    MaxQBAdditionalNotesRetDataModel loAdditionalNotesRetDataModel = new MaxQBAdditionalNotesRetDataModel();
                    MaxIndex loAdditionalNotesRetIndex = new MaxIndex();
                    loAdditionalNotesRetIndex.Add(loAdditionalNotesRetDataModel.Note, GetAsString(loAdditionalNotesRet.Note));
                    loAdditionalNotesRetIndex.Add(loAdditionalNotesRetDataModel.NoteID, GetAsInt(loAdditionalNotesRet.NoteID));
                    loAdditionalNotesRetIndex.Add(loAdditionalNotesRetDataModel.Date, GetAsDateTime(loAdditionalNotesRet.Date));
                    laAdditionalNotesRetIndex[lnN] = loAdditionalNotesRetIndex;
                }

                loDataReturn.Set(loDataModel.AdditionalNotesRetList, MaxConvertLibrary.SerializeObjectToString(laAdditionalNotesRetIndex));
            }

            loDataReturn.Set(loDataModel.AltContact, GetAsString(loDetail.AltContact));
            loDataReturn.Set(loDataModel.AltPhone, GetAsString(loDetail.AltPhone));
            loDataReturn.Set(loDataModel.Balance, GetAsDouble(loDetail.Balance));

            if (null != loDetail.BillAddress)
            {
                loDataReturn.Set(loDataModel.BillAddress, MapAddressContent(loDetail.BillAddress));
            }

            if (null != loDetail.BillAddressBlock)
            {
                MaxQBAddressBlockDataModel loBillAddressBlockDataModel = new MaxQBAddressBlockDataModel();
                MaxIndex loBillAddressBlockIndex = new MaxIndex();
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr1, GetAsString(loDetail.BillAddressBlock.Addr1));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr2, GetAsString(loDetail.BillAddressBlock.Addr2));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr3, GetAsString(loDetail.BillAddressBlock.Addr3));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr4, GetAsString(loDetail.BillAddressBlock.Addr4));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr5, GetAsString(loDetail.BillAddressBlock.Addr5));

                loDataReturn.Set(loDataModel.BillAddressBlock, MaxConvertLibrary.SerializeObjectToString(loBillAddressBlockIndex));
            }

            loDataReturn.Set(loDataModel.Cc, GetAsString(loDetail.Cc));
            loDataReturn.Set(loDataModel.CompanyName, GetAsBool(loDetail.CompanyName));
            loDataReturn.Set(loDataModel.Contact, GetAsBool(loDetail.Contact));

            if (null != loDetail.ContactsRetList)
            {
                MaxIndex[] laContactsRetListIndex = new MaxIndex[loDetail.ContactsRetList.Count];
                for (int lnN = 0; lnN < loDetail.ContactsRetList.Count; lnN++)
                {
                    IContactsRet loContactsRet = loDetail.ContactsRetList.GetAt(lnN);
                    MaxQBContactDataModel loContactDataModel = new MaxQBContactDataModel();
                    MaxIndex loContactsRetIndex = new MaxIndex();
                    loContactsRetIndex.Add(loContactDataModel.ListID, GetAsString(loContactsRet.ListID));
                    loContactsRetIndex.Add(loContactDataModel.TimeCreated, GetAsDateTime(loContactsRet.TimeCreated));
                    loContactsRetIndex.Add(loContactDataModel.TimeModified, GetAsDateTime(loContactsRet.TimeModified));
                    loContactsRetIndex.Add(loContactDataModel.EditSequence, GetAsString(loContactsRet.EditSequence));
                    loContactsRetIndex.Add(loContactDataModel.Contact, GetAsString(loContactsRet.Contact));
                    loContactsRetIndex.Add(loContactDataModel.Salutation, GetAsString(loContactsRet.Salutation));
                    loContactsRetIndex.Add(loContactDataModel.FirstName, GetAsString(loContactsRet.FirstName));
                    loContactsRetIndex.Add(loContactDataModel.MiddleName, GetAsString(loContactsRet.MiddleName));
                    loContactsRetIndex.Add(loContactDataModel.LastName, GetAsString(loContactsRet.LastName));
                    loContactsRetIndex.Add(loContactDataModel.JobTitle, GetAsString(loContactsRet.JobTitle));
                    laContactsRetListIndex[lnN] = loContactsRetIndex;
                }

                loDataReturn.Set(loDataModel.ContactsRetList, MaxConvertLibrary.SerializeObjectToString(laContactsRetListIndex));
            }

            if (null != loDetail.CreditCardInfo)
            {
                MaxQBCreditCardDataModel loCreditCardDataModel = new MaxQBCreditCardDataModel();
                MaxIndex loCreditCardIndex = new MaxIndex();
                loCreditCardIndex.Add(loCreditCardDataModel.CreditCardAddress, GetAsString(loDetail.CreditCardInfo.CreditCardAddress));
                loCreditCardIndex.Add(loCreditCardDataModel.CreditCardNumber, GetAsString(loDetail.CreditCardInfo.CreditCardNumber));
                loCreditCardIndex.Add(loCreditCardDataModel.CreditCardPostalCode, GetAsString(loDetail.CreditCardInfo.CreditCardPostalCode));
                loCreditCardIndex.Add(loCreditCardDataModel.NameOnCard, GetAsString(loDetail.CreditCardInfo.NameOnCard));
                loCreditCardIndex.Add(loCreditCardDataModel.ExpirationMonth, GetAsInt(loDetail.CreditCardInfo.ExpirationMonth));
                loCreditCardIndex.Add(loCreditCardDataModel.ExpirationYear, GetAsInt(loDetail.CreditCardInfo.ExpirationYear));

                loDataReturn.Set(loDataModel.CreditCardInfo, MaxConvertLibrary.SerializeObjectToString(loCreditCardIndex));
            }

            loDataReturn.Set(loDataModel.CreditLimit, GetAsDouble(loDetail.CreditLimit));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.CurrencyRef));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.CustomerTypeRef));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.DataExtRetList));
            //loDataReturn.Set(loDataModel.DeliveryMethod, GetAsString(loDetail.DeliveryMethod));
            loDataReturn.Set(loDataModel.Email, GetAsString(loDetail.Email));
            loDataReturn.Set(loDataModel.ExternalGUID, GetAsString(loDetail.ExternalGUID));
            loDataReturn.Set(loDataModel.Fax, GetAsString(loDetail.Fax));
            loDataReturn.Set(loDataModel.FirstName, GetAsString(loDetail.FirstName));
            loDataReturn.Set(loDataModel.FullName, GetAsString(loDetail.FullName));
            loDataReturn.Set(loDataModel.IsStatementWithParent, GetAsBool(loDetail.IsStatementWithParent));
            //loDataReturn.Set(loDataModel.ItemSalesTaxRef, GetAsBool(loDetail.ItemSalesTaxRef));
            loDataReturn.Set(loDataModel.JobDesc, GetAsString(loDetail.JobDesc));
            loDataReturn.Set(loDataModel.JobEndDate, GetAsDateTime(loDetail.JobEndDate));
            loDataReturn.Set(loDataModel.JobProjectedEndDate, GetAsDateTime(loDetail.JobProjectedEndDate));
            loDataReturn.Set(loDataModel.JobStartDate, GetAsDateTime(loDetail.JobStartDate));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.JobStatus));
            loDataReturn.Set(loDataModel.JobTitle, GetAsString(loDetail.JobTitle));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.JobTypeRef));
            loDataReturn.Set(loDataModel.LastName, GetAsString(loDetail.LastName));
            loDataReturn.Set(loDataModel.MiddleName, GetAsString(loDetail.MiddleName));
            loDataReturn.Set(loDataModel.Mobile, GetAsString(loDetail.Mobile));
            loDataReturn.Set(loDataModel.Notes, GetAsString(loDetail.Notes));
            loDataReturn.Set(loDataModel.Pager, GetAsString(loDetail.Pager));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.ParentRef));
            loDataReturn.Set(loDataModel.Phone, GetAsString(loDetail.Phone));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.PreferredDeliveryMethod));
            //loDataReturn.Set(loDataModel.IsActive, GetAsBool(loDetail.PreferredPaymentMethodRef));
            //loDataReturn.Set(loDataModel.IsActive, GetAsString(loDetail.PriceLevelRef));
            loDataReturn.Set(loDataModel.PrintAs, GetAsString(loDetail.PrintAs));
            loDataReturn.Set(loDataModel.ResaleNumber, GetAsString(loDetail.ResaleNumber));
            //loDataReturn.Set(loDataModel.IsActive, GetAsString(loDetail.SalesRepRef));
            //loDataReturn.Set(loDataModel.IsActive, GetAsString(loDetail.SalesTaxCodeRef));
            //loDataReturn.Set(loDataModel.IsActive, GetAsString(loDetail.SalesTaxCountry));
            loDataReturn.Set(loDataModel.Salutation, GetAsString(loDetail.Salutation));

            if (null != loDetail.ShipAddress)
            {
                loDataReturn.Set(loDataModel.ShipAddress, MapAddressContent(loDetail.ShipAddress));
            }

            if (null != loDetail.ShipAddressBlock)
            {
                MaxQBAddressBlockDataModel loShipAddressBlockDataModel = new MaxQBAddressBlockDataModel();
                MaxIndex loShipAddressBlockIndex = new MaxIndex();
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr1, GetAsString(loDetail.ShipAddressBlock.Addr1));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr2, GetAsString(loDetail.ShipAddressBlock.Addr2));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr3, GetAsString(loDetail.ShipAddressBlock.Addr3));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr4, GetAsString(loDetail.ShipAddressBlock.Addr4));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr5, GetAsString(loDetail.ShipAddressBlock.Addr5));

                loDataReturn.Set(loDataModel.ShipAddressBlock, MaxConvertLibrary.SerializeObjectToString(loShipAddressBlockIndex));
            }


            //loDataReturn.Set(loDataModel.IsActive, GetAsString(loDetail.ShipToAddressList));
            loDataReturn.Set(loDataModel.Sublevel, GetAsInt(loDetail.Sublevel));
            loDataReturn.Set(loDataModel.Suffix, GetAsString(loDetail.Suffix));
            loDataReturn.Set(loDataModel.TaxRegistrationNumber, GetAsString(loDetail.TaxRegistrationNumber));
            //loDataReturn.Set(loDataModel.TaxRegistrationNumber, GetAsString(loDetail.TermsRef));
            loDataReturn.Set(loDataModel.TotalBalance, GetAsDouble(loDetail.TotalBalance));

            return loDataReturn;
        }

        private static MaxData MapItemContent(IORItemRet loDetail)
        {
            MaxQBItemDataModel loDataModel = new MaxQBItemDataModel();
            MaxData loR = new MaxData(loDataModel);
            if (null != loDetail.ItemServiceRet)
            {
                MaxQBItemServiceDataModel loItemServiceModel = new MaxQBItemServiceDataModel();
                MaxIndex loItemServiceIndex = new MaxIndex();
                loItemServiceIndex.Add(loItemServiceModel.BarCodeValue, GetAsString(loDetail.ItemServiceRet.BarCodeValue));
                //loItemServiceIndex.Add(loItemServiceModel.ClassRef, GetAsString(loDetail.ItemServiceRet.ClassRef));
                //loItemServiceIndex.Add(loItemServiceModel.DataExtRetList, GetAsString(loDetail.ItemServiceRet.DataExtRetList));
                loItemServiceIndex.Add(loItemServiceModel.EditSequence, GetAsString(loDetail.ItemServiceRet.EditSequence));
                loItemServiceIndex.Add(loItemServiceModel.ExternalGUID, GetAsString(loDetail.ItemServiceRet.ExternalGUID));
                loItemServiceIndex.Add(loItemServiceModel.FullName, GetAsString(loDetail.ItemServiceRet.FullName));
                loItemServiceIndex.Add(loItemServiceModel.IsActive, GetAsBool(loDetail.ItemServiceRet.IsActive));
                loItemServiceIndex.Add(loItemServiceModel.IsTaxIncluded, GetAsBool(loDetail.ItemServiceRet.IsTaxIncluded));
                loItemServiceIndex.Add(loItemServiceModel.ListID, GetAsString(loDetail.ItemServiceRet.ListID));
                loItemServiceIndex.Add(loItemServiceModel.Name, GetAsString(loDetail.ItemServiceRet.Name));
                //loItemServiceIndex.Add(loItemServiceModel.ORSalesPurchase, GetAsString(loDetail.ItemServiceRet.ORSalesPurchase));
                //loItemServiceIndex.Add(loItemServiceModel.ParentRef, GetAsString(loDetail.ItemServiceRet.ParentRef));
                //loItemServiceIndex.Add(loItemServiceModel.SalesTaxCodeRef, GetAsString(loDetail.ItemServiceRet.SalesTaxCodeRef));
                loItemServiceIndex.Add(loItemServiceModel.Sublevel, GetAsInt(loDetail.ItemServiceRet.Sublevel));
                loItemServiceIndex.Add(loItemServiceModel.TimeCreated, GetAsDateTime(loDetail.ItemServiceRet.TimeCreated));
                loItemServiceIndex.Add(loItemServiceModel.TimeModified, GetAsDateTime(loDetail.ItemServiceRet.TimeModified));
                //loItemServiceIndex.Add(loItemServiceModel.UnitOfMeasureSetRef, GetAsString(loDetail.ItemServiceRet.UnitOfMeasureSetRef));

                loR.Set(loDataModel.ItemServiceRet, MaxConvertLibrary.SerializeObjectToString(loItemServiceIndex));
            }

            if (null != loDetail.ItemDiscountRet)
            {
                MaxQBItemDiscountDataModel loItemDiscountModel = new MaxQBItemDiscountDataModel();
                MaxIndex loItemDiscountIndex = new MaxIndex();
                loItemDiscountIndex.Add(loItemDiscountModel.BarCodeValue, GetAsString(loDetail.ItemDiscountRet.BarCodeValue));
                //loItemDiscountIndex.Add(loItemServiceModel.ClassRef, GetAsString(loDetail.ItemDiscountRet.ClassRef));
                //loItemServiceIndex.Add(loItemServiceModel.DataExtRetList, GetAsString(loDetail.ItemDiscountRet.DataExtRetList));
                loItemDiscountIndex.Add(loItemDiscountModel.EditSequence, GetAsString(loDetail.ItemDiscountRet.EditSequence));
                loItemDiscountIndex.Add(loItemDiscountModel.ExternalGUID, GetAsString(loDetail.ItemDiscountRet.ExternalGUID));
                loItemDiscountIndex.Add(loItemDiscountModel.FullName, GetAsString(loDetail.ItemDiscountRet.FullName));
                loItemDiscountIndex.Add(loItemDiscountModel.IsActive, GetAsBool(loDetail.ItemDiscountRet.IsActive));
                loItemDiscountIndex.Add(loItemDiscountModel.ListID, GetAsString(loDetail.ItemDiscountRet.ListID));
                loItemDiscountIndex.Add(loItemDiscountModel.Name, GetAsString(loDetail.ItemDiscountRet.Name));
                //loItemServiceIndex.Add(loItemServiceModel.ORSalesPurchase, GetAsString(loDetail.ItemServiceRet.ORSalesPurchase));
                //loItemServiceIndex.Add(loItemServiceModel.ParentRef, GetAsString(loDetail.ItemServiceRet.ParentRef));
                //loItemServiceIndex.Add(loItemServiceModel.SalesTaxCodeRef, GetAsString(loDetail.ItemServiceRet.SalesTaxCodeRef));
                loItemDiscountIndex.Add(loItemDiscountModel.Sublevel, GetAsInt(loDetail.ItemDiscountRet.Sublevel));
                loItemDiscountIndex.Add(loItemDiscountModel.TimeCreated, GetAsDateTime(loDetail.ItemDiscountRet.TimeCreated));
                loItemDiscountIndex.Add(loItemDiscountModel.TimeModified, GetAsDateTime(loDetail.ItemDiscountRet.TimeModified));
                //loItemServiceIndex.Add(loItemServiceModel.UnitOfMeasureSetRef, GetAsString(loDetail.ItemServiceRet.UnitOfMeasureSetRef));

                loR.Set(loDataModel.ItemDiscountRet, MaxConvertLibrary.SerializeObjectToString(loItemDiscountIndex));
            }

            if (null != loDetail.ItemFixedAssetRet)
            {
                MaxQBItemFixedAssetDataModel loItemFixedAssetModel = new MaxQBItemFixedAssetDataModel();
                MaxIndex loItemFixedAssetIndex = new MaxIndex();
                loItemFixedAssetIndex.Add(loItemFixedAssetModel.BarCodeValue, GetAsString(loDetail.ItemFixedAssetRet.BarCodeValue));

                loR.Set(loDataModel.ItemFixedAssetRet, MaxConvertLibrary.SerializeObjectToString(loItemFixedAssetIndex));
            }

            if (null != loDetail.ItemGroupRet)
            {
                MaxQBItemGroupDataModel loItemGroupModel = new MaxQBItemGroupDataModel();
                MaxIndex loItemGroupIndex = new MaxIndex();
                loItemGroupIndex.Add(loItemGroupModel.BarCodeValue, GetAsString(loDetail.ItemGroupRet.BarCodeValue));

                loR.Set(loDataModel.ItemGroupRet, MaxConvertLibrary.SerializeObjectToString(loItemGroupIndex));
            }

            if (null != loDetail.ItemInventoryAssemblyRet)
            {
                MaxQBItemInventoryAssemblyDataModel loItemInventoryAssemblyModel = new MaxQBItemInventoryAssemblyDataModel();
                MaxIndex loItemInventoryAssemblyIndex = new MaxIndex();
                loItemInventoryAssemblyIndex.Add(loItemInventoryAssemblyModel.BarCodeValue, GetAsString(loDetail.ItemInventoryAssemblyRet.BarCodeValue));

                loR.Set(loDataModel.ItemInventoryAssemblyRet, MaxConvertLibrary.SerializeObjectToString(loItemInventoryAssemblyIndex));
            }

            if (null != loDetail.ItemInventoryRet)
            {
                MaxQBItemInventoryDataModel loItemInventoryModel = new MaxQBItemInventoryDataModel();
                MaxIndex loItemInventoryIndex = new MaxIndex();
                loItemInventoryIndex.Add(loItemInventoryModel.BarCodeValue, GetAsString(loDetail.ItemInventoryRet.BarCodeValue));

                loR.Set(loDataModel.ItemInventoryRet, MaxConvertLibrary.SerializeObjectToString(loItemInventoryIndex));
            }

            if (null != loDetail.ItemNonInventoryRet)
            {
                loR.Set(loDataModel.ItemNonInventoryRet, MapItemNonInventoryContent(loDetail.ItemNonInventoryRet));
            }

            if (null != loDetail.ItemOtherChargeRet)
            {
                MaxQBItemOtherChargeDataModel loItemOtherChargeModel = new MaxQBItemOtherChargeDataModel();
                MaxIndex loItemOtherChargeIndex = new MaxIndex();
                loItemOtherChargeIndex.Add(loItemOtherChargeModel.BarCodeValue, GetAsString(loDetail.ItemOtherChargeRet.BarCodeValue));

                loR.Set(loDataModel.ItemOtherChargeRet, MaxConvertLibrary.SerializeObjectToString(loItemOtherChargeIndex));
            }

            if (null != loDetail.ItemPaymentRet)
            {
                MaxQBItemPaymentDataModel loItemPaymentModel = new MaxQBItemPaymentDataModel();
                MaxIndex loItemPaymentIndex = new MaxIndex();
                loItemPaymentIndex.Add(loItemPaymentModel.BarCodeValue, GetAsString(loDetail.ItemPaymentRet.BarCodeValue));

                loR.Set(loDataModel.ItemPaymentRet, MaxConvertLibrary.SerializeObjectToString(loItemPaymentIndex));
            }

            if (null != loDetail.ItemSalesTaxGroupRet)
            {
                MaxQBItemSalesTaxGroupDataModel loItemSalesTaxGroupModel = new MaxQBItemSalesTaxGroupDataModel();
                MaxIndex loItemSalesTaxGroupIndex = new MaxIndex();
                loItemSalesTaxGroupIndex.Add(loItemSalesTaxGroupModel.BarCodeValue, GetAsString(loDetail.ItemSalesTaxGroupRet.BarCodeValue));

                loR.Set(loDataModel.ItemSalesTaxGroupRet, MaxConvertLibrary.SerializeObjectToString(loItemSalesTaxGroupIndex));
            }

            if (null != loDetail.ItemSalesTaxRet)
            {
                MaxQBItemSalesTaxDataModel loItemSalesTaxModel = new MaxQBItemSalesTaxDataModel();
                MaxIndex loItemSalesTaxIndex = new MaxIndex();
                loItemSalesTaxIndex.Add(loItemSalesTaxModel.BarCodeValue, GetAsString(loDetail.ItemSalesTaxRet.BarCodeValue));

                loR.Set(loDataModel.ItemSalesTaxRet, MaxConvertLibrary.SerializeObjectToString(loItemSalesTaxIndex));
            }

            if (null != loDetail.ItemSubtotalRet)
            {
                MaxQBItemSubtotalDataModel loItemSubtotalModel = new MaxQBItemSubtotalDataModel();
                MaxIndex loItemSubtotalIndex = new MaxIndex();
                loItemSubtotalIndex.Add(loItemSubtotalModel.BarCodeValue, GetAsString(loDetail.ItemSubtotalRet.BarCodeValue));

                loR.Set(loDataModel.ItemSubtotalRet, MaxConvertLibrary.SerializeObjectToString(loItemSubtotalIndex));
            }


            return loR;
        }

        private static MaxData MapItemNonInventoryContent(IItemNonInventoryRet loDetail)
        {
            MaxQBItemNonInventoryDataModel loDataModel = new MaxQBItemNonInventoryDataModel();
            MaxData loR = new MaxData(loDataModel);
            loR.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
            loR.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
            loR.Set(loDataModel.TimeModified, GetAsDateTime(loDetail.TimeModified));
            loR.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));

            loR.Set(loDataModel.Name, GetAsString(loDetail.Name));
            loR.Set(loDataModel.FullName, GetAsString(loDetail.FullName));
            loR.Set(loDataModel.BarCodeValue, GetAsString(loDetail.BarCodeValue));
            loR.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));

            loR.Set(loDataModel.ParentRef, MapRefContent(loDetail.ParentRef));

            loR.Set(loDataModel.ExternalGUID, GetAsString(loDetail.ExternalGUID));
            loR.Set(loDataModel.IsTaxIncluded, GetAsBool(loDetail.IsTaxIncluded));
            loR.Set(loDataModel.Sublevel, GetAsInt(loDetail.Sublevel));
            loR.Set(loDataModel.ManufacturerPartNumber, GetAsString(loDetail.ManufacturerPartNumber));

            if (loDetail.ORSalesPurchase.ortype == ENORSalesPurchase.orspSalesAndPurchase && null != loDetail.ORSalesPurchase.SalesAndPurchase)
            {
                loR.Set(loDataModel.ORSalesAndPurchase, MapORSalesPurchaseContent(loDetail.ORSalesPurchase.SalesAndPurchase));
            }
            else if (loDetail.ORSalesPurchase.ortype == ENORSalesPurchase.orspSalesOrPurchase && null != loDetail.ORSalesPurchase.SalesOrPurchase)
            {
                loR.Set(loDataModel.ORSalesOrPurchase, MapORSalesPurchaseContent(loDetail.ORSalesPurchase.SalesOrPurchase));
            }

            
            return loR;
        }

        private static MaxData MapItemOtherChargeContent(IItemOtherChargeRet loDetail)
        {
            MaxQBItemOtherChargeDataModel loDataModel = new MaxQBItemOtherChargeDataModel();
            MaxData loR = new MaxData(loDataModel);
            loR.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
            loR.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
            loR.Set(loDataModel.TimeModified, GetAsDateTime(loDetail.TimeModified));
            loR.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));

            loR.Set(loDataModel.Name, GetAsString(loDetail.Name));
            loR.Set(loDataModel.FullName, GetAsString(loDetail.FullName));
            loR.Set(loDataModel.BarCodeValue, GetAsString(loDetail.BarCodeValue));
            loR.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));

            loR.Set(loDataModel.ParentRef, MapRefContent(loDetail.ParentRef));

            loR.Set(loDataModel.ExternalGUID, GetAsString(loDetail.ExternalGUID));
            loR.Set(loDataModel.IsTaxIncluded, GetAsBool(loDetail.IsTaxIncluded));
            loR.Set(loDataModel.Sublevel, GetAsInt(loDetail.Sublevel));
            return loR;
        }

        private static MaxData MapItemServiceContent(IItemServiceRet loDetail)
        {
            MaxQBItemServiceDataModel loDataModel = new MaxQBItemServiceDataModel();
            MaxData loR = new MaxData(loDataModel);
            loR.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
            loR.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
            loR.Set(loDataModel.TimeModified, GetAsDateTime(loDetail.TimeModified));
            loR.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));

            loR.Set(loDataModel.Name, GetAsString(loDetail.Name));
            loR.Set(loDataModel.FullName, GetAsString(loDetail.FullName));
            loR.Set(loDataModel.BarCodeValue, GetAsString(loDetail.BarCodeValue));
            loR.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));

            loR.Set(loDataModel.ParentRef, MapRefContent(loDetail.ParentRef));

            loR.Set(loDataModel.ExternalGUID, GetAsString(loDetail.ExternalGUID));
            loR.Set(loDataModel.IsTaxIncluded, GetAsBool(loDetail.IsTaxIncluded));
            loR.Set(loDataModel.Sublevel, GetAsInt(loDetail.Sublevel));
            return loR;
        }

        private static MaxData MapORSalesPurchaseContent(ISalesAndPurchase loDetail)
        {
            MaxQBORSalesAndPurchaseDataModel loDataModel = new MaxQBORSalesAndPurchaseDataModel();
            MaxData loR = new MaxData(loDataModel);
            loR.Set(loDataModel.SalesDesc, GetAsString(loDetail.SalesDesc));
            loR.Set(loDataModel.SalesPrice, GetAsDouble(loDetail.SalesPrice));
            loR.Set(loDataModel.IncomeAccountRef, MapRefContent(loDetail.IncomeAccountRef));
            loR.Set(loDataModel.PurchaseDesc, GetAsString(loDetail.PurchaseDesc));
            loR.Set(loDataModel.PurchaseCost, GetAsDouble(loDetail.PurchaseCost));
            loR.Set(loDataModel.PurchaseTaxCodeRef, MapRefContent(loDetail.PurchaseTaxCodeRef));
            loR.Set(loDataModel.ExpenseAccountRef, MapRefContent(loDetail.ExpenseAccountRef));
            loR.Set(loDataModel.PrefVendorRef, MapRefContent(loDetail.PrefVendorRef));
            return loR;
        }

        private static MaxData MapORSalesPurchaseContent(ISalesOrPurchase loDetail)
        {
            MaxQBORSalesOrPurchaseDataModel loDataModel = new MaxQBORSalesOrPurchaseDataModel();
            MaxData loR = new MaxData(loDataModel);
            loR.Set(loDataModel.Desc, GetAsString(loDetail.Desc));
            loR.Set(loDataModel.ORPrice, GetAsDouble(loDetail.ORPrice));
            loR.Set(loDataModel.AccountRef, MapRefContent(loDetail.AccountRef));
            return loR;
        }

        private static MaxData MapRefContent(IQBBaseRef loDetail)
        {
            MaxQBBaseRefDataModel loDataModel = new MaxQBBaseRefDataModel();
            MaxData loR = new MaxData(loDataModel);
            if (null != loDetail)
            {
                loR.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
                loR.Set(loDataModel.FullName, GetAsString(loDetail.FullName));
                loR.Set(loDataModel.Type, GetAsString(loDetail.Type));
            }

            return loR;
        }

        private static MaxData MapStandardTermsContent(IStandardTermsRet loDetail)
        {
            MaxQBStandardTermsDataModel loDataModel = new MaxQBStandardTermsDataModel();
            MaxData loR = new MaxData(loDataModel);
            if (null != loDetail)
            {
                loR.Set(loDataModel.ListID, GetAsString(loDetail.ListID));
                loR.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
                loR.Set(loDataModel.TimeModified, GetAsString(loDetail.TimeModified));
                loR.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));
                loR.Set(loDataModel.Name, GetAsString(loDetail.Name));
                loR.Set(loDataModel.IsActive, GetAsBool(loDetail.IsActive));
                loR.Set(loDataModel.StdDueDays, GetAsInt(loDetail.StdDueDays));
                loR.Set(loDataModel.StdDiscountDays, GetAsInt(loDetail.StdDiscountDays));
                loR.Set(loDataModel.DiscountPct, GetAsInt(loDetail.DiscountPct));
            }

            return loR;
        }

        private static void MapRefContent(IQBBaseRef loQBData, MaxIndex loIndex)
        {
            MaxQBBaseRefDataModel loDataModel = new MaxQBBaseRefDataModel();
            string lsListId = loIndex.GetValueString(loDataModel.ListID);
            if (!string.IsNullOrEmpty(lsListId))
            {
                loQBData.ListID.SetValue(lsListId);
            }

            string lsFullName = loIndex.GetValueString(loDataModel.FullName);
            if (!string.IsNullOrEmpty(lsFullName))
            {
                loQBData.FullName.SetValue(lsFullName);
            }
        }

        private static MaxData MapInvoiceContent(IInvoiceRet loDetail)
        {
            MaxQBInvoiceDataModel loDataModel = new MaxQBInvoiceDataModel();
            MaxData loDataReturn = new MaxData(loDataModel);
            loDataReturn.Set(loDataModel.RefNumber, GetAsString(loDetail.RefNumber));
            loDataReturn.Set(loDataModel.CustomerRef, GetAsString(loDetail.CustomerRef));
            loDataReturn.Set(loDataModel.EditSequence, GetAsString(loDetail.EditSequence));
            loDataReturn.Set(loDataModel.TimeCreated, GetAsDateTime(loDetail.TimeCreated));
            loDataReturn.Set(loDataModel.TimeModified, GetAsDateTime(loDetail.TimeModified));
            loDataReturn.Set(loDataModel.TxnID, GetAsString(loDetail.TxnID));
            loDataReturn.Set(loDataModel.TxnDate, GetAsDateTime(loDetail.TxnDate));
            loDataReturn.Set(loDataModel.ExternalGUID, GetAsString(loDetail.ExternalGUID));

            if (null != loDetail.BillAddress)
            {
                loDataReturn.Set(loDataModel.BillAddress, MapAddressContent(loDetail.BillAddress));
            }

            if (null != loDetail.BillAddressBlock)
            {
                MaxQBAddressBlockDataModel loBillAddressBlockDataModel = new MaxQBAddressBlockDataModel();
                MaxIndex loBillAddressBlockIndex = new MaxIndex();
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr1, GetAsString(loDetail.BillAddressBlock.Addr1));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr2, GetAsString(loDetail.BillAddressBlock.Addr2));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr3, GetAsString(loDetail.BillAddressBlock.Addr3));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr4, GetAsString(loDetail.BillAddressBlock.Addr4));
                loBillAddressBlockIndex.Add(loBillAddressBlockDataModel.Addr5, GetAsString(loDetail.BillAddressBlock.Addr5));

                loDataReturn.Set(loDataModel.BillAddressBlock, MaxConvertLibrary.SerializeObjectToString(loBillAddressBlockIndex));
            }


            if (null != loDetail.ShipAddress)
            {
                loDataReturn.Set(loDataModel.ShipAddress, MapAddressContent(loDetail.ShipAddress));
            }

            if (null != loDetail.ShipAddressBlock)
            {
                MaxQBAddressBlockDataModel loShipAddressBlockDataModel = new MaxQBAddressBlockDataModel();
                MaxIndex loShipAddressBlockIndex = new MaxIndex();
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr1, GetAsString(loDetail.ShipAddressBlock.Addr1));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr2, GetAsString(loDetail.ShipAddressBlock.Addr2));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr3, GetAsString(loDetail.ShipAddressBlock.Addr3));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr4, GetAsString(loDetail.ShipAddressBlock.Addr4));
                loShipAddressBlockIndex.Add(loShipAddressBlockDataModel.Addr5, GetAsString(loDetail.ShipAddressBlock.Addr5));

                loDataReturn.Set(loDataModel.ShipAddressBlock, MaxConvertLibrary.SerializeObjectToString(loShipAddressBlockIndex));
            }

            if (null != loDetail.ORInvoiceLineRetList)
            {
                string[] laLine = new string[loDetail.ORInvoiceLineRetList.Count];
                MaxQBInvoiceLineDataModel loLineDataModel = new MaxQBInvoiceLineDataModel();
                for (int lnIL = 0; lnIL < loDetail.ORInvoiceLineRetList.Count; lnIL++)
                {
                    IORInvoiceLineRet loLine = loDetail.ORInvoiceLineRetList.GetAt(lnIL);
                    MaxIndex loLineIndex = new MaxIndex();
                    loLineIndex.Add(loLineDataModel.TxnLineID, GetAsString(loLine.InvoiceLineRet.TxnLineID));
                    loLineIndex.Add(loLineDataModel.Quantity, GetAsDouble(loLine.InvoiceLineRet.Quantity));

                    loLineIndex.Add(loLineDataModel.Desc, GetAsString(loLine.InvoiceLineRet.Desc));
                    if (null != loLine.InvoiceLineRet.ORRate)
                    {
                        loLineIndex.Add(loLineDataModel.ORRatePriceLevel, GetAsDouble(loLine.InvoiceLineRet.ORRate.Rate));
                    }

                    loLineIndex.Add(loLineDataModel.Amount, GetAsDouble(loLine.InvoiceLineRet.Amount));
                    if (null != loLine.InvoiceLineRet.SalesTaxCodeRef)
                    {
                        loLineIndex.Add(loLineDataModel.SalesTaxCodeRef, GetAsString(loLine.InvoiceLineRet.SalesTaxCodeRef.FullName));
                    }

                    loLineIndex.Add(loLineDataModel.Other1, GetAsString(loLine.InvoiceLineRet.Other1));
                    loLineIndex.Add(loLineDataModel.Other2, GetAsString(loLine.InvoiceLineRet.Other2));
                    if (null != loLine.InvoiceLineRet.ItemRef)
                    {
                        loLineIndex.Add(loLineDataModel.ItemRef, GetAsString(loLine.InvoiceLineRet.ItemRef.FullName));
                    }
                    laLine[lnIL] = MaxConvertLibrary.SerializeObjectToString(loLineIndex);
                }

                loDataReturn.Set(loDataModel.ORInvoiceLineAddList, MaxConvertLibrary.SerializeObjectToString(laLine));
            }

            return loDataReturn;
        }

        private static void MapInvoiceContent(IInvoiceAdd loQBData, MaxData loData)
        {
            MaxQBInvoiceDataModel loDataModel = loData.DataModel as MaxQBInvoiceDataModel;

            if (null != loData.Get(loDataModel.CustomerRef))
            {
                loQBData.CustomerRef.ListID.SetValue(loData.Get(loDataModel.CustomerRef) as string);
            }

            loQBData.TxnDate.SetValue(MaxConvertLibrary.ConvertToDateTime(typeof(object), loData.Get(loDataModel.TxnDate)));

            loQBData.RefNumber.SetValue(loData.Get(loDataModel.RefNumber) as string);
            loQBData.Memo.SetValue(loData.Get(loDataModel.Memo) as string);
            loQBData.PONumber.SetValue(loData.Get(loDataModel.PONumber) as string);

            string lsBillAddress = loData.Get(loDataModel.BillAddress) as string;
            if (!string.IsNullOrEmpty(lsBillAddress))
            {
                MaxIndex loBillAddressIndex = MaxConvertLibrary.DeserializeObject(lsBillAddress, typeof(MaxIndex)) as MaxIndex;
                if (null != loBillAddressIndex)
                {
                    MapAddressContent(loQBData.BillAddress, loBillAddressIndex);
                }
            }

            string lsShipAddress = loData.Get(loDataModel.ShipAddress) as string;
            if (!string.IsNullOrEmpty(lsShipAddress))
            {
                MaxIndex loShipAddressIndex = MaxConvertLibrary.DeserializeObject(lsShipAddress, typeof(MaxIndex)) as MaxIndex;
                if (null != loShipAddressIndex)
                {
                    MapAddressContent(loQBData.ShipAddress, loShipAddressIndex);
                }
            }

            string lsTermsRef = loData.Get(loDataModel.TermsRef) as string;
            if (!string.IsNullOrEmpty(lsTermsRef))
            {
                MaxIndex loTermsIndex = MaxConvertLibrary.DeserializeObject(lsTermsRef, typeof(MaxIndex)) as MaxIndex;
                if (null != loTermsIndex)
                {
                    MapRefContent(loQBData.TermsRef, loTermsIndex);
                }
            }

            string lsCustomerSalesTaxCodeRef = loData.Get(loDataModel.CustomerSalesTaxCodeRef) as string;
            if (!string.IsNullOrEmpty(lsCustomerSalesTaxCodeRef))
            {
                loQBData.CustomerSalesTaxCodeRef.FullName.SetValue(lsCustomerSalesTaxCodeRef);
            }

            string lsItemSalesTaxRef = loData.Get(loDataModel.ItemSalesTaxRef) as string;
            if (!string.IsNullOrEmpty(lsItemSalesTaxRef))
            {
                loQBData.ItemSalesTaxRef.FullName.SetValue(loData.Get(loDataModel.ItemSalesTaxRef) as string);
            }

            //loQBData.DueDate.SetValue(MaxConvertLibrary.ConvertToDateTime(typeof(object), loData.Get(loDataModel.DueDate)));

            //loQBData.CustomerMsgRef.FullName.SetValue(loData.Get(loDataModel.CustomerMsgRef) as string);

            //loQBData.ExchangeRate.SetValue(Convert.ToSingle(MaxConvertLibrary.ConvertToDouble(typeof(object), loData.Get(loDataModel.ExchangeRate))));


            string[] laItem = MaxConvertLibrary.DeserializeObject(loData.Get(loDataModel.ORInvoiceLineAddList) as string, typeof(string[])) as string[];
            MaxQBInvoiceLineDataModel loItemDataModel = new MaxQBInvoiceLineDataModel();
            for (int lnI = 0; lnI < laItem.Length; lnI++)
            {
                IORInvoiceLineAdd loItem = loQBData.ORInvoiceLineAddList.Append();
                MaxIndex loItemIndex = MaxConvertLibrary.DeserializeObject(laItem[lnI], typeof(MaxIndex)) as MaxIndex;
                //// TODO: Full Name or ListID of an item?
                if (null != loItemIndex[loItemDataModel.ItemRef])
                {
                    loItem.InvoiceLineAdd.ItemRef.FullName.SetValue(loItemIndex.GetValueString(loItemDataModel.ItemRef));
                }

                loItem.InvoiceLineAdd.Desc.SetValue(loItemIndex.GetValueString(loItemDataModel.Desc));
                string lsQuantity = loItemIndex.GetValueString(loItemDataModel.Quantity);
                if (!string.IsNullOrEmpty(lsQuantity))
                {
                    double lnQuantity = MaxConvertLibrary.ConvertToDouble(typeof(object), lsQuantity);
                    if (lnQuantity > 0)
                    {
                        loItem.InvoiceLineAdd.Quantity.SetValue(lnQuantity);
                    }
                }

                loItem.InvoiceLineAdd.ORRatePriceLevel.Rate.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loItemIndex.GetValueString(loItemDataModel.ORRatePriceLevel)));
                loItem.InvoiceLineAdd.Amount.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loItemIndex.GetValueString(loItemDataModel.Amount)));
                loItem.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loItemIndex.GetValueString(loItemDataModel.SalesTaxCodeRef)));
                loItem.InvoiceLineAdd.Other1.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loItemIndex.GetValueString(loItemDataModel.Other1)));
                loItem.InvoiceLineAdd.Other2.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loItemIndex.GetValueString(loItemDataModel.Other2)));
            }
        }

        private static void MapInvoiceContent(IInvoiceMod loQBData, MaxData loData)
        {
            MaxQBInvoiceDataModel loDataModel = loData.DataModel as MaxQBInvoiceDataModel;

            string lsEditSequence = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.EditSequence));
            loQBData.EditSequence.SetValue(lsEditSequence);

            string lsTxnId = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.TxnID));
            loQBData.TxnID.SetValue(lsTxnId);

            if (null != loData.Get(loDataModel.CustomerRef))
            {
                loQBData.CustomerRef.ListID.SetValue(loData.Get(loDataModel.CustomerRef) as string);
            }

            loQBData.TxnDate.SetValue(MaxConvertLibrary.ConvertToDateTime(typeof(object), loData.Get(loDataModel.TxnDate)));

            loQBData.RefNumber.SetValue(loData.Get(loDataModel.RefNumber) as string);
            loQBData.Memo.SetValue(loData.Get(loDataModel.Memo) as string);


            string lsPONumber = loData.Get(loDataModel.PONumber) as string;
            if (!string.IsNullOrEmpty(lsPONumber))
            {
                loQBData.PONumber.SetValue(lsPONumber);
            }

            string lsBillAddress = loData.Get(loDataModel.BillAddress) as string;
            if (!string.IsNullOrEmpty(lsBillAddress))
            {
                MaxIndex loBillAddressIndex = MaxConvertLibrary.DeserializeObject(lsBillAddress, typeof(MaxIndex)) as MaxIndex;
                if (null != loBillAddressIndex)
                {
                    MapAddressContent(loQBData.BillAddress, loBillAddressIndex);
                }
            }

            string lsShipAddress = loData.Get(loDataModel.ShipAddress) as string;
            if (!string.IsNullOrEmpty(lsShipAddress))
            {
                MaxIndex loShipAddressIndex = MaxConvertLibrary.DeserializeObject(lsShipAddress, typeof(MaxIndex)) as MaxIndex;
                if (null != loShipAddressIndex)
                {
                    MapAddressContent(loQBData.ShipAddress, loShipAddressIndex);
                }
            }

            string lsTermsRef = loData.Get(loDataModel.TermsRef) as string;
            if (!string.IsNullOrEmpty(lsTermsRef))
            {
                MaxIndex loTermsIndex = MaxConvertLibrary.DeserializeObject(lsTermsRef, typeof(MaxIndex)) as MaxIndex;
                if (null != loTermsIndex)
                {
                    MapRefContent(loQBData.TermsRef, loTermsIndex);
                }
            }

            string lsCustomerSalesTaxCodeRef = loData.Get(loDataModel.CustomerSalesTaxCodeRef) as string;
            if (!string.IsNullOrEmpty(lsCustomerSalesTaxCodeRef))
            {
                loQBData.CustomerSalesTaxCodeRef.FullName.SetValue(lsCustomerSalesTaxCodeRef);
            }

            string lsItemSalesTaxRef = loData.Get(loDataModel.ItemSalesTaxRef) as string;
            if (!string.IsNullOrEmpty(lsItemSalesTaxRef))
            {
                loQBData.ItemSalesTaxRef.FullName.SetValue(loData.Get(loDataModel.ItemSalesTaxRef) as string);
            }

            //loQBData.DueDate.SetValue(MaxConvertLibrary.ConvertToDateTime(typeof(object), loData.Get(loDataModel.DueDate)));

            //loQBData.CustomerMsgRef.FullName.SetValue(loData.Get(loDataModel.CustomerMsgRef) as string);

            //loQBData.ExchangeRate.SetValue(Convert.ToSingle(MaxConvertLibrary.ConvertToDouble(typeof(object), loData.Get(loDataModel.ExchangeRate))));


            string[] laItem = MaxConvertLibrary.DeserializeObject(loData.Get(loDataModel.ORInvoiceLineAddList) as string, typeof(string[])) as string[];
            MaxQBInvoiceLineDataModel loItemDataModel = new MaxQBInvoiceLineDataModel();
            for (int lnI = 0; lnI < laItem.Length; lnI++)
            {
                MaxIndex loItemIndex = MaxConvertLibrary.DeserializeObject(laItem[lnI], typeof(MaxIndex)) as MaxIndex;
                string lsTxnLineId = loItemIndex.GetValueString(loItemDataModel.TxnLineID);
                IORInvoiceLineMod loItem = loQBData.ORInvoiceLineModList.Append();
                if (!string.IsNullOrEmpty(lsTxnLineId))
                {
                    loItem.InvoiceLineMod.TxnLineID.SetValue(lsTxnLineId);
                }
                else
                {
                    loItem.InvoiceLineMod.TxnLineID.SetValue("-1");
                }

                //// TODO: Full Name or ListID of an item?
                if (null != loItemIndex[loItemDataModel.ItemRef])
                {
                    loItem.InvoiceLineMod.ItemRef.FullName.SetValue(loItemIndex.GetValueString(loItemDataModel.ItemRef));
                }

                loItem.InvoiceLineMod.Desc.SetValue(loItemIndex.GetValueString(loItemDataModel.Desc));
                string lsQuantity = loItemIndex.GetValueString(loItemDataModel.Quantity);
                if (!string.IsNullOrEmpty(lsQuantity))
                {
                    double lnQuantity = MaxConvertLibrary.ConvertToDouble(typeof(object), lsQuantity);
                    if (lnQuantity > 0)
                    {
                        loItem.InvoiceLineMod.Quantity.SetValue(lnQuantity);
                    }
                }

                loItem.InvoiceLineMod.ORRatePriceLevel.Rate.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loItemIndex.GetValueString(loItemDataModel.ORRatePriceLevel)));
                loItem.InvoiceLineMod.Amount.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loItemIndex.GetValueString(loItemDataModel.Amount)));
                loItem.InvoiceLineMod.SalesTaxCodeRef.FullName.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loItemIndex.GetValueString(loItemDataModel.SalesTaxCodeRef)));
                loItem.InvoiceLineMod.Other1.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loItemIndex.GetValueString(loItemDataModel.Other1)));
                loItem.InvoiceLineMod.Other2.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loItemIndex.GetValueString(loItemDataModel.Other2)));
            }
        }

        private static void MapCustomerContent(ICustomerAdd loQBData, MaxData loData)
        {
            MaxQBCustomerDataModel loDataModel = loData.DataModel as MaxQBCustomerDataModel;
            Guid loExternalId = MaxConvertLibrary.ConvertToGuid(typeof(object), loData.Get(loDataModel.ExternalGUID));
            if (loExternalId != Guid.Empty)
            {
                loQBData.ExternalGUID.SetValue("{" + loExternalId.ToString() + "}");
            }

            string lsCompany = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.CompanyName));
            if (!string.IsNullOrEmpty(lsCompany))
            {
                loQBData.CompanyName.SetValue(lsCompany);
            }

            loQBData.Email.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Email)));

            string lsFirstName = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.FirstName));

            if (!string.IsNullOrEmpty(lsFirstName))
            {
                loQBData.FirstName.SetValue(lsFirstName);
            }

            string lsLastName = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.LastName));
            if (!string.IsNullOrEmpty(lsLastName))
            {
                loQBData.LastName.SetValue(lsLastName);
            }

            string lsAccountNumber = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.AccountNumber));

            if (!string.IsNullOrEmpty(lsAccountNumber))
            {
                loQBData.AccountNumber.SetValue(lsAccountNumber);
            }

            loQBData.Name.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Name)));

            string lsBillingAddress = loData.Get(loDataModel.BillAddress) as string;
            if (!string.IsNullOrEmpty(lsBillingAddress))
            {
                MaxIndex loBillAddressIndex = MaxConvertLibrary.DeserializeObject(loData.Get(loDataModel.BillAddress) as string, typeof(MaxIndex)) as MaxIndex;
                if (null != loBillAddressIndex)
                {
                    MapAddressContent(loQBData.BillAddress, loBillAddressIndex);
                }
            }

            string lsShippingAddress = loData.Get(loDataModel.ShipAddress) as string;
            if (!string.IsNullOrEmpty(lsShippingAddress))
            {
                MaxIndex loShipAddressIndex = MaxConvertLibrary.DeserializeObject(loData.Get(loDataModel.ShipAddress) as string, typeof(MaxIndex)) as MaxIndex;
                if (null != loShipAddressIndex)
                {
                    MapAddressContent(loQBData.ShipAddress, loShipAddressIndex);
                }
            }

            string lsSalesTax = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ItemSalesTaxRef));
            if (!string.IsNullOrEmpty(lsSalesTax))
            {
                loQBData.ItemSalesTaxRef.FullName.SetValue(lsSalesTax);
            }

            string lsTerms = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.TermsRef));
            if (!string.IsNullOrEmpty(lsTerms))
            {
                loQBData.TermsRef.FullName.SetValue(lsTerms);
            }
        }

        private static void MapCustomerContent(ICustomerMod loQBData, MaxData loData)
        {
            MaxQBCustomerDataModel loDataModel = loData.DataModel as MaxQBCustomerDataModel;
            string lsListId = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ListID));
            loQBData.ListID.SetValue(lsListId);

            string lsEditSequence = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.EditSequence));
            loQBData.EditSequence.SetValue(lsEditSequence);

            string lsCompany = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.CompanyName));
            if (!string.IsNullOrEmpty(lsCompany))
            {
                loQBData.CompanyName.SetValue(lsCompany);
            }

            loQBData.Email.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Email)));

            string lsFirstName = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.FirstName));

            if (!string.IsNullOrEmpty(lsFirstName))
            {
                loQBData.FirstName.SetValue(lsFirstName);
            }

            string lsLastName = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.LastName));
            if (!string.IsNullOrEmpty(lsLastName))
            {
                loQBData.LastName.SetValue(lsLastName);
            }

            string lsAccountNumber = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.AccountNumber));

            if (!string.IsNullOrEmpty(lsAccountNumber))
            {
                loQBData.AccountNumber.SetValue(lsAccountNumber);
            }

            loQBData.Name.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Name)));

            string lsBillingAddress = loData.Get(loDataModel.BillAddress) as string;
            if (!string.IsNullOrEmpty(lsBillingAddress))
            {
                MaxIndex loBillAddressIndex = MaxConvertLibrary.DeserializeObject(loData.Get(loDataModel.BillAddress) as string, typeof(MaxIndex)) as MaxIndex;
                if (null != loBillAddressIndex)
                {
                    MapAddressContent(loQBData.BillAddress, loBillAddressIndex);
                }
            }

            string lsShippingAddress = loData.Get(loDataModel.ShipAddress) as string;
            if (!string.IsNullOrEmpty(lsShippingAddress))
            {
                MaxIndex loShipAddressIndex = MaxConvertLibrary.DeserializeObject(loData.Get(loDataModel.ShipAddress) as string, typeof(MaxIndex)) as MaxIndex;
                if (null != loShipAddressIndex)
                {
                    MapAddressContent(loQBData.ShipAddress, loShipAddressIndex);
                }
            }

            string lsSalesTax = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ItemSalesTaxRef));
            if (!string.IsNullOrEmpty(lsSalesTax))
            {
                loQBData.ItemSalesTaxRef.FullName.SetValue(lsSalesTax);
            }

            string lsTerms = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.TermsRef));
            if (!string.IsNullOrEmpty(lsTerms))
            {
                loQBData.TermsRef.FullName.SetValue(lsTerms);
            }
        }


        private static void MapAddressContent(IAddress loQBData, MaxIndex loIndex)
        {
            MaxQBAddressDataModel loDataModel = new MaxQBAddressDataModel();
            loQBData.Addr1.SetValue(loIndex.GetValueString(loDataModel.Addr1));
            string lsAddr2 = loIndex.GetValueString(loDataModel.Addr2);
            if (!string.IsNullOrEmpty(lsAddr2))
            {
                loQBData.Addr2.SetValue(lsAddr2);
            }

            string lsAddr3 = loIndex.GetValueString(loDataModel.Addr3);
            if (!string.IsNullOrEmpty(lsAddr3))
            {
                loQBData.Addr3.SetValue(lsAddr3);
            }

            string lsAddr4 = loIndex.GetValueString(loDataModel.Addr4);
            if (!string.IsNullOrEmpty(lsAddr4))
            {
                loQBData.Addr4.SetValue(lsAddr4);
            }
            string lsAddr5 = loIndex.GetValueString(loDataModel.Addr5);
            if (!string.IsNullOrEmpty(lsAddr5))
            {
                loQBData.Addr5.SetValue(lsAddr5);
            }

            loQBData.City.SetValue(loIndex.GetValueString(loDataModel.City));
            string lsCountry = loIndex.GetValueString(loDataModel.Country);
            if (!string.IsNullOrEmpty(lsCountry))
            {
                loQBData.Country.SetValue(lsCountry);
            }

            loQBData.PostalCode.SetValue(loIndex.GetValueString(loDataModel.PostalCode));
            loQBData.State.SetValue(loIndex.GetValueString(loDataModel.State));
        }

        private static MaxData MapAddressContent(IAddress loDetail)
        {
            MaxQBAddressDataModel loDataModel = new MaxQBAddressDataModel();
            MaxData loR = new MaxData(loDataModel);
            if (null != loDetail)
            {
                loR.Set(loDataModel.Addr1, GetAsString(loDetail.Addr1));
                loR.Set(loDataModel.Addr2, GetAsString(loDetail.Addr2));
                loR.Set(loDataModel.Addr3, GetAsString(loDetail.Addr3));
                loR.Set(loDataModel.Addr4, GetAsString(loDetail.Addr4));
                loR.Set(loDataModel.Addr5, GetAsString(loDetail.Addr5));
                loR.Set(loDataModel.City, GetAsString(loDetail.City));
                loR.Set(loDataModel.Country, GetAsString(loDetail.Country));
                loR.Set(loDataModel.PostalCode, GetAsString(loDetail.PostalCode));
                loR.Set(loDataModel.State, GetAsString(loDetail.State));
            }

            return loR;
        }

        private static MaxData MapReceivePaymentContent(IReceivePaymentRet loDetail)
        {
            MaxQBReceivePaymentDataModel loDataModel = new MaxQBReceivePaymentDataModel();
            MaxData loR = new MaxData(loDataModel);
            if (null != loDetail)
            {
                /*
                loDetail.TimeCreated;
                loDetail.TimeModified;
                loDetail.EditSequence;
                loDetail.TxnNumber;
                loDetail.CustomerRef;
                loDetail.CustomerRef.ListID;
                loDetail.CustomerRef.FullName;
                loDetail.ARAccountRef;
                loDetail.ARAccountRef.ListID;
                loDetail.ARAccountRef.FullName;
                loDetail.TxnDate;
                loR.Set(loDataModel.RefNumber, GetAsString(loDetail.RefNumber));
                loDetail.TotalAmount;
                loDetail.CurrencyRef;
                loDetail.CurrencyRef.ListID;
                loDetail.CurrencyRef.FullName;
                loDetail.ExchangeRate;
                loDetail.TotalAmountInHomeCurrency;
                loDetail.PaymentMethodRef;
                loDetail.PaymentMethodRef.ListID;
                loDetail.PaymentMethodRef.FullName;
                loDetail.Memo;
                loDetail.DepositToAccountRef;
                loDetail.DepositToAccountRef.ListID;
                loDetail.DepositToAccountRef.FullName;
                loDetail.CreditCardTxnInfo;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.CreditCardNumber;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.ExpirationMonth;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.ExpirationYear;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.NameOnCard;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.CreditCardAddress;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.CreditCardPostalCode;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.CommercialCardCode;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.TransactionMode;
                loDetail.CreditCardTxnInfo.CreditCardTxnInputInfo.CreditCardTxnType;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.ResultCode;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.ResultMessage;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.CreditCardTransID;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.MerchantAccountNumber;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.AuthorizationCode;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.AVSStreet;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.AVSZip;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.CardSecurityCodeMatch;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.ReconBatchID;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.PaymentGroupingCode;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.PaymentStatus;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.TxnAuthorizationTime;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.TxnAuthorizationStamp;
                loDetail.CreditCardTxnInfo.CreditCardTxnResultInfo.ClientTransID;
                loDetail.UnusedPayment;
                loDetail.UnusedCredits;
                loDetail.ExternalGUID;
                loDetail.AppliedToTxnRetList;
                loDetail.AppliedToTxnRetList.GetAt(0).TxnID;
                loDetail.AppliedToTxnRetList.GetAt(0).TxnType;
                loDetail.AppliedToTxnRetList.GetAt(0).TxnDate;
                loDetail.AppliedToTxnRetList.GetAt(0).RefNumber;
                loDetail.AppliedToTxnRetList.GetAt(0).BalanceRemaining;
                loDetail.AppliedToTxnRetList.GetAt(0).Amount;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountAmount;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountAccountRef;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountAccountRef.ListID;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountAccountRef.FullName;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountClassRef;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountClassRef.ListID;
                loDetail.AppliedToTxnRetList.GetAt(0).DiscountClassRef.FullName;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList.GetAt(0).TxnID;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList.GetAt(0).TxnType;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList.GetAt(0).TxnDate;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList.GetAt(0).RefNumber;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList.GetAt(0).LinkType;
                loDetail.AppliedToTxnRetList.GetAt(0).LinkedTxnList.GetAt(0).Amount;
                loDetail.DataExtRetList.GetAt(0).OwnerID;
                loDetail.DataExtRetList.GetAt(0).DataExtName;
                loDetail.DataExtRetList.GetAt(0).DataExtType;
                loDetail.DataExtRetList.GetAt(0).DataExtValue;

                */




            }

            return loR;
        }

        private static void MapReceivePaymentContent(IReceivePaymentAdd loQBData, MaxData loData, IMsgSetRequest loRequest)
        {
            MaxQBReceivePaymentDataModel loDataModel = new MaxQBReceivePaymentDataModel();
            loQBData.CustomerRef.FullName.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.CustomerFullName)));

            string lsARAccountRefFullName = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ARAccountRefFullName));
            if (!string.IsNullOrEmpty(lsARAccountRefFullName))
            {
                loQBData.ARAccountRef.FullName.SetValue(lsARAccountRefFullName);
            }

            loQBData.TxnDate.SetValue(MaxConvertLibrary.ConvertToDateTime(typeof(object), loData.Get(loDataModel.TxnDate)));
            //loQBData.RefNumber.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.RefNumber)));
            loQBData.TotalAmount.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loData.Get(loDataModel.TotalAmount)));
            //loQBData.ExchangeRate.SetValue(Convert.ToSingle(loData.Get(loDataModel.ExchangeRate)));
            //loQBData.PaymentMethodRef.FullName.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.PaymentMethodRefFullName)));
            //loQBData.Memo.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Memo)));


            string lsDepositToAccountRefFullName = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.DepositToAccountRefFullName));
            if (!string.IsNullOrEmpty(lsDepositToAccountRefFullName))
            {
                loQBData.DepositToAccountRef.FullName.SetValue(lsDepositToAccountRefFullName);
            }

            //loQBData.CreditCardTxnInfo
            Guid loExternalGuid = MaxConvertLibrary.ConvertToGuid(typeof(object), loData.Get(loDataModel.ExternalGUID));
            if (Guid.Empty != loExternalGuid)
            {
                loQBData.ExternalGUID.SetValue(loExternalGuid.ToString());
            }
            bool lbIsAutoApply = MaxConvertLibrary.ConvertToBoolean(typeof(object), loData.Get(loDataModel.IsAutoApply));
            if (lbIsAutoApply)
            {
                loQBData.ORApplyPayment.IsAutoApply.SetValue(lbIsAutoApply);
            }

            MaxQBAppliedToTxnDataModel loDataModelTxn = new MaxQBAppliedToTxnDataModel();

            MaxIndex loAppliedToTxnIndex = MaxConvertLibrary.DeserializeObject(typeof(object),
                MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.AppliedToTxnListText)),
                typeof(MaxIndex)) as MaxIndex;

            if (null != loAppliedToTxnIndex)
            {
                string[] laKey = loAppliedToTxnIndex.GetSortedKeyList();
                foreach (string lsKey in laKey)
                {
                    MaxIndex loAppliedToTxn = loAppliedToTxnIndex[lsKey] as MaxIndex;
                    IAppliedToTxnAdd loTxnAdd = loQBData.ORApplyPayment.AppliedToTxnAddList.Append();
                    loTxnAdd.TxnID.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loAppliedToTxn[loDataModelTxn.InvoiceTxnId]));
                    loTxnAdd.PaymentAmount.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loAppliedToTxn[loDataModelTxn.PaymentAmount]));

                    double lnDiscount = MaxConvertLibrary.ConvertToDouble(typeof(object), loAppliedToTxn[loDataModelTxn.DiscountAmount]);
                    if (lnDiscount > double.MinValue)
                    {
                        loTxnAdd.DiscountAmount.SetValue(lnDiscount);
                        loTxnAdd.DiscountAccountRef.FullName.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loAppliedToTxn[loDataModelTxn.DiscountAccountRefFullName]));
                        loTxnAdd.DiscountClassRef.FullName.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loAppliedToTxn[loDataModelTxn.DiscountClassRefFullName]));
                    }
                }
            }

            /*
            ISetCredit loCredit = loTxnAdd.SetCreditList.Append();
            loCredit.CreditTxnID.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.CreditTxnId)));
            loCredit.AppliedAmount.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loData.Get(loDataModel.AppliedAmount)));
            loCredit.Override.SetValue(MaxConvertLibrary.ConvertToBoolean(typeof(object), loData.Get(loDataModel.Override)));
            */

            string[] laIncludeRetElementList = MaxConvertLibrary.DeserializeObject(typeof(object),
                MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.IncludeRetElementListText)),
                typeof(string[])) as string[];

            if (null != laIncludeRetElementList)
            {
                foreach (string lsIncludeRetElementList in laIncludeRetElementList)
                {
                    loQBData.IncludeRetElementList.Add(lsIncludeRetElementList);
                }
            }
        }

        private static void MapItemServiceContent(IItemServiceAdd loQBData, MaxData loData)
        {
            MaxQBItemServiceDataModel loDataModel = loData.DataModel as MaxQBItemServiceDataModel;
            loQBData.Name.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Name)));
            loQBData.BarCode.BarCodeValue.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.BarCodeValue)));
            loQBData.IsTaxIncluded.SetValue(MaxConvertLibrary.ConvertToBoolean(typeof(object), loData.Get(loDataModel.IsTaxIncluded)));
            loQBData.IsActive.SetValue(MaxConvertLibrary.ConvertToBoolean(typeof(object), loData.Get(loDataModel.IsActive)));
            loQBData.ExternalGUID.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ExternalGUID)));
            //loQBData.ORSalesPurchase.SalesOrPurchase.Desc.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ORSalesPurchase)));
        }

        private static void MapItemNonInventoryContent(IItemNonInventoryAdd loQBData, MaxData loData)
        {
            MaxQBItemNonInventoryDataModel loDataModel = loData.DataModel as MaxQBItemNonInventoryDataModel;
            loQBData.Name.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.Name)));
            string lsBarCode = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.BarCodeValue));
            if (!string.IsNullOrEmpty(lsBarCode))
            {
                loQBData.BarCode.BarCodeValue.SetValue(lsBarCode);
            }

            bool lbIsTaxIncluded = MaxConvertLibrary.ConvertToBoolean(typeof(object), loData.Get(loDataModel.IsTaxIncluded));
            if (lbIsTaxIncluded)
            {
                loQBData.IsTaxIncluded.SetValue(lbIsTaxIncluded);
            }

            bool lbIsActive = MaxConvertLibrary.ConvertToBoolean(typeof(object), loData.Get(loDataModel.IsActive));
            if (lbIsActive)
            {
                loQBData.IsActive.SetValue(lbIsActive);
            }

            string lsExternalGuid = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ExternalGUID));
            if (!string.IsNullOrEmpty(lsExternalGuid))
            {
                loQBData.ExternalGUID.SetValue("{" + lsExternalGuid + "}");
            }

            string lsPartNumber = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ManufacturerPartNumber));

            if (!string.IsNullOrEmpty(lsPartNumber))
            {
                loQBData.ManufacturerPartNumber.SetValue(lsPartNumber);
            }


            string lsORSalesOrPurchase = MaxConvertLibrary.ConvertToString(typeof(object), loData.Get(loDataModel.ORSalesOrPurchase));
            if (!string.IsNullOrEmpty(lsORSalesOrPurchase))
            {
                MaxIndex loSalesIndex = MaxConvertLibrary.DeserializeObject(lsORSalesOrPurchase, typeof(MaxIndex)) as MaxIndex;
                if (null != loSalesIndex)
                {
                    MaxQBORSalesOrPurchaseDataModel loSalesDataModel = new MaxQBORSalesOrPurchaseDataModel();
                    loQBData.ORSalesPurchase.SalesOrPurchase.Desc.SetValue(MaxConvertLibrary.ConvertToString(typeof(object), loSalesIndex.GetValueString(loSalesDataModel.Desc)));
                    loQBData.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.SetValue(MaxConvertLibrary.ConvertToDouble(typeof(object), loSalesIndex.GetValueString(loSalesDataModel.ORPrice)));

                    MaxIndex loAccountRefIndex = MaxConvertLibrary.DeserializeObject(loSalesIndex.GetValueString(loSalesDataModel.AccountRef), typeof(MaxIndex)) as MaxIndex;
                    MaxQBBaseRefDataModel loAccountRefDataModel = new MaxQBBaseRefDataModel();
                    string lsFullName = loAccountRefIndex.GetValueString(loAccountRefDataModel.FullName);
                    if (!string.IsNullOrEmpty(lsFullName))
                    {
                        loQBData.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(lsFullName);
                    }

                    string lsListId = loAccountRefIndex.GetValueString(loAccountRefDataModel.ListID);
                    if (!string.IsNullOrEmpty(lsListId))
                    {
                        loQBData.ORSalesPurchase.SalesOrPurchase.AccountRef.ListID.SetValue(lsListId);
                    }                   
                }
            }
        }

        private IMsgSetResponse GetSetResponse(IMsgSetRequest loRequest)
        {
            IMsgSetResponse loR = null;
            lock (_oLock)
            {
                if (this.OpenConnection())
                {
                    try
                    {
                        this.BeginSession();
                        try
                        {
                            loR = this._oQBSessionManager.DoRequests(loRequest); 
                        }
                        catch (Exception loESession)
                        {
                            MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "GetSetResponse", MaxEnumGroup.LogCritical, "Exception within session", loESession));
                        }
                        finally
                        {
                            this.EndSession();
                        }
                    }
                    catch (Exception loEConnection)
                    {
                        MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "GetSetResponse", MaxEnumGroup.LogCritical, "Exception within connection", loEConnection));
                    }
                    finally
                    {
                        this.CloseConnection();
                    }
                }
            }

            return loR;
        }

        protected static string GetAsString(object loQBObject)
        {
            string lsR = string.Empty;
            if (loQBObject is IObjectType)
            {
                lsR = ((IObjectType)loQBObject).GetAsString();
            }
            else if (loQBObject is IQBStringType)
            {
                if (((IQBStringType)loQBObject).IsSet())
                {
                    lsR = ((IQBStringType)loQBObject).GetValue();
                }
            }
            else if (loQBObject is IQBIDType)
            {
                if (((IQBIDType)loQBObject).IsSet())
                {
                    lsR = ((IQBIDType)loQBObject).GetValue();
                }
            }
            else if (loQBObject is IQBBaseRef)
            {
                if (((IQBBaseRef)loQBObject).ListID.IsSet())
                {
                    lsR = ((IQBBaseRef)loQBObject).ListID.GetValue();
                }
            }

            return lsR;
        }

        protected static string[] GetAsStringArray(object loQBObject)
        {
            string[] laR = new string[0];
            if (loQBObject is IBSTRList)
            {
                laR = new string[((IBSTRList)loQBObject).Count];
                for (int lnL = 0; lnL < ((IBSTRList)loQBObject).Count; lnL++)
                {
                    laR[lnL] = ((IBSTRList)loQBObject).GetAt(lnL);
                }
            }

            return laR;
        }

        protected static DateTime GetAsDateTime(object loQBObject)
        {
            DateTime ldR = DateTime.MinValue;
            if (loQBObject is IQBDateTimeType)
            {
                if (((IQBDateTimeType)loQBObject).IsSet())
                {
                    ldR = ((IQBDateTimeType)loQBObject).GetValue();
                }
            }
            else if (loQBObject is IQBDateType)
            {
                if (((IQBDateType)loQBObject).IsSet())
                {
                    ldR = ((IQBDateType)loQBObject).GetValue();
                }
            }

            return ldR;
        }

        protected static int GetAsInt(object loQBObject)
        {
            int lnR = int.MinValue;
            if (loQBObject is IQBIntType)
            {
                if (((IQBIntType)loQBObject).IsSet())
                {
                    lnR = ((IQBIntType)loQBObject).GetValue();
                }
            }

            return lnR;
        }

        protected static double GetAsDouble(object loQBObject)
        {
            double lnR = double.MinValue;
            if (loQBObject is IQBAmountType)
            {
                if (((IQBAmountType)loQBObject).IsSet())
                {
                    lnR = ((IQBAmountType)loQBObject).GetValue();
                }
            }
            else if (loQBObject is IQBQuanType)
            {
                if (((IQBQuanType)loQBObject).IsSet())
                {
                    lnR = ((IQBQuanType)loQBObject).GetValue();
                }
            }
            else if (loQBObject is IQBPriceType)
            {
                if (((IQBPriceType)loQBObject).IsSet())
                {
                    lnR = ((IQBPriceType)loQBObject).GetValue();
                }
            }

            return lnR;
        }

        protected static bool GetAsBool(object loQBObject)
        {
            bool lbR = false;
            if (loQBObject is IQBBoolType)
            {
                if (((IQBBoolType)loQBObject).IsSet())
                {
                    lbR = ((IQBBoolType)loQBObject).GetValue();
                }
            }

            return lbR;
        }

        /// <summary>
        /// Writes stream data to storage.
        /// </summary>
        /// <param name="loData">The data index for the object</param>
        /// <param name="lsKey">Data element name to write</param>
        /// <returns>Number of bytes written to storage.</returns>
        public override bool StreamSave(MaxData loData, string lsKey)
        {
            bool lbR = false;
            string lsAlternateId = loData.Get(((MaxBaseIdDataModel)loData.DataModel).AlternateId) as string;
            if (string.IsNullOrEmpty(lsAlternateId) || lsAlternateId != "QBDesktop")
            {
                IMaxDataContextProvider loProvider = MaxDataLibrary.GetContextProvider(this, loData);
                if (null != loProvider)
                {
                    lbR = loProvider.StreamSave(loData, lsKey);
                }
            }

            return lbR;
        }

        /// <summary>
        /// Removes stream from storage.
        /// </summary>
        /// <param name="loData">The data index for the object</param>
        /// <param name="lsKey">Data element name to remove</param>
        /// <returns>true if successful.</returns>
        public override bool StreamDelete(MaxData loData, string lsKey)
        {
            
            bool lbR = false;
            string lsAlternateId = loData.Get(((MaxBaseIdDataModel)loData.DataModel).AlternateId) as string;
            if (string.IsNullOrEmpty(lsAlternateId) || lsAlternateId != "QBDesktop")
            {
                IMaxDataContextProvider loProvider = MaxDataLibrary.GetContextProvider(this, loData);
                if (null != loProvider)
                {
                    lbR = loProvider.StreamDelete(loData, lsKey);
                }
            }

            return lbR;
        }
    }
}



