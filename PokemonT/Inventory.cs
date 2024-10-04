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
        public Dictionary<string, (int count, bool isEquipped , int attack, int defence )> inventory= new Dictionary<string, (int, bool,int,int)>();
        public Dictionary<int, string> Equippeditems = new Dictionary<int, string>();
        int EquippedNum = 1;
        int unEquipNum = 0;
        int reOrderbyCount = 0;
        
        MainScene mainScene;
        

        public Inventory()
        {
            shopItems = InitializeShopItems(); // 상점 아이템 초기화
            foreach (var item in inventory)
            {
                if (!Equippeditems.ContainsValue(item.Key) && item.Value.isEquipped && EquippedNum <= 6)
                {
                    Equippeditems.Add(EquippedNum++, item.Key);
                }
            }
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
                ShowInventory(ref inventory, mainScene);
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

        public void ShowInventory(ref Dictionary<string, (int count, bool isEquipped, int attack, int defence)> inventory, MainScene Main)
        {
            mainScene = Main;
            unEquipNum = 0;
            Console.Clear();
            Console.WriteLine("\n=== 가방 ===");
            if (inventory.Count == 0)
            {
                Console.WriteLine("가방에 아이템이 없습니다.");
                Thread.Sleep(1000);
                mainScene.DisplayMainUI();
            }

            foreach (var item in inventory)
            {
                if(!Equippeditems.ContainsValue(item.Key) && item.Value.isEquipped && EquippedNum <= 6)
                {
                     Equippeditems.Add(EquippedNum++, item.Key);                    
                }
            }
            
            foreach (KeyValuePair<int, string> item in Equippeditems) // 여기를 수정해야되네
            {
                
                string equippedStatus = inventory[item.Value].isEquipped ? " (E)" : "";
                Console.WriteLine($"{unEquipNum-1}. ({item.Value}){equippedStatus}");
                unEquipNum--;
            }
            EquipItem(ref inventory); // 장비 아이템 장착 기능 추가
        }

        public void EquipItem(ref Dictionary<string, (int count, bool isEquipped, int attack, int defence)> inventory)
        {
            Console.WriteLine("\n장착할 장비 아이템을 선택하세요:");
            List<string> equipableItems = new List<string>();
            

            foreach (var item in inventory)
            {
                if (item.Value.isEquipped == false && item.Value.count > 0)
                {
                    equipableItems.Add(item.Key);
                    Console.WriteLine($"{equipableItems.Count}. {item.Key}");
                }
            }

            Console.WriteLine("0. 나가기");

            //string choice = Console.ReadLine();
            int itemIndex = CInput.CheckInput(-inventory.Count, inventory.Count);
            int choice = itemIndex;
            if (choice == 0) mainScene.DisplayMainUI();
            if (EquippedNum > 6 && itemIndex <= equipableItems.Count && itemIndex >0) // ?? 누구세요..?
            {
                Console.WriteLine("착용한 장비를 선택해주세요.");
                int EquipIndex = CInput.CheckInput(1, Equippeditems.Count);
                var selectedItem = equipableItems[itemIndex - 1];
                inventory[selectedItem] = (inventory[selectedItem].count, true, inventory[selectedItem].attack, inventory[selectedItem].defence);

                var selectedEquippeditem = Equippeditems[EquipIndex];
                inventory[selectedEquippeditem] = (inventory[selectedEquippeditem].count, false, inventory[selectedEquippeditem].attack, inventory[selectedEquippeditem].defence);
                Equippeditems[EquipIndex] = selectedItem;


                ShowInventory(ref inventory, mainScene);
            }
            else if (itemIndex > 0 && itemIndex <= equipableItems.Count)
            {
                var selectedItem = equipableItems[itemIndex - 1];
                inventory[selectedItem] = (/*inventory[selectedItem].count*/1, true, inventory[selectedItem].attack, inventory[selectedItem].defence);

                // 장착한 포켓몬 값 플레이어 스탯값에 추가
                Character.ExtraAtk += inventory[selectedItem].attack;
                Character.ExtraDef += inventory[selectedItem].defence;

                Console.WriteLine($"{selectedItem}을(를) 장착했습니다.");
                ShowInventory(ref inventory, mainScene);

            } else if (itemIndex <0 && itemIndex >= unEquipNum ) //삭제 구현
            {
                string selectedItem = "";
                foreach (KeyValuePair<int, string> item in Equippeditems)
                {
                    if (item.Key == -itemIndex ) {
                       selectedItem = item.Value;
                    }
                }
                    
                //var selectedItem = Equippeditems[-itemIndex-1];
                inventory[selectedItem] = (/*inventory[selectedItem].count*/1, false, inventory[selectedItem].attack, inventory[selectedItem].defence);

                Character.ExtraAtk -= inventory[selectedItem].attack;
                Character.ExtraDef -= inventory[selectedItem].defence;
                Equippeditems.Remove(-itemIndex);
                EquippedNum--;
                //Equippeditems = Equippeditems.OrderBy(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
                reOrderbyCount = Equippeditems.Count;

                for (int i = 0; i < reOrderbyCount+itemIndex; i++) 
                {
                    RenameEquippeditems(Equippeditems, -itemIndex + 1 + i, -itemIndex+i);
                }
                    ShowInventory(ref inventory, mainScene);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                ShowInventory(ref inventory, mainScene);
            }
        }


        public void RenameEquippeditems<TKey, TValue>(IDictionary<TKey,TValue> dic, TKey fromKey , TKey toKey ) {
                TValue value = dic[fromKey];
                dic.Remove(fromKey);
                dic[toKey] = value;
        }
    }

    
}
