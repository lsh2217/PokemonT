using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PokemonT
{
    public class Inventory
    {
        Shop shop = new Shop();   
        Input CInput = new Input();
        public Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems;
        public Dictionary<string, (int count, bool isEquipped)> inventory= new Dictionary<string, (int, bool)>();
        MainScene mainScene;

        public Inventory()
        {
            shopItems = InitializeShopItems(); // 상점 아이템 초기화
        }

        public void DisplayInventoryUI(Character player, MainScene main)
        {

            mainScene = main;

            Console.Clear();
            Console.WriteLine("\n=== 게임 ===");
            Console.WriteLine("1. 가방 보기");
            Console.WriteLine("0. 종료");
            Console.Write("원하시는 행동을 입력해주세요: ");
            
            int choice = CInput.CheckInput(0, 1);
            
            if (choice == 1)
            {
                ShowInventory(ref inventory);
            }
            else if (choice == 0)
            {
                mainScene.DisplayMainUI();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            
        }

        public enum ItemType
        {
            Equipment
        }

        public Dictionary<string, (string description, int attack, int defense, ItemType type)> InitializeShopItems()
        {
            return new Dictionary<string, (string description, int attack, int defense, ItemType type)>
            {
                { "리자드", ("날카로운 발톱을 가진 포켓몬입니다.", 12, 6, ItemType.Equipment) },
                { "어니부기", ("단단한 껍질을 가진 포켓몬입니다.", 6, 8, ItemType.Equipment) },
                { "이상해풀", ("큰 꽃을 가진 진화된 포켓몬입니다.", 14, 8, ItemType.Equipment) },
                { "리자몽", ("강력한 불꽃을 내뿜는 포켓몬입니다.", 16, 10, ItemType.Equipment) },
                { "거북왕", ("강력한 방어력을 가진 물 포켓몬입니다.", 14, 12, ItemType.Equipment) },
                { "이상해꽃", ("거대한 꽃을 가진 포켓몬입니다.", 18, 14, ItemType.Equipment) },
                { "피카츄", ("전기를 내뿜는 귀여운 포켓몬입니다.", 8, 4, ItemType.Equipment) },
                { "라이츄", ("빠른 속도를 자랑하는 전기 포켓몬입니다.", 12, 6, ItemType.Equipment) },
                { "꼬마돌", ("돌로 이루어진 작은 포켓몬입니다.", 9, 8, ItemType.Equipment) },
                { "뮤", ("신비로운 능력을 가진 포켓몬입니다.", 20, 15, ItemType.Equipment) },
                { "잉어킹", ("힘이 약한 물 포켓몬입니다.", 5, 5, ItemType.Equipment) },
                { "갸라도스", ("강력한 힘을 가진 물 포켓몬입니다.", 18, 12, ItemType.Equipment) }
            };
        }

        public void ShowInventory(ref Dictionary<string, (int count, bool isEquipped)> inventory)
        {
            Console.Clear();
            Console.WriteLine("\n=== 가방 ===");
            if (inventory.Count == 0)
            {
                Console.WriteLine("가방에 아이템이 없습니다.");
                return;
            }

            foreach (var item in inventory)
            {
                string equippedStatus = item.Value.isEquipped ? " (E)" : "";
                Console.WriteLine($"{item.Key} (x{item.Value.count}){equippedStatus}");
            }

            EquipItem(ref inventory); // 장비 아이템 장착 기능 추가
        }

        public void EquipItem(ref Dictionary<string, (int count, bool isEquipped)> inventory)
        {
            Console.WriteLine("\n장착할 장비 아이템을 선택하세요:");
            List<string> equipableItems = new List<string>();

            foreach (var item in inventory)
            {
                if (item.Value.isEquipped == false && item.Value.count > 0)
                {
                    equipableItems.Add(item.Key);
                    Console.WriteLine($"{equipableItems.Count}. {item.Key} (x{item.Value.count})");
                }
            }

            Console.WriteLine("0. 나가기");

            //string choice = Console.ReadLine();
            int itemIndex = CInput.CheckInput(0, inventory.Count);
            int choice = itemIndex;
            if (choice == 0) mainScene.DisplayMainUI();

            if (itemIndex > 0 && itemIndex <= equipableItems.Count)
            {
                var selectedItem = equipableItems[itemIndex - 1];
                inventory[selectedItem] = (inventory[selectedItem].count, true);
                Console.WriteLine($"{selectedItem}을(를) 장착했습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
               // EquipItem(ref inventory);
            }
        }

        
    }

    
}
