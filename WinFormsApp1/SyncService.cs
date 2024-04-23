using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http.Json;

namespace WinFormsApp1
{
    public class SyncService
    {
        public bool running=false;
        private static HttpClient client;
        private static FirebaseApp app;
#if DEBUG
        //const string url = "http://localhost:8000/api/";
        const string url = "http://192.168.254.12:8000/api/";
#else
        const string url = "https://apikhazom.gharsaaman.com/api/";

#endif

        public SyncService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("data.json"),
            });
        }


        async void syncProcess(Data.Link link)
        {
            var res = await client.PostAsJsonAsync(url + "add", link);
            if (res.IsSuccessStatusCode)
            {
                using (var db = new Data.Context())
                {
                    db.Database.ExecuteSqlRaw("update links set sync=1 where ID = " + link.ID + "");
                }

                Scrapper.log("ended sync " + link.ID.ToString() + "::" + link.Title);
            }
            else
            {
                Scrapper.log("error sync " + link.ID.ToString() + "::" + link.Title);

                //Scrapper.log(await res.Content.ReadAsStringAsync());
            }
        }

        public async void SendNotification(List<Data.Link> links,int count)
        {
            var messages=links.Select(o=>new FirebaseAdmin.Messaging.Message()
            {
                Android=new AndroidConfig()
                {
                    Notification=new AndroidNotification()
                    {
                        Title=o.Title
                    }
                },
                Data=new Dictionary<string, string>()
                {
                    {"count",count.ToString() }
                },
                Topic = "news",
            }).ToList();

            // Send a message to the devices subscribed to the provided topic.
            await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);

        }

        public async void sync()
        {
            using (var db = new Data.Context())
            {
                db.Database.EnsureCreated();
                if (db.Links.Where(o => !o.sync && !o.onsync).Count() == 0)
                {
                    Form1.context.timer3.Enabled = true;
                    running = false;
                    return;
                }
                Form1.context.timer3.Enabled = false;
                running = true;
                var ids = new List<int>();
                var links = db.Links.Where(o => !o.sync && !o.onsync).Take(50).ToList();
                ids = links.Select(o => o.ID).ToList();
                db.Database.ExecuteSqlRaw("update links set onsync=1 where  ID in ( " + String.Join(",", ids) + ")");

                Scrapper.log("Started syncing from " + ids.Min() + " to " + ids.Max());
                var res = await client.PostAsJsonAsync(url + "add", new {
                    links=links,
                    api="need123456@"
                    });
                if (res.IsSuccessStatusCode)
                {
                    Scrapper.log("Success syncing from " + ids.Min() + " to " + ids.Max());
                    db.Database.ExecuteSqlRaw("update links set sync=1 where  ID in ( " + String.Join(",", ids) + ")");
                    SendNotification(links.OrderByDescending(o => o.ID).Take(5).ToList(), links.Count);
                }
                else
                {
                    Scrapper.log("Error syncing from " + ids.Min() + " to " + ids.Max());
                    db.Database.ExecuteSqlRaw("update links set onsync=0 where  ID in ( " + String.Join(",", ids) + ")");

                }
                Debug.Write(await res.Content.ReadAsStringAsync());


                if (db.Links.Where(o => !o.sync).Count() > 0)
                {
                    this.sync();
                }
                else
                {
                    running = false;
                    Form1.context.timer3.Enabled = true;
                    

                }
            }


        }

    }
}
