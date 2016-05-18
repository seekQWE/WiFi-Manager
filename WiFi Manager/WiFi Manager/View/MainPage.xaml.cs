using Tsing.Model;
using WiFi_Manager.ViewModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WiFi_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ID wifiID;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void MainPageStackPanel_Loaded(object sender, RoutedEventArgs e)
        {

            AppData.user.activateID(IDType.WiFiID, accountTextBox, passwordBox);
            wifiID = new ID(IDType.WiFiID, accountTextBox, passwordBox);

            Login.ID = wifiID;
            Login.outputTextBlock = loginResponseTextBlock;

            if (autoLoginToggleSwitch.IsOn && accountTextBox.Text != "" && passwordBox.Password != "")
            {
                visibilitySwitch(!await Login.doLogin(true));
            }
        }

        private async void changeIDButton_Click(object sender, RoutedEventArgs e)
        {
            visibilitySwitch(true);
            await Login.doLogout(true);
        }

        private void visibilitySwitch(bool showInput)
        {
            if (showInput)
            {
                changeIDButton.Visibility = Visibility.Collapsed;
                idInputStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                idInputStackPanel.Visibility = Visibility.Collapsed;
                changeIDButton.Visibility = Visibility.Visible;
            }
        }

        private void accountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wifiID.account = accountTextBox.Text;
            if (accountTextBox.Text.Length < 12)
            {
                accountTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                accountTextBox.BorderBrush = default(SolidColorBrush);
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            wifiID.password = passwordBox.Password;
            if (passwordBox.Password.Length < 6)
            {
                passwordBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                passwordBox.BorderBrush = default(SolidColorBrush);
            }
        }

        private void keepOnlineToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn == true)
            {
                KeepOnline.timer.Change(0, 1000 * 60);
            }
            else
            {
                KeepOnline.timer.Change(-1, 1000 * 60);
            }
        }

        private void activateSetting(object sender, RoutedEventArgs e)
        {
            AppData.user.activateSettingItem((Control)sender);
        }

        private void saveSetting(object sender, RoutedEventArgs e)
        {
            AppData.user.saveSettingItem((Control)sender);
        }


        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Login.saveID(AppData.user, saveAccountToggleSwitch.IsOn, savePasswordToggleSwitch.IsOn);
            if (accountTextBox.Text != "" && passwordBox.Password != "")
            {
                visibilitySwitch(!await Login.doLogin(true));
            }
        }

        private async void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            keepOnlineToggleSwitch.IsOn = false;

            Login.saveID(AppData.user, saveAccountToggleSwitch.IsOn, savePasswordToggleSwitch.IsOn);
            if (accountTextBox.Text != "" && passwordBox.Password != "" && await Login.doLogout(true))
            {
                visibilitySwitch(true);
            }
        }
    }
}
