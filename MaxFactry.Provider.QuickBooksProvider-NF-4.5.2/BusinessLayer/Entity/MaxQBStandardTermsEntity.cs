// <copyright file="MaxQBStandardTermsEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/18/2021" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBStandardTermsEntity : MaxQBBaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxQBStandardTermsEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxQBStandardTermsEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxQBStandardTermsEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBStandardTermsEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string ListID
        {
            get
            {
                return this.GetString(this.DataModel.ListID);
            }

            set
            {
                this.Set(this.DataModel.ListID, value);
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

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }

        public int StdDueDays
        {
            get
            {
                return this.GetInt(this.DataModel.StdDueDays);
            }

            set
            {
                this.Set(this.DataModel.StdDueDays, value);
            }
        }

        public int StdDiscountDays
        {
            get
            {
                return this.GetInt(this.DataModel.StdDiscountDays);
            }

            set
            {
                this.Set(this.DataModel.StdDiscountDays, value);
            }
        }

        public double DiscountPct
        {
            get
            {
                return this.GetDouble(this.DataModel.DiscountPct);
            }

            set
            {
                this.Set(this.DataModel.DiscountPct, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBStandardTermsDataModel DataModel
        {
            get
            {
                return (MaxQBStandardTermsDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBStandardTermsEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBStandardTermsEntity),
                typeof(MaxQBStandardTermsDataModel)) as MaxQBStandardTermsEntity;
        }
    }
}
