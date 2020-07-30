using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Model
{
  public  class CardBindInfo
    {
        /// <summary>
        /// 商户名
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 商户密码
        /// </summary>
        public string MerchantPsd { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 商户登录token
        /// </summary>
        public string MerchantToken { get; set; }
    }
}
