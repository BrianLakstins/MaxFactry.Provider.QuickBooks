// <copyright file="MaxProvider.cs" company="Lakstins Family, LLC">
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
// <change date="9/24/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.Mvc4
{
	using System;
	using System.Collections;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MaxFactry.Core;

	/// <summary>
	/// Class used to define initialization tasks for a module.
	/// </summary>
	public class MaxStartup : MaxFactry.Base.MaxStartup
	{
        /// <summary>
        /// Internal storage of single object
        /// </summary>
        private static object _oInstance = null;

        /// <summary>
        /// Gets the single instance of this class.
        /// </summary>
        public new static MaxStartup Instance
        {
            get
            {
                _oInstance = CreateInstance(typeof(MaxStartup), _oInstance);
                return _oInstance as MaxStartup;
            }
        }

        /// <summary>
        /// To be run first, before anything else in the application.
        /// </summary>
        public override void RegisterProviders()
        {
            MaxFactry.Provider.QuickbooksProvider.MaxStartup.Instance.RegisterProviders();
        }

        /// <summary>
        /// To be run after providers have been registered
        /// </summary>
        /// <param name="loConfig">The configuration for the default repository provider.</param>
        public override void SetProviderConfiguration(MaxIndex loConfig)
        {
            MaxFactry.Provider.QuickbooksProvider.MaxStartup.Instance.SetProviderConfiguration(loConfig);
        }

        /// <summary>
        /// To be run after providers have been configured.
        /// </summary>
        public override void ApplicationStartup()
        {
            MaxFactry.Provider.QuickbooksProvider.MaxStartup.Instance.ApplicationStartup();

            RouteTable.Routes.MapRoute(
               name: "MaxQuickbooksProviderRoute",
               url: "MaxQuickbooksProviderManage/{action}/{id}",
               defaults: new { controller = "MaxQuickbooksProviderManage", action = "Index", id = UrlParameter.Optional }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "MaxQuickbooksProviderApiRoute",
                routeTemplate: "MaxQuickbooksProviderApi/{action}",
                defaults: new { controller = "MaxQuickbooksProviderApi", action = "index" }
            );

        }
	}
}
