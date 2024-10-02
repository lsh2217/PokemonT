using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static PokemonT.Inventory;

namespace PokemonT
{
    public class Battle
    {
        int PlayerAct = -1; // 스테이지 선택 값
        int PlayerSelect = -1; // 플레이어가 선택한 몬스터 번호
        int MonsterSelect = -1;// 몬스터가 선택한 플레이어 번호

        int CurrentStage = -1; // 현재 스테이지 번호
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
        Monster PlayerSetMonster = new Monster();

        MainScene mainScene;
        Inventory inven;
        Character playerBattle;
        Quest quest;

        string[] MonsterDieID = new string[6];
        int MCount = 0;

        string[] PlayerDieID = new string[6];
        int PCount = 0;

        public Battle()
        {
            DungeonManager = new DungeonManager(MonsterManager.DungeonMonsters);
        }

        public void DisPlayBattelUI(MainScene displayMainUI, Quest questInformation, Character player, Inventory inventory)
        {
            mainScene = displayMainUI;
            playerBattle = player;
            quest = questInformation;
            inven = inventory;
            Console.Clear();
            Console.WriteLine("던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i + 1}. Stage.{i + 1} {(DungeonManager.Dungeons[i].Monstercount + "마리 등장" + "")}");
            }
            Console.WriteLine("6. 장착관리");
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

                default:
                    InputFail();
                    DisPlayBattelUI(displayMainUI, quest, playerBattle, inven);
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
                StagePlayer();
                StageEnumy(DungeonManager.Dungeons[stage - 1].Monstercount); // 몬스터 생성

                int recount = 0;
                for (int i = 0; i < PlayerMonster.Count; i++)
                {
                    if (PlayerMonster[i].Die) recount++;
                }
                if (recount == PlayerMonster.Count)
                {
                    Console.WriteLine($"전투가 가능한 포켓몬이 없습니다!\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("아무 키나 누르세요.");
                    PlayerAction();
                    ResetData();
                    DisPlayBattelUI(mainScene, quest, playerBattle, inven);
                }
                else recount = 0;
            }

            

            Console.WriteLine("[적 포켓몬]");
            for (int i = 0; i < DungeonManager.Dungeons[stage - 1].Monstercount; i++)
            {
                if (StageMonster[i].Die == true) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{(PlayerTurn == true ? (i + 1) + "." : "")}{StageMonster[i].Name} HP {StageMonster[i].Hp} 공격력 {StageMonster[i].Attack}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {(MonsterTurnNum == i + 1 && MonsterTurn && !StageMonster[i].Die ? "<== 현재 턴" : "")}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("[내 포켓몬]");
            for (int i = 0; i < PlayerMonster.Count; i++)
            {
                if (PlayerMonster[i].Die == true) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{PlayerMonster[i].Name} HP {PlayerMonster[i].Hp} 공격력 {PlayerMonster[i].Attack}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" {(PlayerTurnNum == i + 1 && PlayerTurn && !PlayerMonster[i].Die ? "<== 현재 턴" : "")}");
                Console.ResetColor();
            }            

            if (DeathCount(StageMonster) == DungeonManager.Dungeons[stage - 1].Monstercount) StageClear();
            if (DeathCount(PlayerMonster) == PlayerMonster.Count) StageFail();
            if (PlayerTurn && !MonsterTurn) PlayerAttack();
            if (!PlayerTurn && MonsterTurn) MonsterAttack();

