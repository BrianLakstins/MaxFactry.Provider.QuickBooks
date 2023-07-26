// <copyright file="MaxQBCustomerEntity.cs" company="Lakstins Family, LLC">
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
// <change date="1/8/2016" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.BusinessLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Provider.QuickbooksProvider.DataLayer;
    using Interop.QBFC15;

    public class MaxQBCustomerEntity : MaxQBBaseEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBCustomerEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBCustomerEntity(Type loDataModelType)
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

        public string AccountNumber
        {
            get
            {
                return this.GetString(this.DataModel.AccountNumber);
            }

            set
            {
                this.Set(this.DataModel.AccountNumber, value);
            }
        }

        public string AltContact
        {
            get
            {
                return this.GetString(this.DataModel.AltContact);
            }

            set
            {
                this.Set(this.DataModel.AltContact, value);
            }
        }

        public string AltPhone
        {
            get
            {
                return this.GetString(this.DataModel.AltPhone);
            }

            set
            {
                this.Set(this.DataModel.AltPhone, value);
            }
        }

        public string Cc
        {
            get
            {
                return this.GetString(this.DataModel.Cc);
            }

            set
            {
                this.Set(this.DataModel.Cc, value);
            }
        }

        public string CompanyName
        {
            get
            {
                return this.GetString(this.DataModel.CompanyName);
            }

            set
            {
                this.Set(this.DataModel.CompanyName, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string Contact
        {
            get
            {
                return this.GetString(this.DataModel.Contact);
            }

            set
            {
                this.Set(this.DataModel.Contact, value);
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

        public string Fax
        {
            get
            {
                return this.GetString(this.DataModel.Fax);
            }

            set
            {
                this.Set(this.DataModel.Fax, value);
            }
        }

        public string FirstName
        {
            get
            {
                return this.GetString(this.DataModel.FirstName);
            }

            set
            {
                this.Set(this.DataModel.FirstName, value.Substring(0, Math.Min(value.Length, 25)));
            }
        }

        public string FullName
        {
            get
            {
                return this.GetString(this.DataModel.FullName);
            }

            set
            {
                this.Set(this.DataModel.FullName, value.Substring(0, Math.Min(value.Length, 159)));
            }
        }

        public string JobDesc
        {
            get
            {
                return this.GetString(this.DataModel.JobDesc);
            }

            set
            {
                this.Set(this.DataModel.JobDesc, value);
            }
        }

        public string JobTitle
        {
            get
            {
                return this.GetString(this.DataModel.JobTitle);
            }

            set
            {
                this.Set(this.DataModel.JobTitle, value);
            }
        }

        public string LastName
        {
            get
            {
                return this.GetString(this.DataModel.LastName);
            }

            set
            {
                this.Set(this.DataModel.LastName, value.Substring(0, Math.Min(value.Length, 25)));
            }
        }

        public string MiddleName
        {
            get
            {
                return this.GetString(this.DataModel.MiddleName);
            }

            set
            {
                this.Set(this.DataModel.MiddleName, value.Substring(0, Math.Min(value.Length, 5)));
            }
        }

        public string Mobile
        {
            get
            {
                return this.GetString(this.DataModel.Mobile);
            }

            set
            {
                this.Set(this.DataModel.Mobile, value);
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
                this.Set(this.DataModel.Name, value.Substring(0, Math.Min(value.Length, 41)));
            }
        }

        public string Notes
        {
            get
            {
                return this.GetString(this.DataModel.Notes);
            }

            set
            {
                this.Set(this.DataModel.Notes, value);
            }
        }

        public string Pager
        {
            get
            {
                return this.GetString(this.DataModel.Pager);
            }

            set
            {
                this.Set(this.DataModel.Pager, value);
            }
        }

        public string Phone
        {
            get
            {
                return this.GetString(this.DataModel.Phone);
            }

            set
            {
                this.Set(this.DataModel.Phone, value);
            }
        }

        public string Email
        {
            get
            {
                return this.GetString(this.DataModel.Email);
            }

            set
            {
                this.Set(this.DataModel.Email, value);
            }
        }

        public string PrintAs
        {
            get
            {
                return this.GetString(this.DataModel.PrintAs);
            }

            set
            {
                this.Set(this.DataModel.PrintAs, value);
            }
        }

        public string ResaleNumber
        {
            get
            {
                return this.GetString(this.DataModel.ResaleNumber);
            }

            set
            {
                this.Set(this.DataModel.ResaleNumber, value);
            }
        }

        public string Salutation
        {
            get
            {
                return this.GetString(this.DataModel.Salutation);
            }

            set
            {
                this.Set(this.DataModel.Salutation, value);
            }
        }

        public string Suffix
        {
            get
            {
                return this.GetString(this.DataModel.Suffix);
            }

            set
            {
                this.Set(this.DataModel.Suffix, value);
            }
        }

        public string TaxRegistrationNumber
        {
            get
            {
                return this.GetString(this.DataModel.TaxRegistrationNumber);
            }

            set
            {
                this.Set(this.DataModel.TaxRegistrationNumber, value);
            }
        }

        public int Sublevel
        {
            get
            {
                return this.GetInt(this.DataModel.Sublevel);
            }

            set
            {
                this.Set(this.DataModel.Sublevel, value);
            }
        }

        public DateTime JobEndDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.JobEndDate);
            }

            set
            {
                this.Set(this.DataModel.JobEndDate, value);
            }
        }

        public DateTime JobProjectedEndDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.JobProjectedEndDate);
            }

            set
            {
                this.Set(this.DataModel.JobProjectedEndDate, value);
            }
        }

        public DateTime JobStartDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.JobStartDate);
            }

            set
            {
                this.Set(this.DataModel.JobStartDate, value);
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

        public string TermsRef
        {
            get
            {
                return this.GetString(this.DataModel.TermsRef);
            }

            set
            {
                this.Set(this.DataModel.TermsRef, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBCustomerDataModel DataModel
        {
            get
            {
                return MaxDataLibrary.GetDataModel(this.DataModelType) as MaxQBCustomerDataModel;
            }
        }

        public static MaxQBCustomerEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBCustomerEntity),
                typeof(MaxQBCustomerDataModel)) as MaxQBCustomerEntity;
        }

        public override string GetDefaultSortString()
        {
            return this.LastName + " " + this.FirstName + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllQBDesktopByFullName(string lsFullName)
        {
            this.Set(this.DataModel.FullName, lsFullName);
            MaxData loDataFilter = new MaxData(this.Data);
            //// Add a Query 
            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(this.DataModel.FullName, "=", lsFullName);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(this.QBBaseDataModel.AlternateId, "=", "QBDesktop");
            loDataQuery.EndGroup();

            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            int lnTotal = int.MinValue;
            MaxDataList loDataList = MaxBaseIdRepository.Select(this.Data, loDataQuery, 0, 0, string.Empty, out lnTotal);
            loR = MaxEntityList.Create(this.GetType(), loDataList);
            loR.Total = lnTotal;
            return loR;
        }

        public bool LoadQBDesktopByFullName(string lsFullName, Guid loId)
        {
            bool lbR = false;
            MaxEntityList loCustomerList = this.LoadAllQBDesktopByFullName(lsFullName);
            if (loCustomerList.Count == 1)
            {
                lbR = this.Load(((MaxQBCustomerEntity)loCustomerList[0]).Data);
                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "LoadQBDesktopByFullName", MaxEnumGroup.LogNotice, "Got QB Customer with name [{Name}]", lsFullName));
                if (loId != Guid.Empty)
                {
                    this.SetId(loId);
                }
            }
            else if (loCustomerList.Count > 1)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(this.GetType(), "LoadQBDesktopByFullName", MaxEnumGroup.LogError, "More than one customer found with name [{Name}] in QB", lsFullName));
            }

            return lbR;
        }
    }
}
