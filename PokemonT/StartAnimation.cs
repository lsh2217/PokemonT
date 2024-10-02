using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonT
{
    public class StartAnimation
    {
        // 그림 및 로고 연출

        // 포켓몬 로고
        public static void PrintLogo()
        {
            string MainLogo = @"  
                                              ,'\
                _.----.        ____         ,'  _\   ___    ___     ____
            _,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.
            \      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |
             \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |
               \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |
                \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |
                 \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |
                  \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |
                   \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |
                    \_.-'       |__|    `-._ |              '-.|     '-.| |   |
                                            `'                            '-._|";
            Console.WriteLine(MainLogo);
        }

    }
}
