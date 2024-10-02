using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("오박사 : ");
            Console.WriteLine("안녕! 포켓몬의 세계에 온 걸 환영해!");
            Console.WriteLine("나는 이 세상에서 '포켓몬'이라 불리는 생명체들을 연구하는 교수를 맡고 있어.");
            Console.WriteLine();
            Console.WriteLine();

            // 1. 플레이어 이름 입력 받기
            Console.WriteLine("먼저, 너의 이름을 알려줄래?");
            Console.Write(">> ");
            string name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("오박사 : ");
            Console.WriteLine($"{name}, 멋진 이름이구나.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("아무 키나 눌러 넘어가기");
            Console.ReadKey();

            // 2. 플레이어 초기 스탯 설정
            int atk = 0;
            int def = 0;
            int hp = 100;
            int gold = 10000;


            // 3. 직업 선택
            ChooseJob selectedJob = new ChooseJob();
            string jobs = selectedJob.SelectJob();

            switch (jobs)
            {
                case "포켓몬 트레이너":
                    atk = 20;
                    def = 10;
                    hp = 120;
                    break;

                case "포켓몬 브리더":
                    atk = 15;
                    def = 15;
                    hp = 100;
                    break;

                case "포켓몬 연구원":
                    atk = 10;
                    def = 20;
                    hp = 80;
                    break;
            }

            // 4. 스타팅 포켓몬 선택
            ChooseStarting firstPokemon = new ChooseStarting();
            firstPokemon.ChooseFirstPokemon(inventory);

            // 객체 생성
            Character Player = new Character(name,jobs, atk, def, hp, gold);
            return Player;
        }

    }
}
