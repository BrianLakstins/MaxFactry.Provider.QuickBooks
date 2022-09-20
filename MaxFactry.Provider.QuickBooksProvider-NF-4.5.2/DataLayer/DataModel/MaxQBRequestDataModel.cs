// <copyright file="MaxQBRequestDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxQBRequestDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Normally not assigned. Use an empty string for appID.
        /// </summary>
        public readonly string AppId = "AppId";

        /// <summary>
        /// The application name used in the log file, in the authorization dialog box, and in menu extensions. This parameter cannot be NULL or an empty string.
        /// </summary>
        public readonly string AppName = "AppName";

        /// <summary>
        /// </summary>
        public readonly string Request = "Request";

        public readonly string SessionId = "SessionId";

        public readonly string ResponseId = "ResponseId";

        public readonly string SentDate = "SentDate";

        public readonly string Username = "Username";

        public readonly string Name = "Name";

        public readonly string Description = "Description";

        /// <summary>
        /// Initializes a new instance of the MaxQBRequestDataModel class
        /// </summary>
        public MaxQBRequestDataModel()
        {
            this.RepositoryProviderType = typeof(MaxFactry.Provider.QuickbooksProvider.DataLayer.Provider.MaxQuickbooksProviderRepositoryProvider);
            this.RepositoryType = typeof(MaxQuickbooksProviderRepository);
            this.SetDataStorageName("MaxQuickbooksProviderRequest");
            this.AddType(this.AppId, typeof(MaxShortString));
            this.AddType(this.AppName, typeof(MaxShortString));
            this.AddType(this.Request, typeof(MaxLongString));
            this.AddType(this.SessionId, typeof(Guid));
            this.AddType(this.ResponseId, typeof(Guid));
            this.AddType(this.SentDate, typeof(DateTime));
            this.AddType(this.Username, typeof(MaxShortString));
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.Description, typeof(string));
        }
    }
}
