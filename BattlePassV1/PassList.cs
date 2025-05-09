using System;
namespace BattlePassV1
{
    public class PassList
    {
        public string Name { get; set; }
        public ushort ItemId { get; set; }

        public string ItemImageUrl { get; set; }
        public string ItemImageUrlUsed { get; set; }
        public string ItemImageUrlClosed { get; set; }

        public int Lvl { get; set; }
        public PassList Next { get; set; }
        public int Amount { get; set; }
        public bool Enable = false;
        public bool WasUsed = false;


        public PassList(string name, ushort itemid, int amount, string itemimageurl, string itemImageUrlUsed, string itemImageUrlClosed, int lvl)
        {
            Name = name;
            Lvl = lvl;
            ItemId = itemid;
            ItemImageUrl = itemimageurl;
            Amount = amount;
            ItemImageUrlUsed = itemImageUrlUsed;
            ItemImageUrlClosed = itemImageUrlClosed;
        }
    }
}

