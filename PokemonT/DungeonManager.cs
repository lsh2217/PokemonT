using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    public class DungeonManager
    {
        public List<Dungeon> Dungeons = new List<Dungeon>();      


        public DungeonManager(List<Monster> monster)
        {
            Dungeons.Add(new Dungeon(1, monster, 2, 1, 1000, 1));
            Dungeons.Add(new Dungeon(2, monster, 3, 2, 2000, 2));
            Dungeons.Add(new Dungeon(3, monster, 4, 3, 3000, 3));
            Dungeons.Add(new Dungeon(4, monster, 5, 4, 4000, 4));
            Dungeons.Add(new Dungeon(5, monster, 6, 5, 5000, 5));
        }
    }

}
