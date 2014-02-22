using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Engine
{
    internal static class Evaluate
    {
        private const long QUEEN_SIDE = unchecked((long)0xF0F0F0F0F0F0F0F);
        private const long KING_SIDE = unchecked((long)0xF0F0F0F0F0F0F0F0);
        private const long CENTER = unchecked((long)0x1818000000);
        private const long EXTENDED_CENTER = unchecked((long)0x3C24243C0000);
        private const long LONG_DIAG = unchecked((long)0x8142241818244281);


    }
}
