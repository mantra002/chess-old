using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.BitBoards bb = new Engine.BitBoards();
            bb.GenerateStartingBoard();
            Engine.Moves.InitializeMoves();
            Engine.Moves.GenerateValidMoves(bb, Engine.Reference.Color.White);
            bb.PrintBitboard(Engine.Moves.ValidPawnMoves);
            bb.PrintBitboard(Engine.Moves.ValidKnightMoves);
            bb.PrintBitboard(Engine.Moves.ValidKingMoves);
            bb.PrintBitboard(Engine.Moves.ValidQueenMoves);
            bb.PrintBitboard(Engine.Moves.ValidRookMoves);
            bb.PrintBitboard(Engine.Moves.ValidBishopMoves);
            Console.Read();
        }
    }
}
