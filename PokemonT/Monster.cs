using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public int Level {  get; set; }
        public int Attack { get; set; }
        public int Hp {  get; set; }
        public int Type {  get; set; }
        public bool Die { get; set; }
        public Monster(int id, string name,  int level, int attack, int hp, int type, bool die) 
        {
            Id = id;
            Name = name;
            Level = level;
            Attack = attack;
            Hp = hp;
            Type = type;
            Die = die;
        }
    }
}
