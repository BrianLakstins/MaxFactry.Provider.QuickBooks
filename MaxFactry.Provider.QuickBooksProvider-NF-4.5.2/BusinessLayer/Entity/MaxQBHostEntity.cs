// <copyright file="MaxQBHostEntity.cs" company="Lakstins Family, LLC">
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
// <change date="11/3/2021" author="Brian A. Lakstins" description="Initial creation">
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

    public class MaxQBHostEntity : MaxQBBaseEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBHostEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBHostEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Country
        {
            get
            {
                return this.GetString(this.DataModel.Country);
            }

            set
            {
                this.Set(this.DataModel.Country, value);
            }
        }

        public bool IsAutomaticLogin
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsAutomaticLogin);
            }

            set
            {
                this.Set(this.DataModel.IsAutomaticLogin, value);
            }
        }

        public string MajorVersion
        {
            get
            {
                return this.GetString(this.DataModel.MajorVersion);
            }

            set
            {
                this.Set(this.DataModel.MajorVersion, value);
            }
        }

        public string MinorVersion
        {
            get
            {
                return this.GetString(this.DataModel.MinorVersion);
            }

            set
            {
                this.Set(this.DataModel.MinorVersion, value);
            }
        }

        public string ProductName
        {
            get
            {
                return this.GetString(this.DataModel.ProductName);
            }

            set
            {
                this.Set(this.DataModel.ProductName, value);
            }
        }

        public string[] SupportedQBXMLVersionList
        {
            get
            {
                return this.GetObject(this.DataModel.SupportedQBXMLVersionList, typeof(string[])) as string[];
            }

            set
            {
                this.SetObject(this.DataModel.SupportedQBXMLVersionList, value);
            }
        }

        public short VersionMajorSupport
        {
            get
            {
                // Perform a little variable initialization prior to obtaining the most recent SDK version
                // supported by the backend QuickBooks instance.
                double lnMaxQBXMLVersion = 0.0;

                // Iterate through each of the versions supported by the remote 
                // QuickBooks instance and return the most recent.
                foreach (string lsVersion in this.SupportedQBXMLVersionList)
                {
                    double lnVersion = MaxConvertLibrary.ConvertToDouble(typeof(object), lsVersion);
                    if (lnVersion > lnMaxQBXMLVersion)
                    {
                        lnMaxQBXMLVersion = lnVersion;
                    }
                }

                // At this point, _qbsdkVersion has the value of the most recent SDK version supported by the
                // backend instance.  Unfortunately, many of the signatures for methods we want to use require
                // that this double be split apart into two separate integers.  The represent the major (left of
                // the decimal point) and minor (right of the decimal point) values.  Perform this extraction 
                // now.
                short lnMaxVersionMajor = (short)lnMaxQBXMLVersion;
                return lnMaxVersionMajor;
            }
        }

        public double VersionMinorSupport
        {
            get
            {
                // Perform a little variable initialization prior to obtaining the most recent SDK version
                // supported by the backend QuickBooks instance.
                double lnMaxQBXMLVersion = 0.0;

                // Iterate through each of the versions supported by the remote 
                // QuickBooks instance and return the most recent.
                foreach (string lsVersion in this.SupportedQBXMLVersionList)
                {
                    double lnVersion = MaxConvertLibrary.ConvertToDouble(typeof(object), lsVersion);
                    if (lnVersion > lnMaxQBXMLVersion)
                    {
                        lnMaxQBXMLVersion = lnVersion;
                    }
                }

                // At this point, _qbsdkVersion has the value of the most recent SDK version supported by the
                // backend instance.  Unfortunately, many of the signatures for methods we want to use require
                // that this double be split apart into two separate integers.  The represent the major (left of
                // the decimal point) and minor (right of the decimal point) values.  Perform this extraction 
                // now.
                short lnMaxVersionMajor = (short)lnMaxQBXMLVersion;
                double lnMaxVersionMinor = lnMaxVersionMajor - lnMaxQBXMLVersion;
                return lnMaxVersionMinor;
            }
        }


        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBHostDataModel DataModel
        {
            get
            {
                return (MaxQBHostDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBHostEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBHostEntity),
                typeof(MaxQBHostDataModel)) as MaxQBHostEntity;
        }
    }
}
