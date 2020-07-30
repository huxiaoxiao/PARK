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
    /// 键盘按钮 的交互逻辑
    /// </summary>
    public partial class CardKeyBoardButton : UserControl
    {
        public delegate void EventHandler(object sender, EventArgs e, CardKeyBoardButton  cardKeyBoardButton);
        public event EventHandler BtnPreviewKeyDown;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public CardKeyBoardButton(string btnValue)
        {
            InitializeComponent();
            this.DataContext = this;
            btn_value = btnValue;
            this.Focusable = false;
        }
        /// <summary>
        /// 按钮键值
        /// </summary>
        private string btn_value;
        public string BtnValue
        {
            get { return btn_value; }
            set
            {
                btn_value = value;
                OnPropertyChanged("BtnValue");
            }
        }
       /// <summary>
       /// 按钮背景
       /// </summary>
        private string btn_background= "Transparent";
        public string Btn_Background
        {
            get { return btn_background; }
            set
            {
                btn_background = value;
                OnPropertyChanged("Btn_Background");
            }
        }
        /// <summary>
        /// 按钮是否可使用
        /// </summary>
        private string btn_enabled="True";
        public string Btn_Enabled
        {
            get { return btn_enabled; }
            set
            {
                btn_enabled = value;
                OnPropertyChanged("Btn_Enabled");
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        ///键按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardKeyBoardButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.BtnPreviewKeyDown != null)
            {
               this.BtnPreviewKeyDown(sender, e, this);
            }
        }
    }
}
