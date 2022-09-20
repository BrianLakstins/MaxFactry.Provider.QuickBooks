// <copyright file="MaxQWCAppDataModel.cs" company="Lakstins Family, LLC">
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
    /// Data Model for QBW App.  Used to generatate QWC File. 
    /// Defined in QBWC_proguide.pdf
    /// </summary>
    public class MaxQBWebConnectApplicationDataModel : MaxQBBaseDataModel
    {
        /// <summary>
        /// The name of the application visible to the user.
        /// Required: Y
        /// </summary>
        public readonly string AppName = "AppName";

        /// <summary>
        /// The AppID of the application, supplied in the call to OpenConnection.
        /// Required: Y
        /// Can be empty
        /// </summary>
        public readonly string AppID = "AppID";

        /// <summary>
        /// The URL of your web service.
        /// Required: Y
        /// The domain name used in the AppSupport URL must match the domain name used in the AppURL.
        /// </summary>
        public readonly string AppURL = "AppURL";

        /// <summary>
        /// This brief description of the application is displayed in the QB web connector
        /// Required: Y
        /// For best results we recommend a maximum description size of 80 characters.
        /// </summary>
        public readonly string AppDescription = "AppDescription";

        /// <summary>
        /// QBWC will use this to display name in the QBWC UI.
        /// Required: N
        /// </summary>
        public readonly string AppDisplayName = "AppDisplayName";

        /// <summary>
        /// The URL where your user can go to get support for your application.
        /// Required: Y
        /// </summary>
        public readonly string AppSupport = "AppSupport";

        /// <summary>
        /// If this element is available in QWC file, QBWC will not go into it’s typical clone/replace mode for AppName and directly use the replace routine.
        /// Required: N
        /// </summary>
        public readonly string AppUniqueName = "AppUniqueName";

        /// <summary>
        /// The name your user must use to access your web service.
        /// Required: Y
        /// The web connector uses this name when it invokes the authenticate call on your web service.
        /// To avoid disclosure of the password, there is no provision for any password field in the QWC file. Your user must enter the password
        /// into the web connector themselves, where it can be stored in the Windows registry securely via encryption.
        /// </summary>
        public readonly string UserName = "UserName";

        /// <summary>
        /// This is a GUID that represents your application or suite of applications, if your application needs to store private data in the company file for one reason or another.
        /// Required: Y
        /// You should generate one GUID per application only and not per application version or per QWC file!
        /// </summary>
        public readonly string OwnerID = "OwnerID";

        /// <summary>
        /// the Web Connector stores this as an extension to the company record with a specific OwnerID known only to your web service
        /// Required: Y
        /// </summary>
        public readonly string FileID = "FileID";

        /// <summary>
        /// Specify the value QBFS if your web service is designed for use with QuickBooks Financial software.
        /// Specify the value QBPOS if your web service is designed for use with QuickBooks Point-of-Sale (QBPOS).
        /// Required: Y
        /// </summary>
        public readonly string QBType = "QBType";
        
        /// <summary>
        /// Your end user can specify the update interval in the QB web connector UI.
        /// Required: N
        /// You can optionally supply a default update interval by including the <Scheduler> aggregate, but be aware that the user can override your settings in the UI.
        /// </summary>
        public readonly string Scheduler = "Scheduler";

        /// <summary>
        /// It specifies which QuickBooks editions are supported by your web service.
        /// Required: N
        /// This element is used only for QuickBooks (QBType=QBFS).
        /// 0x0 (All, default)
        /// 0x1 (SupportQBSimpleStart)
        /// 0x2 (SupportQBPro)
        /// 0x4 (SupportQBPremier)
        /// 0x8 (SupportQBEnterprise)
        /// </summary>
        public readonly string AuthFlags = "AuthFlags";

        /// <summary>
        /// Used to inform QBXMLRP2 (request processor) whether your service is reading data only, or is also writing data to the company.
        /// Specify true if write access is needed, or false if not.
        /// </summary>
        public readonly string IsReadOnly = "IsReadOnly";

        /// <summary>
        /// Value of true will enable notification (pop up at systray) at app level. Anything else will disable notification.
        /// </summary>
        public readonly string Notify = "Notify";

        /// <summary>
        /// Used to inform QBXMLRP2 (request processor) whether your service requires access to personal/sensitive data.
        /// Required: N
        /// Specify pdpNotNeeded/pdpOptional if it does not, and pdpRequired if it does.
        /// </summary>
        public readonly string PersonalDataPref = "PersonalDataPref";

        /// <summary>
        /// The SOAP encoding style used by your web service. If not supplied, the default used is Document.
        /// Required: N
        /// </summary>
        public readonly string Style = "Style";

        /// <summary>
        /// Used to inform QBXMLRP2 (request processor) whether your service needs permissions to run in Unattended Mode supply the value
        /// umpRequired if it does, or umpOptional if it does not.
        /// Required: N
        /// </summary>
        public readonly string UnattendedModePref = "UnattendedModePref";
        
        /// <summary>
        /// Initializes a new instance of the MaxQBWebConnectApplicationDataModel class
        /// </summary>
        public MaxQBWebConnectApplicationDataModel()
        {
            this.SetDataStorageName("MaxQuickbooksProviderQWCApp");
            this.AddType(this.AppName, typeof(MaxShortString));
            this.AddType(this.AppID, typeof(MaxShortString));
            this.AddType(this.AppURL, typeof(string));
            this.AddType(this.AppDescription, typeof(MaxShortString));
            this.AddNullable(this.AppDisplayName, typeof(MaxShortString));
            this.AddNullable(this.AppUniqueName, typeof(MaxShortString));
            this.AddType(this.AppSupport, typeof(string));
            this.AddType(this.UserName, typeof(MaxShortString));
            this.AddType(this.OwnerID, typeof(Guid));
            this.AddType(this.FileID, typeof(MaxShortString));
            this.AddType(this.QBType, typeof(MaxShortString));
            this.AddType(this.Scheduler, typeof(string));
            this.AddNullable(this.AuthFlags, typeof(MaxShortString));
            this.AddNullable(this.IsReadOnly, typeof(bool));
            this.AddNullable(this.Notify, typeof(bool));
            this.AddNullable(this.PersonalDataPref, typeof(MaxShortString));
            this.AddNullable(this.Style, typeof(MaxShortString));
            this.AddNullable(this.UnattendedModePref, typeof(MaxShortString));
        }
    }
}
