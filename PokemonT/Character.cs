using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public int PlayerDef { get; set; }
        public int PlayerHp { get; set; }
        public int PlayerGold { get; set; }

        // 몬스터 장착으로 인한 추가 공격력 / 추가 방어력 
        public int ExtraAtk { get; private set; }
        public int ExtraDef { get; private set; }


        // 플레이어가 들고 시작할 아이템
        Dictionary<string, (int count, bool isEquipped)> StartingInventory { get; set; }


        // 캐릭터 클래스 생성자
        public Character(string name, string job, int atk, int def, int hp, int gold) 
        {
            PlayerName = name;
            PlayerJob = job;
            PlayerAtk = atk;
            PlayerDef = def;
            PlayerHp = hp;
            PlayerGold = gold;

            StartingInventory = new Dictionary<string, (int, bool)>();
        }

        // 각 페이지에 해당하는 함수에 호출할 함수들
        // 스테이터스 - 캐릭터 스탯 표시 함수
        public void DisplayCharacterStatus() 
        {
            Console.WriteLine($"- 이름 {PlayerName} ({PlayerJob})" );
            Console.WriteLine();
            Console.WriteLine(ExtraAtk == 0 ? $"- 공격력 : {PlayerAtk}" : $"- 공격력 : {PlayerAtk} (+{ExtraAtk})");
            Console.WriteLine();
            //Console.WriteLine(ExtraAtk == 0 ? $"- 방어력 : {PlayerDef}" : $"- 방어력 : {PlayerDef} (+{ExtraDef})");
            Console.WriteLine($"- 체력 {PlayerHp}");
            Console.WriteLine();
            Console.WriteLine($"- 골드 {PlayerGold} G");
        } 

    }
}
