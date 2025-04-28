// <copyright file="MaxQWCAppEntity.cs" company="Lakstins Family, LLC">
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

    public class MaxQBResponseEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBResponseEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBResponseEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }


        public string Response
        {
            get
            {
                return this.GetString(this.DataModel.Response);
            }

            set
            {
                this.Set(this.DataModel.Response, value);
            }
        }

        public Guid RequestId
        {
            get
            {
                return this.GetGuid(this.DataModel.RequestId);
            }

            set
            {
                this.Set(this.DataModel.RequestId, value);
            }
        }

        public Guid SessionId
        {
            get
            {
                return this.GetGuid(this.DataModel.SessionId);
            }

            set
            {
                this.Set(this.DataModel.SessionId, value);
            }
        }

        public string Result
        {
            get
            {
                return this.GetString(this.DataModel.Result);
            }

            set
            {
                this.Set(this.DataModel.Result, value);
            }
        }

        public string Message
        {
            get
            {
                return this.GetString(this.DataModel.Message);
            }

            set
            {
                this.Set(this.DataModel.SessionId, value);
            }
        }

        public string CompanyFileName
        {
            get
            {
                return this.GetString(this.DataModel.CompanyFileName);
            }

            set
            {
                this.Set(this.DataModel.CompanyFileName, value);
            }
        }

        public string XMLCountry
        {
            get
            {
                return this.GetString(this.DataModel.XMLCountry);
            }

            set
            {
                this.Set(this.DataModel.XMLCountry, value);
            }
        }

        public int XMLMajorVers
        {
            get
            {
                return this.GetInt(this.DataModel.XMLMajorVers);
            }

            set
            {
                this.Set(this.DataModel.XMLMajorVers, value);
            }
        }

        public int XMLMinorVers
        {
            get
            {
                return this.GetInt(this.DataModel.XMLMinorVers);
            }

            set
            {
                this.Set(this.DataModel.XMLMinorVers, value);
            }
        }

        public string ProcessLog
        {
            get
            {
                return this.GetString(this.DataModel.ProcessLog);
            }

            set
            {
                this.Set(this.DataModel.ProcessLog, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBResponseDataModel DataModel
        {
            get
            {
                return (MaxQBResponseDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBResponseEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBResponseEntity),
                typeof(MaxQBResponseDataModel)) as MaxQBResponseEntity;
        }
    }
}
