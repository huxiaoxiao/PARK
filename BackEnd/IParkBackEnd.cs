using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    interface IParkBackEnd
    {
    /// <summary>
    /// 验证商户登录账户是否正确
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="loginPsd"></param>
    /// <returns></returns>
        bool isVerifyLoginAccount(string loginName, string loginPsd);
        /// <summary>
        /// 验证当前车牌是否已绑定
        /// </summary>
        /// <param name="cardBindInfo"></param>
        /// <returns></returns>
        bool isCheckCardNoBind(CardBindInfo cardBindInfo);
       
        /// <summary>
        /// 商户添加车牌绑定信息
        /// </summary>
        /// <param name="cardBindInfo"></param>
        /// <returns></returns>
        bool AddCardNoBind(CardBindInfo cardBindInfo);
        string AddCardNosBind(List<string> cardNoList, string token);
    }
}
