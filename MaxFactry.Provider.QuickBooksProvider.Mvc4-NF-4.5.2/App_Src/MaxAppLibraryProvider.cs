// <copyright file="MaxApplicationLibraryDefaultProvider.cs" company="Lakstins Family, LLC">
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
// <change date="7/5/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.Mvc4
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;

    /// <summary>
    /// Security Test provider for MaxApplicationLibrary
    /// </summary>
    public class MaxAppLibraryProvider : MaxFactry.General.AspNet.IIS.Mvc4.Provider.MaxAppLibraryDefaultProvider
    {

        public override void RegisterProviders()
        {
            base.RegisterProviders();
            MaxFactry.Provider.QuickbooksProvider.Mvc4.MaxStartup.Instance.RegisterProviders();
        }

        public override void SetProviderConfiguration(MaxIndex loConfig)
        {
            base.SetProviderConfiguration(loConfig);
            MaxFactry.Provider.QuickbooksProvider.Mvc4.MaxStartup.Instance.SetProviderConfiguration(loConfig);
            MaxFactry.Core.MaxFactryLibrary.SetValue(typeof(MaxFactry.Base.DataLayer.Provider.MaxDataContextDefaultProvider) + "-Config", loConfig);
        }

        public override void ApplicationStartup()
        {
            base.ApplicationStartup();
            Guid loId = new Guid("54CAF349-93FC-4FD5-A0F0-F093FE5D49B6");
            MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeApplication, MaxFactryLibrary.MaxStorageKeyName, loId);
            MaxFactry.General.AspNet.IIS.Mvc4.MaxAppLibrary.AddValidStorageKey(loId.ToString());

            MaxFactry.Provider.QuickbooksProvider.Mvc4.MaxStartup.Instance.ApplicationStartup();

            //// Add a default route last for any routes that have not been added, but will still match controllers and actions already loaded.
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}