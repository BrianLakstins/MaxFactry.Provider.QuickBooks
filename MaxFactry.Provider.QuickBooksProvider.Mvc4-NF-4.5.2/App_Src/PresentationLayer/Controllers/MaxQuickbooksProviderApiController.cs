// <copyright file="MaxQuickbooksProviderApiController.cs" company="Lakstins Family, LLC">
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
// <change date="5/5/2015" author="Brian A. Lakstins" description="Initial creation">
// <change date="6/8/2015" author="Brian A. Lakstins" description="Added explicitly saving cart.">
// <change date="10/21/2018" author="Brian A. Lakstins" description="Base on new object and use base method for creating response.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4.PresentationLayer
{

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Xml;
    using System.Xml.Serialization;
    using MaxFactry.Core;
    using MaxFactry.Provider.QuickbooksProvider;
    using MaxFactry.Provider.QuickbooksProvider.PresentationLayer;
    using MaxFactry.General.AspNet.PresentationLayer;
    using MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer;

    /// <summary>
    /// http://wiki.consolibyte.com/wiki/doku.php/quickbooks_web_connector
    /// </summary>
    [System.Web.Http.AllowAnonymous]
    public class MaxQuickbooksProviderApiController : MaxBaseApiController
    {
        private const string ProviderName = "Test";

        [HttpGet]
        [HttpPost]
        [ActionName("index")]
        public async Task<HttpResponseMessage> Index()
        {
            Guid loId = Guid.NewGuid();
            //MaxExceptionLibrary.LogException("Quickbooks API Start", new MaxException("Request " + loId.ToString()));
            string lsR = string.Empty;
            object loReturn = null;
            MaxFactry.General.BusinessLayer.MaxEmailEntity loEmail = null;
            string lsLog = DateTime.Now.ToString() + "\r\n";
            string lsProcess = string.Empty;
            bool lbSendEmail = false;
            try
            {

                Stream loContent = await this.Request.Content.ReadAsStreamAsync();
                XmlDocument loDocument = new XmlDocument();
                loDocument.Load(loContent);
                //loEmail = MaxFactry.Module.Core.BusinessLayer.MaxEmailEntity.Create();
                //loEmail.Subject = "Quickbooks API Process " + loId.ToString();
                //loEmail.ToAddressList = "brianlakstins@gmail.com";
                //loEmail.Content = "Request\r\n" + loDocument.InnerXml;
                //loEmail.RelationType = "Quickbooks";
                //loEmail.Send();
                lsLog += "Request\r\n" + loDocument.InnerXml;

                XmlNode loBody = loDocument.GetElementsByTagName("Body", "http://schemas.xmlsoap.org/soap/envelope/")[0];
                lsProcess = loBody.FirstChild.Name;
                if (loBody.FirstChild.Name == "clientVersion")
                {
                    string lsVersion = loDocument.GetElementsByTagName("strVersion", "http://developer.intuit.com/")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "clientVersion(" + lsVersion + ")";
                    loReturn = new MaxSoapResponseClientVersion();
                    ((MaxSoapResponseClientVersion)loReturn).Result = this.ClientVersion(lsVersion);
                }
                else if (loBody.FirstChild.Name == "serverVersion")
                {
                    //string lsTicket = loDocument.GetElementsByTagName("ticket")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "serverVersion()";
                    loReturn = new MaxSoapResponseServerVersion();
                    ((MaxSoapResponseServerVersion)loReturn).Result = this.GetServerVersion(string.Empty);
                }
                else if (loBody.FirstChild.Name == "authenticate")
                {
                    string lsUserName = loDocument.GetElementsByTagName("strUserName", "http://developer.intuit.com/")[0].InnerText;
                    string lsPassword = loDocument.GetElementsByTagName("strPassword", "http://developer.intuit.com/")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "authenticate(" + lsUserName + ", " + lsPassword + ")";
                    loReturn = new MaxSoapResponseAuthenticate();
                    ((MaxSoapResponseAuthenticate)loReturn).Result = this.Authenticate(lsUserName, lsPassword);
                }
                else if (loBody.FirstChild.Name == "closeConnection")
                {
                    string lsTicket = loDocument.GetElementsByTagName("ticket", "http://developer.intuit.com/")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "closeConnection(" + lsTicket + ")";
                    loReturn = new MaxSoapResponseCloseConnection();
                    ((MaxSoapResponseCloseConnection)loReturn).Result = this.CloseConnection(lsTicket);
                    lbSendEmail = true;
                }
                else if (loBody.FirstChild.Name == "connectionError")
                {
                    string lsTicket = loDocument.GetElementsByTagName("ticket", "http://developer.intuit.com/")[0].InnerText;
                    string lsHresult = loDocument.GetElementsByTagName("hresult", "http://developer.intuit.com/")[0].InnerText;
                    string lsMessage = loDocument.GetElementsByTagName("message", "http://developer.intuit.com/")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "connectionError(" + lsTicket + ", " + lsHresult + ", " + lsMessage + ")";
                    loReturn = new MaxSoapResponseConnectionError();
                    ((MaxSoapResponseConnectionError)loReturn).Result = this.ConnectionError(lsTicket, lsHresult, lsMessage);
                    lbSendEmail = true;
                }
                else if (loBody.FirstChild.Name == "getLastError")
                {
                    string lsTicket = loDocument.GetElementsByTagName("ticket", "http://developer.intuit.com/")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "connectionError(" + lsTicket + ")";
                    loReturn = new MaxSoapResponseGetLastError();
                    ((MaxSoapResponseGetLastError)loReturn).Result = this.GetLastError(lsTicket);
                    lbSendEmail = true;
                }
                else if (loBody.FirstChild.Name == "sendRequestXML")
                {
                    string lsTicket = loDocument.GetElementsByTagName("ticket", "http://developer.intuit.com/")[0].InnerText;
                    string lsResponse = loDocument.GetElementsByTagName("strHCPResponse", "http://developer.intuit.com/")[0].InnerText;
                    string lsCompany = loDocument.GetElementsByTagName("strCompanyFileName", "http://developer.intuit.com/")[0].InnerText;
                    string lsCountry = loDocument.GetElementsByTagName("qbXMLCountry", "http://developer.intuit.com/")[0].InnerText;
                    int lnMajor = MaxConvertLibrary.ConvertToInt(typeof(object), loDocument.GetElementsByTagName("qbXMLMajorVers")[0].InnerText);
                    int lnMinor = MaxConvertLibrary.ConvertToInt(typeof(object), loDocument.GetElementsByTagName("qbXMLMinorVers")[0].InnerText);
                    lsLog += "\r\n\r\nCommand\r\n" + "sendRequestXML(" + lsTicket + ", *Response*, " + lsCompany + ", " + lsCountry + ", " + lnMajor.ToString() + ", " + lnMinor.ToString() + ")";
                    loReturn = new MaxSoapResponseSendRequestXML();
                    ((MaxSoapResponseSendRequestXML)loReturn).Result = this.SendRequestXML(lsTicket, lsResponse, lsCompany, lsCountry, lnMajor, lnMinor);
                    lbSendEmail = true;
                }
                else if (loBody.FirstChild.Name == "receiveResponseXML")
                {
                    string lsTicket = loDocument.GetElementsByTagName("ticket", "http://developer.intuit.com/")[0].InnerText;
                    string lsResponse = loDocument.GetElementsByTagName("response", "http://developer.intuit.com/")[0].InnerText;
                    string lsResult = loDocument.GetElementsByTagName("hresult", "http://developer.intuit.com/")[0].InnerText;
                    string lsMessage = loDocument.GetElementsByTagName("message", "http://developer.intuit.com/")[0].InnerText;
                    lsLog += "\r\n\r\nCommand\r\n" + "receiveResponseXML(" + lsTicket + ", *Response*, " + lsResult + ", " + lsMessage + ")";
                    loReturn = new MaxSoapResponseReceiveResponseXML();
                    ((MaxSoapResponseReceiveResponseXML)loReturn).Result = this.ReceiveResponseXML(lsTicket, lsResponse, lsResult, lsMessage);
                    lbSendEmail = true;
                }                

                if (null != loReturn && loReturn is MaxSoapResponse)
                {
                    MaxSoapEnvelope loEnvelope = new MaxSoapEnvelope(loReturn as MaxSoapResponse);
                    lsR = loEnvelope.ToXmlString();
                }
                else
                {
                    MaxLogLibrary.Log(new MaxLogEntryStructure(MaxEnumGroup.LogError, "Quickbooks API", new MaxException("Return is missing or not proper object")));
                    lbSendEmail = true;
                }

                lsLog += "\r\n\r\nReturn\r\n" + lsR;
            }
            catch (Exception loE)
            {
                MaxLogLibrary.Log(new MaxLogEntryStructure(MaxEnumGroup.LogError, "Quickbooks API Error", loE));
                lsLog += "\r\n\r\nError\r\n" + loE.ToString();
                lbSendEmail = true;
            }

            if (lbSendEmail)
            {
                loEmail = MaxFactry.General.BusinessLayer.MaxEmailEntity.Create();
                loEmail.Subject = "Quickbooks API " + lsProcess + "(" + loId.ToString() + ")";
                loEmail.ToAddressList.Add("brianlakstins@gmail.com");
                loEmail.Content = lsLog;
                loEmail.RelationType = "Quickbooks";
                loEmail.Send();
            }

            HttpResponseMessage loR = this.GetResponseMessage(lsR);
            loR.Content.Headers.Remove("Content-Type");
            loR.Content.Headers.Add("Content-Type", "text/xml");
            return loR;
        }

        [HttpGet]
        [ActionName("authenticate")]
        public string[] Authenticate(string strUserName, string strPassword)
        {
            return MaxQuickbooksWebConnectProviderLibrary.Authenticate(ProviderName, strUserName, strPassword);
        }

        [HttpPost]
        [ActionName("authenticate")]
        public string[] Authenticate(FormDataCollection loFormData)
        {
            string lsUserName = loFormData.Get("strUserName");
            string lsPassword = loFormData.Get("strPassword");

            return MaxQuickbooksWebConnectProviderLibrary.Authenticate(ProviderName, lsUserName, lsPassword);
        }

        [HttpGet]
        [ActionName("clientVersion")]
        public string ClientVersion(string strVersion)
        {
            return MaxQuickbooksWebConnectProviderLibrary.GetClientVersion(ProviderName, strVersion);
        }

        [HttpPost]
        [ActionName("clientVersion")]
        public string ClientVersion(FormDataCollection loFormData)
        {
            string lsVersion = loFormData.Get("strVersion");
            return MaxQuickbooksWebConnectProviderLibrary.GetClientVersion(ProviderName, lsVersion);
        }

        [HttpGet]
        [ActionName("closeConnection")]
        public string CloseConnection(string ticket)
        {
            return MaxQuickbooksWebConnectProviderLibrary.CloseConnection(ProviderName, ticket);
        }

        [HttpPost]
        [ActionName("closeConnection")]
        public string CloseConnection(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("ticket");
            return MaxQuickbooksWebConnectProviderLibrary.CloseConnection(ProviderName, lsTicket);
        }

        [HttpGet]
        [ActionName("connectionError")]
        public string ConnectionError(string ticket, string hresult, string message)
        {
            return MaxQuickbooksWebConnectProviderLibrary.ConnectionError(ProviderName, ticket, hresult, message);
        }

        [HttpPost]
        [ActionName("connectionError")]
        public string ConnectionError(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("ticket");
            string lsResult = loFormData.Get("hresult");
            string lsMessage = loFormData.Get("message");
            return MaxQuickbooksWebConnectProviderLibrary.ConnectionError(ProviderName, lsTicket, lsResult, lsMessage);
        }
        
        [HttpGet]
        [ActionName("getInteractiveURL")]
        public string GetInteractiveURL(string wcTicket, string sessionID)
        {
            return MaxQuickbooksWebConnectProviderLibrary.GetInteractiveURL(ProviderName, wcTicket, sessionID);
        }

        [HttpPost]
        [ActionName("getInteractiveURL")]
        public string GetInteractiveURL(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("wcTicket");
            string lsSessionId = loFormData.Get("sessionID");
            return MaxQuickbooksWebConnectProviderLibrary.GetInteractiveURL(ProviderName, lsTicket, lsSessionId);
        }

        [HttpGet]
        [ActionName("getLastError")]
        public string GetLastError(string ticket)
        {
            return MaxQuickbooksWebConnectProviderLibrary.GetLastError(ProviderName, ticket);
        }

        [HttpPost]
        [ActionName("getLastError")]
        public string GetLastError(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("ticket");
            return MaxQuickbooksWebConnectProviderLibrary.GetLastError(ProviderName, lsTicket);
        }

        [HttpGet]
        [ActionName("getServerVersion")]
        public string GetServerVersion(string ticket)
        {
            return MaxQuickbooksWebConnectProviderLibrary.GetServerVersion(ProviderName, ticket);
        }

        [HttpPost]
        [ActionName("getServerVersion")]
        public string GetServerVersion(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("ticket");
            return MaxQuickbooksWebConnectProviderLibrary.GetServerVersion(ProviderName, lsTicket);
        }

        [HttpGet]
        [ActionName("interactiveDone")]
        public string InteractiveDone(string wcTicket)
        {
            return MaxQuickbooksWebConnectProviderLibrary.InteractiveDone(ProviderName, wcTicket);
        }

        [HttpPost]
        [ActionName("interactiveDone")]
        public string InteractiveDone(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("wcTicket");
            return MaxQuickbooksWebConnectProviderLibrary.InteractiveDone(ProviderName, lsTicket);
        }

        [HttpGet]
        [ActionName("interactiveRejected")]
        public string InteractiveRejected(string wcTicket, string reason)
        {
            return MaxQuickbooksWebConnectProviderLibrary.InteractiveRejected(ProviderName, wcTicket, reason);
        }

        [HttpPost]
        [ActionName("interactiveRejected")]
        public string InteractiveRejected(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("wcTicket");
            string lsReason = loFormData.Get("reason");
            return MaxQuickbooksWebConnectProviderLibrary.InteractiveRejected(ProviderName, lsTicket, lsReason);
        }

        [HttpGet]
        [ActionName("receiveResponseXML")]
        public int ReceiveResponseXML(string ticket, string response, string hresult, string message)
        {
            return MaxQuickbooksWebConnectProviderLibrary.ReceiveResponseXML(ProviderName, ticket, response, hresult, message);
        }

        [HttpPost]
        [ActionName("receiveResponseXML")]
        public int ReceiveResponseXML(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("ticket");
            string lsResponse = loFormData.Get("response");
            string lsResult = loFormData.Get("hresult");
            string lsMessage = loFormData.Get("message");
            return MaxQuickbooksWebConnectProviderLibrary.ReceiveResponseXML(ProviderName, lsTicket, lsResponse, lsResult, lsMessage);
        }

        [HttpGet]
        [ActionName("sendRequestXML")]
        public string SendRequestXML(string ticket, string strHCPResponse, string strCompanyFileName, string qbXMLCountry, int qbXMLMajorVers, int qbXMLMinorVers)
        {
            return MaxQuickbooksWebConnectProviderLibrary.SendRequestXML(ProviderName, ticket, strHCPResponse, strCompanyFileName, qbXMLCountry, qbXMLMajorVers, qbXMLMinorVers);
        }

        [HttpPost]
        [ActionName("sendRequestXML")]
        public string SendRequestXML(FormDataCollection loFormData)
        {
            string lsTicket = loFormData.Get("ticket");
            string lsResponse = loFormData.Get("strHCPResponse");
            string lsCompanyFileName = loFormData.Get("strCompanyFileName");
            string lsCountry = loFormData.Get("qbXMLCountry");
            int lnMajorVers = MaxConvertLibrary.ConvertToInt(typeof(object), loFormData.Get("qbXMLMajorVers"));
            int lnMinorVers = MaxConvertLibrary.ConvertToInt(typeof(object), loFormData.Get("qbXMLMinorVers"));
            return MaxQuickbooksWebConnectProviderLibrary.SendRequestXML(ProviderName, lsTicket, lsResponse, lsCompanyFileName, lsCountry, lnMajorVers, lnMinorVers);
        }

    }
    public class MaxJsonContent
    {
        public MaxJsonContent(string lsContent)
        {
            Content = lsContent;
        }

        public string Content
        {
            get;
            set;
        }
    }
}