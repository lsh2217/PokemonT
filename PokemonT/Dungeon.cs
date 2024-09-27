using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    public class Dungeon
    {
        public int Id;
        List<Monster> Monsters = new List<Monster>();
        public int Monstercount;
        public int Level;
        public int Gold;
        //아이템 보상
        public int Exp;

        public Dungeon(int id, List<Monster> monsters, int monstercount, int level, int gold, int exp)
        {
            Id = id;
            Monsters = monsters;
            Monstercount = monstercount;
            Level = level;
            Gold = gold;
            Exp = exp;
        }
    }
}
