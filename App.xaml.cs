using CommonBase.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PARK
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private LogOther log = new LogOther();
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => log.Error(e.Exception.Message));
            e.Handled = true;
        }


        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            string ExeName = Process.GetCurrentProcess().MainModule.ModuleName;
            string Path = System.IO.Path.GetFileNameWithoutExtension(ExeName);
            Process[] myprocess = Process.GetProcessesByName(Path);
            if (myprocess.Length > 1)
            {
                MessageBox.Show("程序已经启动", "提示");
                Environment.Exit(0);
            }

            RegisterEvents();

            base.OnStartup(e);
        }
        private void RegisterEvents()
        {
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                //MessageBox.Show(args.Exception.Message);
                log.Error("UnobservedTaskException:" + args.Exception.Message + ", StackTrace:" + args.Exception.StackTrace);
                args.SetObserved();
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) => log.Error("UnhandledException:" + args.ExceptionObject.ToString());
        }
    }
}
