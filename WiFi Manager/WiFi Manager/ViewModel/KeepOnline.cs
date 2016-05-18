using System;
using System.Threading;

namespace WiFi_Manager.ViewModel
{
    class KeepOnline
    {
        public static Timer timer = new Timer(autoLogin, autoEvent, -1, 10000000);
        public static AutoResetEvent autoEvent = new AutoResetEvent(false);
        public static TimerCallback timerCallback = autoLogin;

        public static async void autoLogin(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            await Login.doLogin(false);
        }
    }
}
