using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
//using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //int times = 0;
        string tempUrl;

        int timesForCourse = 0;
        int tempForCourse = 0;//0

        int timesForTest = 0;
        int tempForTest = -1;//-1

        int timesForPage = 0;
        int tempForPage = 0;//0

        bool boolForTest = true;
        bool isGoing = false;
        int tempForWhile = 0;
        int timesForWhile = 10;

        bool isFirstGoing = true;
        //数据库
        private string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=test.mdb";//此处为前面复制出来的代码
        private OleDbConnection conn = null;
        private OleDbDataAdapter adapter = null;
        private DataSet ds = null;
        private Dictionary<string, string> hashMap;


        private static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                Application.DoEvents();//可执行某无聊的操作
            }
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
            DateTime usetime = Convert.ToDateTime("2024年12月30日");
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
                MessageBox.Show("本软件的试用期还有" + day.ToString() + "天！", "提示");
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hashMap = new Dictionary<string, string>();

            // 连接数据库，需要传递连接字符串
            conn = new OleDbConnection(connStr);
            // 打开数据库连接
            conn.Open();

            adapter = new OleDbDataAdapter("Select * from biao1", conn);
            ds = new DataSet();
            adapter.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
            {
                hashMap.Add(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
            }
            //MessageBox.Show("" + ds.Tables[0].Rows.Count);
            ds.Reset();
            ds = null;
            // "Select * from 表1"为SQL语句，意思是从数据库中选择叫做“表1”的表，“conn”为连接
            //adapter = new OleDbDataAdapter("Select * from biao1", conn);
            // CommandBuilder对应的是数据适配器，需要传递参数
            //var cmd = new OleDbCommandBuilder(adapter);

            // 在内存中创建一个DataTable，用来存放、修改数据库表
            //DataTable dt = new DataTable();
            //DataSet ds = new DataSet();     //填充ds，保存数据 
            //// 通过适配器把表的数据填充到内存dt
            ////adapter.Fill(dt);
            //adapter.Fill(ds);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            //    {
            //        MessageBox.Show(ds.Tables[0].Rows[i][j].ToString());//需要转成String类型  
            //    }
            //}
            //ds.Reset();

            check_Date();
            string update_Txt = "";
            update_Txt += "1.4更新事项：\n1.适配实验，随堂练习；\n2.增加视频和做题界面防死等功能（十分钟自动返回）；\n3.新增课程和任务导航功能；\n4.优化界面显示；\n5.新增停止按钮功能（待优化，退出方式最好按下stop后退出，不支持start重入）\n";
            update_Txt += "\n1.5更新事项：\n1.适配已经完成的随堂练习；\n2.适配多选题回答；\n";
            update_Txt += "\n1.6更新事项：\n1.适配大学英语新视频界面；\n2.加速做题过程；\n3.新增试用功能；\n";
            update_Txt += "\n1.7更新事项：\n1.适配刷题80分下限功能；\n2.适配多页面切换功能\n3.优化刷题卡顿，视频卡顿；\n";
            update_Txt += "\n1.8更新事项：\n1.适配填空题；\n2.新增视频跳过；\n3.优化稳定性；\n";
            update_Txt += "\n1.9更新事项：\n1.修改填空题bug；\n2.新增删除题库功能；\n3.新增图片题功能；\n";
            MessageBox.Show(update_Txt);
        }
        private void RunProc1()//执行的部分 
        {
            //MessageBox.Show("加载完成1");
            webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('select')[0].value = '1';document.getElementById('account').value = '341124198206067422';document.getElementById('password').value = '067422'; document.getElementById('btnQuery').click();");

            Delay(1000);
            webView1.CoreWebView2.ExecuteScriptAsync("document.getElementById('btnaID').click();");
        }
        private void RunProc2()//执行的部分 
        {
            //MessageBox.Show("加载完成2");
            webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('work-box')[0].children["+tempForTest+"].children[0].children[0].children[0].children[1].click()");
        }
        private async Task<int> RunProc3()//执行的部分 
        {
            richTextBox3.Text = "";
            richTextBox2.Text = "开始执行！\n";
            richTextBox2.Text += "into RunProc3\n";

            string text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q-panel').length; return len1 ; })();");
            string text2 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q').length; return len1 ; })();");
            int len2 = int.Parse(text2);
            //ds = new DataSet();

            for (int i = 0; i < len2; i++)
            {
                if (isGoing == false) return 0;

                string question = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q-quest')[" + i + "].children[0].innerText; return len1 ; })();")).ToString();
                if(question == "\"\"") // 适配图片题
                {
                    question = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q-quest')[" + i + "].children[0].children[0].children[0].children[0].src; return len1 ; })();")).ToString();
                    //richTextBox2.Text += question + "\n";
                }
                question = question.Replace("'", "");  //消除‘在查询表达式中的异常
                //string sql = "select * from biao1 where question = '" + question + "'";
                //adapter = new OleDbDataAdapter(sql, conn);
                
                //adapter.Fill(ds);
                //richTextBox1.Text += ds.Tables[0].Rows.Count;
                if (hashMap.ContainsKey(question))
                {
                    richTextBox3.Text += 1;

                    //适配填空题
                    await webView1.CoreWebView2.ExecuteScriptAsync("if (document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].tagName == 'TEXTAREA') document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].click();document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].value = 1;");
                    //await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].click();document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].value = 1;");
                    //await webView1.CoreWebView2.ExecuteScriptAsync("if (document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children.length == 3) document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].children[0].children[1].children[0].contentDocument.getElementsByTagName('p')[0].innerHTML = 1;");

                    if (hashMap[question].ToString().Contains("、"))
                    {
                        text1 = hashMap[question].ToString();

                        if (text1.Contains("A"))
                        {
                            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 0 + "].click();");
                        }
                        Delay(250);
                        if (text1.Contains("B"))
                        {
                            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 1 + "].click();");
                        }
                        Delay(250);
                        if (text1.Contains("C"))
                        {
                            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 2 + "].click();");
                        }
                        Delay(250);
                        if (text1.Contains("D"))
                        {
                            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 3 + "].click();");
                        }
                        Delay(250);
                        if (text1.Contains("E"))
                        {
                            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 4 + "].click();");
                        }
                    }
                    else if (hashMap[question].ToString() == "\"A\"")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 0 + "].click();");
                    }
                    else if (hashMap[question].ToString() == "\"B\"")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 1 + "].click();");
                    }
                    else if (hashMap[question].ToString() == "\"C\"")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 2 + "].click();");
                    }
                    else if (hashMap[question].ToString() == "\"D\"")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 3 + "].click();");
                    }
                    else if (hashMap[question].ToString() == "\"正确\"")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 0 + "].click();");
                    }
                    else if (hashMap[question].ToString() == "\"错误\"")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 1 + "].click();");
                    }
                    else
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[" + 0 + "].click();");
                    }
                    
                }
                else
                {
                    richTextBox3.Text += 0;
                    //适配填空题
                    await webView1.CoreWebView2.ExecuteScriptAsync("if (document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].tagName == 'TEXTAREA') document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].value = 1; else document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].click();");
                    //await webView1.CoreWebView2.ExecuteScriptAsync("if (document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children.length == 3) document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].children[1].children[0].children[1].children[0].contentDocument.getElementsByTagName('p')[0].innerHTML = 1; else document.getElementsByClassName('e-q-quest')[" + i + "].children[1].children[0].children[0].click();");
                }
                
                Delay(400);
                //ds.Reset();
                adapter = null;
                
            }

            await webView1.CoreWebView2.ExecuteScriptAsync("$('textarea.answer-input').focus();");

            ds = null;
            await webView1.CoreWebView2.ExecuteScriptAsync("window.confirm=function(str){return true;};if(document.getElementsByClassName('e-save-b btn_save').length!=0) document.getElementsByClassName('e-save-b btn_save')[0].click();if(document.getElementsByClassName('btn_save e-save-next').length!=0) document.getElementsByClassName('btn_save e-save-next')[0].click();document.getElementsByClassName('ui-MDialog-bottom')[0].children[0].click();");

            return 0;
        }
        private async Task<int> RunProc4()//执行的部分 
        {
            string text2 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q').length; return len1 ; })();");
            int len2 = int.Parse(text2);
            int len3;
            bool len4;
            richTextBox3.Text += "\n";
            richTextBox2.Text = "开始执行！\n";
            richTextBox2.Text += "RunProc4\n";
            //ds = new DataSet();
            for (int i = 0; i < len2; i++)
            {
                if (isGoing == false) return 0;

                string question = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q')[" + i + "].children[0].children[1].children[1].children[1].children[0].innerText; return len1 ; })();")).ToString();
                if (question == "\"\"") // 适配图片题
                {
                    question = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q')[" + i + "].children[0].children[1].children[1].children[1].children[0].children[0].children[0].src; return len1 ; })();")).ToString();
                    //richTextBox2.Text += question+"\n";
                }
                question = question.Replace("'", "");  //消除‘在查询表达式中的异常
                //string sql = "select * from biao1 where question = '" + question + "'";
                ////执行查询数据sql
                //adapter = new OleDbDataAdapter(sql, conn);
                
                //adapter.Fill(ds);
                if (!hashMap.ContainsKey(question))
                {
                    boolForTest = false;
                    string answer = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q')[" + i + "].children[1].children[0].children[0].children[0].innerText; return len1 ; })();")).ToString();
                    string sql = "insert into biao1(question, answer) values('" + question + "', '" + answer + "')";
                    hashMap.Add(question, answer);
                    //执行插入数据sql
                    OleDbCommand comm = new OleDbCommand(sql, conn);
                    comm.ExecuteNonQuery();
                    Delay(1000);
                    richTextBox3.Text += "3";
                }
                else
                {
                    Delay(250);
                    richTextBox3.Text += "2";
                }
                //Delay(350);
                //ds.Reset();
                //adapter = null;
            }
            ds = null;
            Delay(1000);
            text2 = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-save').length; return len1 ; })();")).ToString();
            len2 = int.Parse(text2);  //适配随堂测试

            //适配分数下限
            text2 = (await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('totalscore')[0].children[0].children[0].innerText; return len1 ; })();")).ToString();
            text2 = text2.Replace("\"", "");
            len4 = int.TryParse(text2, out len3);

            if ((len2 != 0 && len3 < 80 && len4 == true) && !boolForTest)
            //if (true)
            {
                text2 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-save')[0].children.length; return len1 ; })();");
                richTextBox2.Text += "into <80\n";
                boolForTest = true;
                if(text2 == "2") //安开
                {
                    await webView1.CoreWebView2.ExecuteScriptAsync("window.confirm=function(str){return true;};document.getElementsByClassName('e-save')[0].children[1].click();");
                } 
                else if(text2 == "1") //安财
                {
                    await webView1.CoreWebView2.ExecuteScriptAsync("window.confirm=function(str){return true;};document.getElementsByClassName('e-save')[0].children[0].click();");
                }
                else
                {
                    MessageBox.Show("异常2");
                }
            }
            else
            {
                richTextBox2.Text += "into goback\n";
                boolForTest = true;
                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('nv nv-goback')[0].children[0].click()");
            }
            richTextBox3.Text = "";
            return 0;
        }
        //private async Task<int> RunProc()//执行的部分 
        //{
        //    if (isGoing == false) return 0;

        //    if (times == 0)
        //    {
        //        await RunProc3();
        //    }
        //    else if (times == 1)
        //    {
        //        await RunProc4();
        //    }
        //    else
        //    {
        //        MessageBox.Show("异常");
        //    }
        //    times++;
        //    return 0;

        //}

        private async Task<int> RunVideo()
        {
            richTextBox2.Text += "into runVideo\n";
            string text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByTagName('video').length; return len1 ; })();");
            int len1 = int.Parse(text1);
            int tempDelay = 0;
            if (len1 != 0)
            {
                if (len1 == 1)
                {
                    richTextBox2.Text += "before while1\n";
                    //播放视频
                    while (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].readyState;") != "4")
                    {
                        if (isGoing == false) break;
                        Delay(3000);
                        tempDelay++;
                        if (tempDelay >= 200) break; //防挂死
                    }

                    richTextBox2.Text += "before if1\n";
                    if (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].ended;") != "true")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].play(); document.getElementsByTagName('video')[0].currentTime=document.getElementsByTagName('video')[0].duration;");
                        //await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].play(); document.getElementsByTagName('video')[0].playbackRate = 8;");

                        ////Delay(10000);
                        ////if (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].ended;") != "true")
                        ////{
                        //    //text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByTagName('video')[0].duration; return len1 ; })();");
                        ////    int len2 = (int)float.Parse(text1);
                        ////    richTextBox2.Text += "before delay " + (len2 / 8 - 10) + "\n";
                        ////    Delay((len2 / 8 - 10) * 1000);
                        ////}   
                    }

                    //richTextBox2.Text += "before while2\n";
                    //while (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[0].ended;") != "true")
                    //{
                    //    richTextBox2.Text += tempDelay;
                    //    if (isGoing == false) break;
                    //    Delay(3000);
                    //    tempDelay++;
                    //    if (tempDelay >= 200) break; //防挂死
                    //}
                }
                else if (len1 == 2)  //适配大学英语小窗口
                {
                    richTextBox2.Text += "before while1\n";
                    //播放视频
                    while (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[1].readyState;") != "4")
                    {
                        if (isGoing == false) break;
                        Delay(3000);
                        tempDelay++;
                        if (tempDelay >= 200) break; //防挂死
                    }

                    richTextBox2.Text += "before if1\n";
                    if (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[1].ended;") != "true")
                    {
                        await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[1].play(); document.getElementsByTagName('video')[1].currentTime=document.getElementsByTagName('video')[1].duration;");
                        //await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[1].play(); document.getElementsByTagName('video')[1].playbackRate = 8;");

                        ////Delay(10000);
                        ////if (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[1].ended;") != "true")
                        ////{
                        ////    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByTagName('video')[1].duration; return len1 ; })();");
                        ////    int len2 = (int)float.Parse(text1);
                        ////    richTextBox2.Text += "before delay " + (len2 / 8 - 10) + "\n";
                        ////    Delay((len2 / 8 - 10) * 1000);
                        ////}

                    }

                    //richTextBox2.Text += "before while2\n";
                    //while (await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('video')[1].ended;") != "true")
                    //{
                    //    richTextBox2.Text += tempDelay;
                    //    if (isGoing == false) break;
                    //    Delay(3000);
                    //    tempDelay++;
                    //    if (tempDelay >= 200) break; //防挂死
                    //}
                }
                
            }
            Delay(3000);
            richTextBox2.Text += "\nbefore goback1\n";
            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('nv nv-goback')[0].children[0].click()");

            return 0;
        }
        async void WebView1_NavigationCompleted(object sender, EventArgs e)
        {
            richTextBox2.Text = "开始执行！\n";
            if (isGoing == false) return;
            Delay(4000);
            string text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = window.location.pathname; return len1 ; })();");
            //MessageBox.Show(text1);
            if (text1 == "\"/study/html/content/process/\"")
            {
                richTextBox2.Text += "into process\n";
                text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('sh-res').length; return len1 ; })();");
                timesForTest = int.Parse(text1);

                //等待首页加载完成
                while (timesForTest == 0 && tempForWhile != timesForWhile)
                {
                    Delay(2000);
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('sh-res').length; return len1 ; })();");
                    timesForTest = int.Parse(text1);
                    tempForWhile++;
                }
                tempForWhile = 0;
                //等待结束
                
                int temp1 = tempForCourse + 1;
                int temp2 = tempForTest + 2;
                int temp3 = tempForPage + 1;
                int temp4 = timesForPage - 1;
                richTextBox1.Text = "当前第 " + temp3 + " 页，总计 " + temp4 + " 页\n当前第 " + temp1 + " 门课，至当前页总计 " + timesForCourse + " 门课\n当前第 " + temp2 + " 个任务，总计 " + timesForTest + " 个任务";
                if (tempForTest+1 < timesForTest)
                {
                    tempUrl = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = window.location.href; return len1 ; })();");
                    tempForTest++;
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('sh-res')[" + tempForTest + "].children[0].children[0].click()");
                }
                else
                {
                    tempForCourse++;
                    tempForTest = -1;
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('nav')[0].children[0].children[2].children[0].click();");
                    
                    //webView1.CoreWebView2.Navigate("https://main.ahjxjy.cn/studentstudio/course/studying?schoolCode=032");
                }
            }
            else if (text1 == "\"/studentstudio/course/studying\"")
            {
                richTextBox2.Text += "into studying1\n";
                if (isFirstGoing)
                {
                    isFirstGoing = false;
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('lesson lesson-position').length; return len1 ; })();");
                    timesForCourse = int.Parse(text1);
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('x-paging-right')[0].children.length; return len1 ; })();");
                    if(text1 != "null")
                    {
                        timesForPage = int.Parse(text1);
                    }
                    else
                    {
                        timesForPage = 2;
                    }
                }
                Gothrough_Course();
            }
            else if (text1 == "\"/study/html/content/studying/\"")
            {
                richTextBox2.Text += "into studying2\n";
                Delay(5000);
                text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-b-g types-l').length; return len1 ; })();");
                int len1 = int.Parse(text1);
                richTextBox2.Text += "into NavigationCompleted\n";

                if (len1 == 0)
                {
                    await RunVideo();
                }
                else
                {
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q-l').length; return len1 ; })();");
                    len1 = int.Parse(text1);
                    if (len1 == 0)
                    {
                        await RunProc3();
                    }
                    else
                    {
                        await RunProc4();
                    }
                }
            }
            else if (text1.Contains("/virexp/lab/handle/")) //适配实验
            {
                richTextBox2.Text += "into handle\n";
                await webView1.CoreWebView2.ExecuteScriptAsync("location.href =" + tempUrl + ";");
            }
            else richTextBox2.Text += "done\n";

        }

        async void WebView1_NavigationCompleted_ForExam(object sender, EventArgs e)
        {
            richTextBox2.Text = "开始执行！\n";
            if (isGoing == false) return;
            Delay(4000);
            string text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = window.location.pathname; return len1 ; })();");
            //MessageBox.Show(text1);
            if (text1 == "\"/study/html/content/process/\"")
            {
                richTextBox2.Text += "into process\n";
                text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('sh-res').length; return len1 ; })();");
                timesForTest = int.Parse(text1);

                //等待首页加载完成
                while (timesForTest == 0 && tempForWhile != timesForWhile)
                {
                    Delay(2000);
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('sh-res').length; return len1 ; })();");
                    timesForTest = int.Parse(text1);
                    tempForWhile++;
                }
                tempForWhile = 0;
                //等待结束

                int temp1 = tempForCourse + 1;
                int temp2 = tempForTest + 2;
                int temp3 = tempForPage + 1;
                int temp4 = timesForPage - 1;
                richTextBox1.Text = "当前第 " + temp3 + " 页，总计 " + temp4 + " 页\n当前第 " + temp1 + " 门课，至当前页总计 " + timesForCourse + " 门课\n当前第 " + temp2 + " 个任务，总计 " + timesForTest + " 个任务";
                if (tempForTest + 1 < timesForTest)
                {
                    tempUrl = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = window.location.href; return len1 ; })();");
                    tempForTest++;
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('sh-res')[" + tempForTest + "].children[0].children[0].click()");
                }
                else
                {
                    tempForCourse++;
                    tempForTest = -1;
                    await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('nav')[0].children[0].children[2].children[0].click();");

                    //webView1.CoreWebView2.Navigate("https://main.ahjxjy.cn/studentstudio/course/studying?schoolCode=032");
                }
            }
            else if (text1 == "\"/studentstudio/course/studying\"")
            {
                //richTextBox2.Text += "into studying1\n";
                //if (isFirstGoing)
                //{
                //    isFirstGoing = false;
                //    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('lesson lesson-position').length; return len1 ; })();");
                //    timesForCourse = int.Parse(text1);
                //    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('x-paging-right')[0].children.length; return len1 ; })();");
                //    if (text1 != "null")
                //    {
                //        timesForPage = int.Parse(text1);
                //    }
                //    else
                //    {
                //        timesForPage = 2;
                //    }
                //}
                //Gothrough_Course();

                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsById('menu_exam').click();");
            }
            else if (text1 == "\"/studentstudio/exam\"")
            {
                //richTextBox2.Text += "into studying1\n";
                //if (isFirstGoing)
                //{
                //    isFirstGoing = false;
                //    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('lesson lesson-position').length; return len1 ; })();");
                //    timesForCourse = int.Parse(text1);
                //    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('x-paging-right')[0].children.length; return len1 ; })();");
                //    if (text1 != "null")
                //    {
                //        timesForPage = int.Parse(text1);
                //    }
                //    else
                //    {
                //        timesForPage = 2;
                //    }
                //}
                //Gothrough_Course();

                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsById('menu_exam').click();");
            }
            else if (text1 == "\"/study/html/content/studying/\"")
            {
                richTextBox2.Text += "into studying2\n";
                Delay(5000);
                text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-b-g types-l').length; return len1 ; })();");
                int len1 = int.Parse(text1);
                richTextBox2.Text += "into NavigationCompleted\n";

                if (len1 == 0)
                {
                    await RunVideo();
                }
                else
                {
                    text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('e-q-l').length; return len1 ; })();");
                    len1 = int.Parse(text1);
                    if (len1 == 0)
                    {
                        await RunProc3();
                    }
                    else
                    {
                        await RunProc4();
                    }
                }
            }
            else if (text1.Contains("/virexp/lab/handle/")) //适配实验
            {
                richTextBox2.Text += "into handle\n";
                await webView1.CoreWebView2.ExecuteScriptAsync("location.href =" + tempUrl + ";");
            }
            else richTextBox2.Text += "done\n";

        }

        private async void Gothrough_Course()
        {
            int ForCourse = tempForCourse % 8;
            string text1;
            await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('x-paging-right')[0].children[" + tempForPage + "].children[0].click();");
            Delay(3000);
            if (tempForCourse < timesForCourse)
            {
                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('lesson lesson-position')[" + ForCourse + "].children[0].click();");
                return;
            }
            else if(tempForPage < timesForPage-2) // 适配分页逻辑
            {
                tempForPage++;

                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('x-paging-next')[0].children[0].click();");
                Delay(3000);

                text1 = await webView1.CoreWebView2.ExecuteScriptAsync("(() => { var len1 = document.getElementsByClassName('lesson lesson-position').length; return len1 ; })();");
                timesForCourse  += int.Parse(text1);
                
                await webView1.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('lesson lesson-position')[" + ForCourse + "].children[0].click();");
                return;
            }

                
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //times = 0;
            boolForTest = true;
            tempForWhile = 0;

            isGoing = true;
            richTextBox2.Text = "开始执行！";
            webView1.NavigationCompleted += WebView1_NavigationCompleted;
            webView1.Reload();

            tempForCourse = int.Parse(textBox3.Text)-1;
            tempForTest = int.Parse(textBox4.Text)-2;
        }

        private void webView1_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webView1.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }
        private void CoreWebView2_NewWindowRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.NewWindow = webView1.CoreWebView2;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //RunProc1();
            boolForTest = true;
            tempForWhile = 0;
            isGoing = true;
            await RunProc3();
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            //RunProc1();
            boolForTest = true;
            tempForWhile = 0;
            isGoing = true;
            await RunProc4();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isGoing = false;
            webView1.NavigationCompleted -= WebView1_NavigationCompleted;
            richTextBox2.Text = "已暂停！！！";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "delete * from biao1";
            OleDbCommand comm = new OleDbCommand(sql, conn);
            comm.ExecuteNonQuery();
            hashMap.Clear();
            MessageBox.Show("删除成功！");
        }


    }
}
