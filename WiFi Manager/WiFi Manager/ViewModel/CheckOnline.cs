using System;
using System.Threading.Tasks;
using Tsing.Helper;

namespace WiFi_Manager.ViewModel
{
    class CheckOnline
    {
        public static HttpHelper checkOnlineHttpHelper = new HttpHelper(new Uri("testhost"));

        public static async Task<bool> checkOnline()
        {
            return await checkOnlineHttpHelper.DoGet("testUri") != null;
        }
    }
}
