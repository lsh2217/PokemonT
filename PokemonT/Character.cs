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

    

    internal class Character
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


        // 플레이어가 들고 있을 아이템
        // public static List<아이템클래스> 인벤토리 = new List<아이템클래스>();  인벤토리에 넣은 아이템 리스트
        // public static List<아이템클래스> 장착한리스트 = new List<아이템클래스> ; 장착한 아이템 리스트


        // 캐릭터 클래스 생성자
        public Character(string name, string job, int atk, int def, int hp, int gold) 
        {
            PlayerName = name;
            PlayerJob = job;
            PlayerAtk = atk;
            PlayerDef = def;
            PlayerHp = hp;
            PlayerGold = gold;
        }

        // 각 페이지에 해당하는 함수에 호출할 함수들
        // 스테이터스 - 캐릭터 스탯 표시 함수
        public void DisplayCharacterStatus() 
        {
            Console.WriteLine($"이름 {PlayerName} {(PlayerJob)}" );
            Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {PlayerAtk}" : $"공격력 : {PlayerAtk} (+{ExtraAtk})");
            Console.WriteLine(ExtraAtk == 0 ? $"방어력 : {PlayerDef}" : $"공격력 : {PlayerDef} (+{ExtraDef})");
            Console.WriteLine($"체력 {PlayerHp}");
            Console.WriteLine($"골드 {PlayerGold} G");
            Console.WriteLine("장착한 포켓몬");
            Console.WriteLine("퀘스트 달성 현황");
        }

        // 인벤토리 표시 함수
        public void DisplayInventory()
        {
           // 인벤토리 리스트 출력 내용
           // for (int i = 0; i < 인벤토리리스트.Count; i++) 
           // {
           //  Monster 선택한아이템 = 인벤토리[i];
           //   
           //  string display장착아이템 = 장착아이템리스트.Contains(선택한아이템)  ? "E" : "";
           //  Console.WriteLine("지닌 포켓몬");
           //
           // }
        }

        // 장착한 아이템(포켓몬) 표시 함수
        public void DisplayEquippedItem() 
        {
            // 장착한 아이템 리스트 출력
            // for (int i = 0; i < 장착한 아이템리스트.Count; i++) 
            // {
            //  Monster 장착한아이템 = 장착한아이템[i];
            //  Console.WriteLine("함께 싸울 포켓몬");
            //
            // }

        }

        // 아이템 장착 함수
        public void EquipItem() // 몬스터(아이템) 클래스로 매개변수 넣어야 함
        {
            // 장착 여부 판단 
            // 캐릭터 클래스에 아이템 값만큼 더하고 빼주기 (프로퍼티 활용함)
            // if(IsEquipped(몬스터클래스변수명))
            // {
            //  장착한리스트.Remove(몬스터 클래스 변수명);
            //  if(아이템 타입에 따른 조건)
            //      ExtraAtk -= 몬스터클래스변수명.Value; 스탯 변수
            //  else
            //      ExtraDef -= 몬스터클래스변수명.Value;
            //  }
            //  else
            //  {
            //  장착한리스트.Add(몬스터 클래스 변수명);
            //  if(아이템 타입에 따른 조건)
            //      ExtraAtk += 몬스터클래스변수명.Value; 스탯 변수
            //  else
            //      ExtraDef += 몬스터클래스변수명.Value;
            //  }
        }

        // 장비 장착 확인용 함수
        public bool IsEquipped() // 몬스터(아이템) 클래스로 매개변수 넣어야 함
        {
            return false; // 임시로 설정. 아래 내용으로 변경 
            // return 장착한아이템리스트.Contains(몬스터클래스변수명);
        }

        // 아이템 구매 함수
        public void BuyItem()  // 몬스터(아이템) 클래스로 매개변수 넣어야 함
        {
            // PlayerGold -= 몬스터클래스변수명.가격변수;
            // 인벤토리아이템리스트.Add(몬스터클래스변수명)
        }

        // 아이템 보유 확인 함수
        public bool HasItem() // 몬스터(아이템) 클래스로 매개변수 넣어야 함
        {
            return false; // 임시
            // return 인벤토리아이템리스트.Contains(몬스터클래스변수명);
        }

    }
}
