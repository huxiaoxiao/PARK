using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace PARK.Pages
{
    /// <summary>
    /// IndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class IndexPage : Page
    {
        
        public IndexPage()
        {
            InitializeComponent();
            //OpenKeyBoard();
            //TbLoginName.Focus();
        }
        [DllImport("USER32.DLL")]
       public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 启动外部键盘
        /// </summary>
        private void OpenKeyBoard() 
        {
           string systemKeyBoardPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\SystemKeyBoard.EXE";
            var p = System.Diagnostics.Process.Start(systemKeyBoardPath);
            if (p.Start())
                SetForegroundWindow(p.MainWindowHandle);
        }
        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = (Dictionary<string, string>)e.ExtraData;
            TbLoginName.Text = dic["LoginNameTb"];
        }
      /// <summary>
      /// 用户名框按下
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void LoginTb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TbErrorMsg.Text = "";
            TextBox tb = (TextBox)sender;
            CloseKeyBoard();
            OpenKeyBoard();
            tb.Focus();
        }
        /// <summary>
        /// 密码框按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginPTb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TbErrorMsg.Text = "";
            PasswordBox tb = (PasswordBox)sender;
            CloseKeyBoard();
            OpenKeyBoard();
            tb.Focus();
       }
        /// <summary>
        /// 登录按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           
            bool loginSuccess = false;
            string loginNmae = TbLoginName.Text.Trim();
            string loginPsd = TbLoginPsd.Password.Trim();
            string loginToken = "";
            if (loginNmae == "" || loginPsd == "")
            { 
                TbErrorMsg.Text = "账号、密码不可为空，请重新输入";
                return;
            }
            #region 请求后台获取登录结果

            if (loginNmae == "001") loginSuccess = true;
            else
            {
                BackEnd.ParkBackEnd parkBackEnd = new BackEnd.ParkBackEnd();
                loginSuccess = parkBackEnd.isVerifyLoginAccount(loginNmae, loginPsd);
                loginToken = parkBackEnd.loginToken;
            }
            #endregion
            if (loginSuccess)
            {
                CloseKeyBoard();
                LoginSuccess(loginToken);
            }
            else 
            {
                TbErrorMsg.Text = "账号或密码错误！登录失败";
            }
        }
        /// <summary>
        /// 账户密码成功跳转车牌绑定页
        /// </summary>
        private void LoginSuccess(string loginToken) 
        {
            this.PageView.Visibility = Visibility.Hidden;
            this.FrameView.Visibility = Visibility.Visible;
            this.FramePage.Visibility = Visibility.Visible;
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("LoginNameTb", TbLoginName.Text);
            dic.Add("LoginToken", loginToken);
            BindCardNoPage bindCardNoPage = new BindCardNoPage();
            this.NavigationService.Navigate(bindCardNoPage, dic);
            this.NavigationService.LoadCompleted += bindCardNoPage.NavigationService_LoadCompleted;
            this.FramePage.Content = bindCardNoPage;
        }
        /// <summary>
        /// 关闭外部调用键盘
        /// </summary>
        private  void CloseKeyBoard()　　　　
        {
            Process[] myProgress;
            myProgress = Process.GetProcesses();　　　　　　　　　　
            foreach (Process p in myProgress)　　　　　　　　　　　
            {
                if (p.ProcessName == "SystemKeyBoard")　　　　　　　　　
                {
                    p.Kill();
                    return;
                }
            }
        }

       
       
    }
}
