// <copyright file="MaxQBItemEntity.cs" company="Lakstins Family, LLC">
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

    public class MaxQBItemEntity : MaxQBBaseEntity
    {

		/// <summary>
        /// Initializes a new instance of the MaxCartEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxQBItemEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxQBItemEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public MaxQBItemServiceEntity ItemServiceRet
        {
            get
            {
                MaxQBItemServiceEntity loR = MaxQBItemServiceEntity.Create();
                string lsData = this.Get(this.DataModel.ItemServiceRet) as string;
                if (!string.IsNullOrEmpty(lsData))
                {
                    loR.Load(lsData);
                }

                return loR;
            }

            set
            {

                Set(this.DataModel.ItemServiceRet, value.ExportToString());
            }
        }

        public MaxQBItemNonInventoryEntity ItemNonInventoryRet
        {
            get
            {
                MaxQBItemNonInventoryEntity loR = MaxQBItemNonInventoryEntity.Create();
                object loData = this.Get(this.DataModel.ItemNonInventoryRet);
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

                Set(this.DataModel.ItemServiceRet, value.ExportToString());
            }
        }

        public MaxQBItemDiscountEntity ItemDiscountRet
        {
            get
            {
                MaxQBItemDiscountEntity loR = MaxQBItemDiscountEntity.Create();
                object loData = this.Get(this.DataModel.ItemDiscountRet);
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

                Set(this.DataModel.ItemDiscountRet, value.ExportToString());
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxQBItemDataModel DataModel
        {
            get
            {
                return (MaxQBItemDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxQBItemEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxQBItemEntity),
                typeof(MaxQBItemDataModel)) as MaxQBItemEntity;
        }

        public static string GetFullName(string lsName)
        {
            MaxEntityList loList = MaxQBItemEntity.Create().LoadAllQBDesktop();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxQBItemEntity loEntity = loList[lnE] as MaxQBItemEntity;
                if (null != loEntity.ItemNonInventoryRet && loEntity.ItemNonInventoryRet.Name == lsName)
                {
                    return loEntity.ItemNonInventoryRet.FullName;
                }
                else if (null != loEntity.ItemServiceRet && loEntity.ItemServiceRet.Name == lsName)
                {
                    return loEntity.ItemServiceRet.FullName;
                }
                else if (null != loEntity.ItemDiscountRet && loEntity.ItemDiscountRet.Name == lsName)
                {
                    return loEntity.ItemDiscountRet.FullName;
                }
            }

            return string.Empty;
        }
    }
}
