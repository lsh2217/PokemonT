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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                         << 게임 시작>>");
            Console.WriteLine();
            Console.WriteLine("                                 아무 버튼이나 눌러 게임 시작하기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                               20조                                        ");
            Console.WriteLine();
            Console.WriteLine("                         박현도        신윤영        이승환        이준형                  ");
            Console.ReadKey();
        }


        // 오박사 이미지
        public static void Professor()
        {
            string professorO = @"
                                                     8vzTW>;?TG+-...
                                               .....,I;;;<;;;;;;;<?7T6u-...
                                             .U7<<<<>>;;>;;;;;;;;;;;;;;;;<zTO+..
                                              ?s;;;;;;;>;>;>;>;;;;;;;>;;;>;;>;;?T&.
                                               (I;;;;++;;;;;;;;;>>;>>;;>;;;;<;;;;;z-
                                                dc;>jU7TTTUVXzAx<;>jAwwwXVVVT4I;>;J{
                                                X2;;j0~:::::::(Wx+&(0~:::::::(k;;;J{
                                               .WC;;($~::::::::(hJfT9(:::::::(k;;;J:
                                                .4+;JI_(J.:~::::?K>~::::((J-:(WzVUK`
                                                  joJIdMHMMm+::::<::_(&kMMgMm(WVXOF
                                                  .VdI<7MBHHHN<:::<gHMHHHHB=<(W4wk:
                                                  w1jI:(}`(W7BI:::(TB""jS. J>:(W6a+S.
                                                  0<Wl:J} (N_(:::~::~`(N  (I:(UWyUJ{
                                                  (Id>(XnJ(A-~~:::::<.-1..J+x_1OH=d:
                                                   ?K>:?7C<:::(_:::::::(?UVY<+1d9jP`
                                                    V<::::::~jb::~::~::::::::jkSZ=
                                                    4<::::((::<<~:~:::~(X+:::zf
                                                     ?n-::(?MUUUUUUUUUWH3C::+dC
                                                       ?A-::(7TUXZUUYY<:::(&W!
                                                    ..JCqMe_::(OOOO>:::((gM@(7&.
                                                ..vOD``.dXHHmJ+J+JJJJ+XMMHHD~~(HCz..
       .Z74Z7o                            ..J7<!``.Z``..WZXWkMHy1zzugHHqqkMI_::dc_~<?7C(..
    .Z7K>_dl<j[                      .-77!```````.Z!`.`(HZyyWHHNvdMHkkqkqqM>:~~~S-~```.` ~?7i.
    (>(K<:zr~(d.                   .U%`````.`..`.J!`.` dKZZXWHWMyHHHMqqkkHH<:~~~jl ```````.`` ?7i,
   .J<(W>:(k::(l                  .C(}`.`.``.``.J\`.``.MHZyWHZyXMHWHMNkkkH#~_`.`.O.`..```.````.`-YG.
  .IJc:qI:(X_:<S.                 J_(}``.`.``.`.C````.(HHZXHZyyWHZyZXHkZyXP``.```(L ``..``.`.``.C .G
  .I?k:(0~:?I::j| .J74,           z (r`````.``(b-.` ..JWWNHHZZWHXZZyZUMkXMt.``. ` j< ``.```.``.f```J-
  .w~4::1<:::::(G.V::j'          .r (]`.`.`.`.` (Ok7!.dSZWHyZXgWZyyZZXUHMb\ (-.((JJ: .``.```` J!`.`.I
   O<(<:::::~::_dD:~(r          .=``($`.``.``.(v=~``..KZZZyZZygWyZZyZyZZZH:`` ?4-. .`.``.`.``.r_``.`I,
   (r::::~:::::(<::~Z`         .t```.D``.``..f``.`.``(HyZZZZyZHWZZyZZyZyZH_.```` ?G. `.````.`(!`.`` `($";
            Console.WriteLine(professorO);   
            Console.WriteLine();
            Console.WriteLine("------ << 오박사>> ------------------------------------------------------------------------------------");
        }

        public static void AsciiBulbasaur() // 이상해씨
        {
            string bulbasaur = @"
______________________________________________________________________
[]   '         /   /  ..   `.  `./ | ;`.'   ,"""" ,.  `.   \      |   []
[]    `.     ,'  ,'   | |\  |      ""       |  ,'\ |   \   `    ,L   []
[]    /|`.  /   '     | `-| '                /`-' |    L   `._/  \  []
[]   / | .`|   |  .   `._.'                 `.__,'   .  |    |  (`  []
[]  '-""""-'_|   `. `.__,.____     .   _,        ____ ,-  j    "".-'""' []
[]         \     `-.  \/.   `""-.._   _,.---'""""\/  ""_,.'     /-'     []
======================================================================";
            Console.WriteLine(bulbasaur);
        }

        public static void AsciiCharmander() //파이리
        {
            string charmander = @"
 ______________________________________________________________________
[]     |      /|          "" __   \  ' \ ' ,-'   ,"".   `-. ' / ' /  /[]
[]  \ '      , |           / |.   .  \ ' /   , ' \ `     \ ' / '  / []
[] '         |,'          !_.'|   | '   ;   ,`"",  L,|     :   '  / /[]
[]      |  ,'             '   |   |     |   .   ,    \    |     / / []
[]   \ '  /              |`--'|   |     |    `-/ |   .    |    / / /[]
[]  '    |    ,   .       `---'   |     :    |   |  ',,   ;   / / / []
======================================================================";
            Console.WriteLine(charmander);
        }

        public static void AsciiSquirtle() // 꼬부기
        {
            string squirtle = @"
______________________________________________________________________
[]    .'\               ,"".       `              ,'          `.    []
[]   ._.'|             / |  `       \            /              \   []
[]   |   |            `-.'  ||       `.        .'                |  []
[]   |   |            '-._,'||       | \      .-       ,---.     |  []
[]   .`.,'             `..,'.'       , |`-.  /        j     `    |  []
[]   l-.._'-   ,          _ _.'`.  _/  |   `.         |     /    j  []
======================================================================";
            Console.WriteLine(squirtle);
        }
        public static void MainMap() // 매인 맵
        {
            string mainMap = @"
(________/~~,~~\______.(_____)\_,---._/(_____)._______/~~,~~\________)
;          (')             ,-'         `-.              (')          ;
)    ||     )    ||       /   \ _---_ /   \       ||     )     ||    (
; \ '  ' /    \ '  ' /   ; -_.'  )  `. _- :   \ '  ' /     \ '  ' / ;
('   /\   '  '   /\   '  |__ :   ((    : __|  '   /\   '   '   /\   ')
;   //\\        //\\     |   :   ) \   :   |     //\\         //\\   ;
)   \\//        \\//     : -"" `.( , ).' ""- ;     \\//         \\//   (
;    ><          ><       \  '   ""|""   `  /       ><           ><    ;
(_.._ || _.._.._.._ || _.._.._.`-__\\\*///__-'..._..._||_...___..._||_.._)
----------------------------------------------------------------------";
            Console.WriteLine(mainMap);
        }
    }
}
