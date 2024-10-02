using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokemonT
{
    public class Battle
    {
        int PlayerAct = -1; // 스테이지 선택 값
        int PlayerSelect = -1; // 플레이어가 선택한 몬스터 번호
        int MonsterSelect = -1;// 몬스터가 선택한 플레이어 번호

        int CurrentStage; // 현재 스테이지 번호
        int MonsterTurnNum = 1; // 적 몬스터의 턴 번호
        int PlayerTurnNum = 1; // 내 몬스터의 턴
        bool MonsterTurn = false; // 적 몬스터의 턴인지 확인
        bool PlayerTurn = true; // 내 몬스터의 턴인지 확인
        bool BatterStert = false; // 전투 시작 시 몬스터 세팅용
        public int TotalMonsterDmg; // 도전 기능용 기본 + 크리티컬 + 상성 값을 저장
        public int TotalPlayerDmg; // 도전 기능용 기본 + 크리티컬 + 상성 값을 저장

        MonsterManager MonsterManager = new MonsterManager(); // 몬스터 관리 클레스
        DungeonManager DungeonManager; // 스테이지 관리 클레스
        public Monster[] StageMonster = new Monster[6]; // 스테이지에 등장 할 몬스터

        MonsterManager PlayerMonsterManager = new MonsterManager(); // 임시 플레이어
        List<Monster> PlayerMonster = new List<Monster>(); // 임시 플레이어

        MainScene mainScene;
        Inventory inven;
        Quest quest;

        int[] MonsterDieID = new int[6];
        int MCount = 0;

        int[] PlayerDieID = new int[6];
        int PCount = 0;

        public Battle()
        {
            DungeonManager = new DungeonManager(MonsterManager.DungeonMonsters);

            foreach (Monster monster in PlayerMonsterManager.Monsters)
            {
                PlayerMonster.Add(monster);
            }
        }

        public void DisPlayBattelUI(MainScene displayMainUI , Quest questInformation)
        {
            mainScene = displayMainUI;
            quest = questInformation;
            Console.Clear();
            Console.WriteLine("던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i + 1}. Stage.{i + 1} Lv.{(DungeonManager.Dungeons[i].Level + " 이상" + "")}");
            }
            Console.WriteLine("6. 장착관리");
            Console.WriteLine("7. 아이템 사용");
            Console.WriteLine("0. 나가기\n");
            PlayerAct = PlayerAction();
            switch (PlayerAct)
            {
                case 0:
                    displayMainUI.DisplayMainUI();
                    break;

                case 1:
                    Fight(PlayerAct);
                    break;

                case 2:
                    Fight(PlayerAct);
                    break;

                case 3:
                    Fight(PlayerAct);
                    break;

                case 4:
                    Fight(PlayerAct);
                    break;

                case 5:
                    Fight(PlayerAct);
                    break;

                case 6:
                    //장착관리
                    break;

                case 7:
                    //아이템 사용
                    break;

                default:
                    InputFail();
                    DisPlayBattelUI(displayMainUI , quest);
                    break;
            }
        }

        public void Fight(int stage)
        {



            Console.Clear();
            CurrentStage = stage;
            Console.WriteLine($"Batterl!\n");
            if (BatterStert == false)
            {
                StageEnumy(DungeonManager.Dungeons[stage - 1].Monstercount); // 몬스터 생성
                int recount = 0;
                for (int i = 0; i < PlayerMonster.Count; i++)
                {
                    if (PlayerMonster[i].Die) recount++;
                }
                if (recount == DungeonManager.Dungeons[stage - 1].Monstercount)
                {
                    Console.WriteLine($"전투가 가능한 포켓몬이 없습니다!\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("아무 키나 누르세요.");
                    ResetData();
                    DisPlayBattelUI(mainScene, quest);
                }
                else recount = 0;
            }

            if (PlayerTurn && !MonsterTurn)
            {
                for (int i = 0; i < DungeonManager.Dungeons[stage - 1].Monstercount; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{PlayerMonster[i].Name}");
                    Console.ResetColor();
                    Console.Write("의 공격!");
                }
            }
            else if (!PlayerTurn && MonsterTurn)
            {
                for (int i = 0; i < DungeonManager.Dungeons[stage - 1].Monstercount; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{StageMonster[i].Name}");
                    Console.ResetColor();
                    Console.Write("의 공격!");
                }
            }

            Console.WriteLine("[적 포켓몬]");
            for (int i = 0; i < DungeonManager.Dungeons[stage - 1].Monstercount; i++)
            {
                if (StageMonster[i].Die == true) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{(PlayerTurn == true ? (i + 1) + ". " : "")}Lv.{StageMonster[i].Level} {StageMonster[i].Name} HP {StageMonster[i].Hp} 공격력 {StageMonster[i].Attack}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {(MonsterTurnNum == i + 1 && MonsterTurn && !StageMonster[i].Die ? "<== 현재 턴" : "")}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("[내 포켓몬]");
            for (int i = 0; i < DungeonManager.Dungeons[stage - 1].Monstercount; i++)
            {
                if (PlayerMonster[i].Die == true) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"Lv.{PlayerMonster[i].Level} {PlayerMonster[i].Name} HP {PlayerMonster[i].Hp} 공격력 {PlayerMonster[i].Attack}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" {(PlayerTurnNum == i + 1 && PlayerTurn && !PlayerMonster[i].Die ? "<== 현재 턴" : "")}");
                Console.ResetColor();
            }

            Console.WriteLine();

            if (DeathCount(StageMonster) == DungeonManager.Dungeons[stage - 1].Monstercount) StageClear();
            if (DeathCount(PlayerMonster) == DungeonManager.Dungeons[stage - 1].Monstercount) StageFail();
            if (PlayerTurn && !MonsterTurn) PlayerAttack();
            if (!PlayerTurn && MonsterTurn) MonsterAttack();


        }
        public int DeathCount(List<Monster> mon)
        {
            int count = 0;
            for (int i = 0; i < mon.Count; i++)
            {
                if (mon[i].Die) count++;
            }
            if (count == DungeonManager.Dungeons[CurrentStage - 1].Monstercount)
            {
                return count;
            }
            else count = 0;
            return count;
        }
        public int DeathCount(Monster[] mon)
        {
            int count = 0;
            for (int i = 0; i < mon.Length; i++)
            {
                if (mon[i] == null) break;
                else if (mon[i].Die) count++;
            }
            if (count == DungeonManager.Dungeons[CurrentStage - 1].Monstercount)
            {
                return count;
            }
            else count = 0;
            return count;
        }
        public void StageEnumy(int stageMonsterCount)
        {
            BatterStert = true;
            List<int> list = new List<int>();
            for (int i = 0; i < MonsterManager.Monsters.Count; i++)
            {
                list.Add(i);
            }
            for (int i = 0; i < stageMonsterCount; i++)
            {
                int rand = list[new Random().Next(list.Count)];
                StageMonster[i] = new MonsterManager().Monsters[rand];
                list.Remove(rand);
            }
        }
        public void PlayerAttack()
        {
            if (DungeonManager.Dungeons[CurrentStage - 1].Monstercount >= PlayerTurnNum)
            {
                if (!PlayerMonster[PlayerTurnNum - 1].Die)
                {
                    MonsterSelect = PlayerAction();
                    if (MonsterSelect <= 0 || MonsterSelect > DungeonManager.Dungeons[CurrentStage - 1].Monstercount)
                    {
                        InputFail();
                        Fight(CurrentStage);
                    }
                    else
                    {
                        if (StageMonster[MonsterSelect - 1].Hp > 0)
                        {
                            StageMonster[MonsterSelect - 1].Hp -= PlayerMonster[PlayerTurnNum - 1].Attack;
                            if (StageMonster[MonsterSelect - 1].Hp <= 0) StageMonster[MonsterSelect - 1].Hp = 0;
                            PlayerTurnNum++;
                        }

                        if (StageMonster[MonsterSelect - 1].Hp <= 0 && MCount < 6)
                        {
                            StageMonster[MonsterSelect - 1].Die = true;
                            MonsterDieID[MCount] = StageMonster[MonsterSelect - 1].Id;
                            MCount++;
                        }
                    }
                }
                else PlayerTurnNum++;
            }
            else if (DungeonManager.Dungeons[CurrentStage - 1].Monstercount < PlayerTurnNum)// 마지막으로 곡격 시 턴 종료
            {
                MonsterTurn = true;
                PlayerTurn = false;
                PlayerTurnNum = 1;
            }
            Fight(CurrentStage);
        }
        public void MonsterAttack()
        {
            while (true)
            {
                MonsterSelect = new Random().Next(1, DungeonManager.Dungeons[CurrentStage - 1].Monstercount + 1);
                if (!PlayerMonster[MonsterSelect - 1].Die)
                {
                    break;
                }
            }
            if (DungeonManager.Dungeons[CurrentStage - 1].Monstercount >= MonsterTurnNum)
            {
                if (!StageMonster[MonsterTurnNum - 1].Die)
                {
                    if (PlayerMonster[MonsterSelect - 1].Hp > 0)
                    {
                        PlayerMonster[MonsterSelect - 1].Hp -= StageMonster[MonsterTurnNum - 1].Attack;
                        if (PlayerMonster[MonsterSelect - 1].Hp < 0) PlayerMonster[MonsterSelect - 1].Hp = 0;
                        MonsterTurnNum++;
                    }
                    Thread.Sleep(1000);

                    if (PlayerMonster[MonsterSelect - 1].Hp <= 0)
                    {
                        PlayerMonster[MonsterSelect - 1].Die = true;
                        PlayerDieID[MCount] = PlayerMonster[MonsterSelect - 1].Id;
                        PCount++;
                    }
                }
                else MonsterTurnNum++;
            }
            else if (DungeonManager.Dungeons[CurrentStage - 1].Monstercount <= MonsterTurnNum)
            {
                MonsterTurn = false;
                PlayerTurn = true;
                MonsterTurnNum = 1;
            }

            Fight(CurrentStage);
        }
        public void StageClear()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Stage Clear!\n");
            Console.WriteLine("[획득 보상]");
            Console.WriteLine($"Gold : {DungeonManager.Dungeons[CurrentStage - 1].Gold}");
            Console.WriteLine($"Exp : {DungeonManager.Dungeons[CurrentStage - 1].Exp}");
            // 아이템

            Console.WriteLine("아무 키나 누르세요.");
            quest.tossQuestInformation_2(DeathCount(StageMonster));
            quest.tossQuestInformation_3(DeathCount(StageMonster));
            PlayerAction();
            ResetData();
            this.DisPlayBattelUI(mainScene, quest);
        }
        public void StageFail()
        {
            Console.WriteLine("전투에서 패배했습니다");
            Thread.Sleep(1000);
            Console.WriteLine("아무 키나 누르세요.");
            PlayerAction();
            ResetData();
            mainScene.DisplayMainUI();
        }
        public void ResetData()
        {
            MCount = 0;
            PCount = 0;
            Array.Clear(StageMonster, 0, 6);
            BatterStert = false;
            MonsterTurn = false;
            PlayerTurn = true;
            MonsterTurnNum = 1;
            PlayerTurnNum = 1;
        }
        public int PlayerAction()
        {
            string SetStr;
            int AtNum;
            int NumCheck;
            Console.Write("원하시는 행동을 입력해주세요.\n>>>");
            SetStr = Console.ReadLine();
            AtNum = int.TryParse(SetStr, out NumCheck) ? int.Parse(SetStr) : -1;
            //Console.Clear();
            return AtNum;
        }
        public void InputFail()
        {
            Console.WriteLine("잘못 입력하셨습니다.");
            Console.ReadLine();
        }
    }
}

