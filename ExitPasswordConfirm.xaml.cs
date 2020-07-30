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

namespace PARK
{
    /// <summary>
    /// ExitPasswordConfirm.xaml 的交互逻辑
    /// </summary>
    public partial class ExitPasswordConfirm : Window
    {
        public ExitPasswordConfirm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
        }

        #region "键盘"
        /// <summary>
        /// 键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_Phone_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BorderKeyBord.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 键盘按下松开,给输入框赋值,显示点击的数字/字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            TextBlock text = (TextBlock)border.Child;
            if (e.ButtonState == MouseButtonState.Released)
            {
                this.PasswordText.Text += text.Text;
            }
            SetExitShow(this.PasswordText.Text.Length);
        }
        private void SetExitShow(int phoneLength)
        {
            if (this.PasswordText.Text.Length == 4)
            {
                if (this.PasswordText.Text == "1199")
                {
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("密码错误！");
                }

            }
            else
            {
                if (this.PasswordText.Text.Length > 4)
                {
                    MessageBox.Show("密码错误！");
                }

            }
        }

        /// <summary>
        /// 数字键盘上的删除键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_PreviewMouseDown_delete(object sender, MouseButtonEventArgs e)
        {
            if (this.PasswordText.Text.Length > 0)
            {
                this.PasswordText.Text = this.PasswordText.Text.Remove(this.PasswordText.Text.Length - 1);
            }
            SetExitShow(this.PasswordText.Text.Length);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_PreviewMouseDown_close(object sender, MouseButtonEventArgs e)
        {
            // this.BorderKeyBord.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
        }

        private void LeftBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BorderKeyBord.Visibility = Visibility.Hidden;
        }

        private void RightBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BorderKeyBord.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
