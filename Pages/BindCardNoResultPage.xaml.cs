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
using System.Windows.Threading;

namespace PARK.Pages
{
    /// <summary>
    /// 车牌绑定成功 的交互逻辑
    /// </summary>
    public partial class BindCardNoSucceedPage : Page
    {
        private DispatcherTimer Timer_MouseMove;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        BackEnd.Model.CardBindInfo cardBindInfo = new BackEnd.Model.CardBindInfo();
        public BindCardNoSucceedPage(string startTime, string endTime)
        {
            InitializeComponent();
            TbTime.Text = startTime + "—"+ endTime;
            this.Timer_MouseMove = new DispatcherTimer();
            this.Timer_MouseMove.Tick += new EventHandler(Timer_MouseMove_Tick);
            this.Timer_MouseMove.Interval = new TimeSpan(0, 0, 5);
            this.Timer_MouseMove.Start();
        }
        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            dic = (Dictionary<string, string>)e.ExtraData;
            if (dic != null)
            {
                cardBindInfo.MerchantName = dic["LoginNameTb"];
                cardBindInfo.MerchantToken = dic["LoginToken"];
            }
        }
        /// <summary>
        /// 返回首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindFinishBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Timer_MouseMove.Stop();
            this.PageView.Visibility = Visibility.Hidden;
            this.FrameView.Visibility = Visibility.Visible;
            this.FramePage.Visibility = Visibility.Visible;

            IndexPage indexPage = new IndexPage();
            this.NavigationService.Navigate(indexPage, dic);
            this.NavigationService.LoadCompleted += indexPage.NavigationService_LoadCompleted;
            this.FramePage.Content = indexPage;
        }
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_MouseMove_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!MouseMonitorHelper.HaveUsedTo())
                {
                    MouseMonitorHelper.CheckCount++;
                    if (MouseMonitorHelper.CheckCount == 3)
                    {
                        this.Timer_MouseMove.Stop();
                        this.PageView.Visibility = Visibility.Hidden;
                        this.FrameView.Visibility = Visibility.Visible;
                        this.FramePage.Visibility = Visibility.Visible;

                        IndexPage indexPage = new IndexPage();
                        this.NavigationService.Navigate(indexPage, dic);
                        this.NavigationService.LoadCompleted += indexPage.NavigationService_LoadCompleted;
                        this.FramePage.Content = indexPage;
                    }
                }
                else MouseMonitorHelper.CheckCount = 0;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
