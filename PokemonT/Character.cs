using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    // 필요한 기능
    // 이름 입력
    // 직업 선택

    

    public class Character
    {
      
        // 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold
        
        public string PlayerName { get; set; }
        public string PlayerJob { get; set; }
        public int PlayerAtk { get; set; }
        public int PlayerDef { get; set; } // 체력으로 대체
        public int PlayerHp { get; set; }
        public int PlayerGold { get; set; }

        // 몬스터 장착으로 인한 추가 공격력 / 추가 방어력 
        public static int ExtraAtk { get; set; }
        public static int ExtraDef { get; set; }


        // 플레이어가 들고 시작할 아이템
        Dictionary<string, (int count, bool isEquipped)> StartingInventory { get; set; }


        // 캐릭터 클래스 생성자
        public Character(string name, string job, int atk, int def, int hp, int gold) 
        {
            PlayerName = name;
            PlayerJob = job;
            PlayerAtk = atk;
            PlayerDef = def;
            // PlayerHp = hp;
            PlayerGold = gold;

            StartingInventory = new Dictionary<string, (int, bool)>();
        }

        // 포켓몬 장착 후 추가 공격력/방어력 설정
        public void SetEquipPokemonStat(Inventory inventory, string pokemonName)
        {
            if (inventory.inventory.ContainsKey(pokemonName))
            {
                
                // 포켓몬 공격력과 방어력 추출
                var pokemon = inventory.inventory[pokemonName];

                ExtraAtk = pokemon.attack;
                ExtraDef = pokemon.defence;
            }
        }

        // 각 페이지에 해당하는 함수에 호출할 함수들
        // 스테이터스 - 캐릭터 스탯 표시 함수
        public void DisplayCharacterStatus() 
        {
            Console.WriteLine($"- 이름 : {PlayerName} ({PlayerJob})" );
            Console.WriteLine(ExtraAtk == 0 ? $"- 공격력 : {PlayerAtk}" : $"- 공격력 : {PlayerAtk} (+{ExtraAtk})");
            Console.WriteLine(ExtraDef == 0 ? $"- 체력 : {PlayerDef}" : $"- 체력 : {PlayerDef} (+{ExtraDef})");
            // Console.WriteLine($"- 체력 : {PlayerHp}");
            Console.WriteLine($"- 골드 : {PlayerGold} G");
        } 


    }
}
