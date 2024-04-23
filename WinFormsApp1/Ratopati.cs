using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Data;

namespace WinFormsApp1
{
    internal class Ratopati
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
        public static async void ratoEconomy()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Ratopati start to fetch Economics data");

            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ratopati.com/category/economy");
                var links = document.QuerySelectorAll(".item").Select(o => new
                {
                    datalink = o.QuerySelector(".item-content a"),
                    image = o.QuerySelector(".item-header img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ratopati.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("src"),
                        Website = 3,
                        Category = 2
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
                //Console.Out.WriteLine(links);

                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Ratopati finished fetching Economics data," + links + " links");
                //Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void ratoSport()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Ratopati start to fetch Sports data");

            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ratopati.com/category/sports");
                var links = document.QuerySelectorAll(".item").Select(o => new
                {
                    datalink = o.QuerySelector(".item-content a"),
                    image = o.QuerySelector(".item-header img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ratopati.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("src"),
                        Website = 3,
                        Category = 3
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
               // Console.Out.WriteLine(links);

                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Ratopati finished fetching Sports data," + links + " links");
               // Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void ratoEntertainment()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Ratopati start to fetch entertainment data");

            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ratopati.com/category/entertainment");
                var links = document.QuerySelectorAll(".item").Select(o => new
                {
                    datalink = o.QuerySelector(".item-content a"),
                    image = o.QuerySelector(".item-header img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ratopati.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("src"),
                        Website = 3,
                        Category = 4
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
               // Console.Out.WriteLine(links);

                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Ratopati finished fetching entertainment data," + links + " links");
               // Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void ratoScience()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Ratopati start to fetch Science data");

            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ratopati.com/category/knowledge-science");
                var links = document.QuerySelectorAll(".item").Select(o => new
                {
                    datalink = o.QuerySelector(".item-content a"),
                    image = o.QuerySelector(".item-header img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ratopati.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("src"),
                        Website = 3,
                        Category = 5
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
               // Console.Out.WriteLine(links);

                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Ratopati finished fetching Science data," + links + " links");
                //Console.Out.WriteLine(links);
            }
            catch (Exception e)
            {
                log(id.ToString() + ":" + e.Message);
            }
        }

        public static async void ratoLifestyle()
        {
            var db = getDB();
            var id = i++;
            log(id.ToString() + ":" + "Ratopati start to fetch lifestyle data");

            try
            {
                ensureInit();
                var document = await web.OpenAsync("https://ratopati.com/category/lifestyle");
                var links = document.QuerySelectorAll(".item").Select(o => new
                {
                    datalink = o.QuerySelector(".item-content a"),
                    image = o.QuerySelector(".item-header img")
                })
                    .Select(o => new Link()
                    {
                        Url = "https://ratopati.com" + o.datalink.GetAttribute("href"),
                        Title = o.datalink.InnerHtml,
                        Image = o.image.GetAttribute("src"),
                        Website = 3,
                        Category = 6
                    }).Where(x => db.Links.Count(o => o.Url == x.Url) == 0).ToList();
                Console.Out.WriteLine(links);

                db.Links.AddRange(links);
                await db.SaveChangesAsync();
                log(id.ToString() + ":" + "Ratopati finished fetching lifestyle data," + links + " links");
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
