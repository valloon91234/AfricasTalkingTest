using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace AfricasTalkingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = "ALVINCY";
            //String username = "sandbox";
            String to = "+254715223003";
            String msg = Uri.EscapeUriString("Hi! I am Valloon.\r\n" + DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
            //to = "+254711111222";ok
            try
            {
                String url = $"https://api.africastalking.com/version1/messaging";
                //String url = $"https://api.sandbox.africastalking.com/version1/messaging";
                byte[] data = Encoding.ASCII.GetBytes($"username={username}&to={to}&message={msg}&from={username}");
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = data.Length;
                httpWebRequest.Headers.Add("apiKey", "f1064bcd73ba9182fee7aacb7708b893a5276ee3716cbd05b1a7bb94b79274ea");
                //httpWebRequest.Headers.Add("apiKey", "df2d0bc04ed3cee215eab2c9ab14e36e5aee8dfd8b031eb292ae7e6076f01c9f");
                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream receiveStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            Console.WriteLine(streamReader.ReadToEnd());
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.ToString());
                var httpWebResponse = (HttpWebResponse)ex.Response;
                if (httpWebResponse != null)
                {
                    HttpStatusCode statusCode = httpWebResponse.StatusCode;
                    {
                        using (Stream receiveStream = httpWebResponse.GetResponseStream())
                        {
                            using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                Console.WriteLine(streamReader.ReadToEnd());
                            }
                        }
                    }
                }
            }
        }
    }
}
