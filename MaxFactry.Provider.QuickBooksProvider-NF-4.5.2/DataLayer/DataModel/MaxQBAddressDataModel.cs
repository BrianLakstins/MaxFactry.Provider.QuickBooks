// <copyright file="MaxQBAddressDataModel.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// </summary>
    public class MaxQBAddressDataModel : MaxQBBaseDataModel
    {
        public readonly string Addr1 = "Addr1";

        public readonly string Addr2 = "Addr2";

        public readonly string Addr3 = "Addr3";

        public readonly string Addr4 = "Addr4";

        public readonly string Addr5 = "Addr5";

        public readonly string City = "City";

        public readonly string State = "State";

        public readonly string PostalCode = "PostalCode";

        public readonly string Country = "Country";

        public readonly string Note = "Note";

        /// <summary>
        /// Initializes a new instance of the MaxQBAddressDataModel class
        /// </summary>
        public MaxQBAddressDataModel()
        {
            this.SetDataStorageName("MaxQBAddress");
            this.AddNullable(this.Addr1, typeof(MaxShortString));
            this.AddNullable(this.Addr2, typeof(MaxShortString));
            this.AddNullable(this.Addr3, typeof(MaxShortString));
            this.AddNullable(this.Addr4, typeof(MaxShortString));
            this.AddNullable(this.Addr5, typeof(MaxShortString));
            this.AddNullable(this.City, typeof(MaxShortString));
            this.AddNullable(this.State, typeof(MaxShortString));
            this.AddNullable(this.PostalCode, typeof(MaxShortString));
            this.AddNullable(this.Country, typeof(MaxShortString));
            this.AddNullable(this.Note, typeof(MaxShortString));
        }
    }
}

