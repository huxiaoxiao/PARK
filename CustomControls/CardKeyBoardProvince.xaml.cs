using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PARK.CustomControls
{
    /// <summary>
    /// 省键盘 的交互逻辑
    /// </summary>
    public partial class CardKeyBoardProvince : UserControl
    {
        private TextBox tb;

        
        public TextBox TB
        {
            get { return tb; }
            set
            {
                tb = value;

            }
        }
        CardKeyBoardButton cardKeyBoardButton = null;
        public  string CardKeyBoardProvinceText = "";
        string[] cardKeyBoardLine1 = { "京", "津", "渝", "沪", "冀", "晋", "辽", "吉", "黑", "苏" };
        string[] cardKeyBoardLine2 = { "浙", "皖", "闽", "赣", "鲁", "豫", "鄂", "湘", "粤", "琼" };
        string[] cardKeyBoardLine3 = { "川", "贵", "云", "陕", "甘", "青", "蒙", "贵", "宁", "新" };
        string[] cardKeyBoardLine4 = {  "藏", "使", "领", "警", "学", "港", "澳", "Del", "收起" };
        public CardKeyBoardProvince()
        {
            InitializeComponent();
            CardKeyBoardProvinceInit();
        }
        /// <summary>
        /// 车牌省键盘初始化
        /// </summary>
        private void CardKeyBoardProvinceInit() 
        {
            foreach (var item in cardKeyBoardLine1)
            {
                cardKeyBoardButton = new CardKeyBoardButton(item);
                cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnWord_PreviewMouseDown);
                
                this.w1.Children.Add(cardKeyBoardButton);
            }
            foreach (var item in cardKeyBoardLine2)
            {
                cardKeyBoardButton = new CardKeyBoardButton(item);
                cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnWord_PreviewMouseDown);
              
                this.w2.Children.Add(cardKeyBoardButton);
            }
            foreach (var item in cardKeyBoardLine3)
            {
                cardKeyBoardButton = new CardKeyBoardButton(item);
                cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnWord_PreviewMouseDown);
              
                this.w3.Children.Add(cardKeyBoardButton);
            }
            foreach (var item in cardKeyBoardLine4)//特殊按钮设置
            {
                if (item == "Del")
                {
                    cardKeyBoardButton = new CardKeyBoardButton(item);
                    cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnX_PreviewMouseDown);
                }
                else if (item == "收起")
                {
                    cardKeyBoardButton = new CardKeyBoardButton(item);
                    cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnPackUp_PreviewMouseDown);
                }
                else
                {
                    cardKeyBoardButton = new CardKeyBoardButton(item);
                    cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnWord_PreviewMouseDown);
                   
                }
                this.w4.Children.Add(cardKeyBoardButton);
            }
        }
       
        /// <summary>
        ///汉字键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardKeyBoardBtnWord_PreviewMouseDown(object sender, EventArgs e, CardKeyBoardButton cardKeyBoardButton)
        {
            if (tb!=null)
           
            tb.Text = cardKeyBoardButton.BtnValue;
        }
        
        /// <summary>
        ///删除键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardKeyBoardBtnX_PreviewMouseDown(object sender, EventArgs e, CardKeyBoardButton cardKeyBoardButton)
        {
            if (tb != null)
            {
                tb.Text = "";
            }
        }
        /// <summary>
        ///收起键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardKeyBoardBtnPackUp_PreviewMouseDown(object sender, EventArgs e, CardKeyBoardButton cardKeyBoardButton)
        {
            this.BoardProvince.Visibility = Visibility.Collapsed;
        }

       
    }
}
