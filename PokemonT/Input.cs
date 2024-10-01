using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    internal class Input
    {
        public int CheckInput(int min, int max) // 이동 시 올바른 입력 판정
        {
            int result; // 입력값 받을 변수

            while (true) // 올바른 값 입력할 때까지 반복
            {
                String input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result); // 입력한 값이 숫자인지 문자인지 판정
                if (isNumber)
                {
                    if (result >= min && result <= max) // 입력할 숫자의 영역 설정
                        return result; // 입력값 반환 
                }
                Console.WriteLine("잘못된 입력입니다");
            }

        }
    }
}
