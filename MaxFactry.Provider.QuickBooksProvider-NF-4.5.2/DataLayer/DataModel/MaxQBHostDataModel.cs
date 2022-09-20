// <copyright file="MaxQBHostDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBHostDataModel : MaxQBBaseDataModel
    {
        public readonly string ProductName = "ProductName";

        public readonly string MajorVersion = "MajorVersion";

        public readonly string MinorVersion = "MinorVersion";

        public readonly string Country = "Country";

        /// <summary>
        ///    [Guid("93AF3800-4B43-48F4-A800-A8CC5E1AC411")]
        ///    string GetAt(int index);
        ///    int Count { get; }
        /// </summary>
        public readonly string SupportedQBXMLVersionList = "SupportedQBXMLVersionList";

        public readonly string IsAutomaticLogin = "IsAutomaticLogin";

        public readonly string QBFileMode = "QBFileMode";

        public readonly string ListMetaData = "ListMetaData";

        /// <summary>
        /// Initializes a new instance of the MaxQBHostDataModel class
        /// </summary>
        public MaxQBHostDataModel()
        {
            this.SetDataStorageName("MaxQBHost");
            this.AddNullable(this.ProductName, typeof(MaxShortString));
            this.AddNullable(this.MajorVersion, typeof(MaxShortString));
            this.AddNullable(this.MinorVersion, typeof(MaxShortString));
            this.AddNullable(this.Country, typeof(MaxShortString));
            this.AddNullable(this.SupportedQBXMLVersionList, typeof(MaxLongString));
            this.AddNullable(this.IsAutomaticLogin, typeof(bool));
            this.AddNullable(this.QBFileMode, typeof(MaxShortString));
            this.AddNullable(this.ListMetaData, typeof(MaxLongString));
        }
    }
}
