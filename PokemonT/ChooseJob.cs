using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    internal class ChooseJob
    {
        Input CInput = new Input();

        public string SelectJob()
        {
            Console.Clear();
            StartAnimation.Professor();
            Console.WriteLine("포켓몬 세계에서 새로운 모험이 곧 시작될 거야.");
            Console.WriteLine("그런데, 너는 어떤 일을 하며 살아갈지 정해야 해. 너는 어떤 직업을 선택할 거야?");
            Console.WriteLine();
            Console.WriteLine();

            List<string> jobs = new List<string>()
            { "포켓몬 트레이너", "포켓몬 브리더", "포켓몬 연구원" };

            for (int i = 0; i < jobs.Count; i++) // 타입 출력
            {
                Console.WriteLine($"{i + 1}. {jobs[i]}");
            }

            Console.WriteLine();
            Console.WriteLine("직업 번호를 선택하세요:");
            Console.Write(">> ");
            int choice = CInput.CheckInput(1, jobs.Count);

            //선택한 숫자에 따라 직업 할당 (switch문)
            string playerJob = string.Empty;
            switch (choice)
            {
                case 1:
                    playerJob = jobs[0]; // "포켓몬 트레이너"
                    break;
                case 2:
                    playerJob = jobs[1]; // "포켓몬 브리더"
                    break;
                case 3:
                    playerJob = jobs[2]; // "포켓몬 연구원"
                    break;
            }


            Console.Clear();
            StartAnimation.Professor();
            Console.WriteLine($"{playerJob}, 좋은 선택이야!");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("아무 키나 눌러 넘어가기");
            Console.ReadKey();

            return playerJob;
        }
    }
}
