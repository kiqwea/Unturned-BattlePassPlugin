using System;
using System.Collections.Generic;
using Rocket.API;

namespace BattlePassV1
{ 
    public class PassConfiguration : IRocketPluginConfiguration
    {

        public static List<PassList> list = new List<PassList>();
        public static List<PlayerInfo> Players = new List<PlayerInfo>();

        public int t { get; set; }                                                                      

        public void LoadDefaults()
        {
            t = 5;
            list.Add(new PassList("item_1", 17, 5, "https://gspics.org/images/2022/10/30/0WPRMT.th.jpg", "https://gspics.org/images/2022/10/30/0WPY6e.th.jpg", "https://gspics.org/images/2022/10/30/0WPE3X.th.jpg", 1));
            list.Add(new PassList("item_2", 1201, 1, "https://gspics.org/images/2022/10/30/0WPSeK.th.jpg", "https://gspics.org/images/2022/10/30/0WPj7X.th.jpg", "https://gspics.org/images/2022/10/30/0WPH2i.th.jpg", 2));
            list.Add(new PassList("item_3", 116, 1, "https://gspics.org/images/2022/10/30/0WPdi7.th.jpg", "https://gspics.org/images/2022/10/30/0WPxFi.th.jpg", "https://gspics.org/images/2022/10/30/0WP4kO.th.jpg", 3));
            list.Add(new PassList("item_4", 18, 1, "https://gspics.org/images/2022/10/30/0WPo6n.th.jpg", "https://gspics.org/images/2022/10/30/0WPzAO.th.jpg", "https://gspics.org/images/2022/10/30/0WP993.th.jpg", 4));
            list.Add(new PassList("item_5", 334, 1, "https://gspics.org/images/2022/10/30/0WPp7u.th.jpg", "https://gspics.org/images/2022/10/30/0WR8a3.th.jpg", "https://gspics.org/images/2022/10/30/0WPASL.th.jpg", 5));
            list.Add(new PassList("item_6", 519, 1, "https://gspics.org/images/2022/10/30/0WP2uo.th.jpg", "https://gspics.org/images/2022/10/30/0WR0zL.th.jpg", "https://gspics.org/images/2022/10/30/0WPDqy.th.jpg", 6));
            list.Add(new PassList("item_7", 1194, 1, "https://gspics.org/images/2022/10/30/0WPZtE.th.jpg", "https://gspics.org/images/2022/10/30/0WRkty.th.jpg", "https://gspics.org/images/2022/10/30/0WPnhD.th.jpg", 7));
            list.Add(new PassList("item_8", 328, 6, "https://gspics.org/images/2022/10/30/0WPbMj.th.jpg", "https://gspics.org/images/2022/10/30/0WRumD.th.jpg", "https://gspics.org/images/2022/10/30/0WP1MI.th.jpg", 8));
            list.Add(new PassList("item_9", 1373, 1, "https://gspics.org/images/2022/10/30/0WPeem.th.jpg", "https://gspics.org/images/2022/10/30/0WRFfI.th.jpg", "https://gspics.org/images/2022/10/30/0WP3ba.th.jpg", 9));
            list.Add(new PassList("item_10", 334, 4, "https://gspics.org/images/2022/10/30/0WPp7u.th.jpg", "https://gspics.org/images/2022/10/30/0WR8a3.th.jpg", "https://gspics.org/images/2022/10/30/0WPTiQ.th.jpg", 10));
            list.Add(new PassList("item_11", 1194, 1, "https://gspics.org/images/2022/10/30/0WPZtE.th.jpg", "https://gspics.org/images/2022/10/30/0WRkty.th.jpg", "https://gspics.org/images/2022/10/30/0WPW3x.th.jpg", 11));
            list.Add(new PassList("item_12", 1373, 1, "https://gspics.org/images/2022/10/30/0WPeem.th.jpg", "https://gspics.org/images/2022/10/30/0WRFfI.th.jpg", "https://gspics.org/images/2022/10/30/0WPX2w.th.jpg", 12));
            list.Add(new PassList("item_13", 1382, 1, "https://gspics.org/images/2022/10/30/0WPNrJ.th.jpg", "https://gspics.org/images/2022/10/30/0WRKTQ.th.jpg", "https://gspics.org/images/2022/10/30/0WPvuh.th.jpg", 13));
            list.Add(new PassList("item_14", 17, 10, "https://gspics.org/images/2022/10/30/0WPRMT.th.jpg", "https://gspics.org/images/2022/10/30/0WPY6e.th.jpg", "https://gspics.org/images/2022/10/30/0WPM9N.th.jpg", 14));
            list.Add(new PassList("item_15", 1194, 1, "https://gspics.org/images/2022/10/30/0WPZtE.th.jpg", "https://gspics.org/images/2022/10/30/0WRkty.th.jpg", "https://gspics.org/images/2022/10/30/0WPmSv.th.jpg", 15));
        }
        public class Data
        {
            public string Name;
        }
    }
}

