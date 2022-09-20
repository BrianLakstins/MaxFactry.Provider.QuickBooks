// <copyright file="MaxQuickbooksProviderDefaultLibrary.cs" company="Lakstins Family, LLC">
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
// <change date="10/21/2015" author="Brian A. Lakstins" description="initial creation">
// </changelog>
#endregion

namespace MaxFactry.Provider.QuickbooksProvider.Provider
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Provider.QuickbooksProvider.BusinessLayer;

    public class MaxQuickbooksWebConnectProviderDefaultLibrary : MaxFactry.Core.MaxProvider, IMaxQuickbooksWebConnectProviderLibrary
    {
        /// <summary>
        /// Provides a way for web-service to notify QBWC of it’s version. 
        /// This version string shows up in the More Information pop-up dialog in QBWC.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector. This is the session token your
        /// web service returned to the web connector’s authenticate call, as the
        /// first element of the returned string array.</param>
        /// <returns>Your web service should return a message string describing the server version and any other information that you want your user to see.</returns>
        public string GetServerVersion(string lsTicket)
        {
            return "2015.10";
        }

        /// <summary>
        /// Optional callback allows the web service to evaluate the current web connector version and react to it. 
        /// Not currently required to support backward compatibility but strongly recommended.
        /// A string telling the web connector what to do next. Supply one of the following strings:
        /// </summary>
        /// <param name="lsVersion">The version of the QB web connector supplied in the web connector’s call to clientVersion.</param>
        /// <returns>• Specify an empty string or Null if you want the web connector to proceed with the
        /// update.
        /// • Specify a text string that begins with the characters "W:" if you want the web connector
        /// to display a WARNING dialog prompting the user to continue with the update or cancel
        /// it. The text string after the “W:” will be displayed in the warning dialog.
        /// • Specify a text string that begins with the characters "E:" if you want the web connector
        /// to cancel the update and display an ERROR dialog. The text string after the “E:” will
        /// be displayed in the error dialog. The user will have to download a new version of the
        /// web connector to continue with the update.
        /// • Supply a value of O: (O as in Okay, not zero, followed by the QBWC version supported
        /// by the web service). For example O:2.0. This tells the user that the server expects a
        /// newer version of QBWC than the user currently has but also tells the user which
        /// version is needed.</returns>
        public string GetClientVersion(string lsVersion)
        {
            string lsR = string.Empty;
            if (null != lsVersion)
            {
                if (lsVersion != "2.1.0.30")
                {
                    lsR = "W:Only tested with veersion 2.1.0.30";
                }
            }

            return lsR;
        }

        /// <summary>
        /// Prompts the web service to authenticate the specified user and specify the company to be used in the session.
        /// </summary>
        /// <param name="lsUsername">The web connector supplies the user name that you provided to your user in the QWC file to allow that username to access your web service.</param>
        /// <param name="lsPassword">The web connector supplies the user password that you provided to your user and which was stored by the user in the web connector.</param>
        /// <returns>return A string array with 4 possible elements.  first element of the returned string array is the ticket from the web connector.</returns>
        public string[] Authenticate(string lsUsername, string lsPassword)
        {
            //// If username and password exist and are not the same, then the authentication is valid
            //// TODO: Integrate with MaxSecurity
            string[] laR = new string[] { "NONE", string.Empty, string.Empty, string.Empty };
            if (lsUsername.Length > 0 && lsPassword.Length > 0)
            {
                if (lsUsername == lsPassword)
                {
                    laR[0] = "NVU";
                }
                else
                {
                    Guid loSessionId = Guid.NewGuid();
                    laR[0] = loSessionId.ToString();
                    DateTime loSessionStart = DateTime.UtcNow;

                    MaxQBRequestEntity loRequest = MaxQBRequestEntity.Create();
                    MaxEntityList loList = loRequest.LoadAllActiveByUser(lsUsername);
                    int lnCount = 0;
                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        loRequest = loList[lnE] as MaxQBRequestEntity;
                        //// Give each request 1 day to be processed before it is resent.
                        if (loRequest.SentDate.AddDays(1) < loSessionStart)
                        {
                            lnCount++;
                        }
                    }

                    if (lnCount > 0)
                    {
                        //// Only create the session if there are items to process.
                        MaxQBSessionEntity loSession = MaxQBSessionEntity.Create();
                        loSession.RequestCount = lnCount;
                        loSession.UserName = lsUsername;
                        loSession.IsActive = true;
                        loSession.RequestCount = 0;
                        loSession.StartDate = loSessionStart;
                        loSession.Insert(loSessionId);
                    }
                    else
                    {
                        //// Specify that there are no requests to process at this time.
                        laR[1] = "none";
                        //// CloseConnection is not called when "none" is returned, so close the session.
                    }
                }
            }


            return laR;
        }

        /// <summary>
        /// Tells your web service that the web connector is finished with the update session.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <returns>Specify a string that you want the web connector to display to the user showing the status of
        /// the web service action on behalf of your user. This string will be displayed in the web
        /// connector UI in the status column.</returns>
        public string CloseConnection(string lsTicket)
        {
            Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsTicket);
            MaxQBSessionEntity loSession = MaxQBSessionEntity.Create();
            if (loSession.LoadByIdCache(loId))
            {
                loSession.IsActive = false;
                loSession.EndDate = DateTime.UtcNow;
                loSession.Update();
            }

            return "Thanks for coming!";
        }

        /// <summary>
        /// Tells your web service about an error the web connector encountered in its attempt to connect to QuickBooks or QuickBooks POS.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsResult">The HRESULT (in HEX) from the exception thrown by the request processor.</param>
        /// <param name="lsMessage">The error message that accompanies the HRESULT from the request processor.</param>
        /// <returns>Specify the string value “done” to indicate that your web service is finished.</returns>
        public string ConnectionError(string lsTicket, string lsResult, string lsMessage)
        {
            return "done";
        }

        /// <summary>
        /// Lets your web service tell QBWC where to get the web page to display in the browser at the start of interactive mode.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsSessionID"></param>
        /// <returns>Your web service should return a message string containing the URL of the web page you want opened in the browser.</returns>
        public string GetInteractiveURL(string lsTicket, string lsSessionID)
        {
            return string.Empty;
        }

        /// <summary>
        /// Allows your web service to return the last web service error, normally for display to the user, before causing the update action to stop.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <returns></returns>
        public string GetLastError(string lsTicket)
        {
            return "Some error occurred";
        }

        /// <summary>
        /// Allows your web service to indicate to QBWC that it is done with interactive mode.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <returns>Your web service should return a message string with the value “Done” when the interactive session is over.</returns>
        public string InteractiveDone(string lsTicket)
        {
            return "Done";
        }

        /// <summary>
        /// Allows your web service to take alternative action when the interactive session it requested was
        /// rejected by the user or by timeout in the absence of the user.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsReason">The reason for the rejection of interactive mode.</param>
        /// <returns>Return a message string to be displayed.</returns>
        public string InteractiveRejected(string lsTicket, string lsReason)
        {
            return "The interactive session has been rejected: " + lsReason;
        }

        /// <summary>
        /// Returns the data request response from QuickBooks or QuickBooks POS.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsResponse">Contains the qbXML response from QuickBooks or qbposXML response from QuickBooks POS.</param>
        /// <param name="lsResult"></param>
        /// <param name="lsMessage"></param>
        /// <returns>A positive integer less than 100 represents the percentage of work completed.  A negative value means an error has occurred and the Web Connector responds to this with a getLastError call.</returns>
        public int ReceiveResponseXML(string lsTicket, string lsResponse, string lsResult, string lsMessage)
        {
            int lnR = 100;
            Guid loSessionId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsTicket);
            MaxQBSessionEntity loSession = MaxQBSessionEntity.Create();
            if (loSession.LoadByIdCache(loSessionId))
            {
                MaxQBResponseEntity loResponse = MaxQBResponseEntity.Create();
                loResponse.SessionId = loSessionId;
                loResponse.Response = lsResponse;
                loResponse.Result = lsResult;
                loResponse.Message = lsMessage;
                loResponse.Insert();
                MaxQBRequestEntity loRequest = MaxQBRequestEntity.Create();
                MaxEntityList loList = loRequest.LoadAllActiveBySessionId(loSessionId);
                if (loList.Count > 0)
                {
                    MaxQBRequestEntity loRequestLatest = null;
                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        loRequest = loList[lnE] as MaxQBRequestEntity;
                        if (null == loRequestLatest)
                        {
                            loRequestLatest = loRequest;
                        }
                        else if (loRequestLatest.SentDate < loRequest.SentDate)
                        {
                            loRequestLatest = loRequest;
                        }
                    }

                    if (null != loRequestLatest)
                    {
                        loRequestLatest.IsActive = false;
                        loRequestLatest.ResponseId = loResponse.Id;
                        loRequestLatest.Update();
                        loResponse.RequestId = loRequestLatest.Id;
                        loResponse.Update();

                        //// TODO: Process response and send error code if processing fails





                        //// Find number left for this session and send back a % complete
                        loRequest = MaxQBRequestEntity.Create();
                        MaxEntityList loActiveList = loRequest.LoadAllActiveByUser(loSession.UserName);
                        int lnCount = 0;
                        for (int lnE = 0; lnE < loActiveList.Count; lnE++)
                        {
                            loRequest = loActiveList[lnE] as MaxQBRequestEntity;
                            //// Give each request 1 day to be processed before it is resent.
                            if (loRequest.SentDate.AddDays(1) < loSession.CreatedDate)
                            {
                                lnCount++;
                            }
                        }

                        lnR = 0;
                        if (lnCount < loSession.RequestCount)
                        {
                            double lnPercentLeft = 100.0 * Convert.ToDouble(lnCount) / Convert.ToDouble(loSession.RequestCount);
                            double lnComplete = 100.0 - lnPercentLeft;
                            lnR = Convert.ToInt32(Math.Floor(lnComplete));
                        }
                    }
                }
            }

            return lnR;
        }

        /// <summary>
        /// The web connector’s invitation to the web service to send a request.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsResponse">Only for the first sendRequestXML call in a data exchange session
        /// will this parameter contains response data from a HostQuery, a
        /// CompanyQuery, and a PreferencesQuery request. This data is
        /// provided at the outset of a data exchange because it is normally
        /// useful for a web service to have this data. In the ensuing data
        /// exchange session, subsequent sendRequestXML calls from the web
        /// processor do not contain this data, (only an empty string is supplied)
        /// as it is assumed your web service already has it for the session.</param>
        /// <param name="lsCompanyFileName">The company file being used in the current data exchange.</param>
        /// <param name="lsXMLCountry">The country version of QuickBooks or QuickBooks POS product being used to access the company. For example, US, CA (Canada), or UK.</param>
        /// <param name="lsXMLMajorVers">The major version number (corresponding to the qbXML or qbposXML spec level) of the request processor being used.</param>
        /// <param name="lsXMLMinorVers">The minor version number (corresponding to the qbXML or qbposXML spec level) of the request processor being used.</param>
        /// <returns>If the web service has no requests to send, specify an empty string. If you want the Web
        /// Connector to pause for an interval of time (currently 5 seconds) return the string “NoOp”,
        /// which will cause the Web Connector to call your getLastError callback: a “NoOp” returned
        /// from GetLastError will cause the QBWC to pause updates for 5 seconds before attempting
        /// to call sendRequestXML() again.
        /// Any other string will be taken as a qbXML for QuickBooks or a qbposXML request for
        /// QuickBooks POS. The Web Connector sends the qbXML or qbposXML to QuickBooks or
        /// QuickBooks POS via the request processor’s ProcessRequest method call.</returns>
        public string SendRequestXML(string lsTicket, string lsResponse, string lsCompanyFileName, string lsXMLCountry, int lnXMLMajorVers, int lnXMLMinorVers)
        {
            Guid loSessionId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsTicket);
            MaxQBSessionEntity loSession = MaxQBSessionEntity.Create();
            if (loSession.LoadByIdCache(loSessionId) && loSession.IsActive)
            {
                if (null != lsResponse && lsResponse.Length > 0)
                {
                    MaxQBResponseEntity loResponse = MaxQBResponseEntity.Create();
                    loResponse.SessionId = loSessionId;
                    loResponse.Response = lsResponse;
                    loResponse.CompanyFileName = lsCompanyFileName;
                    loResponse.XMLCountry = lsXMLCountry;
                    loResponse.XMLMajorVers = lnXMLMajorVers;
                    loResponse.XMLMinorVers = lnXMLMinorVers;
                    loResponse.Insert();

                    loSession.ResponseId = loResponse.Id;
                    loSession.Update();
                }

                MaxQBRequestEntity loRequest = MaxQBRequestEntity.Create();
                MaxEntityList loList = loRequest.LoadAllActiveByUser(loSession.UserName);
                for (int lnE = 0; lnE < loList.Count; lnE++ )
                {
                    loRequest = loList[lnE] as MaxQBRequestEntity;
                    //// Give each request 1 day to be processed before it is resent.
                    if (loRequest.SentDate.AddDays(1) < loSession.CreatedDate)
                    {
                        loRequest.SentDate = DateTime.UtcNow;
                        loRequest.SessionId = loSession.Id;
                        loRequest.Update();
                        return loRequest.Request;
                    }
                }
            }

            return string.Empty;
        }

    }
}