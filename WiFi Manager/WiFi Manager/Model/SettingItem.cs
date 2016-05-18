using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tsing.Model
{
    class SettingItem
    {
        public string name { get; set; }
        public string value { get; set; }


        public SettingItem(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public SettingItem(string Name, bool Value) : this(Name, Value.ToString().ToLower()) { }

        public SettingItem(FrameworkElement fe) : this(fe.Name, getValue(fe)) { }

        public SettingItem(User User, string Name) : this(Name, User.settingContainerHelper.getSetting(Name)) { }



        public bool save(User User)
        {
            return User.settingContainerHelper.writeSetting(name, value);
        }




        public static string getValue(FrameworkElement fe)
        {

            if (fe is TextBox)
            {
                return ((TextBox)fe).Text;

            }
            else if (fe is TextBlock)
            {
                return ((TextBlock)fe).Text;

            }
            else if (fe is PasswordBox)
            {
                return ((PasswordBox)fe).Password;

            }
            else if (fe is CheckBox)
            {
                return ((CheckBox)fe).IsChecked.Value.ToString().ToLower();
            }
            else if (fe is ToggleSwitch)
            {
                return ((ToggleSwitch)fe).IsOn.ToString().ToLower();

            }
            return null;
        }



        public bool activate(FrameworkElement fe)
        {
            if (value != null)
            {
                if (fe is TextBox)
                {
                    ((TextBox)fe).Text = value;
                    return true;
                }
                else if (fe is TextBlock)
                {
                    ((TextBlock)fe).Text = value;
                    return true;

                }
                else if (fe is PasswordBox)
                {
                    ((PasswordBox)fe).Password = value;
                }
                else if (fe is CheckBox)
                {
                    ((CheckBox)fe).IsChecked = Boolean.Parse(value);
                    return true;
                }
                else if (fe is ToggleSwitch)
                {
                    ((ToggleSwitch)fe).IsOn = Boolean.Parse(value);
                    return true;

                }
            }
            return false;
        }
    }
}
