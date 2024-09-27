using System;
namespace poke
{
    public class Inventory
    {

        public void DisplayInventoryUI()
        {



            int playerGold = 1000;
            Dictionary<string, (string description, int price, ItemType type)> shopItems = InitializeShopItems();
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
            Healing, // 회복 아이템
            Equipment // 장비 아이템
        }

        static Dictionary<string, (string description, int price, ItemType type)> InitializeShopItems()
        {
            return new Dictionary<string, (string description, int price, ItemType type)>
            {
                // 회복 아이템
                { "체력 물약", ("체력을 50 회복합니다.", 100, ItemType.Healing) },
                { "마나 물약", ("마나를 30 회복합니다.", 150, ItemType.Healing) },
                { "대형 체력 물약", ("체력을 150 회복합니다.", 300, ItemType.Healing) },

                // 장비 아이템
                { "수련자 갑옷", ("방어력 +5", 200, ItemType.Equipment) },
                { "무쇠갑옷", ("방어력 +10", 350, ItemType.Equipment) },
                { "스파르타의 갑옷", ("방어력 +15", 500, ItemType.Equipment) },
                { "전설의 검", ("공격력 +10", 300, ItemType.Equipment) },
                { "마법사의 지팡이", ("마나 공격력 +5", 400, ItemType.Equipment) },
                { "궁수의 활", ("공격력 +7", 250, ItemType.Equipment) }
            };
        }

        static Dictionary<string, (int count, bool isEquipped)> InitializeInventory()
        {
            return new Dictionary<string, (int, bool)>
            {
                { "체력 물약", (5, false) }
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

        static void Shop(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int price, ItemType type)> shopItems)
        {
            while (true)
            {
                Console.WriteLine("\n=== 상점 ===");
                Console.WriteLine($"[보유 골드] {gold} G");
                Console.WriteLine("[아이템 목록]");

                int index = 1;
                foreach (var item in shopItems)
                {
                    Console.WriteLine($"{index++}. {item.Key} | {item.Value.description} | {item.Value.price} G");
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

        static void PurchaseItem(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int price, ItemType type)> shopItems)
        {
            Console.WriteLine("\n구매할 아이템을 선택하세요:");
            int index = 1;
            foreach (var item in shopItems)
            {
                Console.WriteLine($"{index++}. {item.Key} ({item.Value.price} G)");
            }

            Console.WriteLine("0. 나가기");
            string choice = Console.ReadLine();

            if (choice == "0") return;

            if (int.TryParse(choice, out int itemIndex) && itemIndex > 0 && itemIndex <= shopItems.Count)
            {
                var selectedItem = new List<string>(shopItems.Keys)[itemIndex - 1];
                var itemDetails = shopItems[selectedItem];

                if (gold >= itemDetails.price)
                {
                    gold -= itemDetails.price;
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

        static void SellItem(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int price, ItemType type)> shopItems)
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
                    Console.Write($"판매할 {selectedItem}의 수량을 입력하세요 (최대 {itemCount}): ");
                    string quantityInput = Console.ReadLine();

                    if (int.TryParse(quantityInput, out int sellCount) && sellCount > 0 && sellCount <= itemCount)
                    {
                        int sellPrice = (int)(0.75 * shopItems[selectedItem].price);
                        gold += sellPrice * sellCount;
                        inventory[selectedItem] = (itemCount - sellCount, inventory[selectedItem].isEquipped);

                        if (inventory[selectedItem].count <= 0)
                        {
                            inventory.Remove(selectedItem);
                        }

                        Console.WriteLine($"{sellCount}개 {selectedItem}을(를) {sellPrice * sellCount} G에 판매했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 수량입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("해당 아이템이 없습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}