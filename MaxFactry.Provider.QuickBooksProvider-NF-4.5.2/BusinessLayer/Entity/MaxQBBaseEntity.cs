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
    using MaxFactry.Provider.QuickbooksProvider.DataLayer;
    using Interop.QBFC15;

    public class MaxQBBaseEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBBaseEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBBaseEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Type
        {
            get
            {
                return this.GetString(this.QBBaseDataModel.Type);
            }

            set
            {
                this.Set(this.QBBaseDataModel.Type, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBBaseDataModel QBBaseDataModel
        {
            get
            {
                return (MaxQBBaseDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
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

            return lsR;
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

        public MaxEntityList LoadAllQBDesktop()
        {
            return this.LoadAllByProperty(this.QBBaseDataModel.AlternateId, "QBDesktop");
        }

        public MaxEntityList LoadAllQBDesktopCache()
        {
            return this.LoadAllByPropertyCache(this.QBBaseDataModel.AlternateId, "QBDesktop");
        }

        public bool InsertQBDesktop()
        {
            this.AlternateId = "QBDesktop";
            return base.Insert();
        }

        public bool UpdateQBDesktop()
        {
            this.AlternateId = "QBDesktop";
            this.SetId(Guid.NewGuid());
            return base.Update();
        }
    }
}
