using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Data;

namespace WinFormsApp1
{
    internal class ekantipur
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

            if (web == null)
            {
                var config = Configuration.Default.WithDefaultLoader();

                web = BrowsingContext.New(config);
            }
        }
        public static string sanitize(string s)
        {

            foreach (var c in arr)
            {

                s = s.Replace(c, "\\" + c);
            }
            s = s.Trim();
            return s;
        }
        

        public static async void kantiEconomics()
        {
                ensureInit();
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Kantipur start to fetch Economics data");

            try
            {
                var document = await web.OpenAsync("https://ekantipur.com/business");
                var links = document.QuerySelectorAll(".normal").Select(o => new
                    {
                        datalink = o.QuerySelector("h2 a"),
                        image = o.QuerySelector(".image img")
                    })
                    .Select(o => new Link()
                    {
                        Url = "https://ekantipur.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("data-src"),
                        Website = 1,
                        Category= 2
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Kantipur finished fetching Economics data," + links + " links");
                //Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void kantipurSports()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Kantipur start to fetch Sports data");

            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ekantipur.com/sports");
                var links = document.QuerySelectorAll(".normal").Select(o => new
                {
                    datalink = o.QuerySelector("h2 a"),
                    image = o.QuerySelector(".image img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ekantipur.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("data-src"),
                        Website = 1,
                        Category = 3
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToArray();
                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Kantipur finished fetching Sports data," + links + " links");
                //Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void kantipurEntertainment()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Kantipur start to fetch entertainment data");

            try
            {
                var document = await web.OpenAsync("https://ekantipur.com/entertainment");
                var links = document.QuerySelectorAll(".normal").Select(o => new
                {
                    datalink = o.QuerySelector("h2 a"),
                    image = o.QuerySelector(".image img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ekantipur.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("data-src"),
                        Website = 1,
                        Category = 4
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
                 db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Kantipur finished fetching entertainment data," + links + " links");
                //Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void kantipurTechnology()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Kantipur start to fetch technology data");

            try
            {
                var document = await web.OpenAsync("https://ekantipur.com/technology");
                var links = document.QuerySelectorAll(".normal").Select(o => new
                {
                    datalink = o.QuerySelector("h2 a"),
                    image = o.QuerySelector(".image img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ekantipur.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("data-src"),
                        Website = 1,
                        Category = 5
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Kantipur finished fetching technology data," + links + " links");
                //Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void kantipurLifestyle()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Kantipur start to fetch lifestyle data");

            try
            {
                var document = await web.OpenAsync("https://ekantipur.com/lifestyle");
                var links = document.QuerySelectorAll(".normal").Select(o => new
                {
                    datalink = o.QuerySelector("h2 a"),
                    image = o.QuerySelector(".image img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ekantipur.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("data-src"),
                        Website = 1,
                        Category = 6
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Kantipur finished fetching lifestyle data," + links + " links");
                //Console.Out.WriteLine(links);
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
                    Form1.context.logbox.Text += txt + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff") + Environment.NewLine;
                });
            }
            else
            {
                Form1.context.logbox.Text += txt + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff") + Environment.NewLine;

            }
        }
    }
}
