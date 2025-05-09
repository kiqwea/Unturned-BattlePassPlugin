using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using BattlePassV1;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using static SDG.Provider.SteamGetInventoryResponse;

namespace BattlePassV1
{
    public class PassCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "bp";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "pass" };

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            BattlePass.Instance.FileParse(player.CSteamID, out int lvl, out uint ExpToNewLvl, out List<int> uses);

            
            EffectManager.sendUIEffect(8593, 1, player.CSteamID, true);
            player.Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, true);

            EffectManager.sendUIEffectText(1, player.CSteamID, true, "PlayerName", player.CharacterName);
            EffectManager.sendUIEffectText(1, player.CSteamID, true, "PlayerLvl", $"LVL: {lvl.ToString()}");
            EffectManager.sendUIEffectText(1, player.CSteamID, true, "PlayerExp", $"{ExpToNewLvl.ToString()}");
            EffectManager.sendUIEffectText(1, player.CSteamID, true, "INFO1", $"NEW LVL EVERY 230 EXP!");



            int rr = PassConfiguration.Players.FindIndex(x => x.Steamid == player.CSteamID);
            PlayerInfo pi = PassConfiguration.Players[rr];

            foreach (var item in PassConfiguration.list)
            {
                
                EffectManager.sendUIEffectImageURL(1, player.CSteamID, true, item.Name, item.ItemImageUrl);
            }

            for (int i = 1; i < PassConfiguration.list.Count+1; i++)
            {
                if(pi.Lvl >= i && !pi.Uses.Contains(i))
                {
                    EffectManager.sendUIEffectImageURL(1, player.CSteamID, true, PassConfiguration.list[i-1].Name, PassConfiguration.list[i-1].ItemImageUrl);
                }
                else if(pi.Lvl < i)
                {
                    EffectManager.sendUIEffectImageURL(1, player.CSteamID, true, PassConfiguration.list[i-1].Name, PassConfiguration.list[i-1].ItemImageUrlClosed);
                }
                else if (pi.Uses.Contains(i))
                {
                    EffectManager.sendUIEffectImageURL(1, player.CSteamID, true, PassConfiguration.list[i-1].Name, PassConfiguration.list[i-1].ItemImageUrlUsed);
                }
                else
                    EffectManager.sendUIEffectImageURL(1, player.CSteamID, true, PassConfiguration.list[i - 1].Name, PassConfiguration.list[i - 1].ItemImageUrlClosed);
            }

            if(command.Length > 0)
            {
                if (command[0] == "info")
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in uses)
                    {
                        sb.Append(item + " ");
                    }
                    UnturnedChat.Say(caller, $"{sb.ToString()}");
                }
            }
            
        }

        
    }
}

