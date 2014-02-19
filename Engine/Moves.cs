using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Engine
{
    public static class Moves
    {
        public const long QUEEN_SIDE = unchecked((long)0xF0F0F0F0F0F0F0F);
        public const long KING_SIDE = unchecked((long)0xF0F0F0F0F0F0F0F0);
        public const long CENTER = unchecked((long)0x1818000000);
        public const long EXTENDED_CENTER = unchecked((long)0x3C24243C0000);
        public const long FILE_H = unchecked((long)0x8080808080808080);
        public const long RANK_1 = unchecked((long)0xFF00000000000000);
        public const long RANK_8 = unchecked((long)0x00000000000000FF);
        public const long FILE_A = unchecked((long)0x101010101010101);
        public const long LONG_DIAG = unchecked((long)0x8142241818244281);

        static int[] lookup67 = new int[68] {
              64,  0,  1, 39,  2, 15, 40, 23,
               3, 12, 16, 59, 41, 19, 24, 54,
               4, -1, 13, 10, 17, 62, 60, 28,
              42, 30, 20, 51, 25, 44, 55, 47,
               5, 32, -1, 38, 14, 22, 11, 58,
              18, 53, 63,  9, 61, 27, 29, 50,
              43, 46, 31, 37, 21, 57, 52,  8,
              26, 49, 45, 36, 56,  7, 48, 35,
               6, 34, 33, -1
            };
        static private void SetStaticBoards()
        {

        }
        static public int TrailingZeroCount(long bb) 
        {
           return lookup67[(bb & -bb) % 67];
        }
    }
}
