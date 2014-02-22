using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Engine
{
    internal static class Moves
    {
        #region CONSTANTS
        private const long DEBRUIJN_64 = 0x03f79d71b4cb0a89;
        private static short[] INDEX64 = new short[64]{
            0, 47,  1, 56, 48, 27,  2, 60,
           57, 49, 41, 37, 28, 16,  3, 61,
           54, 58, 35, 52, 50, 42, 21, 44,
           38, 32, 29, 23, 17, 11,  4, 62,
           46, 55, 26, 59, 40, 36, 15, 53,
           34, 51, 20, 43, 31, 22, 10, 45,
           25, 39, 14, 33, 19, 30,  9, 24,
           13, 18,  8, 12,  7,  6,  5, 63
           };

        private const long RANK_1 = unchecked((long)0xFF00000000000000);
        private const long RANK_2 = unchecked((long)0x00FF000000000000);
        private const long RANK_7 = unchecked((long)0x000000000000FF00);
        private const long RANK_8 = unchecked((long)0x00000000000000FF);
        private const long FILE_A = unchecked((long)0x101010101010101);
        private const long FILE_B = unchecked((long)0x202020202020202);
        private const long FILE_G = unchecked((long)0x4040404040404040);
        private const long FILE_H = unchecked((long)0x8080808080808080);
#endregion
        private static long[] AllWhitePawnMoves = new long[64];
        private static long[] AllBlackPawnMoves = new long[64];
        private static long[] AllWhitePawnCaptures = new long[64];
        private static long[] AllBlackPawnCaptures = new long[64];
        private static long[] AllKnightMoves = new long[64];
        private static long[] AllKingMoves = new long[64];

        internal static long[] RayAttackNorthWest = new long[64];
        internal static long[] RayAttackNorth = new long[64];
        internal static long[] RayAttackNorthEast = new long[64];
        internal static long[] RayAttackEast = new long[64];
        internal static long[] RayAttackSouthEast = new long[64];
        internal static long[] RayAttackSouth = new long[64];
        internal static long[] RayAttackSouthWest = new long[64];
        internal static long[] RayAttackWest = new long[64];

        internal static long ValidPawnMoves;
        internal static long ValidKnightMoves;
        internal static long ValidBishopMoves;
        internal static long ValidRookMoves;
        internal static long ValidQueenMoves;
        internal static long ValidKingMoves;

        internal static long ValidPawnCaptures;
        internal static long ValidKnightCaptures;
        internal static long ValidBishopCaptures;
        internal static long ValidRookCaptures;
        internal static long ValidQueenCaptures;
        internal static long ValidKingCaptures;

        private static long WhitePieceBoard;
        private static long BlackPieceBoard;
        private static long AllPieceBoard;
        private static long WhiteAttackBoard;
        private static long BlackAttackBoard;
        private static long KingCheckBoard;

        internal static void GenerateValidMoves(BitBoards bb, Reference.Color sideMoving)
        {
            if(sideMoving == Reference.Color.White)
            {
                WhitePieceBoard = bb.WhitePawn | bb.WhiteBishop | bb.WhiteKnight | bb.WhiteRook | bb.WhiteKing | bb.WhiteQueen;
                BlackPieceBoard = bb.BlackPawn | bb.BlackBishop | bb.BlackKnight | bb.BlackRook | bb.BlackKing | bb.BlackQueen;
                AllPieceBoard = WhitePieceBoard | BlackPieceBoard;

                ValidPawnMoves = 0;
                ValidKnightMoves = 0;
                ValidBishopMoves = 0;
                ValidRookMoves = 0;
                ValidQueenMoves = 0;
                ValidKingMoves = 0;

                ValidPawnCaptures = 0;
                ValidKnightCaptures = 0;
                ValidBishopCaptures = 0;
                ValidRookCaptures = 0;
                ValidQueenCaptures = 0;
                ValidKingCaptures = 0;

                WhiteAttackBoard = 0;
                BlackAttackBoard = 0;
                KingCheckBoard = 0;

                for(int i = 0; i < 64; i++)
                {
                    //Pawns
                    if(((long)1<<i & bb.WhitePawn) != 0)
                    {
                        ValidPawnMoves |= AllWhitePawnMoves[i] & ~AllPieceBoard;
                        if ((((long)1 << i) & RANK_2) != 0)
                        {
                            //When starting from the second rank jump two squares
                            ValidPawnMoves |= (AllWhitePawnMoves[i] >> 8) & ~AllPieceBoard;
                        }
                        ValidPawnCaptures |= (AllWhitePawnCaptures[i] & BlackPieceBoard);
                    }
                    //Knights
                    if(((long)1<<i & bb.WhiteKnight) != 0)
                    {
                        ValidKnightMoves |= AllKnightMoves[i] & ~AllPieceBoard;
                        ValidKnightCaptures |= AllKnightMoves[i] & BlackPieceBoard;
                    }
                    //King
                    if (((long)1 << i & bb.WhiteKing) != 0)
                    {
                        ValidKingMoves |= AllKingMoves[i] & ~AllPieceBoard;
                        ValidKingCaptures |= AllKingMoves[i] & BlackPieceBoard;
                    }
                    //Rook
                    if (((long)1 << i & bb.WhiteRook) != 0)
                    {

                    }
                    //Bishop
                    if (((long)1 << i & bb.WhiteBishop) != 0)
                    {

                    }
                    //Queen
                    if (((long)1 << i & bb.WhiteQueen) != 0)
                    {

                    }
                }
            }
            else
            {
                bb.FlipBoards();
                GenerateValidMoves(bb, Reference.Color.White);
                bb.FlipBoards();
            }
        }

        #region DumbMoveGeneration
        /* 
         * >>9 shifts up and to the right
         * >>7 shifts up and to the left
         * >>8 shifts up
         * <<8 shifts down
         * <<7 shifts down and the left
         * <<9 shift down and to the right
         */
        public static void InitializeMoves()
        {
            for(int i = 0; i < 64; i++)
            {
                AllWhitePawnMoves[i] = GetWhitePawnMoves((long)1<<i);
                AllBlackPawnMoves[i] = GetBlackPawnMoves((long)1<<i);
                AllWhitePawnCaptures[i] = GetWhitePawnCaptures((long)1<<i);
                AllBlackPawnCaptures[i] = GetBlackPawnCaptures((long)1<<i);
                AllKnightMoves[i] = GetKnightMoves((long)1<<i);
                AllKingMoves[i] = GetKingMoves((long)1<<i);
                InitializeRays(i);
            }
        }
        private static long GetWhitePawnMoves(long bitboard)
        {
            long result = 0;
            if ((bitboard & RANK_8) == 0)
            {
                result |= bitboard >> 8;
            }
            return result;
        }
        private static long GetBlackPawnMoves(long bitboard)
        {
            long result = 0;
            if ((bitboard & RANK_1) == 0)
            {
                result |= bitboard << 8;
            }
            if ((bitboard & RANK_7) == 0)
            {
                result |= bitboard >> 16;
            }
            return result;
        }
        private static long GetWhitePawnCaptures(long bitboard)
        {
            long result = 0;
            if ((bitboard & FILE_A) == 0)
            {
                result |= bitboard >> 9;
            }
            if ((bitboard & FILE_H) == 0)
            {
                result |= bitboard >> 7;
            }
            return result;
        }
        private static long GetBlackPawnCaptures(long bitboard)
        {
            long result = 0;
            if ((bitboard & FILE_A) == 0)
            {
                result |= bitboard << 7;
            }
            if ((bitboard & FILE_H) == 0)
            {
                result |= bitboard << 9;
            }
            return result;
        }
        private static long GetKnightMoves(long bitboard)
        {
            long result = 0;
            int index = BitScanReverse(bitboard);
            if ((bitboard & (FILE_H | RANK_1 | RANK_2)) == 0)
            {
                result |= (long)1<<(index + 17);
            }
            if ((bitboard & (FILE_A | RANK_1 | RANK_2)) == 0)
            {
                result |= (long)1<<(index + 15);
            }
            if ((bitboard & (FILE_H | RANK_1 | FILE_G)) == 0)
            {
                result |= (long)1<<(index + 10);
            }
            if ((bitboard & (FILE_A | RANK_1 | FILE_B)) == 0)
            {
                result |= (long)1<<(index + 6);
            }
            if ((bitboard & (FILE_H | RANK_8 | FILE_G)) == 0)
            {
                result |= (long)1<<(index - 6);
            }
            if ((bitboard & (FILE_A | RANK_8 | FILE_B)) == 0)
            {
                result |= (long)1<<(index - 10);
            }
            if ((bitboard & (FILE_H | RANK_8 | RANK_7)) == 0)
            {
                result |= (long)1<<(index - 15);
            }
            if ((bitboard & (FILE_A | RANK_8 | RANK_7)) == 0)
            {
                result |= (long)1<<(index - 17);
            }
            return result;
        }
        private static void InitializeRays(int index)
        {
            RayAttackNorthWest[index] = 0;
            RayAttackNorth[index] = 0;
            RayAttackNorthEast[index] = 0;
            RayAttackEast[index] = 0;
            RayAttackSouthEast[index] = 0;
            RayAttackSouth[index] = 0;
            RayAttackSouthWest[index] = 0;
            RayAttackWest[index] = 0;
            int forwardSlash, backwardSlash;
            bool forwardStopMinus = false, backwardStopMinus = false;
            bool forwardStopPlus = false, backwardStopPlus = false;

            if (((long)1<<index & (RANK_8 | FILE_A)) != 0) forwardStopMinus = true;
            if (((long)1<<index & (RANK_8 | FILE_H)) != 0) backwardStopMinus = true;
            if (((long)1<<index & (RANK_1 | FILE_H)) != 0) forwardStopPlus = true;
            if (((long)1<<index & (RANK_1 | FILE_A)) != 0) backwardStopPlus = true;

            int minIndex = (index / 8) * 8;

            for (int i = 1; i < 8; i++)
            {
                //Forward is a8 to h1
                //Backward is a1 to h8
                forwardSlash = (8*i)+i;
                backwardSlash = (8*i)-i;

                if (!forwardStopPlus && index + forwardSlash < 64)
                {
                    RayAttackSouthEast[index] |= (long)1 << (index + forwardSlash);
                    if (((long)1<<(index + forwardSlash) & (RANK_1 | FILE_H)) != 0) forwardStopPlus = true;
                }
                if (!forwardStopMinus && index - forwardSlash >= 0)
                {
                    RayAttackNorthWest[index] |= (long)1 << (index - forwardSlash);
                    if (((long)1<<(index - forwardSlash) & (RANK_8 | FILE_A)) != 0) forwardStopMinus = true;
                }
                if (!backwardStopPlus && index + backwardSlash < 63)
                {
                    RayAttackSouthWest[index] |= (long)1 << (index + backwardSlash);
                    if (((long)1<<(index + backwardSlash) & (RANK_1 | FILE_A)) != 0) backwardStopPlus = true;
                }
                if (!backwardStopMinus && index - backwardSlash > 0)
                {
                    RayAttackNorthEast[index] |= (long)1 << (index - backwardSlash);
                    if (((long)1<<(index - backwardSlash) & (RANK_8 | FILE_H)) != 0) backwardStopMinus = true;
                }
                if (index + (8 * i) < 64)
                {
                    RayAttackSouth[index] |= (long)1 << (index + (8 * i));
                }
                if (index - (8 * i) > 0)
                {
                    RayAttackNorth[index] |= (long)1 << (index - (8 * i));
                }
                if (index + i < (minIndex + 8))
                {
                    RayAttackEast[index] |= (long)1 << (index + i);
                }
                if (index - i >= minIndex)
                {
                    RayAttackWest[index] |= (long)1 << (index - i);
                }
            }
        }
        private static long GetKingMoves(long bitboard)
        {
            long result = 0;
            int index = BitScanReverse(bitboard);
            int minIndex = (index / 8) * 8;
            if (index + 8 < 64)
            {
                result |= (long)1<<(index + 8);
            }
            if (index - 8 > 0)
            {
                result |= (long)1<<(index - 8);
            }
            if (index + 1 < (minIndex + 8))
            {
                result |= (long)1<<(index + 1);
            }
            if (index - 1 >= minIndex)
            {
                result |= (long)1<<(index - 1);
            }
            if ((bitboard & (FILE_H | RANK_1)) == 0)
            {
                result |= (long)1<<(index + 9);
            }
            if ((bitboard & (FILE_A | RANK_8)) == 0)
            {
                result |= (long)1<<(index - 9);
            }
            if ((bitboard & (FILE_A | RANK_1)) == 0)
            {
                result |= (long)1<<(index + 7);
            }
            if ((bitboard & (FILE_H | RANK_8)) == 0)
            {
                result |= (long)1<<(index - 7);
            }
            return result;
        }

        #endregion
        private static int BitScanReverse(long bitboard)
        {
            ulong bb = (ulong)bitboard;
            bb |= bb >> 1;
            bb |= bb >> 2;
            bb |= bb >> 4;
            bb |= bb >> 8;
            bb |= bb >> 16;
            bb |= bb >> 32;
            return INDEX64[(bb * DEBRUIJN_64) >> 58];
        }
        private static int BitScanForward(long b)
        {
            ulong bb = (ulong)b;
            return INDEX64[((bb ^ (bb-1)) * DEBRUIJN_64) >> 58];
        }
    }
}
