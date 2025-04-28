// <copyright file="MaxQBSessionEntity.cs" company="Lakstins Family, LLC">
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

    public class MaxQBSessionEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private QBSessionManager _oQBSessionManager = null;

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBSessionEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBSessionEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public DateTime EndDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.EndDate);
            }

            set
            {
                this.Set(this.DataModel.EndDate, value);
            }
        }

        public string UserName
        {
            get
            {
                return this.GetString(this.DataModel.UserName);
            }

            set
            {
                this.Set(this.DataModel.UserName, value);
            }
        }

        public int RequestCount
        {
            get
            {
                return this.GetInt(this.DataModel.RequestCount);
            }

            set
            {
                this.Set(this.DataModel.RequestCount, value);
            }
        }

        public string InitialResponse
        {
            get
            {
                return this.GetString(this.DataModel.InitialResponse);
            }

            set
            {
                this.Set(this.DataModel.InitialResponse, value);
            }
        }

        public Guid ResponseId
        {
            get
            {
                return this.GetGuid(this.DataModel.ResponseId);
            }

            set
            {
                this.Set(this.DataModel.ResponseId, value);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.StartDate);
            }

            set
            {
                this.Set(this.DataModel.StartDate, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBSessionDataModel DataModel
        {
            get
            {
                return (MaxQBSessionDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBSessionEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBSessionEntity),
                typeof(MaxQBSessionDataModel)) as MaxQBSessionEntity;
        }

        public QBSessionManager QBSessionManager
        {
            get
            {
                if (null == _oQBSessionManager)
                {
                    _oQBSessionManager = new QBSessionManager();
                }

                return _oQBSessionManager;
            }
        }

        public IMsgSetRequest GetRequestList()
        {
            IMsgSetRequest loR = QBSessionManager.CreateMsgSetRequest("US", 13, 0);
            return loR;
        }

        public IMsgSetResponse ProcessRequestLocal(IMsgSetRequest loRequest)
        {
            loRequest.ToXMLString();

            IMsgSetResponse loR = null;
            try
            {
                QBSessionManager.OpenConnection2("AppId", "AppName", ENConnectionType.ctLocalQBD);
                try
                {
                    QBSessionManager.BeginSession(string.Empty, ENOpenMode.omDontCare);
                    loR = QBSessionManager.DoRequests(loRequest);
                }
                finally
                {
                    QBSessionManager.EndSession();
                }
            }
            finally
            {
                QBSessionManager.CloseConnection();
                _oQBSessionManager = null;
            }

            return loR;
        }

        public IMsgSetResponse GetResponseList(string lsXML)
        {
            IMsgSetResponse loR = QBSessionManager.ToMsgSetResponse(lsXML, "US", 13, 0);
            return loR;
        }
    }
}
