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
    /// 数字键盘 的交互逻辑
    /// </summary>
    public partial class CardKeyBoardAlphanumeric : UserControl
    {
        CardKeyBoardButton cardKeyBoardButton = null;
        public string CardKeyBoardProvinceText = "";
        string[] cardKeyBoardLine1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        string[] cardKeyBoardLine2 = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };
        string[] cardKeyBoardLine3 = { "A", "S", "D", "F", "G", "H", "J", "K", "L" };
        string[] cardKeyBoardLine4 = { "Z", "X", "C", "V", "B", "N", "M", "Del", "收起" };
        public CardKeyBoardAlphanumeric()
        {
            InitializeComponent();
            CardKeyBoardAlphanumericInit();
        }
       
        private TextBox tb;
        public TextBox TB
        {
            get { return tb; }
            set
            {
                tb = value;

            }
        }
        private string boardType="0";
        public string BoardType
        {
            get { return boardType; }
            set
            {
                boardType = value;
                SetW1();
            }
        }
       
      
        /// <summary>
        /// 车牌数字字母键盘初始化
        /// </summary>
        private void CardKeyBoardAlphanumericInit()
        {
            
            foreach (var item in cardKeyBoardLine2)
            {
                cardKeyBoardButton = new CardKeyBoardButton(item);
              
                if (item == "I" || item == "O") 
                {
                    cardKeyBoardButton.Btn_Enabled = "False";
                }
                else
                {
                    cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnWord_PreviewMouseDown);
                }
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
        /// 数字禁用非禁用切换
        /// </summary>
        private void SetW1() 
        {
            this.w1.Children.Clear();
            foreach (var item in cardKeyBoardLine1)
            {
                cardKeyBoardButton = new CardKeyBoardButton(item);

                if (boardType == "1")
                {
                    cardKeyBoardButton.Btn_Enabled = "False";
                }
                else
                {
                    cardKeyBoardButton.BtnPreviewKeyDown += new CardKeyBoardButton.EventHandler(this.CardKeyBoardBtnWord_PreviewMouseDown);
                }
                this.w1.Children.Add(cardKeyBoardButton);
            }
        }
        /// <summary>
        ///非功能键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardKeyBoardBtnWord_PreviewMouseDown(object sender, EventArgs e, CardKeyBoardButton cardKeyBoardButton)
        {
            if (tb != null)

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
            BoardAlphanumeric.Visibility = Visibility.Collapsed;
        }
    }
}
