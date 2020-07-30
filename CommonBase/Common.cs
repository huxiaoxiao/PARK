using CommonBase.Log;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase
{
   public class Common
    {
        private static LogOther log = new LogOther();
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        /// <summary>
        /// 请求的超时时间(秒), 默认2秒
        /// </summary>
        private const int TIME_OUT = 2000;
        /// <summary>
        /// 发送POST请求参数拼接。
        /// </summary>
        /// <param name="Path">访问地址</param>
        /// <param name="MethodParams">接口参数</param>
        /// <returns></returns>
        public static string SendPostRequestParams(string Path, Dictionary<string, string> MethodParams)
        {
            // 拼接参数
            StringBuilder ParamData = new StringBuilder();
            ParamData.Append("{");
            if (MethodParams != null && MethodParams.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in MethodParams)
                {
                    string value = kvp.Value == null ? "" : kvp.Value;

                    if (value.Contains("[") || value.Contains("{"))
                    {
                        // 说明值是集合数据（[{}]||{}），不需要引号
                        ParamData.Append("  \"").Append(kvp.Key).Append("\":").Append(value).Append(",");
                    }
                    else
                    {
                        ParamData.Append("  \"").Append(kvp.Key).Append("\":\"").Append(value).Append("\",");
                    }
                }
                ParamData.Remove(ParamData.Length - 1, 1);
            }

            ParamData.Append("}");
            return ParamData.ToString();
        }
        /// <summary>
        /// 创建POST方式的HTTP请求 
        /// </summary>
        /// <param name="PostUrl"></param>
        /// <param name="ParamData"></param>
        /// <param name="DataEncode"></param>
        /// <param name="timeout"></param>
        /// <param name="ContentType">"application/x-www-form-urlencoded" or "application/json"</param>
        /// <returns></returns>
        public static string CreatePostHttpRequest(string PostUrl, string ParamData, Encoding DataEncode, int? timeout, string AuthorizationValue, string ContentType = null, Encoding ResponeEncode = null)
        {
            //垃圾回收，回收没有正常关闭的http连接
            GC.Collect();

            string ret = string.Empty;

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                byte[] byteArray = DataEncode.GetBytes(ParamData); //转化
                request = (HttpWebRequest)WebRequest.Create(new Uri(PostUrl));
                request.Method = "POST";
                request.UserAgent = DefaultUserAgent;
                request.Proxy = null;
                if (!string.IsNullOrEmpty(AuthorizationValue))
                {
                    request.Headers.Add("Authorization", AuthorizationValue);
                    request.Headers.Add("Access-Token", AuthorizationValue);
                }

                if (ContentType == null || "".Equals(ContentType.Trim()))
                {
                    ContentType = "application/x-www-form-urlencoded"; // default type
                }
                request.ContentType = ContentType;

                if (timeout.HasValue)
                {
                    request.Timeout = timeout.Value; // 秒
                }
                request.ContentLength = byteArray.Length;
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                    response = (HttpWebResponse)request.GetResponse();

                    Encoding encode = DataEncode;
                    if (ResponeEncode != null)
                    {
                        encode = ResponeEncode;
                    }
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), encode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                        response.Close();
                        response = null;
                        newStream.Close();
                    }
                }
            }
            catch (System.Threading.ThreadAbortException e)
            {
                log.Error("HttpService Thread - caught ThreadAbortException - resetting.");
                log.Error("Exception message: " + e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                log.Error("HttpService " + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    log.Error("HttpService " + "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    log.Error("HttpService " + "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new WebServiceException(e.ToString());
            }
            catch (System.Exception ex)
            {
                log.Error("HttpService " + ex.ToString() + ex.Message + ",StackTrace:" + ex.StackTrace);
                throw new WebServiceException(ex.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
            }
            return ret;
        }
        /// <summary>
        /// POST传数据, 传的ContentType="application/json"
        /// </summary>
        /// <param name="PostUrl"></param>
        /// <param name="ParamData"></param>
        /// <returns></returns>
        public static string CreatePostHttpRequestJson(string PostUrl, string ParamData, string AuthorizationValue)
        {
            return CreatePostHttpRequest(PostUrl, ParamData, Encoding.UTF8, TIME_OUT,  AuthorizationValue, "application/json");
        }
       
    }
}
