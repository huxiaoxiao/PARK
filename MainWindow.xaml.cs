using PARK.Pages;
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

namespace PARK
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 主界面页
        /// </summary>
        public IndexPage indexPage;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            //this.Topmost = true;
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            // 加载首页面
            this.LoadIndex();
        }
        /// <summary>
        /// 加载页面
        /// </summary>
        public void LoadIndex()
        {
            this.indexPage = new IndexPage();
            this.indexFrame.Content = this.indexPage;
        }

        /// <summary>
        /// 返回首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void st_ReturnHome(object sender, EventArgs e)
        {
            this.indexFrame.Content = null;
            this.indexFrame.Visibility = Visibility.Visible;
            this.indexFrame.Content = this.indexPage;
           
        }

        /// <summary>
        /// 实现点击4个角，退出程序
        /// </summary>
        private bool[] p = new bool[4];
        private void MainWin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int tempPoint = 100;
            if (e.GetPosition(MainWin).X < tempPoint && e.GetPosition(MainWin).Y < tempPoint)
            {
                //top left
                p[0] = true;
            }
            else if (e.GetPosition(MainWin).X > (this.MainWin.Width - tempPoint) && e.GetPosition(MainWin).Y < tempPoint)
            {
                //top right
                p[1] = true;
            }
            else if (e.GetPosition(MainWin).X < tempPoint && e.GetPosition(MainWin).Y > (this.MainWin.Height - tempPoint))
            {
                //bottom left
                p[2] = true;
            }
            else if (e.GetPosition(MainWin).X > (this.MainWin.Width - tempPoint)
                && e.GetPosition(MainWin).Y > (this.MainWin.Height - tempPoint))
            {
                //bottom right
                p[3] = true;
            }
            else
            {
                p[0] = false;
                p[1] = false;
                p[2] = false;
                p[3] = false;
            }

            if (p[0] && p[1] && p[2] && p[3])
            {
               
               // _LOG.Info("调用退出框 ");
                ExitPasswordConfirm exitPasswordConfirm = new ExitPasswordConfirm();
                exitPasswordConfirm.ShowDialog();

                p[0] = false;
                p[1] = false;
                p[2] = false;
                p[3] = false;
               
            }
        }
    }
}
