﻿// <copyright file="MaxQBAdditionalNotesRetDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxQBAdditionalNotesRetDataModel : MaxQBBaseDataModel
    {
        public readonly string Date = "Date";

        public readonly string Note = "Note";

        public readonly string NoteID = "NoteID";

        /// <summary>
        /// Initializes a new instance of the MaxQBAdditionalNotesRetDataModel class
        /// </summary>
        public MaxQBAdditionalNotesRetDataModel()
        {
            this.SetDataStorageName("MaxQBAdditionalNotesRet");
            this.AddNullable(this.Date, typeof(DateTime));
            this.AddNullable(this.NoteID, typeof(int));
            this.AddNullable(this.Note, typeof(string));
        }
    }
}
