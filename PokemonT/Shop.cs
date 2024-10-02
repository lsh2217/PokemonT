﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokemonT.Inventory;


namespace PokemonT
{
    public class Shop
    {
        MainScene mainScene;
        Quest quest;
        Input CInput = new Input();
        public void MainShop(MainScene displayMainUI, Quest questInfomation, int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems)
        {
            quest = questInfomation;
            mainScene = displayMainUI;
            while (true)
            {
                Console.Clear();
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
                    SellItem(gold, inventory, shopItems);
                }
                else if (choice == "0")
                {
                    mainScene.DisplayMainUI();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public void PurchaseItem(ref int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems)
        {
            Console.Clear();
            Console.WriteLine("\n구매할 아이템을 선택하세요:");
            int index = 1;
            foreach (var item in shopItems)
            {
                if (!inventory.ContainsKey(item.Key)) // 인벤토리에 없는 아이템만 보여줌
                {
                    Console.WriteLine($"{index++}. {item.Key}({item.Value.description}) | 가격: {item.Value.attack * 10} G");
                }
                else if (inventory.ContainsKey(item.Key))
                {
                    Console.WriteLine($"{index++}. {item.Key}({item.Value.description}) | 가격: {item.Value.attack * 10} G  판매완료");
                }
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


                    if (inventory.ContainsKey(selectedItem)) //인벤토리 1 2, 3안에 키가 있고 그 키를 상점의 키랑 비교 1,2,3,4,5,6,7
                    {
                        //inventory[selectedItem] = (inventory[selectedItem].count + 1, inventory[selectedItem].isEquipped);
                        //inventory[5] = {6,  변수명이 isEquipped 인거지 그냥 샀냐 ? 안샀냐 ?의 변수}
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    else
                    {
                        gold -= price;
                        inventory.Add(selectedItem, (1, false));
                        quest.tossQuestInformation_0();
                        quest.tossQuestInformation_1(price);
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

        public void SellItem(int gold, Dictionary<string, (int count, bool isEquipped)> inventory, Dictionary<string, (string description, int attack, int defense, ItemType type)> shopItems)
        {
            Console.Clear();
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
}
