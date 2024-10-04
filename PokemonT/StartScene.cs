using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PokemonT
{
    // 캐릭터 이름 및 직업에 따른 값 설정
    internal class StartScene
    {
        Input CInput = new Input();
        StartAnimation animation = new StartAnimation();

        public Character CharacterSetting(Inventory inventory) 
        {
            Character player = SetData(inventory);
            return player;
        }
       
        public Character SetData(Inventory inventory) // 데이터 값 설정하는 함수 (캐릭터 클래스 변수 받아오기)
        {
            StartAnimation.PrintLogo();

            // 시작 대사
            Console.Clear();
            StartAnimation.Professor();
            Console.WriteLine("안녕! 포켓몬의 세계에 온 걸 환영해!");
            Console.WriteLine("나는 이 세상에서 '포켓몬'이라 불리는 생명체들을 연구하는 교수를 맡고 있어.");
            Console.WriteLine();
            Console.WriteLine();

            // 1. 플레이어 이름 입력 받기
            Console.WriteLine("먼저, 너의 이름을 알려줄래?");
            Console.Write(">> ");
            string name = Console.ReadLine();

            Console.Clear();
            StartAnimation.Professor();
            Console.WriteLine($"{name}, 멋진 이름이구나.");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("아무 키나 눌러 넘어가기");
            Console.ReadKey();

            // 2. 플레이어 초기 스탯 설정
            int atk = 0;
            int def = 0; // 체력으로 사용
            int hp = 100; // 체력 사용하지 않음
            int gold = 10000;


            // 3. 직업 선택
            ChooseJob selectedJob = new ChooseJob();
            string jobs = selectedJob.SelectJob();

            switch (jobs)
            {
                case "포켓몬 트레이너":
                    atk = 20;
                    def = 120;
                    // hp = 120;
                    break;

                case "포켓몬 브리더":
                    atk = 15;
                    def = 150;
                    // hp = 100;
                    break;

                case "포켓몬 연구원":
                    atk = 10;
                    def = 80;
                    // hp = 80;
                    break;
            }

           
            // 4. 객체 생성
            Character player = new Character(name,jobs, atk, def, hp, gold);

            // 5. 스타팅 포켓몬 선택
            ChooseStarting firstPokemon = new ChooseStarting();
            firstPokemon.ChooseFirstPokemon(inventory, player);

            return player;
        }

    }
}
