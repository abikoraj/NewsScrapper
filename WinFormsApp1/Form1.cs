using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
namespace WinFormsApp1
{

    public partial class Form1 : Form
    {
        TelegramBotClient botClient;
        public static Form1 context;
        public static SyncService service;
        public Form1()
        {
            InitializeComponent();
            context = this;
            botClient = new TelegramBotClient();
            service = new SyncService();
            timer2.Enabled = true;


        }

        private void button1_ClickAsync(object sender, EventArgs e)
        {
            work();
            timer1.Interval = Convert.ToInt32(seconds.Value) * 1000;
            timer1.Enabled = true;
            logbox.Text += "Timer Started" + Environment.NewLine;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            work();
        }

        void work()
        {
            //new Thread(new ThreadStart(Scrapper.kantipur)).Start();
            //new Thread(new ThreadStart(Scrapper.setopati)).Start();
            //new Thread(new ThreadStart(Scrapper.kantipurno1)).Start();

            new Thread(new ThreadStart(ekantipur.kantiEconomics)).Start();
            new Thread(new ThreadStart(ekantipur.kantipurSports)).Start();
            new Thread(new ThreadStart(ekantipur.kantipurEntertainment)).Start();
            new Thread(new ThreadStart(ekantipur.kantipurTechnology)).Start();
            new Thread(new ThreadStart(ekantipur.kantipurLifestyle)).Start();

            new Thread(new ThreadStart(Ratopati.ratoEconomy)).Start();
            new Thread(new ThreadStart(Ratopati.ratoSport)).Start();
            new Thread(new ThreadStart(Ratopati.ratoEntertainment)).Start();
            new Thread(new ThreadStart(Ratopati.ratoScience)).Start();
            new Thread(new ThreadStart(Ratopati.ratoLifestyle)).Start();
        }

        private void logbox_TextChanged(object sender, EventArgs e)
        {
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            logbox.Text += "Timer Stopped" + Environment.NewLine;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            service.sync();
            Scrapper.ensureInit();
            ekantipur.ensureInit();
        }

        private async void timer2_TickAsync(object sender, EventArgs e)
        {
            //using (var db = new Data.Context())
            //{
            //    db.Database.EnsureCreated();
            //    var ids = new List<int>();
            //    foreach (var link in db.Links.Where(o => !o.sent).Take(10).ToList())
            //    {
            //        try
            //        {
            //            Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
            //            chatId: new ChatId(-1001302159589),
            //            text: link.Title,
            //            parseMode: ParseMode.MarkdownV2,
            //            disableNotification: false,
            //            replyMarkup: new InlineKeyboardMarkup(
            //                InlineKeyboardButton.WithUrl(
            //                    "View Full",
            //                    link.Url)));



            //            ids.Add(link.ID);
            //        }
            //        catch (Exception ex)
            //        {
            //            Scrapper.log(ex.Message);
            //        }
            //        if (ids.Count > 0)
            //        {

            //            db.Database.ExecuteSqlRaw("update links set sent=1 where ID in (" + string.Join(",", ids) + ")");
            //        }
            //    }
            //}
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!service.running)
            {
                using (var db = new Data.Context())
                {
                    db.Database.EnsureCreated();
                    if (db.Links.Where(o => !o.sync && !o.onsync).Count()> 0)
                    {
                        service.sync();

                    }
                }
            }
        }
    }


}
