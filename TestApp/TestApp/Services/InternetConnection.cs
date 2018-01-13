using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TestApp.Services
{
    public class InternetConnection
    {
        private string _url;
        private string _htmlQuellcode;

        public InternetConnection(string url)
        {
            Url = url;
            HtmlQuellcode = GetHTML();
        }



        #region Property
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }

        public string HtmlQuellcode
        {
            get
            {
                return _htmlQuellcode;
            }

            set
            {
                _htmlQuellcode = value;
            }
        }

        #endregion

        public string GetHTML()//http://www.lotto24.de/webshop/product/lottonormal/result
        {
            string html = "";


            if (CheckInternetConnection())
            {
                try
                {
                    string url = Url;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "text/html";
                    HttpWebResponse myResp = (HttpWebResponse)request.GetResponse();

                    using (var response = request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }

                catch (WebException exception)
                {
                        Console.WriteLine(exception.Message);
                }
            }
            else
            {
                html = "";
                ShowInformationMassageAsync("No connection", "Es ist nicht möglich eine Verbindung zum Internet herzustellen !");
            }
            //Quellcode zurückgeben
            return html;
        }

        private bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private async System.Threading.Tasks.Task ShowInformationMassageAsync(string titel, string text)
        {
            await App.Current.MainPage.DisplayAlert(titel, text, "OK");
        }
    }
}
