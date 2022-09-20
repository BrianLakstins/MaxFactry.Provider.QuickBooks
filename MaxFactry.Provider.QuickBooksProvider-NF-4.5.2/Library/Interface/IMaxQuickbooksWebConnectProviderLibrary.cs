// <copyright file="IMaxQuickbooksProviderLibrary.cs" company="Lakstins Family, LLC">
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

namespace MaxFactry.Provider.QuickbooksProvider
{
    using System;
    using MaxFactry.Core;

    public interface IMaxQuickbooksWebConnectProviderLibrary : IMaxProvider
    {

        /// <summary>
        /// Provides a way for web-service to notify QBWC of it’s version. 
        /// This version string shows up in the More Information pop-up dialog in QBWC.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector. This is the session token your
        /// web service returned to the web connector’s authenticate call, as the
        /// first element of the returned string array.</param>
        /// <returns>Your web service should return a message string describing the server version and any other information that you want your user to see.</returns>
        string GetServerVersion(string lsTicket);

        /// <summary>
        /// Optional callback allows the web service to evaluate the current web connector version and react to it. 
        /// Not currently required to support backward compatibility but strongly recommended.
        /// </summary>
        /// <param name="lsVersion">The version of the QB web connector supplied in the web connector’s call to clientVersion.</param>
        /// <returns>A string telling the web connector what to do next.</returns>
        string GetClientVersion(string lsVersion);

        /// <summary>
        /// Prompts the web service to authenticate the specified user and specify the company to be used in the session.
        /// </summary>
        /// <param name="lsUsername">The web connector supplies the user name that you provided to your user in the QWC file to allow that username to access your web service.</param>
        /// <param name="lsPassword">The web connector supplies the user password that you provided to your user and which was stored by the user in the web connector.</param>
        /// <returns>return A string array with 4 possible elements.  first element of the returned string array is the ticket from the web connector.</returns>
        string[] Authenticate(string lsUsername, string lsPassword);


        /// <summary>
        /// Tells your web service that the web connector is finished with the update session.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <returns>Specify a string that you want the web connector to display to the user showing the status of
        /// the web service action on behalf of your user. This string will be displayed in the web
        /// connector UI in the status column.</returns>
        string CloseConnection(string lsTicket);

        /// <summary>
        /// Tells your web service about an error the web connector encountered in its attempt to connect to QuickBooks or QuickBooks POS.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsResult">The HRESULT (in HEX) from the exception thrown by the request processor.</param>
        /// <param name="lsMessage">The error message that accompanies the HRESULT from the request processor.</param>
        /// <returns>Specify the string value “done” to indicate that your web service is finished.</returns>
        string ConnectionError(string lsTicket, string lsResult, string lsMessage);

        /// <summary>
        /// Lets your web service tell QBWC where to get the web page to display in the browser at the start of interactive mode.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsSessionID"></param>
        /// <returns>Your web service should return a message string containing the URL of the web page you want opened in the browser.</returns>
        string GetInteractiveURL(string lsTicket, string lsSessionID);

        /// <summary>
        /// Allows your web service to return the last web service error, normally for display to the user, before causing the update action to stop.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <returns></returns>
        string GetLastError(string lsTicket);

        /// <summary>
        /// Allows your web service to indicate to QBWC that it is done with interactive mode.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <returns>Your web service should return a message string with the value “Done” when the interactive session is over.</returns>
        string InteractiveDone(string lsTicket);

        /// <summary>
        /// Allows your web service to take alternative action when the interactive session it requested was
        /// rejected by the user or by timeout in the absence of the user.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsReason">The reason for the rejection of interactive mode.</param>
        /// <returns>Return a message string to be displayed.</returns>
        string InteractiveRejected(string lsTicket, string lsReason);

        /// <summary>
        /// Returns the data request response from QuickBooks or QuickBooks POS.
        /// </summary>
        /// <param name="lsTicket">The ticket from the web connector.</param>
        /// <param name="lsResponse">Contains the qbXML response from QuickBooks or qbposXML response from QuickBooks POS.</param>
        /// <param name="lsResult"></param>
        /// <param name="lsMessage"></param>
        /// <returns>A positive integer less than 100 represents the percentage of work completed.  A negative value means an error has occurred and the Web Connector responds to this with a getLastError call.</returns>
        int ReceiveResponseXML(string lsTicket, string lsResponse, string lsResult, string lsMessage);

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
        string SendRequestXML(string lsTicket, string lsResponse, string lsCompanyFileName, string lsXMLCountry, int lsXMLMajorVers, int lsXMLMinorVers);
    }
}