using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
                    //Anfrage an die Übergebene URL starten
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);

                    //Antwort-Objekt erstellen
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //Antwort Stream an Streamreader übergeben
                    StreamReader sr = new StreamReader(response.GetResponseStream());

                    //Antwort (HTML Code) auslesen
                    html = sr.ReadToEnd();

                    //Streamreader und Webanfrage schließen
                    sr.Close();
                    response.Close();
                }
                catch (Exception)
                {
                    ShowInformationMassageAsync("No connection", "Es ist nicht möglich eine Verbindung zum Internet herzustellen !");
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
