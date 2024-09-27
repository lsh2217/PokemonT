using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    public class MonsterManager
    {
        public List<Monster> Monsters = new List<Monster>();
        public List<Monster> DungeonMonsters = new List<Monster>();
        public Dictionary<int, Monster> MonsterDic = new Dictionary<int, Monster>();
        public Dictionary<int, Monster> DungeonMonsterDic = new Dictionary<int, Monster>();
        public MonsterManager() 
        {
            Monsters.Add(new Monster(1, "피카츄", 1, 1, 10, 1, false));            
            Monsters.Add(new Monster(2, "파이리", 1, 3, 10, 2, false));
            Monsters.Add(new Monster(3, "꼬부기", 1, 5, 10, 3, false));
            Monsters.Add(new Monster(4, "이상해씨", 1, 7, 10, 4, false));
            Monsters.Add(new Monster(5, "찌리리공", 1, 9, 10, 1, false));
            Monsters.Add(new Monster(6, "식스테일", 1, 11, 10, 2, false));
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
