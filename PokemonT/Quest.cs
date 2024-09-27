using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    class Quest
    {
        List<int> acceptQuestList = new List<int>();
        Dictionary<int, QuestInformation> allQuestList = new Dictionary<int, QuestInformation>();

        int charLevel = 5; //지워야 할 부분 , 레벨값을 스탯에서 받아야 할 부분
        int acceptQuestListNumber;
        int questListNumber;
        int playerChoose;
        string chooseMent = "원하시는 행동을 입력해주세요";
        string retryMent = "다시 선택해주세요";
        bool retry = false;

        public void QuestMainView() {
            do {
                Console.Clear();
                questListNumber = 0;
                Console.WriteLine("수락 가능한 임무 목록");
                Console.WriteLine("");
                if (charLevel <= 5)
                {
                    for (int i = 0; i < allQuestList.Count; i++)
                    {
                        if (allQuestList[i].QuestLevel == 0)
                        {
                            Console.WriteLine(questListNumber + 1 + ". " + allQuestList[i].QuestName);
                            Console.WriteLine();
                            questListNumber++;
                        }
                    }
                }
                if (charLevel >= 5)
                {

                    for (int i = 0; i < allQuestList.Count; i++)
                    {
                        if (allQuestList[i].QuestLevel == 1)
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
                    try
                    {
                        playerChoose = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        playerChoose = 99;
                    }

                    if ((playerChoose - 1) <= questListNumber && playerChoose > 0 && playerChoose != 0)
                    {
                        QuestInformationDetailView(playerChoose - 1);
                        retry = false;
                    }
                    else if (playerChoose == 0)
                    {
                        return;
                    }
                    else
                    {
                        retry = true;
                    }

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

            questValue.Add(new QuestInformation(0, "포켓몬 구매하기", 0, "상점에서 이상해씨를 구매하세요.", 5, 0, null));
            questValue.Add(new QuestInformation(1, "Gold 사용하기", 0, "500Gold를 소모하세요", 5, 0, null));
            questValue.Add(new QuestInformation(2, "포켓몬 전투에서 승리", 0, "전투에서 상대 아무포켓몬 2마리를 전투불능으로 만드세요", 0, 1, "파이리"));
            questValue.Add(new QuestInformation(3, "진화의 돌 사용", 1, "아무포켓몬에게 진화의 돌을 사용하여 진화시켜 보세요", 0, 2, "진화의 돌"));
            questValue.Add(new QuestInformation(4, "리자드 전투에서 승리", 1, "상대 리자드를 전투불능으로 만드세요", 15, 2, "상처약"));

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
            QuestMainView();
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
                if (allQuestList[i].QuestEXP == 0 && allQuestList[i].QuestItemType != 0)
                {
                    
                    Console.WriteLine("  아이템 = " + allQuestList[i].QuestItem);
                }
                else if (allQuestList[i].QuestEXP != 0 && allQuestList[i].QuestItemType == 0)
                {
                    Console.WriteLine("  경험치 = " + allQuestList[i].QuestEXP);
                }
                else if (allQuestList[i].QuestEXP != 0 && allQuestList[i].QuestItemType != 0)
                {
                    Console.WriteLine("  경험치 = " + allQuestList[i].QuestEXP + "\n  아이템 = " + allQuestList[i].QuestItem);
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
                try
                {
                    playerChoose = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    playerChoose = 99;
                }
                if (playerChoose == 1)
                {
                    AcceptQuestSet(i);
                }
                else if (playerChoose == 2)
                {
                    retry = false;
                    QuestMainView();
                }
                else
                {
                    retry = true;
                }


            }while(retry);

        }


    }

    


    class QuestInformation
    {
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public int QuestLevel { get; set; }
        public string QuestManual { get; set; }
        public int QuestEXP { get; set; }
        public int QuestItemType { get; set; }
        public string QuestItem { get; set; }

     
        public QuestInformation(int id, string name, int level, string manual, int exp, int itemType, string item)
        {
            QuestId = id;
            QuestName = name;
            QuestLevel = level;
            QuestManual = manual;
            QuestEXP = exp;
            QuestItemType = itemType;
            QuestItem = item;

        }

        


        public string QuestText()
        {
            
            return $"{QuestId},{QuestName},{QuestLevel},{QuestManual},{QuestEXP},{QuestItemType},{QuestItem}" ;
        }

    }




}
