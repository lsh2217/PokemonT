using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokemonT.Inventory;

namespace PokemonT
{

    public class ChooseStarting  // 스타팅 포켓몬 고르기
    {
        Input CInput = new Input();
        Dictionary<string, (string description, int attack, int defense, Inventory.ItemType type)> FirstPokemons = new Dictionary<string, (string, int, int, Inventory.ItemType)>
        {
            { "이상해씨", ("꽃을 등에 지고 있는 포켓몬입니다.", 7, 6, Inventory.ItemType.Equipment)},
            { "파이리", ("불을 내뿜는 포켓몬입니다.", 10, 5, Inventory.ItemType.Equipment) },
            { "꼬북이", ("물속에서 사는 포켓몬입니다.", 5, 7, Inventory.ItemType.Equipment) }

        };


        public void ChooseFirstPokemon(Inventory inventory)
        {
            // 세 포켓몬 초기화
            Console.WriteLine("처음에 데리고 갈 포켓몬을 선택하세요:");
            int index = 1;

            foreach (var pokemon in FirstPokemons)
            {
                Console.WriteLine($"{index}. {pokemon.Key} (공격력: {pokemon.Value.attack}, 방어력: {pokemon.Value.defense})");
                index++;
            }

            // 입력 받기
            int choice = CInput.CheckInput(1, FirstPokemons.Count);
            var chosenPokemonKey = FirstPokemons.Keys.ElementAt(choice - 1);
            var chosenPokemon = FirstPokemons[chosenPokemonKey];

            // 선택한 포켓몬을 인벤토리에 추가
            inventory.inventory.Add(chosenPokemonKey, (1, true));


            // 선택하지 않은 포켓몬들을 상점 아이템(InitializeShopItems)에 넣음
            foreach (var pokemon in FirstPokemons)
            {
                if (pokemon.Key != chosenPokemonKey)
                {
                    inventory.shopItems.Add(pokemon.Key, pokemon.Value);
                }
            }
            Console.WriteLine($"{chosenPokemonKey}을(를) 선택했습니다!");
        }  

    }
}
