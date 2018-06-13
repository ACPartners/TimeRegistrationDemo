using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace TimeRegistration.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _accessToken = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var options = new OidcClientOptions
            {
                Authority = "http://localhost:5000",
                ClientId = "WPF",
                ClientSecret = "secret2",
                RedirectUri = "http://localhost/TimeRegistrationClient",
                Scope = "openid profile HolidayRequests",
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.FormPost,
                Flow = OidcClientOptions.AuthenticationFlow.Hybrid,
                Browser = new WpfEmbeddedBrowser()
            };

            var client = new OidcClient(options);
            var result = await client.LoginAsync();
            if (result.IsError) return;
            _accessToken = result.AccessToken;
            LoginStatus.Content = "Logged In";
            
        }

        private async void requestHoliday_Click(object sender, RoutedEventArgs e)
        {
            var proxy = new TimeRegistrationProxy("http://localhost:3000");
            var requestDate = holidayPicker.SelectedDate;

            var request = new RegisterHolidayRequestModel();
            request.HolidayType = "P";
            request.To = requestDate;
            request.From = requestDate;
            request.Remarks = "Testing";

            proxy.AccessToken = _accessToken;
            await proxy.ApiHolidayRequestPostAsync(request);

            await DoSlackPost(requestDate);

        }

        private static async Task DoSlackPost(DateTime? requestDate)
        {
            var slackPost = new HttpClient();
            //https://hooks.slack.com/services/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
            slackPost.BaseAddress = new Uri("https://hooks.slack.com/services/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");
            var content = new StringContent($"{{ 'text':'Holiday request in demo for {requestDate}' }}");

            await slackPost.PostAsync("", content);
        }

        private async void slackPost_Click(object sender, RoutedEventArgs e)
        {
            await DoSlackPost(DateTime.Now);
        }
    }
}
