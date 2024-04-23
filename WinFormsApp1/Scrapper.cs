using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Data;
namespace WinFormsApp1
{
    public class Scrapper
    {
        private static int i = 0;
        private static IBrowsingContext web;
        private static string[] arr = new string[] {
            "_", "*", "[", "]", "(", ")", "~", "`", ">", "#", "+", "-", "=", "|", "{", "}", ".", "!"


        };



        private static Context getDB()
        {
            var db = new Context();
            db.Database.EnsureCreated();
            return db;
        }
        public static void ensureInit()
        {
            
            if(web == null)
            {
                var config = Configuration.Default.WithDefaultLoader();

                web =  BrowsingContext.New(config);
            }
        }
        public static string sanitize(string s)
        {
           
            foreach (var c in arr)
            {

                s = s.Replace(c, "\\" + c);
            }
            s=s.Trim();
            return s;
        }

        public static async void kantipurno1()
        {
            try
            {
                var document = await web.OpenAsync("https://ekantipur.com/pradesh-1");
                var links = document.QuerySelectorAll(".normal")
                    .Select(o => new
                    {
                         datalink=o.QuerySelector("h2 a"),
                         image=o.QuerySelector(".image img")
                    })
                    .Select(o => new 
                    {
                        link= "https://ekantipur.com"+o.datalink.GetAttribute("href"),
                        title =o.datalink.InnerHtml,
                        image=o.image.GetAttribute("data-src"),
                    })
                    .ToList();
            }
            catch (Exception)
            {

                
            }
        }
        public static async void kantipur()
        {
            var db=getDB();
            var id=i++;
            log(id.ToString()+":"+"Kantipur start to fetch data");
            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ekantipur.com");
                var links = document.QuerySelectorAll("a").Select(o => new Data.Link() { 
                    Url = o.GetAttribute("href"), 
                    Title =sanitize( o.TextContent),
                    Website =1 ,
                    Category=1,
        
                }).Where(o => o.Url != null && o.Title != "" && !o.Url.Contains("video")).Where(o => o.Url.Contains(".html")).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToArray();
                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":"+"Kantipur finished fetching data,"+links.Length.ToString()+" links");
                
            }
            catch (Exception e)
            {

                log(id.ToString() + ":"+e.Message);
            }
        }

        public static async void setopati()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "setopati start to fetch data");
            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://www.setopati.com/");
                var links = document.QuerySelectorAll(".items a")
                    .Where(o=>o.QuerySelector(".main-title")!=null)
                    .Where(o=>o.GetAttribute("href").Contains("www.setopati.com"))
                    .Select(o=>new Data.Link()
                    {
                        Url = o.GetAttribute("href"),
                        Title =sanitize( o.QuerySelector(".main-title").TextContent),
                        Website =2
                    })
                    .Where(o=>o.Url.Where(x=>(x=='/')).Count()>3)
                    .Where(o => o.Url != null && o.Title.Length>0)
                    .Where(x => db.Links.Count(o => o.Url == x.Url) == 0)
                    .ToArray();
                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "setopati finished fetching data," + links.Length.ToString() + " links");
               
            }
            catch (Exception e)
            {

                log(id.ToString() + ":" + e.Message);
            }
        }
        public static void log(string txt)
        {
            if (Form1.context.logbox.InvokeRequired)
            {
                Form1.context.logbox.Invoke((MethodInvoker)delegate
                {
                    Form1.context.logbox.Text +=  txt + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff") + Environment.NewLine;
                });
            }
            else
            {
                Form1.context.logbox.Text +=  txt + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff") + Environment.NewLine;

            }
        }
    }

}
