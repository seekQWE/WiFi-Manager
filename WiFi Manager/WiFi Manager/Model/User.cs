using Tsing.Helper;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tsing.Model
{
    class User
    {
        public string name { get; set; }

        public SettingContainerHelper settingContainerHelper;

        public User(string Name)
        {
            name = Name;
            settingContainerHelper = new SettingContainerHelper(name);
        }


        public bool saveID(IDType IDType, TextBox AccountTextBox, PasswordBox PasswordBox)
        {
            return new ID(IDType, AccountTextBox, PasswordBox).save(this);
        }

        public bool saveID(ID ID)
        {
            return ID.save(this);
        }

        public ID getID(IDType IDType)
        {
            return new ID(this, IDType);
        }

        public bool activateID(IDType IDType, TextBox AccountTextBox, PasswordBox PasswordBox)
        {
            return getID(IDType).activate(AccountTextBox, PasswordBox);
        }





        public bool saveSettingItem(string Name, string Value)
        {
            return new SettingItem(Name, Value).save(this);
        }

        public bool saveSettingItem(FrameworkElement fe)
        {
            return new SettingItem(fe).save(this);
        }


        public SettingItem getSettingItem(string Name)
        {
            return new SettingItem(this, Name);
        }

        public SettingItem getSettingItem(FrameworkElement fe)
        {
            return new SettingItem(this, fe.Name);
        }


        public bool activateSettingItem(FrameworkElement fe)
        {
            return getSettingItem(fe).activate(fe);
        }
    }
}
