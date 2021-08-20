using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace SubmitSMS
{
    /// <summary>
    /// Summary description for SMS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SMS : System.Web.Services.WebService
    {
        [WebMethod]
        public ResultSMS SubmitMessage(string Msisdn, string Msg, string Header)
        {
            ResultSMS result = new ResultSMS();
            result.ResultCode = "001";
            result.ResultDesc = "Please try again later.";
            var data = new
            {
                FROM = Header,
                TO = "856" + Msisdn.Trim(),
                CHARGE = "8562052199062",
                SERVICE_CODE = "45140377001",
                CONTENT = Msg.Trim(),
                Auth = "$LtcMT@Bouly2020"
            };
            SendSMS send = new SendSMS();
            result = send.ProccssPOST(data);
            return result;
        }
        private class SendSMS
        {
            public ResultSMS ProccssPOST(Object DATA)
            {
                ResultSMS ReturnResult = new ResultSMS();
                try
                {
                    string url = string.Format("http://172.28.12.25:5007/MT");
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    var serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(DATA);
                    byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = byteArray.Length;
                    string JsonString = "";
                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse tResponse = request.GetResponse())
                        {
                            if (((HttpWebResponse)tResponse).StatusDescription == "OK")
                            {
                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        JsonString = tReader.ReadToEnd();
                                        JavaScriptSerializer oJson = new JavaScriptSerializer();
                                        ReturnResult = oJson.Deserialize<ResultSMS>(JsonString);
                                        if (ReturnResult.ResultCode == "405000000")
                                        {
                                            ReturnResult.ResultStatus = true;
                                            ReturnResult.ResultCode = "200";
                                            ReturnResult.ResultDesc = "Operation Succeeded.";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ReturnResult.ResultCode = "402";
                    ReturnResult.ResultDesc = ex.Message;
                    ReturnResult.ResultStatus = false;
                }
                return ReturnResult;
            }
        }
        public class ResultSMS
        {
            public string ResultCode { get; set; } 
            public string ResultDesc { get; set; }
            public bool ResultStatus { get; set; }
        }
    }
}
