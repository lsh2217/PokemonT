using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static PokemonT.Inventory;

namespace PokemonT
{

    public class ChooseStarting  // 스타팅 포켓몬 고르기
    {
        Input CInput = new Input();
        Dictionary<string, (string description, int attack, int defense, Inventory.ItemType type)> FirstPokemons = new Dictionary<string, (string, int, int, Inventory.ItemType)>
        {
            { "이상해씨", ("등에 씨앗이 자라며 자연의 힘을 사용해 전투하는 풀 타입포켓몬입니다.", 7, 6, Inventory.ItemType.Equipment)},
            { "파이리", ("꼬리 끝의 불꽃이 꺼지지 않으며 강력한 불꽃 공격을 하는 불 타입 포켓몬입니다.", 10, 5, Inventory.ItemType.Equipment) },
            { "꼬부기", ("물을 발사해 적을 공격하는 물 타입 포켓몬입니다.", 5, 7, Inventory.ItemType.Equipment) }

        };


        public void ChooseFirstPokemon(Inventory inventory)
        {
                        
            // 세 포켓몬 초기화
            Console.Clear();
            StartAnimation.Professor();
            Console.WriteLine("이제 네가 모험을 시작할 때 필요한 첫 번째 포켓몬을 골라보자.");
            Console.WriteLine("여기 세 마리의 포켓몬이 있어. 그 중 하나를 선택할 수 있어. 잘 생각해봐!");
            Console.WriteLine();
            Console.WriteLine();

            int index = 1;
            foreach (var pokemon in FirstPokemons)
            {
                Console.WriteLine($"{index}. {pokemon.Key} | {pokemon.Value.description} | (공격력: {pokemon.Value.attack}, 방어력: {pokemon.Value.defense})");
                index++;
            }
            

            // 입력 받기
            Console.WriteLine();
            Console.WriteLine("원하는 포켓몬 번호를 선택해");
            Console.Write(">> ");
            int choice = CInput.CheckInput(1, FirstPokemons.Count);
            var chosenPokemonKey = FirstPokemons.Keys.ElementAt(choice - 1);
            var chosenPokemon = FirstPokemons[chosenPokemonKey];

            // 선택한 포켓몬을 인벤토리에 추가
            inventory.inventory.Add(chosenPokemonKey, (1, true, FirstPokemons[chosenPokemonKey].attack, FirstPokemons[chosenPokemonKey].defense));


            // 선택하지 않은 포켓몬들을 상점 아이템(InitializeShopItems)에 넣음
            foreach (var pokemon in FirstPokemons)
            {
                if (pokemon.Key != chosenPokemonKey)
                {
                    inventory.shopItems.Add(pokemon.Key, pokemon.Value);
                }
            }
            Console.Clear();
            StartAnimation.Professor();
            Console.WriteLine($"{chosenPokemonKey}를 선택했구나. 아주 멋진 선택이야!");
           
            // 선택한 포켓몬 ASCII 아트
            switch (chosenPokemonKey)
            {
                case "이상해씨":
                    StartAnimation.AsciiBulbasaur();
                    break;
                case "파이리":
                    StartAnimation.AsciiCharmander();
                    break;
                case "꼬부기":
                    StartAnimation.AsciiSquirtle();
                    break;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"이제 진정한 모험이 시작될 시간이야. 준비됐니? ");
            Console.WriteLine();
            Console.WriteLine(">> 아무 버튼이나 입력하여 모험 시작");
            Console.ReadKey();
            
        }  

    }
}
