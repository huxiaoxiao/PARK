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
    /// LicensePlateControl.xaml 的交互逻辑
    /// </summary>
    public partial class LicensePlateControl : UserControl
    {
        public bool isDelete=false;
        public int index = 0;
        LicensePlateKeyBoard lpbWin;
        private TextBox tb;
     
        public LicensePlateControl()
        {
            InitializeComponent();
            lpbWin = new LicensePlateKeyBoard(tb);
            lpbWin.ChangeType(4, tb);
            lpbWin.Show();
        }
        /// <summary>
        /// 关闭键盘
        /// </summary>
        public void ColseLicensePlate() 
        {
            lpbWin.Close();
        }
        /// <summary>
        ///  省输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardNumTb__PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ErrMsg.Visibility = Visibility.Collapsed;
            tb = (TextBox)sender;
            lpbWin.ChangeType(0, tb);
        }
        
        /// <summary>
        /// 省输入框按下操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardNumTb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tb.Focus();
            tb = (TextBox)sender;
            lpbWin.ChangeType(0, tb);
        }
        /// <summary>
        ///  数字字母输入框禁用数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardNumTb1__PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ErrMsg.Visibility = Visibility.Collapsed;
            tb = (TextBox)sender;
            lpbWin.ChangeType(1, tb);
        }
        /// <summary>
        /// 禁用数字输入框按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardNumTb1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tb = (TextBox)sender;
            lpbWin.ChangeType(1, tb);
            tb.Focus();
        }
        /// <summary>
        /// 非禁用数字框按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardNumTb2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tb = (TextBox)sender;
            lpbWin.ChangeType(2, tb);
            tb.Focus();
        }
        /// <summary>
        /// 数字字母输入框不禁用数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardNumTb2__PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ErrMsg.Visibility = Visibility.Collapsed;
            tb = (TextBox)sender;
            lpbWin.ChangeType(2, tb);
        }
       
        /// <summary>
        /// 添加新能源按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ErrMsg.Visibility = Visibility.Collapsed;
            this.DelNewBtn.Visibility = Visibility.Visible;
            this.tb9.Visibility = Visibility.Visible;
            tb9.Focus();
            this.AddNewBtn.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 删除新能源按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelNewBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ErrMsg.Visibility = Visibility.Collapsed;
            this.DelNewBtn.Visibility = Visibility.Hidden;
            this.tb9.Visibility = Visibility.Hidden;
            this.AddNewBtn.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 获得车牌号
        /// </summary>
        /// <returns></returns>
        public string GetLicensePlate() 
        {
            if (!string.IsNullOrEmpty( tb1.Text)  && !string.IsNullOrEmpty(tb2.Text) && !string.IsNullOrEmpty(tb4.Text) && !string.IsNullOrEmpty(tb5.Text) &&  !string.IsNullOrEmpty(tb6.Text) && !string.IsNullOrEmpty(tb7.Text) && !string.IsNullOrEmpty(tb8.Text))
            {
                if (this.tb9.IsVisible == true)
                {
                    if (!string.IsNullOrEmpty(tb9.Text))
                    {
                        return tb1.Text + tb2.Text + tb4.Text + tb5.Text + tb6.Text + tb7.Text + tb8.Text + tb9.Text;
                    }
                    return "";
                }
                else
                { 
                    return tb1.Text + tb2.Text + tb4.Text + tb5.Text + tb6.Text + tb7.Text + tb8.Text;
                }
            }
            else
            {
                return "";
            }
        }

        #region 输入框输入内容改变
        private void tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb2.Focus();
        }

        private void tb2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb2.Text))
                tb1.Focus();
            else
            tb4.Focus();
        }

        private void tb4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb4.Text))
                tb2.Focus();
            else
                tb5.Focus();
        }

        private void tb5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb5.Text))
                tb4.Focus();
            else
                tb6.Focus();
        }

        private void tb6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb6.Text))
                tb5.Focus();
            else
                tb7.Focus();
        }

        private void tb7_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb7.Text))
                tb6.Focus();
            else
                tb8.Focus();
        }
        private void tb8_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb8.Text))
                tb7.Focus();
            else
                tb8.ReleaseMouseCapture();
        }
        #endregion

        /// <summary>
        /// 键盘删除按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelLicensePlateBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDelete = true;
            lpbWin.Close();
            this.Visibility = Visibility.Collapsed;
        }
       
    }
}
