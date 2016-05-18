using System;
using Tsing.Model;

namespace WiFi_Manager.ViewModel
{
    class AppData
    {
        internal static User user = new User("Default");
        internal static Host host = new Host("QLSC_STU", new Uri("http://192.168.8.10"));
    }
}
