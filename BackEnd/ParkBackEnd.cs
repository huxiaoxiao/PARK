using BackEnd.Model;
using CommonBase.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    /// <summary>
    /// 后台数据交互api接口
    /// </summary>
    public class ParkBackEnd : IParkBackEnd
    {
        private LogBackEnd log = new LogBackEnd();
        /// <summary>
        /// 取得Configuration对象config
        /// </summary>
        /// <returns></returns>
        private static Configuration GetConfig()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory+ "/Config/BackEnd.config";
            return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
        }
        public static string ReadBaseURL()
        {
            return GetConfig().AppSettings.Settings["base_url"].Value;
        }
        #region 调用后端交互接口名
        /// <summary>
        /// 商户绑定车牌(添加白名单)
        /// </summary>
        private static readonly string _AddCardNoBind = "/api/zizhuji/addWhiteList";
        /// <summary>
        /// 商户批量绑定车牌(添加白名单)
        /// </summary>
        private static readonly string _AddCardNosBind = "/api/zizhuji/addWhiteListMore";
        /// <summary>
        /// 检测车牌是否已绑定过(查询单个车牌是否是白名单)
        /// </summary>
        private static readonly string _CheckCardNoBind = "/api/zizhuji/searchWhiteOne";
        /// <summary>
        /// 检测商户账户密码是否正确(自助机登录)
        /// </summary>
        private static readonly string _VerifyLogin = "/api/zizhuji/admin/login";

        public string loginToken="";
        #endregion
        public bool AddCardNoBind(CardBindInfo cardBindInfo)
        {
            Dictionary<string, string> methodParams = new Dictionary<string, string>();
            if (cardBindInfo != null)
            {
                methodParams.Add("plateNo", cardBindInfo.CardNo);
                loginToken = cardBindInfo.MerchantToken;
                return AddCardNoBind_Api(methodParams);
            }
            return false;
        }
        /// <summary>
        /// 检测车牌是否绑定过
        /// </summary>
        /// <param name="cardBindInfo"></param>
        /// <returns></returns>
        public bool isCheckCardNoBind(CardBindInfo cardBindInfo)
        {
            Dictionary<string, string> methodParams = new Dictionary<string, string>();
            if (cardBindInfo.CardNo != null)
            {
                methodParams.Add("plateNo", cardBindInfo.CardNo);
                loginToken = cardBindInfo.MerchantToken;
                return isCheckCardNoBind_Api(methodParams);
            }
            return false;
        }

        public bool isVerifyLoginAccount(string loginName, string loginPsd)
        {
            Dictionary<string, string> methodParams = new Dictionary<string, string>();
            if (loginName != null&& loginPsd!=null)
            {
                methodParams.Add("username", loginName);
                methodParams.Add("password", loginPsd);
                return isVerifyLoginAccount_Api(methodParams);
            }
            return false;
        }
        public string AddCardNosBind(List<string> cardNoList, string token)
        {
            Dictionary<string, string> methodParams = new Dictionary<string, string>();
            if (cardNoList != null)
            {
                List<object> objList = new List<object>();
                foreach (var item in cardNoList) 
                {
                    objList.Add(new  { plateNo = item });
                }
                methodParams.Add("list",JsonConvert.SerializeObject(objList));
                loginToken = token;
                return AddCardNosBind_Api(methodParams);
            }
            return "";
        }
        /// <summary>
        /// 调用接口响应的结果。
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        private bool ResponseResult(JObject jo)
        {
            if (jo == null)
            {
                log.Info("参数jo为null。");
                return false;
            }

            // 解析返回值
            string iResult = jo.Property("message").Value.ToString();
            if ("SUCCESS".Equals(iResult.ToUpper()))
            {
                // 成功
                return true;
            }
            else
            {
                // 获取上层函数信息调用 GetFrame(1)
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(3);
                MethodBase methodBase = stackFrame.GetMethod();

                // 失败
                string Msg = jo.Property("message").Value.ToString();
                log.Info(methodBase.Name + "响应失败，失败信息为:" + Msg);
                return false;
            }
        }
        
        private string AddCardNosBind_Api(Dictionary<string, string> methodParams)
        {
            string Path = ReadBaseURL() + _AddCardNosBind;
            log.Info("【AddCardNosBind_Api】获得传入参数methodParams为：" + JsonConvert.SerializeObject(methodParams));
            string ParamData = CommonBase.Common.SendPostRequestParams(Path, methodParams);
            log.Debug("访问地址：【" + Path + "】,拼接后接口参数：" + ParamData.ToString());
            string postResult = CommonBase.Common.CreatePostHttpRequestJson(Path, ParamData, loginToken);
            log.Debug("调用地址【" + Path + "】，返回结果信息：" + postResult);

            // 获取响应的数据
            string ResposeData = postResult;
            if (ResposeData == null || "".Equals(ResposeData.Trim()))
            {
                log.Info("方法【AddCardNosBind_Api】响应的数据为空");
                return "";
            }
            else
            {
                // 解析数据内容json格式, 解析返回值
                JObject joResponse = JObject.Parse(ResposeData);
                if (this.ResponseResult(joResponse))
                {
                    if (joResponse.Property("message").Value.ToString() == "SUCCESS")
                    {
                        JObject resdata = JObject.Parse(joResponse.Property("data").Value.ToString());
                        string cardNos= resdata.Property("returnList").Value.ToString();
                        string startTime = resdata.Property("startTime").Value.ToString();
                        string endTime = resdata.Property("endTime").Value.ToString();
                        if (cardNos == "[]")
                            return "SUCCESS"+","+ startTime+","+ endTime;
                        else
                            return cardNos;
                    }
                }
            }
            return "";
        }
        private bool AddCardNoBind_Api(Dictionary<string, string> methodParams) 
        {
            string Path = ReadBaseURL() + _AddCardNoBind;
            log.Info("【AddCardNoBind_Api】获得传入参数methodParams为：" + JsonConvert.SerializeObject(methodParams));
            string ParamData = CommonBase.Common.SendPostRequestParams(Path, methodParams);
            log.Debug("访问地址：【" + Path + "】,拼接后接口参数：" + ParamData.ToString());
            string postResult = CommonBase.Common.CreatePostHttpRequestJson(Path, ParamData, loginToken);
            log.Debug("调用地址【" + Path + "】，返回结果信息：" + postResult);
            
            // 获取响应的数据
            string ResposeData = postResult;
            if (ResposeData == null || "".Equals(ResposeData.Trim()))
            {
                log.Info("方法【AddCardNoBind_Api】响应的数据为空");
                return false;
            }
            else
            {
                // 解析数据内容json格式, 解析返回值
                JObject joResponse = JObject.Parse(ResposeData);
                if (this.ResponseResult(joResponse))
                {
                    if (joResponse.Property("message").Value.ToString() == "SUCCESS")
                    {
                        JObject resdata = JObject.Parse(joResponse.Property("data").Value.ToString());
                        return true; 
                    }
                }
            }
            return false;
        }
        private bool isCheckCardNoBind_Api(Dictionary<string, string> methodParams)
        {
            string Path = ReadBaseURL() + _CheckCardNoBind;
            log.Info("【isCheckCardNoBind_Api】获得传入参数methodParams为：" + JsonConvert.SerializeObject(methodParams));
            string ParamData = CommonBase.Common.SendPostRequestParams(Path, methodParams);
            log.Debug("访问地址：【" + Path + "】,拼接后接口参数：" + ParamData.ToString());
            string postResult = CommonBase.Common.CreatePostHttpRequestJson(Path, ParamData, loginToken);
            log.Debug("调用地址【" + Path + "】，返回结果信息：" + postResult);

            // 获取响应的数据
            string ResposeData = postResult;
            if (ResposeData == null || "".Equals(ResposeData.Trim()))
            {
                log.Info("方法【isCheckCardNoBind_Api】响应的数据为空");
                return false;
            }
            else
            {
                // 解析数据内容json格式, 解析返回值
                JObject joResponse = JObject.Parse(ResposeData);
                if (this.ResponseResult(joResponse))
                {
                    if (joResponse.Property("message").Value.ToString() == "SUCCESS")
                        return true;
                }
            }
            return false;
        }

        private bool isVerifyLoginAccount_Api(Dictionary<string, string> methodParams)
        {
            string Path = ReadBaseURL() + _VerifyLogin;
            log.Info("【isVerifyLoginAccount_Api】获得传入参数methodParams为：" + JsonConvert.SerializeObject(methodParams));

            string ParamData = CommonBase.Common.SendPostRequestParams(Path, methodParams);
            log.Debug("访问地址：【" + Path + "】,拼接后接口参数：" + ParamData.ToString());
            string postResult = CommonBase.Common.CreatePostHttpRequestJson(Path, ParamData, loginToken);
            log.Debug("调用地址【" + Path + "】，返回结果信息：" + postResult);

            // 获取响应的数据
            string ResposeData = postResult;
            if (ResposeData == null || "".Equals(ResposeData.Trim()))
            {
                log.Info("方法【isVerifyLoginAccount_Api】响应的数据为空");
                return false;
            }
            else
            {
                // 解析数据内容json格式, 解析返回值
                JObject joResponse = JObject.Parse(ResposeData);
                if (this.ResponseResult(joResponse))
                {
                    if (joResponse.Property("message").Value.ToString() == "SUCCESS")
                    {
                        JObject resdata = JObject.Parse(joResponse.Property("data").Value.ToString());
                        loginToken = resdata.Property("token").Value.ToString();
                        return true;
                    }
                }
            }
            return false;
        }

    
    }
}
