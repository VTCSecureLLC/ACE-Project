using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using log4net;
using com.vtcsecure.ace.windows.Services;
using VATRP.Core.Model;
using HockeyApp;
using System.Threading;
using com.vtcsecure.ace.windows.Views;

using com.vtcsecure.ace.windows.Utilities;
using Newtonsoft.Json;
using VATRP.Core.Model.Utils;
using VATRP.Core;
using System.Net;
using System.ComponentModel;
using System.Configuration;
namespace com.vtcsecure.ace.windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        #region Members

        private static readonly log4net.ILog _log = LogManager.GetLogger(typeof (App));
        private static bool _allowDestroyWindows = false;
        private Mutex mutex;
        #endregion

        #region Properties
        public static bool AllowDestroyWindows
        {
            get { return _allowDestroyWindows; }
            set { _allowDestroyWindows = value; }
        }
        public static VATRPAccount CurrentAccount { get; set; }
        public static bool CanMakeVideoCall { get; set; }

        internal static bool AppClosing { get; set; }
        #endregion


        AppVersion[] appVersionArray;

        public App()
        {
            Mutex testmutex;
            Mutex.TryOpenExisting("Global\\84D29A79-09A3-4CBF-A12A-B15CEF971672", out testmutex);
            if (testmutex != null) //*** If testmutex is null means application instance is not running.
            {
                MessageBox.Show("Instance already running");
                Environment.Exit(0);
                return;
            }
            mutex = new Mutex(true, "Global\\84D29A79-09A3-4CBF-A12A-B15CEF971672");

           

        }
        protected override async void OnStartup(StartupEventArgs e)
        {

            //***********************************************************************************************************************************
            // A type that derives from Application may override OnStartup. 
            // The overridden method must call OnStartup in the base class if the Startup event needs to be raised.
            //************************************************************************************************************************************
            base.OnStartup(e);
            CheckNewVersionAvailable();

            //main configuration of HockeySDK
            HockeyClient.Current.Configure(HOCKEYAPP_ID);
                //.UseCustomResourceManager(HockeyApp.ResourceManager) //register your own resourcemanager to override HockeySDK i18n strings
                //.RegisterCustomUnhandledExceptionLogic((eArgs) => { /* do something here */ }) // define a callback that is called after unhandled exception
                //.RegisterCustomUnobserveredTaskExceptionLogic((eArgs) => { /* do something here */ }) // define a callback that is called after unobserved task exception
                //.RegisterCustomDispatcherUnhandledExceptionLogic((args) => { }) // define a callback that is called after dispatcher unhandled exception
                //.SetApiDomain("https://your.hockeyapp.server")
                //.SetContactInfo("John Smith", "email@example.com");

            //optional should only used in debug builds. register an event-handler to get exceptions in HockeySDK code that are "swallowed" (like problems writing crashlogs etc.)
#if DEBUG
            ((HockeyClient) HockeyClient.Current).OnHockeySDKInternalException += (sender, args) =>
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            };
#endif
            try
            {

                //send crashes to the HockeyApp server
               await HockeyClient.Current.SendCrashesAsync();
            }
            catch (Exception eArgs)
            {
                if (_log != null)
                    _log.Error("HockeyApp SendCrashesAsync exception: " + eArgs.ToString());
            }

            //check for updates on the HockeyApp server
            await HockeyClient.Current.CheckForUpdatesAsync(true, () =>
            {
                if (Application.Current.MainWindow != null)
                {
                    Application.Current.MainWindow.Close();
                }
                return true;
            });
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {

            //********************************************************************************************************************
            //      A type that derives from Application may override OnStartup. 
            //      The overridden method must call OnStartup in the base class if the Startup event needs to be raised.
            //********************************************************************************************************************

            _log.Info("====================================================");
            _log.Info(String.Format("============== Starting VATRP v{0} =============",
                System.Reflection.Assembly.GetEntryAssembly().GetName().Version));
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            try
            {
                var appDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                if (string.IsNullOrEmpty(appDirectory))
                {
                    MessageBox.Show("Current directory is null", "ACE", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown(1);
                    return;
                }

                if (currentDirectory != appDirectory )
                {
                    try
                    {
                        System.IO.Directory.SetCurrentDirectory(appDirectory);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to change application directory" + Environment.NewLine + ex.Message, "ACE", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to get application directory" + Environment.NewLine + ex.Message, "ACE", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(1);
            }

            string linphoneLibraryVersion = VATRP.LinphoneWrapper.LinphoneAPI.linphone_core_get_version_asString();
            _log.Info(String.Format("======= LinphoneLib Version v{0} =======",
                linphoneLibraryVersion));

            _log.Info("====================================================");

            CurrentAccount = null;
            AppDomain.CurrentDomain.SetData("DataDirectory",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            var culture = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!ServiceManager.Instance.Initialize())
            {
                MessageBox.Show("Failed to initialize service manager");
                this.Shutdown();
            }

            ServiceManager.Instance.Start();
            var mainWnd = new MainWindow();
            this.MainWindow = mainWnd;

            if (ServiceManager.Instance.ConfigurationService.Get(Configuration.ConfSection.GENERAL,
                Configuration.ConfEntry.SHOW_LEGAL_RELEASE, true))
            {
                LegalReleaseWindow lrWnd = new LegalReleaseWindow();
                var dlgResult = lrWnd.ShowDialog();
                if (dlgResult == null || (bool)!dlgResult)
                {
                    ServiceManager.Instance.Stop();
                    this.Shutdown();
                    return;
                }
                else
                {
                    ServiceManager.Instance.ConfigurationService.Set(Configuration.ConfSection.GENERAL,
                       Configuration.ConfEntry.SHOW_LEGAL_RELEASE, false);
                    ServiceManager.Instance.ConfigurationService.SaveConfig();
                }
            }

            mainWnd.InitializeMainWindow();
            mainWnd.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            //************************************************************************************************************************
            // This method will called when user quit the application. "On Application Exit"
            //************************************************************************************************************************
            mutex.Dispose();

            if (System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString() != appVersionArray[0].Version)
            {
                InstallBuild();
                System.Environment.Exit(1);
               
            }
            



        }

        private void InstallBuild()
        {
            var appDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            if (System.IO.File.Exists(appDirectory + "\\Setup_" + appVersionArray[0].Version.ToString() + ".exe"))
            {
                Process process = Process.Start(appDirectory + "\\Setup_" + appVersionArray[0].Version.ToString() + ".exe");
                int id = process.Id;
                Process tempProc = Process.GetProcessById(id);
                //tempProc.Visible = false;
                // tempProc.WaitForExit();


                //tempProc.Visible = true;
            }
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (_log != null)
                _log.Error("Not handled exception: " + e.Exception.ToString() + "\n" + e.Exception.StackTrace);
        }

        private bool CheckNewVersionAvailable()
        {

            string response = Utilities.JsonWebRequest.MakeJsonWebRequest("http://isolherbal.com/Ace/version.json");

            //using System.Data;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;

            string appVersion= string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);


          


            appVersionArray = JsonConvert.DeserializeObject<AppVersion[]>(response);
            var appDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (appVersion != appVersionArray[0].Version)
            {
                if (!System.IO.File.Exists(appDirectory + "\\Setup_" + appVersionArray[0].Version.ToString() + ".exe"))
                {
                    DownloadInstaller(appVersionArray[0].Path, appVersionArray[0].Version);
                }
                else
                {
                    long length = new System.IO.FileInfo(appDirectory + "\\Setup_" + appVersionArray[0].Version.ToString() + ".exe").Length;
                    if (length / 1024 != appVersionArray[0].Size)
                    {
                        DownloadInstaller(appVersionArray[0].Path, appVersionArray[0].Version);
                    }
                    else
                    {
                        //NEW VERSION ALREADY DOWNLOADED and ONLY NEED TO INSTALL

                        if (MessageBox.Show("New version of ACE is available. Do you wish to install? You would be required to restart your application.", "ACE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                           // App_OnExit(null, null);
                            InstallBuild();
                            System.Environment.Exit(1);

                        }
                    }
                }

            }
            else
            {
                if (!System.IO.File.Exists(appDirectory + "\\Setup_" + appVersionArray[0].Version.ToString() + ".exe"))
                {
                    System.IO.File.Delete(appDirectory + "\\Setup_" + appVersionArray[0].Version.ToString() + ".exe");
                }
               
            }
             //var table = JsonConvert.DeserializeObject<Array>(response);
            //List<VATRPDomain> domains = Utilities.JsonWebRequest.MakeJsonWebRequest<List<VATRPDomain>>("http://isolherbal.com/Ace/version.json"); 
             //DataRow dr = table.Rows(0);

            //Utilities.JsonWebRequest.MakeJsonWebRequest<List<VATRPDomain>>(CDN_DOMAIN_URL); 

            return true;
        }

        private void DownloadInstaller(string path, string version){

            var appDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            System.Net.ServicePointManager.Expect100Continue = true;

            WebClient downloader = new WebClient();
           
            downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            downloader.DownloadFileAsync(new Uri(path), appDirectory + "\\Setup_" + version + ".exe");
            //downloader.DownloadFileAsync(new Uri(path), "G:\\Setup_"+ version +".exe");
            //downloader.DownloadFileAsync(new Uri("http://192.168.5.132/mit/setup.exe"), "G:\\Setup_" + version + ".exe");
           // downloader.DownloadFileAsync(new Uri(path), System.IO.Path.GetTempPath() + "\\" + System.IO.Path.GetFileName(path));
            //var downloadHelper = new HttpDownloadHelper();
           
            //downloadHelper.DownloadCompleted += OnDownloadCompleted;
            //ThreadPool.QueueUserWorkItem((x) => downloadHelper.DownloadImage(path, "G:\\ABC1.exe"));

        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {

            if (e.Error != null)
            {
                Console.WriteLine("Completed" + e.Error );
                CheckNewVersionAvailable();
                return;
            }
            else
            {
                if (MessageBox.Show("New version of ACE is available. Do you wish to install? You would be required to restart your application.", "ACE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    System.Environment.Exit(1);
                    //App_OnExit(null, null);

                }
                Console.WriteLine("Completed");
            }
            
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("Download %:" + e.ProgressPercentage);
           // Console.WriteLine("Download %:" + e.);
        }


        private void OnDownloadCompleted(object sender, VATRP.Core.Events.HttpDownloadEventArgs e)
        {
            Console.WriteLine("Download completed: " + e.Succeeded + " " + e.URI);
        }

    }



    public class AppVersion
    {
        public string Version { get; set; }
        public string Date { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }

  
}



