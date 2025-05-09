using System;
using System.Collections.Generic;
using Rocket.Unturned.Player;

namespace BattlePassV1
{
    public class PlayerInfo
    {
        public List<int> Uses = new List<int>();
        public int Lvl;
        public uint ExpToNewLvl;
        public Steamworks.CSteamID Steamid;
        public uint PlayerExp = 0;

        public PlayerInfo(Steamworks.CSteamID steamid, int lvl, uint exptonewlvl, List<int> uses, uint exp)
        {
            Steamid = steamid;
            Lvl = lvl;
            ExpToNewLvl = exptonewlvl;
            Uses = uses;
            PlayerExp = exp;
        }

        
    }
}

