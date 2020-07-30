using PARK.CustomControls;
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
    /// 车牌绑定 的交互逻辑
    /// </summary>
    public partial class BindCardNoPage : Page
    {
        private DispatcherTimer Timer_MouseMove;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        BackEnd.Model.CardBindInfo cardBindInfo = new BackEnd.Model.CardBindInfo();
        private int LicensePlateCount = 1;
        private List<string> LicensePlateList = new List<string>();
        private List<LicensePlateControl> LicensePlateControlList = new List<LicensePlateControl>();
        public BindCardNoPage()
        {
            InitializeComponent();
            AddNewLicensePlateControl();
            this.Timer_MouseMove = new DispatcherTimer();
            this.Timer_MouseMove.Tick += new EventHandler(Timer_MouseMove_Tick);
            this.Timer_MouseMove.Interval = new TimeSpan(0, 0,30);
            this.Timer_MouseMove.Start();
        }

        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            dic= (Dictionary<string, string>)e.ExtraData;
            if (dic != null)
            {
                cardBindInfo.MerchantName = dic["LoginNameTb"];
                cardBindInfo.MerchantToken = dic["LoginToken"];
            }
        }
        /// <summary>
        /// 车牌绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardBindBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //获取当前添加的车牌
            LicensePlateControl lp = GetLastUnDeleteLicensePlateControl();
            if (lp != null)
            {
                bool isCheckSucceed = CheckLicensePlate(lp);
                string lastLicensePlate = lp.GetLicensePlate();
                if (isCheckSucceed)
                {
                    lp.IsEnabled = false;
                    lp.SucceedMsg.Visibility = Visibility.Visible;
                    var unDel = LicensePlateControlList.Where(it => it.isDelete == false);
                    foreach (var item in unDel)
                    {
                        LicensePlateList.Add(item.GetLicensePlate());
                    }
                    BackEnd.ParkBackEnd parkBackEnd = new BackEnd.ParkBackEnd();
                    //调用批量绑定接口进行绑定操作
                    string bingResult = parkBackEnd.AddCardNosBind(LicensePlateList, cardBindInfo.MerchantToken);
                    if (bingResult.StartsWith( "SUCCESS")) //全部绑定成功
                    {
                        lp.ColseLicensePlate();
                        string[] time = bingResult.Split( new char[] { ',' });
                        CheckSucceed(time[1],time[2]);
                    }
                    else if (bingResult == "") //未知错误
                    {
                        TbErrorMsg.Text = "网络异常,10秒后返回首页";
                    }
                    else //有失败的车牌，暂时不处理
                    {

                    }
                }
                else
                {
                    lp.ErrMsg.Visibility = Visibility.Visible;
                    return;
                }
            }
        }
        /// <summary>
        /// 绑定车牌号成功
        /// </summary>
        private void CheckSucceed(string   startTime ,string endTime ) 
        {
            this.PageView.Visibility = Visibility.Hidden;
            this.FrameView.Visibility = Visibility.Visible;
            this.FramePage.Visibility = Visibility.Visible;
            BindCardNoSucceedPage bindCardNoSucceedPage = new BindCardNoSucceedPage(startTime,endTime);
            this.NavigationService.Navigate(bindCardNoSucceedPage, dic);
            this.NavigationService.LoadCompleted += bindCardNoSucceedPage.NavigationService_LoadCompleted;
            this.FramePage.Content = bindCardNoSucceedPage;
        }
        /// <summary>
        /// 返回首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Timer_MouseMove.Stop();
            this.PageView.Visibility = Visibility.Hidden;
            this.FrameView.Visibility = Visibility.Visible;
            this.FramePage.Visibility = Visibility.Visible;
            LicensePlateControl lp = GetLastUnDeleteLicensePlateControl();
            if(lp!=null)
            lp.ColseLicensePlate();
            IndexPage indexPage = new IndexPage();
            this.NavigationService.Navigate(indexPage, dic);
            this.NavigationService.LoadCompleted += indexPage.NavigationService_LoadCompleted;
            this.FramePage.Content = indexPage;
        }

        private void CardNumTb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TbErrorMsg.Text = "";
            TextBox tb = (TextBox)sender;
            tb.Focus();
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
                        LicensePlateControl lp = GetLastUnDeleteLicensePlateControl();
                        if(lp!=null)
                        lp.ColseLicensePlate();
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


        /// <summary>
        /// 继续添加车牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardAddBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //获取当前添加的车牌
            LicensePlateControl lp = GetLastUnDeleteLicensePlateControl();
            if(lp==null)//删没了
            {
                AddNewLicensePlateControl();
                return;
            }
            else 
            {
                bool isCheckSucceed = CheckLicensePlate( lp);//检测是否可绑定
            
                if (isCheckSucceed)
                {
                    lp.LicensePlateInput.IsEnabled = false;
                    lp.SucceedMsg.Visibility = Visibility.Visible;
              
                    AddNewLicensePlateControl();
                    lp.ColseLicensePlate();
                    return;
                }
                else 
                {
                    lp.ErrMsg.Visibility = Visibility.Visible;
                    return;
                }
            }
        }
        /// <summary>
        /// 检测当前车牌
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        private bool CheckLicensePlate(LicensePlateControl lp) 
        {
            bool isCheckSucceed = false;
            string lastLicensePlate = lp.GetLicensePlate();
            if (string.IsNullOrEmpty(lastLicensePlate))//车牌没有全部填写
            {
                lp.ErrMsg.Visibility = Visibility.Visible;
               
                return false;
            }
            else //调用车牌查询接口
            {
                cardBindInfo.CardNo = lastLicensePlate;
                BackEnd.ParkBackEnd parkBackEnd = new BackEnd.ParkBackEnd();
                #region  检测车牌号（是否已存在，是否绑定成功）
                if (cardBindInfo.CardNo == "京A11111")
                    isCheckSucceed = true;
                else
                {
                    isCheckSucceed = parkBackEnd.isCheckCardNoBind(cardBindInfo);
                   
                }
                #endregion
            }
            return isCheckSucceed;
        }
        /// <summary>
        /// 获取当前最后一个非删除的车牌输入控件
        /// </summary>
        /// <returns></returns>
        private LicensePlateControl GetLastUnDeleteLicensePlateControl() 
        {
            if (LicensePlateControlList != null && LicensePlateControlList.Count > 0)
            {
                var undelLicensePlateControlList = LicensePlateControlList.Where(it => it.isDelete == false).LastOrDefault();
                return undelLicensePlateControlList;
            }
            return null;

        }
        /// <summary>
        /// 添加新的车牌输入控件
        /// </summary>
        private void AddNewLicensePlateControl() 
        {
            LicensePlateControl licensePlateControl = new LicensePlateControl();
            licensePlateControl.index = LicensePlateCount;
            RegisterName("lp" + LicensePlateCount, licensePlateControl);
            LicensePlateControlList.Add(licensePlateControl);
            this.LicensePlateListControl.Children.Add(licensePlateControl);
            LicensePlateCount++;
        }

        
    }
}
