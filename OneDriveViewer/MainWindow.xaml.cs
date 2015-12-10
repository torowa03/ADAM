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
using Microsoft.Live;


namespace OneDriveViewer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiveAuthClient liveAuthClient;
        private LiveConnectClient liveConnectClient;
        private readonly string clientId = "000000004416F9EB";
        private readonly string clientsecret = "yvy98QVuQLf-huQKyMp745Yca4AvRj7a";
        string[] scopes = new string[] { "wl.signin", "wl.skydrive" };


        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.liveAuthClient = new LiveAuthClient(clientId);
            webBrowser.Navigate(this.liveAuthClient.GetLoginUrl(scopes));

        }

        private async void webBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            //Shared signInUrl As Uri = New Uri(String.Format("https://login.live.com/oauth20_authorize.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_type=code&scope={1}", client_id, scope))

            if (this.webBrowser.Source.AbsoluteUri.StartsWith("https://login.live.com/oauth20_desktop.srf"))

            {
                //認証後のurlからcodeパラメーターを取得
                string authenticationCode = this.webBrowser.Source
                                            .Query.TrimStart('?').Split('&')
                                            .Where(x => x.IndexOf("code=") == 0)
                                            .Single()
                                            .Substring(5);

                LiveConnectSession session = await this.liveAuthClient.ExchangeAuthCodeAsync(authenticationCode);
                this.liveConnectClient = new LiveConnectClient(session);
                LiveOperationResult meRs = await this.liveConnectClient.GetAsync("me");
                dynamic meData = meRs.Result;

                MessageBox.Show("Name: " + meData.name + "ID: " + meData.id);

            }
        }
    }
}
