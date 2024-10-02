using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    // 필요한 기능
    // 캐릭터의 정보 표시 
    // 장비 반영에 따른 정보 - 장착한 포켓몬
    // 퀘스트 달성 현황 확인


    public class StatusScene
    {
        Input CInput = new Input();
        public MainScene mainScene;
        Character player;


        public StatusScene(MainScene mainScene) // MainScene 인스턴스 받기
        {
            this.mainScene = mainScene;
        }


        public void DisplayStatusUI(Character player) // 상태보기 UI - 메인 씬 중복 내용 추후 삭제
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("<<상태보기>>");
            Console.WriteLine();
            player.DisplayCharacterStatus();

            Console.WriteLine("장착한 포켓몬");
            Console.WriteLine();
            // DisplayEquippedItem() 호출
            Console.WriteLine();
            Console.WriteLine("퀘스트 달성 현황");
            // 퀘스트 현황 호출
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0을 눌러 메인 화면으로 이동하기");
            Console.Write(">> ");

            int result = CInput.CheckInput(0, 0);

            switch (result)
            {

                case 0:
                    mainScene.DisplayMainUI();
                    break;
            }


        }

    }

}