            Console.WriteLine();
            if (PlayerTurn && !MonsterTurn)
            {
                for (int i = 0; i < PlayerMonster.Count; i++)
                {
                    if (PlayerTurnNum - 1 == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{PlayerMonster[i].Name}");
                        Console.ResetColor();
                        Console.WriteLine("의 공격!");
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{StageMonster[MonsterSelect - 1].Name}");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                    }
                }
            }
            else if (!PlayerTurn && MonsterTurn)
            {
                for (int i = 0; i < DungeonManager.Dungeons[stage - 1].Monstercount; i++)
                {
                    if (MonsterTurnNum - 1 == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{StageMonster[i].Name}");
                        Console.ResetColor();
                        Console.WriteLine("의 공격!");
                        Thread.Sleep(1000);
                    }
                }
            }

        }
        public int DeathCount(List<Monster> mon)
        {
            int count = 0;
            for (int i = 0; i < mon.Count; i++)
            {
                if (mon[i].Die) count++;
            }
            if (count == PlayerMonster.Count)
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
        public void StagePlayer()
        {            
            foreach (var item in inven.inventory)
            {
                var playerMonster = new Monster
                {
                    Id = item.Key,
                    Name = item.Key,
                    Attack = item.Value.attack,
                    Hp = item.Value.defence,
                    Die = item.Value.defence <= 0
                };
                if(item.Value.isEquipped) PlayerMonster.Add(playerMonster);
            }
        }
        public void PlayerAttack()
        {
            if (PlayerMonster.Count >= PlayerTurnNum)
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
                        bool success = PerformAttack(PlayerMonster[PlayerTurnNum - 1], StageMonster[MonsterSelect - 1],
                                            ref PlayerTurnNum, PlayerMonster.Count,
                                            id => MonsterDieID[MCount++] = id, true);

                        if (!success)
                        {
                            PlayerAttack();
                        }

                    }
                }
                else PlayerTurnNum++;
            }
            else if (PlayerMonster.Count < PlayerTurnNum)// 마지막으로 곡격 시 턴 종료
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
                MonsterSelect = new Random().Next(1, PlayerMonster.Count + 1);
                if (!PlayerMonster[MonsterSelect - 1].Die)
                {
                    break;
                }
            }
            if (DungeonManager.Dungeons[CurrentStage - 1].Monstercount >= MonsterTurnNum)
            {
                if (!StageMonster[MonsterTurnNum - 1].Die)
                {
                    bool success = PerformAttack(StageMonster[MonsterTurnNum - 1], PlayerMonster[MonsterSelect - 1],
                                        ref MonsterTurnNum, DungeonManager.Dungeons[CurrentStage - 1].Monstercount,
                                        id => PlayerDieID[PCount++] = id, false);

                    if (!success)
                    {
                        MonsterAttack();
                    }

                    Thread.Sleep(1000);

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
        private bool PerformAttack(Monster attacker, Monster defender, ref int attackerTurnNum, int maxTurnNum, Action<string> onDeath, bool isPlayer)
        {
            // 이미 죽은 몬스터를 공격할 수 없도록 처리
            if (defender.Die)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{defender.Name}");
                Console.WriteLine("은(는) 기절했습니다! 다른 몬스터를 선택하세요.");
                Console.ResetColor();
                return false; // 공격 실패, 턴 유지
            }

            if (!attacker.Die && defender.Hp > 0)
            {
                Random rand = new Random();
                int critChance = rand.Next(1, 101);   // 1~100 사이의 랜덤 값
                int dodgeChance = rand.Next(1, 101);  // 1~100 사이의 랜덤 값

                int attackDamage = attacker.Attack;

                // 회피 처리 (20% 확률)
                if (dodgeChance <= 20)
                {
                    Thread.Sleep(1000);
                    if(isPlayer)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{defender.Name}");
                    Console.ResetColor();
                    Console.WriteLine("이(가) 공격을 회피했습니다!");
                    Thread.Sleep(1500);
                }
                else
                {
                    // 크리티컬 처리 (20% 확률)
                    if (critChance <= 50)
                    {
                        attackDamage *= 2;
                        Thread.Sleep(1000);
                        if (isPlayer)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{attacker.Name}");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("이(가) 크리티컬 공격!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                    }

                    // HP 감소
                    defender.Hp -= attackDamage;
                    if (defender.Hp <= 0) defender.Hp = 0;
                    Thread.Sleep(1000);
                    if (isPlayer)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{defender.Name}");
                    Console.ResetColor();
                    Console.WriteLine($"이(가) {attackDamage}의 피해를 입었습니다. 남은 HP: {defender.Hp}");
                    Thread.Sleep(2000);
                    // 사망 처리
                    if (defender.Hp <= 0)
                    {
                        defender.Die = true;
                        onDeath(defender.Id);
                    }
                }

                attackerTurnNum++; // 공격이 성공했을 때만 턴 증가
                return true; // 공격 성공
            }

            return false; // 공격 실패, 턴 유지
        }
        public void StageClear()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Stage Clear!\n");
            Console.WriteLine("[획득 보상]");
            Console.WriteLine($"Gold : {DungeonManager.Dungeons[CurrentStage - 1].Gold}");
            playerBattle.PlayerGold += DungeonManager.Dungeons[CurrentStage - 1].Gold;

            Console.WriteLine("아무 키나 누르세요.");
            quest.tossQuestInformation_2(DeathCount(StageMonster));
            quest.tossQuestInformation_3(DeathCount(StageMonster));
            PlayerAction();
            ResetData();
            this.DisPlayBattelUI(mainScene, quest, playerBattle, inven);
        }
        public void StageFail()
        {
            Console.WriteLine("전투에서 패배했습니다");
            Thread.Sleep(1000);
            foreach (var playerMonster in PlayerMonster)
            {
                if (inven.inventory.ContainsKey(playerMonster.Id))
                {
                    var inventoryItem = inven.inventory[playerMonster.Id];

                    inven.inventory[playerMonster.Id] = (
                        inventoryItem.count,
                        inventoryItem.isEquipped,
                        inventoryItem.attack,
                        playerMonster.Hp
                    );
                }
            }
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
            PlayerAct = -1;
            PlayerSelect = -1;
            MonsterSelect = -1;
            CurrentStage = -1;
            PlayerMonster.Clear();
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

