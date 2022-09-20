// <copyright file="MaxQBSessionDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBSessionDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        public readonly string UserName = "UserName";

        public readonly string StartDate = "StartDate";

        public readonly string EndDate = "EndDate";

        public readonly string RequestCount = "RequestCount";

        public readonly string InitialResponse = "InitialResponse";

        public readonly string ResponseId = "ResponseId";

        /// <summary>
        /// Initializes a new instance of the MaxQBResponseDataModel class
        /// </summary>
        public MaxQBSessionDataModel()
        {
            this.RepositoryProviderType = typeof(MaxFactry.Provider.QuickbooksProvider.DataLayer.Provider.MaxQuickbooksProviderRepositoryProvider);
            this.RepositoryType = typeof(MaxQuickbooksProviderRepository);
            this.SetDataStorageName("MaxQuickbooksProviderSession");
            this.AddType(this.UserName, typeof(MaxShortString));
            this.AddType(this.StartDate, typeof(DateTime));
            this.AddType(this.EndDate, typeof(DateTime));
            this.AddType(this.RequestCount, typeof(int));
            this.AddType(this.InitialResponse, typeof(MaxLongString));
            this.AddNullable(this.ResponseId, typeof(Guid));
        }
    }
}
