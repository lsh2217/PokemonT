using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    // 캐릭터 이름 및 직업에 따른 값 설정
    internal class StartScene
    {
        Input CInput = new Input();
        public void CharacterSetting() 
        {
            Character player = SetData();
            
        }
       
        public Character SetData() // 데이터 값 설정하는 함수 (캐릭터 클래스 변수 받아오기)
        {
            // 1. 플레이어 이름 입력 받기
            Console.Write("플레이어 이름을 입력하세요: ");
            string name = Console.ReadLine();

            // 2. 플레이어 초기 스탯 설정
            int atk = 0;
            int def = 0;
            int hp = 100;
            int gold = 10000;


            // 3. 직업 선택
            string jobs = ChooseJob();

            switch (jobs)
            {
                case "풀 타입":
                    atk = 12;
                    def = 8;
                    hp = 90;
                    break;

                case "물 타입":
                    atk = 10;
                    def = 10;
                    hp = 100;
                    break;

                case "불 타입":
                    atk = 14;
                    def = 6;
                    hp = 110;
                    break;
            }

            // 3. 스타팅 포켓몬 선택
            Monster firstPokemon = ChooseFirstPokemon();


            // 객체 생성
            Character Player = new Character(name,jobs, atk, def, hp, gold);
            return Player;
        }


        public string ChooseJob() 
        {
            List<string> jobs = new List<string>() { "풀 타입", "물 타입", "불 타입" };
            Console.WriteLine("트레이너의 타입을 선택하세요:");        
            for (int i = 0; i < jobs.Count; i++) // 타입 출력
            {
                Console.WriteLine($"{i + 1}. {jobs[i]}");
            }

            int choice = CInput.CheckInput(1, jobs.Count);

            //선택한 숫자에 따라 직업 할당 (switch문)
            string playerJob = string.Empty;
            switch (choice)
            {
                case 1:
                    playerJob = jobs[0]; // "풀 타입"
                    break;
                case 2:
                    playerJob = jobs[1]; // "물 타입"
                    break;
                case 3:
                    playerJob = jobs[2]; // "불 타입"
                    break;
            }
            return playerJob;
        }

        // 스타팅 포켓몬 선택 함수
        public Monster ChooseFirstPokemon()
        {
            List<Monster> FirstMon = new List<Monster>();
            {
                // new Monster("이상해씨")
                // new Monster("꼬부기")
                // new Monster("파이리")
            };

            Console.WriteLine("처음에 데리고 갈 포켓몬을 선택하세요:");
            for (int i = 0; i < FirstMon.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {FirstMon[i].Name} (공격력: {FirstMon[i].Attack}"); // 방어력 추가해야하는데 Monster에 없음
            }

            int choice = CInput.CheckInput(1, FirstMon.Count);
            return FirstMon[choice - 1];

        }

    }
}
