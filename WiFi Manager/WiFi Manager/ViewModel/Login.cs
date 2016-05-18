using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsing.Helper;
using Tsing.Model;
using Windows.UI.Xaml.Controls;

namespace WiFi_Manager.ViewModel
{
    class Login
    {
        public static HttpHelper loginHelper = new HttpHelper(AppData.host.uri);
        public static List<KeyValuePair<string, string>> postContent = new List<KeyValuePair<string, string>>();
        public static string clientIP;
        public static string sessionID;

        public static ID ID;
        public static TextBlock outputTextBlock;

        public static async Task<bool> gatherInfo(bool print)
        {
            conditionPrint(print, "连接中");
            try
            {
                string loginGetResponseText = await loginHelper.DoGet("/portal/index_default.jsp?Language=Chinese");
                clientIP = StringHelper.getStringBetween(loginGetResponseText, "name=ClientIP value=", ">');");
                sessionID = StringHelper.getStringBetween(loginGetResponseText, "name=sessionID value=", ">');");

                if (clientIP == null || sessionID == null)
                {
                    conditionPrint(print, "连接失败，请确认已连接" + AppData.host.name);
                    return false;
                }

                postContent.Clear();
                KeyValuePair<string, string> pair1 = new KeyValuePair<string, string>("Language", "Chinese");
                KeyValuePair<string, string> pair2 = new KeyValuePair<string, string>("ClientIP", clientIP == null ? "" : clientIP);
                KeyValuePair<string, string> pair3 = new KeyValuePair<string, string>("sessionID", sessionID == null ? "" : sessionID);
                KeyValuePair<string, string> pair4 = new KeyValuePair<string, string>("timeoutvalue", "45");
                KeyValuePair<string, string> pair5 = new KeyValuePair<string, string>("heartbeat", "240");
                KeyValuePair<string, string> pair6 = new KeyValuePair<string, string>("fastwebornot", "false");
                KeyValuePair<string, string> pair7 = new KeyValuePair<string, string>("StartTime", new TimeStampHelper().ToString());
                KeyValuePair<string, string> pair8 = new KeyValuePair<string, string>("username", ID.account);
                KeyValuePair<string, string> pair9 = new KeyValuePair<string, string>("password", ID.password);
                KeyValuePair<string, string> pair10 = new KeyValuePair<string, string>("shkOvertime", "720");
                KeyValuePair<string, string> pair11 = new KeyValuePair<string, string>("strOldPrivateIP", clientIP == null ? "" : clientIP);
                KeyValuePair<string, string> pair12 = new KeyValuePair<string, string>("strOldPublicIP", clientIP == null ? "" : clientIP);
                KeyValuePair<string, string> pair13 = new KeyValuePair<string, string>("strPrivateIP", clientIP == null ? "" : clientIP);
                KeyValuePair<string, string> pair14 = new KeyValuePair<string, string>("PublicIP", clientIP == null ? "" : clientIP);
                KeyValuePair<string, string> pair15 = new KeyValuePair<string, string>("iIPCONFIG", "0");
                KeyValuePair<string, string> pair16 = new KeyValuePair<string, string>("sHttpPrefix", AppData.host.uri.ToString());
                postContent.Add(pair1);
                postContent.Add(pair2);
                postContent.Add(pair3);
                postContent.Add(pair4);
                postContent.Add(pair5);
                postContent.Add(pair6);
                postContent.Add(pair7);
                postContent.Add(pair8);
                postContent.Add(pair9);
                postContent.Add(pair10);
                postContent.Add(pair11);
                postContent.Add(pair12);
                postContent.Add(pair13);
                postContent.Add(pair14);
                postContent.Add(pair15);
                postContent.Add(pair16);

                conditionPrint(print, "连接成功");
                return true;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                conditionPrint(print, "连接失败，请确认已连接" + AppData.host.name);
                return false;
            }
        }

        public static async Task<bool> doLogin(bool print)
        {
            if (await gatherInfo(print))
            {
                try
                {
                    conditionPrint(print, "登录中");
                    string loginPostResponseText = await loginHelper.DoPost("/portal/login.jsp?Flag=0", postContent);

                    if(loginPostResponseText != null)
                    {
                        string output = StringHelper.getStringBetween(loginPostResponseText, "<td class=\"tWhite\">", "</td>");
                        if (output != null)
                        {
                            output = StringHelper.removeString(output, "<b>");
                            output = StringHelper.removeString(output, "</b>");
                            output = StringHelper.removeString(output, "\"");
                            if (output.Contains("您已经建立了连接。"))
                            {
                                output += "如无法上网，或因账户已在其他设备上登录，请尝试点击登出后重新登录";
                            }
                            conditionPrint(print, output);
                            if (output.Contains("E") || output.Contains("Rejected by server"))
                            {
                                return false;
                            }
                            return true;
                        }
                        else
                        {
                            conditionPrint(print, "登录成功");
                            return true;
                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    conditionPrint(print, "登录失败，请确认已连接" + AppData.host.name);
                }
            }
            return false;
        }

        public static async Task<bool> doLogout(bool print)
        {
            if (await gatherInfo(print))
            {
                try
                {
                    conditionPrint(print, "登出中");
                    string logoutPostResponseText = await loginHelper.DoPost("/portal/logout.jsp", postContent);
                    string output = StringHelper.getStringBetween(logoutPostResponseText, "<td class=\"tWhite\">", "</td>");
                    if (output != null)
                    {
                        conditionPrint(print, output.Substring(0, 13));
                    }
                    return true;
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    conditionPrint(print, "登出失败，请确认已连接" + AppData.host.name);
                    return false;
                }
            }
            return false;
        }

        public static bool conditionPrint(bool print,string Text)
        {
            if (print)
            {
                try
                {
                    outputTextBlock.Text = Text;
                    return true;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool saveID(User User, bool SaveAccount, bool SavePassword)
        {
            if (!SaveAccount)
            {
                ID.deleteAccount();
            }
            if (!SavePassword)
            {
                ID.deletePassword();
            }
            return ID.save(User);
        }
    }
}
