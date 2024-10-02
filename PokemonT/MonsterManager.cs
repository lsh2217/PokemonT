using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokemonT.Inventory;

namespace PokemonT
{
    public class MonsterManager
    {
        public List<Monster> Monsters = new List<Monster>();
        public List<Monster> DungeonMonsters = new List<Monster>();
        public Dictionary<int, Monster> MonsterDic = new Dictionary<int, Monster>();
        public Dictionary<int, Monster> DungeonMonsterDic = new Dictionary<int, Monster>();
        Inventory CInvertory = new Inventory();
        public MonsterManager() 
        {
            Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems = CInvertory.InitializeShopItems();
            foreach(var item in shopItems)
            Monsters.Add(new Monster(item.Key, item.Key, item.Value.attack, item.Value.defense,false));

            foreach(Monster monster in Monsters)
            {
                DungeonMonsters.Add(monster);
            }
            
        }
    }
    enum MonsterType
    {
        Electric,
        Fire,
        Water,
        Grass
    }
}
