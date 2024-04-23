using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Data
{


    public class Link
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Website { get; set; }
        public int Category { get; set; }
        public bool sent { get; set; }
        public bool sync { get; set; }
        public bool onsync { get; set; }
    }

}
