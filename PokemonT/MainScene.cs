using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{

    public class MainScene
    {
        Quest quest = new Quest();
        Battle Battle = new Battle();


        public MainScene()
        {
            quest.QuestSet();
        }


        
        
        // 필요한 기능 : 
        // 선택한 화면으로 이동하기

        public void DisplayMainUI() // 메인 페이지 UI
        {
            
            Console.Clear();
            Console.WriteLine("포켓몬스터 T");
            Console.WriteLine("메인 화면");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("5. 전투");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("숫자를 눌러 원하는 곳으로 이동하기");
            Console.Write(">>");


            int result = CheckInput(1,5); // 이동할 선택지 범위

            switch (result)
            {

                case 1:
                    DisplayStatsUI();
                    break;
                case 2:
                    DisplayInventoryUI();
                    break;
                case 3:
                    DisplayStoreUI();
                    break;
                case 4:
                    quest.DisplayQuestUI();
                    break;
                case 5:
                    Battle.DisPlayBattelUI(this);
                    break;
            }
        }

        public void DisplayStatsUI() // 상태보기 UI
        {
            Console.Clear();
            Console.WriteLine("포켓몬스터 T");
            Console.WriteLine("상태보기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("이름");
            Console.WriteLine("스탯");
            Console.WriteLine("장착한 포켓몬");
            Console.WriteLine("퀘스트 달성 현황");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0을 눌러 메인 화면으로 이동하기");
            Console.Write(">>");

            int result = CheckInput(0,0);

            switch (result)
            {

                case 0:
                    DisplayMainUI();
                    break;
            }
        }

        public void DisplayInventoryUI() // 인벤토리 UI
        {
            Console.Clear();
            Console.WriteLine("포켓몬스터 T");
            Console.WriteLine("인벤토리");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("숫자를 눌러 원하는 행동하기");
            Console.Write(">>");

            int result = CheckInput(0, 0);

            switch (result)
            {

                case 0:
                    DisplayMainUI();
                    break;
            }
        }

        public void DisplayStoreUI() // 상점 UI
        {
            Console.Clear();
            Console.WriteLine("포켓몬스터 T");
            Console.WriteLine("상점");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("숫자를 눌러 원하는 행동하기");
            Console.Write(">>");

            int result = CheckInput(0, 0);

            switch (result)
            {

                case 0:
                    DisplayMainUI();
                    break;
            }
        }

        /*public void DisplayQuestUI() // 퀘스트 페이지 UI
        {
            Console.Clear();
            Console.WriteLine("포켓몬스터 T");
            Console.WriteLine("퀘스트");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("숫자를 눌러 원하는 행동하기");
            Console.Write(">>");

            int result = CheckInput(0, 0);

            switch (result)
            {

                case 0:
                    DisplayMainUI();
                    break;
            }
        }*/

        public void DisplayBattleUI() // 전투 화면 UI
        {
            Console.Clear();
            Console.WriteLine("포켓몬스터 T");
            Console.WriteLine("전투");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 스테이지 선택하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("숫자를 눌러 원하는 행동하기");
            Console.Write(">>");

            int result = CheckInput(0, 0);

            switch (result)
            {

                case 0:
                    DisplayMainUI();
                    break;
            }
        }

        public int CheckInput(int min, int max) // 이동 시 올바른 입력 판정
        {
            int result; // 입력값 받을 변수

            while (true) // 올바른 값 입력할 때까지 반복
            {
                String input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result); // 입력한 값이 숫자인지 문자인지 판정
                if (isNumber)
                {
                    if (result >= min && result <= max  ) // 입력할 숫자의 영역 설정
                        return result; // 입력값 반환 
                }
                Console.WriteLine("잘못된 입력입니다");
            }
            
        }

    }

    
}
