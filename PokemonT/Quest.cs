using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{

    public struct ReturnValues
    {
        public string returnValueItem;
        public int returnValueGold;
        public bool returnValueClear;
        public int returnValueItemAttack;
        public int returnValueItemDefence;
    };
    public class Quest
    {
        MainScene mainScene;
        Input CInput = new Input();
        ReturnValues returnValues = new ReturnValues();
        List<int> acceptQuestList = new List<int>();
        Dictionary<int, QuestInformation> allQuestList = new Dictionary<int, QuestInformation>();
        Character player;
        Battle battle;
        Inventory inventory;

        int acceptQuestListNumber;
        int questListNumber;
        int playerChoose;
        int monsterDeathCountQ2;
        int monsterDeathCountQ3;
        int useGoldAmount;
        bool isEquipQuest = false;
        bool retry = false;

        Dictionary<string, (string description, int attack, int defense, Inventory.ItemType type)> rewardPoketmon = new Dictionary<string, (string, int, int, Inventory.ItemType)>
        {
            { "캐터피", ("입에서 끈적한 실을 내뿜는 애벌레 모습의 풀 타입포켓몬입니다.", 2, 2, Inventory.ItemType.Equipment)},
            { "고오스", ("옅은 가스 같은 몸을 하고있는 고스트 타입 포켓몬입니다.", 10, 10, Inventory.ItemType.Equipment) }
        };

        public void DisplayQuestUI(MainScene displayMainUI, Character character, Inventory inv)
        {
            mainScene = displayMainUI;
            player = character;
            inventory = inv;
            Console.Clear();
            questListNumber = 0;
            Console.WriteLine("수락 가능한 임무 목록");
            Console.WriteLine("");
            for (int i = 0; i < allQuestList.Count; i++)
            {
                if (acceptQuestList.Contains(allQuestList[i].QuestId) && !allQuestList[i].QuestIsClear)
                {
                    Console.WriteLine(questListNumber + 1 + ". " + allQuestList[i].QuestName + "  [임무 진행중..]");
                    Console.WriteLine();
                    questListNumber++;
                }
                else if (!acceptQuestList.Contains(allQuestList[i].QuestId) && !allQuestList[i].QuestIsClear)
                {
                    Console.WriteLine(questListNumber + 1 + ". " + allQuestList[i].QuestName);
                    Console.WriteLine();
                    questListNumber++;
                }
                else
                {
                    Console.WriteLine(questListNumber + 1 + ". " + allQuestList[i].QuestName + "  [임무 완료!]");
                    Console.WriteLine();
                    questListNumber++;
                }
            }
            Console.WriteLine("0. 나가기\n");
            Console.Write(">>");

            playerChoose = CInput.CheckInput(0, allQuestList.Count);

            if ((playerChoose - 1) < questListNumber && playerChoose > 0
                 && playerChoose != 0 && !acceptQuestList.Contains(allQuestList[playerChoose - 1].QuestId) && !allQuestList[playerChoose - 1].QuestIsClear)
            {
                QuestInformationDetailView(playerChoose - 1);
            }
            else if (playerChoose == 0)
            {
                mainScene.DisplayMainUI();
            }
            else if ((playerChoose - 1) < questListNumber && playerChoose > 0
                && playerChoose != 0 && acceptQuestList.Contains(allQuestList[playerChoose - 1].QuestId))
            {
                AcceptQuestInformationDetailView(playerChoose - 1);
            }
            else
            {

            }

        }


        //모든 퀘스트의 정보를 저장하는 allQuestList 객체 생성
        public void QuestSet()
        {
            List<QuestInformation> questValue = new List<QuestInformation>();

            questValue.Add(new QuestInformation(0, "포켓몬 구매하기", 0, "상점에서 포켓몬 1마리를 구매하세요.", 1000, null, false));
            questValue.Add(new QuestInformation(1, "Gold 사용하기", 0, "500Gold를 소모하세요", 1000, null, false));
            questValue.Add(new QuestInformation(2, "포켓몬 전투에서 승리", 0, "전투에서 상대 아무포켓몬 2마리를 전투불능으로 만드세요", 0, "캐터피", false));
            questValue.Add(new QuestInformation(3, "포켓몬 전투에서 대승리", 1, "전투에서 상대 아무포켓몬 8마리를 전투불능으로 만드세요", 1500, "고오스", false));
            //questValue.Add(new QuestInformation(4, "진화의 돌 사용", 1, "아무포켓몬에게 진화의 돌을 사용하여 진화시켜 보세요", 0, 2, "진화의 돌"));


            foreach (QuestInformation quest in questValue)
            {
                allQuestList.Add(quest.QuestId, quest);
            }



        }

        //모든 퀘스트 정보에서 해당 넘버값의 퀘스트정보를 수락퀘스트 배열인 acceptQuestList에 추가
        public void AcceptQuestSet(int i)
        {
            acceptQuestList.Add(allQuestList[i].QuestId);
            retry = false;
            acceptQuestListNumber++;
            DisplayQuestUI(mainScene, player, inventory);
        }

        public void tossQuestInformation_0()
        {
            if (acceptQuestList.Contains(0))
            {
                isEquipQuest = true;
            }

        }
        public void tossQuestInformation_1(int useGold)
        {
            if (acceptQuestList.Contains(1))
            {
                useGoldAmount += useGold;
            }
        }
        public void tossQuestInformation_2(int deathCount)
        {
            if (acceptQuestList.Contains(2))
            {
                monsterDeathCountQ2 += deathCount;
            }
        }
        public void tossQuestInformation_3(int deathCount)
        {

            if (acceptQuestList.Contains(3))
            {
                monsterDeathCountQ3 += deathCount;
            }
        }
        public void tossQuestInformation_4()
        {
            if (acceptQuestList.Contains(4))
            {
            }
        }

        public ReturnValues QuestCheck_0()
        {
            if (isEquipQuest == true)
            {
                returnValues.returnValueItem = allQuestList[0].QuestItem;
                returnValues.returnValueGold = allQuestList[0].QuestGold;
                returnValues.returnValueClear = true;
                returnValues.returnValueItemAttack = allQuestList[0].QuestItemAttack;
                returnValues.returnValueItemDefence = allQuestList[0].QuestItemDefence;
                return returnValues;
            }
            else
            {
                returnValues.returnValueItem = null;
                returnValues.returnValueGold = 0;
                returnValues.returnValueClear = false;
                returnValues.returnValueItemAttack = 0;
                returnValues.returnValueItemDefence = 0;
                return returnValues;
            }
        }

        public ReturnValues QuestCheck_1()
        {
            if (useGoldAmount >= 500)
            {
                returnValues.returnValueItem = allQuestList[1].QuestItem;
                returnValues.returnValueGold = allQuestList[1].QuestGold;
                returnValues.returnValueItemAttack = allQuestList[1].QuestItemAttack;
                returnValues.returnValueItemDefence = allQuestList[1].QuestItemDefence;
                returnValues.returnValueClear = true;
                return returnValues;

            }
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            return returnValues;
        }

        public ReturnValues QuestCheck_2()
        {
            if (monsterDeathCountQ2 >= 2)
            {
                returnValues.returnValueItem = allQuestList[2].QuestItem;
                returnValues.returnValueGold = allQuestList[2].QuestGold;
                returnValues.returnValueItemAttack = allQuestList[2].QuestItemAttack;
                returnValues.returnValueItemDefence = allQuestList[2].QuestItemDefence;
                returnValues.returnValueClear = true;
                return returnValues;

            }
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            returnValues.returnValueItemAttack = 0;
            returnValues.returnValueItemDefence = 0;
            return returnValues;
        }

        public ReturnValues QuestCheck_3()
        {

            if (monsterDeathCountQ3 >= 8)
            {
                returnValues.returnValueItem = allQuestList[3].QuestItem;
                returnValues.returnValueGold = allQuestList[3].QuestGold;
                returnValues.returnValueItemAttack = allQuestList[3].QuestItemAttack;
                returnValues.returnValueItemDefence = allQuestList[3].QuestItemDefence;
                returnValues.returnValueClear = true;
                return returnValues;

            }
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            returnValues.returnValueItemAttack = 0;
            returnValues.returnValueItemDefence = 0;
            return returnValues;
        }



        public ReturnValues QuestCheck_4()
        {
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            return returnValues;
        }


        public void QuestProgress(int i)
        {

            if (i == 0)
            {
                QuestCheck_0();
                Console.WriteLine($"[구매한 포켓몬] : [/1]\n");
            }
            else if (i == 1)
            {
                QuestCheck_1();
                Console.WriteLine($"[Gold 사용] : [{useGoldAmount}/500]\n");
            }
            else if (i == 2)
            {
                QuestCheck_2();
                if (monsterDeathCountQ2 >= 2)
                {
                    Console.WriteLine($"[전투불능으로 만든 포켓몬] : [2/2]\n");
                }
                else
                {
                    Console.WriteLine($"[전투불능으로 만든 포켓몬] : [{monsterDeathCountQ2}/2]\n");
                }

            }
            else if (i == 3)
            {
                QuestCheck_3();
                if (monsterDeathCountQ3 >= 8)
                {
                    Console.WriteLine($"[전투불능으로 만든 포켓몬] : [8/8]\n");
                }
                else
                {
                    Console.WriteLine($"[전투불능으로 만든 포켓몬] : [{monsterDeathCountQ3}/8]\n");
                }
            }
            else if (i == 4)
            {
                QuestCheck_4();
                Console.WriteLine($"[진화의 돌 사용 갯수] : [/1]\n");
            }
        }


        public void QuestInformationDetailView(int i)
        {
            Console.Clear();
            Console.WriteLine("임무 상세 내용\n");
            Console.WriteLine(allQuestList[i].QuestName + "\n\n");
            Console.WriteLine(allQuestList[i].QuestManual + "\n\n");
            Console.WriteLine(" - 보상 목록 -");
            if (allQuestList[i].QuestGold == 0 && allQuestList[i].QuestItem != null)
            {

                Console.WriteLine("  포켓몬 = " + allQuestList[i].QuestItem);
            }
            else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItem == null)
            {
                Console.WriteLine("  Gold = " + allQuestList[i].QuestGold);
            }
            else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItem != null)
            {
                Console.WriteLine("  Gold = " + allQuestList[i].QuestGold + "\n  포켓몬 = " + allQuestList[i].QuestItem);
            }
            Console.WriteLine();
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");

            Console.Write(">>");

            playerChoose = CInput.CheckInput(1, 2);

            if (playerChoose == 1)
            {
                AcceptQuestSet(i);
            }
            else if (playerChoose == 2)
            {
                retry = false;
                DisplayQuestUI(mainScene, player, inventory);
            }
            else
            {
                retry = true;
            }



        }

        public void AcceptQuestInformationDetailView(int i)
        {

            Console.Clear();
            Console.WriteLine("임무 상세 내용\n");
            Console.WriteLine(allQuestList[i].QuestName + "\n\n");
            Console.WriteLine(allQuestList[i].QuestManual + "\n\n");
            Console.WriteLine(" - 보상 목록 -");
            if (allQuestList[i].QuestGold == 0 && allQuestList[i].QuestItem != null)
            {

                Console.WriteLine("  포켓몬 = " + allQuestList[i].QuestItem);
            }
            else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItem == null)
            {
                Console.WriteLine("  Gold = " + allQuestList[i].QuestGold);
            }
            else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItem != null)
            {
                Console.WriteLine("  Gold = " + allQuestList[i].QuestGold + "\n  포켓몬 = " + allQuestList[i].QuestItem);
            }
            Console.WriteLine("\n[임무 진행도]");
            QuestProgress(i);
            if (returnValues.returnValueClear && !allQuestList[i].QuestIsClear)
            {
                Console.WriteLine("1. 보상받기");
            }
            else if (allQuestList[i].QuestIsClear)
            {
                Console.WriteLine("이미 보상을 받은 임무 입니다.");
            }
            else
            {
                Console.WriteLine("임무 진행중..");
            }
            Console.WriteLine("2. 나가기");
            if (!allQuestList[i].QuestIsClear)
            {
                playerChoose = CInput.CheckInput(1, 2);
            } else if (allQuestList[i].QuestIsClear)
            {
                playerChoose = CInput.CheckInput(2, 2);
            }
            if (playerChoose == 1 && returnValues.returnValueClear && !allQuestList[i].QuestIsClear)
            {
                if (returnValues.returnValueGold != 0)
                {
                    Console.WriteLine(player.PlayerGold);
                    player.PlayerGold += returnValues.returnValueGold;
                }
                if (returnValues.returnValueItem != null)
                {
                    inventory.inventory.Add(returnValues.returnValueItem, (1, false ,returnValues.returnValueItemAttack , returnValues.returnValueItemDefence ));
                }
                allQuestList[i].QuestIsClear = returnValues.returnValueClear;
            }
            else if (playerChoose == 2)
            {
                DisplayQuestUI(mainScene, player, inventory);
            }
            else
            {
            }
            DisplayQuestUI(mainScene, player, inventory);
        }
    }




    class QuestInformation
    {
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public int QuestLevel { get; set; }
        public string QuestManual { get; set; }
        public int QuestGold { get; set; }
        public string QuestItem { get; set; }
        public bool QuestIsClear { get; set; }
        public int QuestItemAttack { get; set; }
        public int QuestItemDefence { get; set; }


        public QuestInformation(int id, string name, int level, string manual, int gold, string item, bool clear)
        {
            QuestId = id;
            QuestName = name;
            QuestLevel = level;
            QuestManual = manual;
            QuestGold = gold;
            QuestItem = item;
            QuestIsClear = false;
            QuestItemAttack = 0;
            QuestItemDefence = 0;

        }

    }

    

}
