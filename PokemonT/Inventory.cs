using System;
using System.Collections.Generic;

namespace PokemonT
{
    public class Inventory
    {
        public void DisplayInventoryUI()
        {
            int playerGold = 1000;
            Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems = InitializeShopItems();
            Dictionary<string, (int count, bool isEquipped)> inventory = InitializeInventory();

            while (true)
            {
                Console.WriteLine("\n=== 게임 ===");
                Console.WriteLine("1. 상점에 가기");
                Console.WriteLine("2. 가방 보기");
                Console.WriteLine("0. 종료");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Shop(ref playerGold, inventory, shopItems);
                }
                else if (choice == "2")
                {
                    ShowInventory(ref inventory);
                }
                else if (choice == "0")
                {
                    Console.WriteLine("게임을 종료합니다.");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        enum ItemType
        {
            Equipment
        }

        static Dictionary<string, (string description, int attack, int defense, ItemType type)> InitializeShopItems()
        {
            return new Dictionary<string, (string description, int attack, int defense, ItemType type)>
            {
                { "파이리", ("불을 내뿜는 포켓몬입니다.", 10, 5, ItemType.Equipment) },
                { "꼬북이", ("물속에서 사는 포켓몬입니다.", 5, 7, ItemType.Equipment) },
                { "이상해씨", ("꽃을 등에 지고 있는 포켓몬입니다.", 7, 6, ItemType.Equipment) },
                { "리자드", ("날카로운 발톱을 가진 포켓몬입니다.", 12, 6, ItemType.Equipment) },
                { "어니북이", ("단단한 껍질을 가진 포켓몬입니다.", 6, 8, ItemType.Equipment) },
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

        static Dictionary<string, (int count, bool isEquipped)> InitializeInventory()
        {
            return new Dictionary<string, (int, bool)>
            {
                { "파이리", (1, false) }
            };
        }

        static void ShowInventory(ref Dictionary<string, (int count, bool isEquipped)> inventory)
        {
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

        static void EquipItem(ref Dictionary<string, (int count, bool isEquipped)> inventory)
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
            string choice = Console.ReadLine();

            if (choice == "0") return;

            if (int.TryParse(choice, out int itemIndex) && itemIndex > 0 && itemIndex <= equipableItems.Count)
            {
                var selectedItem = equipableItems[itemIndex - 1];
                inventory[selectedItem] = (inventory[selectedItem].count, true);
                Console.WriteLine($"{selectedItem}을(를) 장착했습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void Shop(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems)
        {
            while (true)
            {
                Console.WriteLine("\n=== 상점 ===");
                Console.WriteLine($"[보유 골드] {gold} G");
                Console.WriteLine("[아이템 목록]");

                int index = 1;
                foreach (var item in shopItems)
                {
                    Console.WriteLine($"{index++}. {item.Key} | {item.Value.description} | 공격력: {item.Value.attack}, 방어력: {item.Value.defense}");
                }

                Console.WriteLine("0. 나가기");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    PurchaseItem(ref gold, inventory, shopItems);
                }
                else if (choice == "2")
                {
                    SellItem(ref gold, inventory, shopItems);
                }
                else if (choice == "0")
                {
                    Console.WriteLine("상점을 종료합니다.");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        static void PurchaseItem(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems)
        {
            Console.WriteLine("\n구매할 아이템을 선택하세요:");
            int index = 1;
            foreach (var item in shopItems)
            {
                Console.WriteLine($"{index++}. {item.Key} ({item.Value.description}) | 가격: {item.Value.attack * 10} G");
            }

            Console.WriteLine("0. 나가기");
            string choice = Console.ReadLine();

            if (choice == "0") return;

            if (int.TryParse(choice, out int itemIndex) && itemIndex > 0 && itemIndex <= shopItems.Count)
            {
                var selectedItem = new List<string>(shopItems.Keys)[itemIndex - 1];
                var itemDetails = shopItems[selectedItem];

                int price = itemDetails.attack * 10; // 가격을 공격력에 비례하게 설정

                if (gold >= price)
                {
                    gold -= price;
                    if (inventory.ContainsKey(selectedItem))
                    {
                        inventory[selectedItem] = (inventory[selectedItem].count + 1, inventory[selectedItem].isEquipped);
                    }
                    else
                    {
                        inventory.Add(selectedItem, (1, false));
                    }
                    Console.WriteLine($"{selectedItem}을(를) 구매했습니다.");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void SellItem(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems)
        {
            Console.WriteLine("\n판매할 아이템을 선택하세요:");
            int index = 1;
            foreach (var item in inventory)
            {
                Console.WriteLine($"{index++}. {item.Key} (x{item.Value.count})");
            }

            Console.WriteLine("0. 나가기");
            string choice = Console.ReadLine();

            if (choice == "0") return;

            if (int.TryParse(choice, out int itemIndex) && itemIndex > 0 && itemIndex <= inventory.Count)
            {
                var selectedItem = new List<string>(inventory.Keys)[itemIndex - 1];
                var itemCount = inventory[selectedItem].count;

                if (itemCount > 0)
                {
                    int price = (shopItems[selectedItem].attack * 10) / 2; // 판매 가격을 구매 가격의 절반으로 설정
                    gold += price;
                    if (itemCount == 1)
                    {
                        inventory.Remove(selectedItem);
                    }
                    else
                    {
                        inventory[selectedItem] = (itemCount - 1, inventory[selectedItem].isEquipped);
                    }
                    Console.WriteLine($"{selectedItem}을(를) 판매했습니다.");
                }
                else
                {
                    Console.WriteLine("판매할 수 있는 아이템이 없습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            inventory.DisplayInventoryUI();
        }
    }
}
