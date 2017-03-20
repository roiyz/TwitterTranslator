using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetSharp;
using Tweetinvi;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Web.UI;

namespace SentimentAnalyzer
{
    class Program
    {
        private const string SubscriptionKey = "ADD_YOUR_KEY";   //Enter here the Key from your Microsoft Translator Text subscription on http://portal.azure.com


        static void Main(string[] args)
        {
            TextWriter fileWriter = new StreamWriter("out.txt");
            TextWriter htmlTextWriter = new StreamWriter("twits.html");
            HtmlTextWriter htmlWriter = new HtmlTextWriter(htmlTextWriter);


            Console.ForegroundColor = ConsoleColor.White;
            //https://github.com/linvi/tweetinvi/wiki/Searches
            Auth.SetUserCredentials("ADD_YOUR_KEY", "ADD_YOUR_KEY", "ADD_YOUR_KEY", "ADD_YOUR_KEY");//Register an app and get the app and kyes ids from dev.twitter.com
            var authenticatedUser = User.GetAuthenticatedUser();
            var tweets = Timeline.GetUserTimeline("theIRC",10);

            var translatorService = new TranslatorService.LanguageServiceClient();
            var authTokenSource = new AzureAuthToken(SubscriptionKey);
            var token = string.Empty;
            token = authTokenSource.GetAccessToken();
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Head);
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Style);
            htmlWriter.Write("table, td, th {border: 1px solid #ddd;}");
            htmlWriter.RenderEndTag();//End Style
            htmlWriter.RenderEndTag();//End Head

            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Body);

            //htmlWriter.AddAttribute(HtmlTextWriterAttribute.Border, "1");
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);

            foreach (var tw in tweets)

            {
                Console.WriteLine(tw);
                string fr = "\t" + translatorService.Translate(token, tw.Text, "en", "fr", "text/plain", "general", string.Empty);
                string he = "\t" + translatorService.Translate(token, tw.Text, "en", "he", "text/plain", "general", string.Empty);
                string ar = "\t" + translatorService.Translate(token, tw.Text, "en", "ar", "text/plain", "general", string.Empty);
                string es = "\t" + translatorService.Translate(token, tw.Text, "en", "es", "text/plain", "general", string.Empty);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\t" + fr);
                Console.ForegroundColor = ConsoleColor.White;
                fileWriter.WriteLine(tw);
                fileWriter.WriteLine(fr);
                fileWriter.WriteLine(es);
                fileWriter.WriteLine(ar);

                

                //Original Twit
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Src, "https://raw.githubusercontent.com/tkrotoff/famfamfam_flags/master/us.png");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Img);
                htmlWriter.RenderEndTag(); //End img
                htmlWriter.RenderEndTag(); //End cell
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("<b>"+ tw+ "</b>");
                htmlWriter.RenderEndTag(); //End cell
                htmlWriter.RenderEndTag(); //End Row
                
                //French
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Src, "https://raw.githubusercontent.com/tkrotoff/famfamfam_flags/master/fr.png");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Img);
                
                htmlWriter.RenderEndTag(); //End img
                htmlWriter.RenderEndTag(); //End cell
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write(fr);
                htmlWriter.RenderEndTag(); //End cell
                htmlWriter.RenderEndTag(); //End Row

                //Spanish
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Src, "https://raw.githubusercontent.com/tkrotoff/famfamfam_flags/master/es.png");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Img);
                
                htmlWriter.RenderEndTag(); //End img
                htmlWriter.RenderEndTag(); //End cell
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write(es);
                htmlWriter.RenderEndTag(); //End cell
                htmlWriter.RenderEndTag(); //End Row

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.Write("--");
                htmlWriter.RenderEndTag(); //End Row

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlWriter.RenderEndTag(); //End Row

                htmlWriter.RenderEndTag(); //End Row

            }
            htmlWriter.RenderEndTag(); //End Table
            htmlWriter.RenderEndTag(); //End Body
            htmlWriter.RenderEndTag(); //End HTML

            htmlWriter.Close();
            fileWriter.Close();
            return;

        }

        
    }
}
