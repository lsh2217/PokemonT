using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PokemonT
{

    public class MainScene
    {
        Quest quest = new Quest();
        Battle Battle = new Battle();
        Input CInput = new Input();
        Inventory inventory = new Inventory();
        Shop shop = new Shop();
        StartScene startScene = new StartScene();
        Character player;
        StatusScene statusScene;



        public MainScene()
        {
            quest.QuestSet();
            player = startScene.CharacterSetting(inventory); // 스타트씬에서 선택한 캐릭터 초기값 세팅
            statusScene = new StatusScene(this); // MainScene 객체 초기화 및 전달
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
            Console.Write(">> ");


            int result = CInput.CheckInput(1,5); // 이동할 선택지 범위

            switch (result)
            {

                case 1:
                    statusScene.DisplayStatusUI(player);
                    break;
                case 2:
                    inventory.DisplayInventoryUI(player);
                    break;
                case 3:
                    shop.MainShop(this, quest,  player.PlayerGold, inventory.inventory , inventory.shopItems);
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
            Console.WriteLine("이름" + player.PlayerName);
            Console.WriteLine("스탯");
            Console.WriteLine("장착한 포켓몬");
            Console.WriteLine("퀘스트 달성 현황");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0을 눌러 메인 화면으로 이동하기");
            Console.Write(">> ");

            int result = CInput.CheckInput(0,0);

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
            Console.Write(">> ");

            int result = CInput.CheckInput(0, 0);

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
            Console.Write(">> ");

            int result = CInput.CheckInput(0, 0);

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
            Console.Write(">> ");

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
            Console.Write(">> ");

            int result = CInput.CheckInput(0, 0);

            switch (result)
            {

                case 0:
                    DisplayMainUI();
                    break;
            }
        }

    }

    
}
