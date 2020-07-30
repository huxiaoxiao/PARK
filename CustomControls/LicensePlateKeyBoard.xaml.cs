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
using System.Windows.Shapes;

namespace PARK.CustomControls
{
    /// <summary>
    /// 绑定车牌键盘 的交互逻辑
    /// </summary>
    public partial class LicensePlateKeyBoard : Window
    {
        public LicensePlateKeyBoard(TextBox tb)
        {
            InitializeComponent();
            CardKeyBoardWinow.Height = content.ActualHeight;
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 55;
            this.Top = 500;
            this.Topmost = true;
            ChangeType(0,  tb);
        }
        /// <summary>
        /// 更改键盘类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tb"></param>
        public void ChangeType(int type,TextBox tb)
        {

            if (type == 0)
            {
                CardKeyBoardA.Visibility = Visibility.Collapsed;
                CardKeyBoardP.BoardProvince.Visibility = Visibility.Visible;
                CardKeyBoardP.Visibility = Visibility.Visible;
                CardKeyBoardP.TB = tb;
            }
            else if (type == 1)
            {
                CardKeyBoardA.BoardType = "0";
                CardKeyBoardA.Visibility = Visibility.Visible;
                CardKeyBoardA.BoardAlphanumeric.Visibility = Visibility.Visible;
                CardKeyBoardA.TB = tb;
                CardKeyBoardP.Visibility = Visibility.Collapsed;
               
            }
            else if (type == 2)
            {
                CardKeyBoardA.BoardType = "1";
                CardKeyBoardA.Visibility = Visibility.Visible;
                CardKeyBoardA.TB = tb;
                CardKeyBoardP.Visibility = Visibility.Collapsed;
            }
            else
            {
                CardKeyBoardA.Visibility = Visibility.Collapsed;
                CardKeyBoardP.Visibility = Visibility.Collapsed;
            }
        }
    }
}
