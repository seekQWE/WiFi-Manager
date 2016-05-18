using Windows.Storage;

namespace Tsing.Helper
{
    class SettingContainerHelper
    {
        ApplicationDataContainer localSettings = null;

        public string containerName;

        public SettingContainerHelper(string ContainerName)
        {
            containerName = ContainerName;
            localSettings = ApplicationData.Current.LocalSettings;
            createContainer();
        }

        public void createContainer()
        {
            ApplicationDataContainer container = localSettings.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
        }

        public void deleteContainer()
        {
            localSettings.DeleteContainer(containerName);
        }





        public bool writeSetting(string SettingName, string SettingValue)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values[SettingName] = SettingValue;
                return true;
            }
            return false;
        }

        public bool writeSetting(string SettingName, bool SettingValue)
        {
            return writeSetting(SettingName, SettingValue.ToString().ToLower());
        }

        public string getSetting(string SettingName)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                object value = localSettings.Containers[containerName].Values[SettingName];
                if (value != null)
                {
                    return string.Format("{0}", value);
                }
            }
            return null;
        }

        public bool getSettingOfBool(string SettingName)
        {
            return getSetting(SettingName) == "true";
        }

        public string getSettingOfString(string SettingName)
        {
            return getSetting(SettingName);
        }

        public void deleteSetting(string SettingName)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values.Remove(SettingName);
            }
        }


        public bool hasContainer()
        {
            return localSettings.Containers.ContainsKey(containerName);
        }

        public bool hasSetting(string SettingName)
        {
            return hasContainer() ? localSettings.Containers[containerName].Values.ContainsKey(SettingName) : false;
        }
    }
}
