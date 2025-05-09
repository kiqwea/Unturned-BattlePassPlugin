using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;

namespace BattlePassV1
{
    public class BattlePass : RocketPlugin<PassConfiguration>
    {
        public static BattlePass Instance { get; private set; }
        protected override void Load()
        {
            Instance = this;
            Logger.Log($"{name} has been loaded");

            EffectManager.onEffectButtonClicked += OnButtonClicked;
            EffectManager.onEffectTextCommitted += OnEffectText;
            U.Events.OnPlayerConnected += OnPlayerConnect;
            U.Events.OnPlayerDisconnected += OnPlayerDisconect;
            UnturnedPlayerEvents.OnPlayerUpdateExperience += OnPlayerExpUpdate;
            PlayerSkills.OnExperienceChanged_Global += PlayerSkills_OnExperienceChanged_Global;
           

            FileStream fs = new FileStream("BattlePass.txt", FileMode.OpenOrCreate);
            fs.Close();
        }

        

        protected override void Unload()
        {
            Logger.Log($"{name} has been unloaded");
            EffectManager.onEffectButtonClicked -= OnButtonClicked;
            EffectManager.onEffectTextCommitted -= OnEffectText;
            U.Events.OnPlayerConnected -= OnPlayerConnect;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconect;
            UnturnedPlayerEvents.OnPlayerUpdateExperience -= OnPlayerExpUpdate;
            PlayerSkills.OnExperienceChanged_Global -= PlayerSkills_OnExperienceChanged_Global;
            
        }

        //############
        private void PlayerSkills_OnExperienceChanged_Global(PlayerSkills Splayer, uint newExp)
        {

            UnturnedPlayer player =  UnturnedPlayer.FromPlayer(Splayer.player);

            int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == player.CSteamID);



            if (newExp < PassConfiguration.Players[rr].PlayerExp)
            {
                PassConfiguration.Players[rr].PlayerExp = newExp;
            }
            else
            {
                uint OnlyNewExp = newExp - PassConfiguration.Players[rr].PlayerExp;
                int GotExp = Convert.ToInt32(OnlyNewExp);
                int NeedExp = Convert.ToInt32(PassConfiguration.Players[rr].ExpToNewLvl);


                if (OnlyNewExp < PassConfiguration.Players[rr].ExpToNewLvl)
                {
                    PassConfiguration.Players[rr].ExpToNewLvl -= OnlyNewExp;

                }
                else
                {
                    while (GotExp > 0)
                    {
                        if (GotExp - NeedExp >= 0)
                        {
                            GotExp -= NeedExp;
                            PassConfiguration.Players[rr].Lvl++;
                            NeedExp = 230;

                            UnturnedChat.Say(player, $"You got level {PassConfiguration.Players[rr].Lvl}!", UnityEngine.Color.green);
                            UnturnedChat.Say(player, $"Open the battle pass and get a new prize:  /bp", UnityEngine.Color.green);
                        }
                        else
                        {
                            NeedExp -= GotExp;
                            GotExp = 0;
                        }
                    }
                    PassConfiguration.Players[rr].ExpToNewLvl = Convert.ToUInt32(NeedExp);
                }

                PassConfiguration.Players[rr].PlayerExp = newExp;
            }
        }

        public void OnPlayerExpUpdate(UnturnedPlayer player, uint newExp)
        {
            
            


            


        }

        public void OnPlayerConnect(UnturnedPlayer player)
        {


            string milk;
            using (StreamReader streamReader = new StreamReader("BattlePass.txt", true))
                milk = streamReader.ReadToEnd();

            var lorn = Regex.Match(milk, $"{player.CSteamID.ToString()},");
            if(lorn.Length == 0)
            {
                PassConfiguration.Players.Add(new PlayerInfo(player.CSteamID, 0, 230, new List<int>(),player.Experience)); // #$%
                
            }
            else
            {
                string str;
                using (StreamReader reader = new StreamReader("BattlePass.txt", true))
                    str = reader.ReadToEnd();

                var qmilk = Regex.Match(str, $"{player.CSteamID.ToString()},(.*):");

                string[] stri = qmilk.Value.Split(new char[] { ',' });

                int lvl = Convert.ToInt32(stri[1]);
                uint ExpToNewLvl = Convert.ToUInt32(stri[2]);

                List<int> uses = new List<int>();
                if (stri.Length > 4)
                {
                    for (int i = 3; i < stri.Length-1; i++)
                    {
                        uses.Add(Convert.ToInt32(stri[i]));
                    }
                }

                PassConfiguration.Players.Add(new PlayerInfo(player.CSteamID, lvl, ExpToNewLvl, uses,player.Experience));
            }

                
        }

        public void OnPlayerDisconect(UnturnedPlayer player)
        {
            string milk;
            using (StreamReader streamReader = new StreamReader("BattlePass.txt", true))
                milk = streamReader.ReadToEnd();

            var lorn = Regex.Match(milk, $"{player.CSteamID.ToString()},");


            int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == player.CSteamID);
            PlayerInfo PI = PassConfiguration.Players[rr];
            if (lorn.Length == 0)
            {
                
                Delay.DisconectNew(PI.Steamid, PI.Lvl, PI.ExpToNewLvl, PI.Uses, PassConfiguration.Players);
                
            }

            else
            {
                Delay.Disconect(PI.Steamid, PI.Lvl, PI.ExpToNewLvl, PI.Uses, PassConfiguration.Players);
                //PassConfiguration.Players.RemoveAt(rr);
            }
        }

        public void OnButtonClicked(Player player, string button)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);
            BattlePass.Instance.FileParse(uPlayer.CSteamID, out int lvl, out uint ExpToNewLvl, out List<int> uses);


            if (button == "CloseButton") 
            {
                uPlayer.Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, false);
                EffectManager.askEffectClearByID(8593, uPlayer.CSteamID);
            }

            foreach (var item in PassConfiguration.list)
            {
                if(button == item.Name && lvl >= item.Lvl && !uses.Contains(item.Lvl))
                {
                    uPlayer.GiveItem(item.ItemId, 1);
                    int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == uPlayer.CSteamID);
                    PassConfiguration.Players[rr].Uses.Add(item.Lvl);
                    EffectManager.sendUIEffectImageURL(1, uPlayer.CSteamID, true, item.Name, item.ItemImageUrlUsed);
                }
            }
        }

        public void OnEffectText(Player player, string button, string text)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);

        }

        public void FileParse(Steamworks.CSteamID steamID, out int lvl, out uint ExpToNewLvl, out List<int> uses)
        {
            int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == steamID);
            PlayerInfo PI = PassConfiguration.Players[rr];
            lvl = PI.Lvl;
            ExpToNewLvl = PI.ExpToNewLvl;
            uses = PI.Uses;

        }

        

        
    }
}
