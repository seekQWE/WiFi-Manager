using Windows.UI.Xaml.Controls;

namespace Tsing.Model
{
    enum IDType
    {
        StudentID, SchoolCardID, BankCardID, WiFiID, HotSpotID, CourseID, Default
    }

    class ID
    {
        public IDType idType { get; set; }
        public string account { get; set; }
        public string password { get; set; }

        public ID(IDType IDType, string Account, string Password)
        {
            idType = IDType;
            account = Account;
            password = Password;
        }

        public ID(IDType IDType, TextBox AccountTextBox, PasswordBox PasswordBox) : this(IDType, AccountTextBox.Text, PasswordBox.Password) { }

        public ID(User User, IDType IDType) : this(IDType, User.settingContainerHelper.getSetting(IDType.ToString() + "Account"), User.settingContainerHelper.getSetting(IDType.ToString() + "Password")) { }



        public bool save(User User)
        {
            return User.settingContainerHelper.writeSetting(idType.ToString() + "Account", account)
                & User.settingContainerHelper.writeSetting(idType.ToString() + "Password", password);
        }

        public bool activate(TextBox AccountTextBox, PasswordBox PasswordBox)
        {

            if (account != null)
            {
                AccountTextBox.Text = account;
                if (password != null)
                {
                    PasswordBox.Password = password;
                    return true;
                }
            }
            return false;
        }



        public bool delete()
        {
            account = null;
            password = null;
            return true;
        }

        public bool deleteAccount()
        {
            account = null;
            return true;
        }

        public bool deletePassword()
        {
            password = null;
            return true;
        }
    }
}
