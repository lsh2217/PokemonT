using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{

    struct ReturnValues { 
        public string returnValueItem;
        public int returnValueGold;
        public bool returnValueClear;
    };
    class Quest
    {
        MainScene mainScene;
        Input CInput = new Input();
        ReturnValues returnValues = new ReturnValues();
        List<int> acceptQuestList = new List<int>();
        Dictionary<int, QuestInformation> allQuestList = new Dictionary<int, QuestInformation>();

        int charLevel = 5; // ##지워야 할 부분 , 레벨값을 스탯에서 받아야 할 부분
        int acceptQuestListNumber;
        int questListNumber;
        int playerChoose;
        int allMonsterDeathCount;
        int lizzardDeathCount;
        int useGoldAmount;
        bool isEquip = false;
        string chooseMent = "원하시는 행동을 입력해주세요";
        string retryMent = "다시 선택해주세요";
        bool retry = false;

        public void DisplayQuestUI() {
            do {

                Console.Clear();
                questListNumber = 0;
                Console.WriteLine("수락 가능한 임무 목록");
                Console.WriteLine("");
                    for (int i = 0; i < allQuestList.Count; i++)
                    {    
                           if (acceptQuestList.Contains(allQuestList[i].QuestId))
                           { 
                                Console.WriteLine(questListNumber + 1 + ". " + allQuestList[i].QuestName + "  [임무 진행중..]");
                                Console.WriteLine();
                                questListNumber++;
                           }
                           else
                           {
                                Console.WriteLine(questListNumber + 1 + ". " + allQuestList[i].QuestName);
                                Console.WriteLine();
                                questListNumber++;
                           }
 
                    }
                    Console.WriteLine("0. 나가기\n");

                    if (!retry)
                    {
                        Console.WriteLine(chooseMent);
                    }
                    else
                    {
                        Console.WriteLine(retryMent);
                    }
                    Console.Write(">>");
                /*try
                {
                    playerChoose = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    playerChoose = 99;
                }*/

                    playerChoose = CInput.CheckInput(0, allQuestList.Count);

                    if ((playerChoose - 1) < questListNumber && playerChoose > 0
                        && playerChoose != 0 && !acceptQuestList.Contains(allQuestList[playerChoose - 1].QuestId))
                    {
                        QuestInformationDetailView(playerChoose - 1);
                        retry = false;
                    }
                    else if (playerChoose == 0)
                    {
                        return;
                    }
                    else if ((playerChoose - 1) < questListNumber && playerChoose > 0
                        && playerChoose != 0 && acceptQuestList.Contains(allQuestList[playerChoose - 1].QuestId))
                    {
                        AcceptQuestInformationDetailView(playerChoose - 1);
                        retry = false;
                    }
                    else
                    {
                        retry = true;
                    }

                
            } while (retry);





            /* allQuestList 에 있는 모든 값들을 출력
            foreach (KeyValuePair<int, QuestInformation> pair in allQuestList) {
                Console.WriteLine(pair.Value.QuestText());
            }*/

        }


        //모든 퀘스트의 정보를 저장하는 allQuestList 객체 생성
        public void QuestSet()
        {
            List<QuestInformation> questValue = new List<QuestInformation>();

            questValue.Add(new QuestInformation(0, "포켓몬 구매하기", 0, "상점에서 포켓몬 1마리를 구매하세요.", 1000, 0, null));
            questValue.Add(new QuestInformation(1, "Gold 사용하기", 0, "500Gold를 소모하세요", 1000, 0, null));
            questValue.Add(new QuestInformation(2, "포켓몬 전투에서 승리", 0, "전투에서 상대 아무포켓몬 2마리를 전투불능으로 만드세요", 0, 1, "꼬렛"));
            questValue.Add(new QuestInformation(3, "리자드 전투에서 승리", 1, "전투에서 상대 리자드를 전투불능으로 만드세요", 1500, 2, "고오스"));
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
            DisplayQuestUI();
        }

        public void tossQuestInformation_0(bool equip)
        {
            if (acceptQuestList.Contains(0))
            {
                isEquip = equip;
            }

        }
        public void tossQuestInformation_1(int useGold)
        {
            if (acceptQuestList.Contains(1))
            {
                useGoldAmount += useGold;
            }
        }
        public void tossQuestInformation_2(int[] monsterId)
        {
            if (acceptQuestList.Contains(2))
            {
                for (int i = 0; i < monsterId.Length; i++)
                {
                    allMonsterDeathCount++;
                }
            }
        }
        public void tossQuestInformation_3(int[] monsterId)
        {
            if (acceptQuestList.Contains(3))
            {
                for (int i = 0; i < monsterId.Length; i++)
                {
                    if (monsterId[i] == 5)              // ## 리자드 id 값으로 수정필요
                    {
                        lizzardDeathCount++;
                    }
                }
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
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            return returnValues;
        }

        public ReturnValues QuestCheck_1()
        {
            if (useGoldAmount >= 500)
            {
                returnValues.returnValueItem = allQuestList[1].QuestItem;
                returnValues.returnValueGold = allQuestList[1].QuestGold;
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
            if (allMonsterDeathCount >= 2)
            {
                    returnValues.returnValueItem = allQuestList[2].QuestItem;
                    returnValues.returnValueGold = allQuestList[2].QuestGold;
                    returnValues.returnValueClear = true;
                    return returnValues;

            }
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            return returnValues;
        }

        public ReturnValues QuestCheck_3()
        {

            if (lizzardDeathCount >= 1)
            {
                    returnValues.returnValueItem = allQuestList[4].QuestItem;
                    returnValues.returnValueGold = allQuestList[4].QuestGold;
                    returnValues.returnValueClear = true;
                    return returnValues;

            }
            returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            return returnValues;
        }

        

        public ReturnValues QuestCheck_4()
        {
                returnValues.returnValueItem = null;
            returnValues.returnValueGold = 0;
            returnValues.returnValueClear = false;
            return returnValues ;
        }


        public void QuestProgress(int i) {

            if (i == 0)
            {
                QuestCheck_0();
                Console.WriteLine($"[이상해씨 구매] : [/1]\n");
            }
            else if (i == 1)
            {
                QuestCheck_1();
                Console.WriteLine($"[Gold 사용] : [/500]\n");
            }
            else if (i == 2)
            {
                QuestCheck_2();
                Console.WriteLine($"[전투불능으로 만든 포켓몬] : [{allMonsterDeathCount}/2]\n");
            }
            else if (i == 3)
            {
                QuestCheck_3();
                Console.WriteLine($"[전투불능으로 만든 리자드] : [{lizzardDeathCount}/1]\n");
            }
            else if (i == 4)
            {
                QuestCheck_4();
                Console.WriteLine($"[진화의 돌 사용 갯수] : [/1]\n");
            }
        }


        public void QuestInformationDetailView(int i)
        {
            do 
            {
                Console.Clear();
                Console.WriteLine("임무 상세 내용\n");
                Console.WriteLine(allQuestList[i].QuestName + "\n\n");
                Console.WriteLine(allQuestList[i].QuestManual + "\n\n");
                Console.WriteLine(" - 보상 목록 -");
                if (allQuestList[i].QuestGold == 0 && allQuestList[i].QuestItemType != 0)
                {
                    
                    Console.WriteLine("  아이템 = " + allQuestList[i].QuestItem);
                }
                else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItemType == 0)
                {
                    Console.WriteLine("  Gold = " + allQuestList[i].QuestGold);
                }
                else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItemType != 0)
                {
                    Console.WriteLine("  Gold = " + allQuestList[i].QuestGold + "\n  아이템 = " + allQuestList[i].QuestItem);
                }
                Console.WriteLine();
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
                if (!retry)
                {
                    Console.WriteLine(chooseMent);
                }
                else
                {
                    Console.WriteLine(retryMent);
                }
                    Console.Write(">>");
                /*try
                {
                    playerChoose = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    playerChoose = 99;
                }*/
                playerChoose = CInput.CheckInput(1, 2);

                if (playerChoose == 1)
                {
                    AcceptQuestSet(i);
                }
                else if (playerChoose == 2)
                {
                    retry = false;
                    DisplayQuestUI();
                }
                else
                {
                    retry = true;
                }


            }while(retry);

        }

        public void AcceptQuestInformationDetailView(int i)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("임무 상세 내용\n");
                Console.WriteLine(allQuestList[i].QuestName + "\n\n");
                Console.WriteLine(allQuestList[i].QuestManual + "\n\n");
                Console.WriteLine(" - 보상 목록 -");
                if (allQuestList[i].QuestGold == 0 && allQuestList[i].QuestItemType != 0)
                {

                    Console.WriteLine("  아이템 = " + allQuestList[i].QuestItem);
                }
                else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItemType == 0)
                {
                    Console.WriteLine("  Gold = " + allQuestList[i].QuestGold);
                }
                else if (allQuestList[i].QuestGold != 0 && allQuestList[i].QuestItemType != 0)
                {
                    Console.WriteLine("  Gold = " + allQuestList[i].QuestGold + "\n  아이템 = " + allQuestList[i].QuestItem);
                }
                Console.WriteLine("\n[임무 진행도]");
                QuestProgress(i);
                if (returnValues.returnValueClear)
                {
                    Console.WriteLine("1. 보상받기");
                }
                else 
                {
                    Console.WriteLine("임무 진행중..");
                }
                Console.WriteLine("2. 나가기");
                if (!retry)
                {
                    Console.WriteLine(chooseMent);
                }
                else
                {
                    Console.WriteLine(retryMent);
                }
                Console.Write(">>");
                /*try
                {
                    playerChoose = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    playerChoose = 99;
                }*/
                playerChoose = CInput.CheckInput(1, 2);

                if (playerChoose == 1 && returnValues.returnValueClear)
                {
                    // ##보상받기 버튼 구현
                }
                else if (playerChoose == 2)
                {
                    retry = false;
                    DisplayQuestUI();
                }
                else
                {
                    retry = true;
                }


            } while (retry);

        }
    }

    


    class QuestInformation
    {
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public int QuestLevel { get; set; }
        public string QuestManual { get; set; }
        public int QuestGold { get; set; }
        public int QuestItemType { get; set; }
        public string QuestItem { get; set; }

     
        public QuestInformation(int id, string name, int level, string manual, int gold, int itemType, string item)
        {
            QuestId = id;
            QuestName = name;
            QuestLevel = level;
            QuestManual = manual;
            QuestGold = gold;
            QuestItemType = itemType;
            QuestItem = item;

        }

        


        public string QuestText()
        {
            
            return $"{QuestId},{QuestName},{QuestLevel},{QuestManual},{QuestGold},{QuestItemType},{QuestItem}" ;
        }

    }




}
