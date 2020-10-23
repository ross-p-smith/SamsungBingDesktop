using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Forms;

namespace BingDesktop
{
    public class BingUtilities
    {
        public dynamic DownloadJson()
        {
            using (WebClient webClient = new WebClient())
            {
                Console.WriteLine("Downloading JSON...");
                string jsonString = webClient.DownloadString("https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-GB");
                return JsonConvert.DeserializeObject(jsonString);
            }
        }
        public string GetBackgroundUrlBase()
        {
            dynamic jsonObject = DownloadJson();
            return "https://www.bing.com" + jsonObject["images"][0]["urlbase"];
        }

        public Uri GeUriWithResolution(string url)
        {
            if (UrlExists(url + "_1920x1200.jpg"))
            {
                return new Uri(url + "_1920x1200.jpg");
            }
            else
            {
                return new Uri(url + "_1920x1080.jpg");
            }
            //Rectangle resolution = Screen.PrimaryScreen.Bounds;
            //string widthByHeight = resolution.Width + "x" + resolution.Height;
            //string potentialExtension = "_" + widthByHeight + ".jpg";
            //if (WebsiteExists(url + potentialExtension))
            //{
            //    Console.WriteLine("Background for " + widthByHeight + " found.");
            //    return potentialExtension;
            //}
            //else
            //{
            //    Console.WriteLine("No background for " + widthByHeight + " was found.");
            //    Console.WriteLine("Using 1920x1080 instead.");
            //    return "_1920x1080.jpg";
            //}
        }

        bool UrlExists(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "HEAD";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public Image GetBingDailyImage()
        {
            var dailyImageUri = GeUriWithResolution(GetBackgroundUrlBase());
            return new Image
            {
                Source = ImageSource.FromUri(dailyImageUri)
            };
        }
    }
}
