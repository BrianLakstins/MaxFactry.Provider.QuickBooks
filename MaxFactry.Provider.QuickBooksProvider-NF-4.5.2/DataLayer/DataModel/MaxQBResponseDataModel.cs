// <copyright file="MaxQBResponseDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxQBResponseDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// The ticket from the web connector.
        /// </summary>
        public readonly string RequestId = "RequestId";

        /// <summary>
        /// Contains the qbXML response from QuickBooks or qbposXML response from QuickBooks POS.
        /// </summary>
        public readonly string Response = "Response";

        public readonly string SessionId = "SessionId";

        /// <summary>
        /// 
        /// </summary>
        public readonly string Result = "Result";

        /// <summary>
        /// 
        /// </summary>
        public readonly string Message = "Message";

        /// <summary>
        /// 
        /// </summary>
        public readonly string CompanyFileName = "CompanyFileName";

        /// <summary>
        /// 
        /// </summary>
        public readonly string XMLCountry = "XMLCountry";

        /// <summary>
        /// 
        /// </summary>
        public readonly string XMLMajorVers = "XMLMajorVers";

        /// <summary>
        /// 
        /// </summary>
        public readonly string XMLMinorVers = "XMLMinorVers";

        /// <summary>
        /// 
        /// </summary>
        public readonly string ProcessLog = "ProcessLog";

        /// <summary>
        /// Initializes a new instance of the MaxQBResponseDataModel class
        /// </summary>
        public MaxQBResponseDataModel()
        {
            this.RepositoryProviderType = typeof(MaxFactry.Provider.QuickbooksProvider.DataLayer.Provider.MaxQuickbooksProviderRepositoryProvider);
            this.RepositoryType = typeof(MaxQuickbooksProviderRepository);
            this.SetDataStorageName("MaxQuickbooksProviderResponse");
            this.AddType(this.RequestId, typeof(Guid));
            this.AddType(this.Response, typeof(MaxLongString));
            this.AddType(this.SessionId, typeof(Guid));
            this.AddType(this.Result, typeof(string));
            this.AddType(this.Message, typeof(MaxShortString));
            this.AddNullable(this.CompanyFileName, typeof(string));
            this.AddNullable(this.XMLCountry, typeof(MaxShortString));
            this.AddNullable(this.XMLMajorVers, typeof(int));
            this.AddNullable(this.XMLMinorVers, typeof(int));
            this.AddNullable(this.ProcessLog, typeof(MaxLongString));
        }
    }
}
