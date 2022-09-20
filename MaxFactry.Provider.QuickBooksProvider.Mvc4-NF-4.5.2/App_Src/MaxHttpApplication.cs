﻿// <copyright file="MaxHttpApplicationMvc4Override.cs" company="Lakstins Family, LLC">
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
// <change date="5/18/2016" author="Brian A. Lakstins" description="Add test configuration">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.Mvc4
{
    using System;
    using System.Web;
    using MaxFactry.Core;

    /// <summary>
    /// Base application for Mvc4 based web applications.
    /// </summary>
    public class MaxHttpApplication : System.Web.MaxHttpApplicationMvc4Override
    {

        /// <summary>
        /// 11. Occurs when the state information (for example, session state or application state) that is associated with the current request has been obtained.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ApplicationPostAcquireRequestState(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 12. Occurs just before ASP.NET starts executing an event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ApplicationPreRequestHandlerExecute(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Runs once for each application instance created.  Anything that updates the
        /// Application instance (like adding event handlers) should be included in Init().
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.PostAcquireRequestState += (new EventHandler(MaxHttpApplication.ApplicationPostAcquireRequestState));
            this.PreRequestHandlerExecute += (new EventHandler(MaxHttpApplication.ApplicationPreRequestHandlerExecute));
        }

        protected virtual MaxIndex GetConfig()
        {
            #region Configuration
            var loConfig = new MaxFactry.Core.MaxIndex();
            //// DataSet Provider Configuration
            loConfig.Add("DefaultContextProviderTypeDataSet", typeof(MaxFactry.Base.DataLayer.Provider.MaxDataContextDefaultProvider));
            loConfig.Add("DataSetFolder", HttpContext.Current.Server.MapPath("~/App_Data/MaxDataSet/"));

            //// Set Default DataContextProvider
            loConfig.Add(typeof(MaxFactry.Core.MaxProvider) + "-DefaultContextProviderName", "DefaultContextProviderTypeDataSet");
            #endregion

            return loConfig;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            MaxIndex loConfig = this.GetConfig();
            this.Application_Start_Handler(sender, e, typeof(MaxAppLibraryProvider), loConfig);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            this.Session_Start_Handler(sender, e);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            this.Session_End_Handler(sender, e);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            this.Application_End_Handler(sender, e);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            this.Application_Error_Handler(sender, e);
        }
    }
}