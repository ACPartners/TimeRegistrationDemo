using IdentityModel.OidcClient;
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
                ClientId = "mvc",
                ClientSecret = "secret",
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
        }
    }
}
