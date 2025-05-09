using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SDG.Unturned;

namespace BattlePassV1
{
    public static class Delay
    {
        public static bool Canor = true;
        public static async void TimeDelay()
        {
            Canor = false;

            await Task.Delay(1000);
            Canor = true;

        }

        public static async void DisconectNew(Steamworks.CSteamID steamID, int lvl, uint ExpToNewLvl, List<int> uses, List<PlayerInfo> players)
        {
            if(!(lvl == 0 && ExpToNewLvl == 230))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{steamID.ToString()},{lvl},{ExpToNewLvl},");

                foreach (var qitem in uses)
                {
                    sb.Append($"{qitem},");
                }
                sb.Append(":");

                while (!Canor)
                {
                    await Task.Delay(20);
                }

                using (FileStream file = new FileStream("BattlePass.txt", FileMode.Append))
                using (StreamWriter writer = new StreamWriter(file))
                    writer.WriteLine(sb.ToString());
                TimeDelay();
                int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == steamID);
                PlayerInfo PI = PassConfiguration.Players[rr];
                PassConfiguration.Players.RemoveAt(rr);
            }
            
        }

        public static async void Disconect(Steamworks.CSteamID steamID, int lvl, uint ExpToNewLvl, List<int> uses, List<PlayerInfo> players)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append($"{steamID.ToString()},{lvl},{ExpToNewLvl},");

            foreach (var qitem in uses)
            {
                sb.Append($"{qitem},");
            }
            sb.Append(":");


            string str;
            using (StreamReader reader = new StreamReader("BattlePass.txt", true))
                str = reader.ReadToEnd();

            string pattern = $"{steamID.ToString()},(.*):";
            Regex regex = new Regex(pattern);

            string final = regex.Replace(str, sb.ToString());

            while (!Canor)
            {
                await Task.Delay(20);
            }

            using (StreamWriter writer = new StreamWriter("BattlePass.txt", false))
                writer.WriteLine(final);

            TimeDelay();
            int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == steamID);
            PlayerInfo PI = PassConfiguration.Players[rr];
            PassConfiguration.Players.RemoveAt(rr);
        }
    }
}

