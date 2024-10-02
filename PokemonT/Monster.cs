using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    public class Monster
    {
        public string Id { get; set; }
        public string Name {  get; set; }
        public int Attack { get; set; }
        public int Hp {  get; set; }
        public bool Die { get; set; }
        public Monster(string id, string name, int attack, int hp, bool die) 
        {
            Id = id;
            Name = name;
            Attack = attack;
            Hp = hp;
            Die = die;
        }
    }
}
