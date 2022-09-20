// <copyright file="MaxQBAddressEntity.cs" company="Lakstins Family, LLC">
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
    using MaxFactry.Provider.QuickbooksProvider.DataLayer;
    using Interop.QBFC15;

    public class MaxQBAddressEntity : MaxQBBaseEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBAddressEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBAddressEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Addr1
        {
            get
            {
                return this.GetString(this.DataModel.Addr1);
            }

            set
            {
                this.Set(this.DataModel.Addr1, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string Addr2
        {
            get
            {
                return this.GetString(this.DataModel.Addr2);
            }

            set
            {
                this.Set(this.DataModel.Addr2, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string Addr3
        {
            get
            {
                return this.GetString(this.DataModel.Addr3);
            }

            set
            {
                this.Set(this.DataModel.Addr3, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string Addr4
        {
            get
            {
                return this.GetString(this.DataModel.Addr4);
            }

            set
            {
                this.Set(this.DataModel.Addr4, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string Addr5
        {
            get
            {
                return this.GetString(this.DataModel.Addr5);
            }

            set
            {
                this.Set(this.DataModel.Addr5, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string City
        {
            get
            {
                return this.GetString(this.DataModel.City);
            }

            set
            {
                this.Set(this.DataModel.City, value.Substring(0, Math.Min(value.Length, 31)));
            }
        }

        public string State
        {
            get
            {
                return this.GetString(this.DataModel.State);
            }

            set
            {
                this.Set(this.DataModel.State, value.Substring(0, Math.Min(value.Length, 21)));
            }
        }

        public string PostalCode
        {
            get
            {
                return this.GetString(this.DataModel.PostalCode);
            }

            set
            {
                this.Set(this.DataModel.PostalCode, value.Substring(0, Math.Min(value.Length, 13)));
            }
        }

        public string Country
        {
            get
            {
                return this.GetString(this.DataModel.Country);
            }

            set
            {
                this.Set(this.DataModel.Country, value.Substring(0, Math.Min(value.Length, 31)));
            }
        }

        public string Note
        {
            get
            {
                return this.GetString(this.DataModel.Note);
            }

            set
            {
                this.Set(this.DataModel.Note, value.Substring(0, Math.Min(value.Length, 41)));
            }
        } 

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBAddressDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(this.DataModelType) as MaxQBAddressDataModel;
            }
        }

        public static MaxQBAddressEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBAddressEntity),
                typeof(MaxQBAddressDataModel)) as MaxQBAddressEntity;
        }

        public override string GetDefaultSortString()
        {
            return this.State + " " + this.PostalCode + " " + this.City + base.GetDefaultSortString();
        }
    }
}
