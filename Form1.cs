using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace guokai
{
    public partial class Form1 : Form
    {
        int times = 0;
        bool runstate = false;
        public Form1()
        {
            InitializeComponent();
        }

        void WebView1_NavigationCompleted(object sender, EventArgs e)
        {

            //if (times == 0)
            {
                //MessageBox.Show("加载完成1");
                
                //object json1 = webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('select')[0].value = '1';document.getElementById('account').value = '340103198410302538';document.getElementById('password').value = '302538'; document.getElementById('btnQuery').click();");

                //Thread.Sleep(1000);
                //object json2 = webView1.CoreWebView2.ExecuteScriptAsync("document.getElementById('btnaID').click();");

            }
            //else if (times == 1)
            //{
            //    MessageBox.Show("加载完成2");
            //    webView1.CoreWebView2.Navigate("https://main.ahjxjy.cn/study/html/content/studying/?courseOpenId=5xd-adgufynkbvohpgciaq&cellId=seqqansuvjxk3-v9h92jzq&schoolCode=032");
            //}
            //times++;
            //richTextBox1.Text = json.ToString();
            //richTextBox1.Text = (webView1.CoreWebView2.ExecuteScriptAsync(@"document.documentElement.outerHTML")).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView1.NavigationCompleted += WebView1_NavigationCompleted;
            //webView1.CoreWebView2.NavigationCompleted += WebView1_FrameNavigationCompleted;
            webView1.CoreWebView2.Navigate("https://iam.pt.ouchn.cn/am/UI/Login?realm=%2F&service=initService&goto=https%3A%2F%2Fiam.pt.ouchn.cn%2Fam%2Foauth2%2Fauthorize%3Fservice%3DinitService%26response_type%3Dcode%26client_id%3D345fcbaf076a4f8a%26scope%3Dall%26redirect_uri%3Dhttps%253A%252F%252Fmenhu.pt.ouchn.cn%252Fouchnapp%252Fwap%252Flogin%252Findex%26decision%3DAllow");
        }

        private void webView1_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            webView1.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }
        private void CoreWebView2_NewWindowRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.NewWindow = webView1.CoreWebView2;
        }
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                Application.DoEvents();//可执行某无聊的操作
            }
        }

        private async Task<int> LogExpand(int temp)
        {
            string text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('clearfix module-name module-title').length; return len1 ; })();");
            int len1 = int.Parse(text1);
            for (int i = 0; i < len1; i++)
            {
                if (i == temp) continue;
                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('clearfix module-name module-title')[" + i + "].click();");
                Delay(200);
            }
            return 0;
        }
        private async void RunProc()//执行的部分 
        {
            string text1;
            int len1 = 0;
            int len2 = 0;

            await LogExpand(0);
            Delay(3000);

            text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('clearfix truncate-text activity-menu-item ng-scope').length; return len1 ; })();");
            len1 = int.Parse(text1);
            for (int i = int.Parse(textBox1.Text); i < len1; i++)
            {
                if (runstate == false) break;
                richTextBox1.Text = "当前第 " + i + " 个任务，总计 " + len1 + " 个任务";
                text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByTagName('video').length; return len1 ; })();");
                string text2 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('exam-basic-info').length; return len1 ; })();");
                len2 = int.Parse(text1);
                int len3 = int.Parse(text2);
                if (len2 != 0)
                {
                    while (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].readyState;") != "4")
                    {
                        Delay(3000);
                    }
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('mvp-toggle-play mvp-first-btn-margin')[0].click();document.getElementsByTagName('video')[0].play(); document.getElementsByTagName('video')[0].playbackRate = 8;");
                    while (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].ended;") != "true")
                    {
                        if (runstate == false) break;
                        Delay(3000);
                    }
                } else if (len3 != 0)
                {
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('button button-green take-exam ng-scope')[0].click();");
                    Delay(3000);
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('exam-rules')[0].parentNode.parentNode.children[1].children[0].click();");
                    Delay(3000);
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('button button-green medium ng-binding')[0].click();");
                    Delay(6000);
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('subject ng-scope single_selection').length; return len1 ; })();");
                    len2 = int.Parse(text1);
                    text2 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('subject ng-scope true_or_false').length; return len1 ; })();");
                    len3 = int.Parse(text2);
                    for(int j = 0; j < len2; ++j)
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('subject ng-scope single_selection')[" + j + "].children[0].children[1].children[0].children[0].children[0].click();");
                        Delay(1000);
                    }
                    for (int j = 0; j < len3; ++j)
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('subject ng-scope true_or_false')[" + j + "].children[0].children[1].children[0].children[0].children[0].click();");
                        Delay(1000);
                    }
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('button full-screen-header-button button-green ng-scope')[0].click();");
                    Delay(1000);
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('reveal-modal popup-area confirmation-popup popup-480 ng-scope open')[0].children[0].children[2].children[0].children[0].click();");
                    Delay(2000);
                    await webView1.CoreWebView2.ExecuteScriptAsync("window.history.go(-2);");
                    await webView1.CoreWebView2.ExecuteScriptAsync("sleep(5000);");
                    //webView1.GoBack();
                    //Delay(5000);
                    //webView1.GoBack();
                    Delay(5000);
                    string text4 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('clearfix module-name module-title').length; return len1 ; })();");
                    int len4 = int.Parse(text4);
                    for (int j = 0; j < len4; j++)
                    {
                        if (j == i) continue;
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('clearfix module-name module-title')[" + j + "].click();");
                        Delay(200);
                    }
                    Delay(3000);

                }
                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('clearfix truncate-text activity-menu-item ng-scope')[" + i + "].click();");
                Delay(3000);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            runstate = true;
            RunProc();
            //thread = new Thread(RunProc);
            //thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            runstate = false;
            richTextBox1.Text = "已终止";
        }

        public static string GetNetDateTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 1000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                { if (h == "Date") { datetime = headerCollection[h]; } }
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }
        private void check_Date()
        {
            DateTime usetime = Convert.ToDateTime("2023年6月30日");
            DateTime daytime = DateTime.Parse(Convert.ToDateTime(GetNetDateTime()).ToLongDateString());
            TimeSpan ts = usetime - daytime;
            int day = ts.Days;
            if (day <= 0)
            {
                if (MessageBox.Show("软件试用期已到，请注册后再使用！", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            else
            {
                //MessageBox.Show("本软件的试用期还有" + day.ToString() + "天！", "提示");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            check_Date();
        }
    }
}
