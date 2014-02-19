using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Engine
{
    class Bitboards
    {
        long WhitePawn {get; set;}
        long WhiteKnight {get; set;}
        long WhiteBishop {get; set;}
        long WhiteRook {get; set;}
        long WhiteQueen {get; set;}
        long WhiteKing {get; set;}
        long BlackPawn {get; set;}
        long BlackKnight {get; set;}
        long BlackBishop {get; set;}
        long BlackRook {get; set;}
        long BlackQueen {get; set;}
        long BlackKing {get; set;}
        public void GenerateStartingBoard()
        {
            ArrayToBitboard(new char[,] {{'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'},
                                         {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                                         {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                         {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                         {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                         {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                         {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                         {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'}});
        }
        public void ArrayToBitboard(char[,] board)
        {
            for(int i = 0; i < 64; i++)
            {
                string binary = "0000000000000000000000000000000000000000000000000000000000000000";
                binary = binary.Substring(i + 1) + "1" + binary.Substring(0, i);
                switch (board[i / 8,i % 8])
                {
                    case 'r':
                        BlackRook += Convert.ToInt64(binary, 2);
                        break;
                    case 'n':
                        BlackKnight += Convert.ToInt64(binary, 2);
                        break;
                    case 'b':
                        BlackBishop += Convert.ToInt64(binary, 2);
                        break;
                    case 'q':
                        BlackQueen += Convert.ToInt64(binary, 2);
                        break;
                    case 'k':
                        BlackKing += Convert.ToInt64(binary, 2);
                        break;
                    case 'p':
                        BlackPawn += Convert.ToInt64(binary, 2);
                        break;
                    case 'R':
                        WhiteRook += Convert.ToInt64(binary, 2);
                        break;
                    case 'N':
                        WhiteKnight += Convert.ToInt64(binary, 2);
                        break;
                    case 'B':
                        WhiteBishop += Convert.ToInt64(binary, 2);
                        break;
                    case 'Q':
                        WhiteQueen += Convert.ToInt64(binary, 2);
                        break;
                    case 'K':
                        WhiteKing += Convert.ToInt64(binary, 2);
                        break;
                    case 'P':
                        WhitePawn += Convert.ToInt64(binary, 2);
                        break;
                    default:
                        break;
                }
            }
        }
        public void PrintBoard()
        {
            char[,] board = BitboardsToArray();
            string line = "| ";
            for (int i = 63; i >= 0; i--)
            {
                line += board[i / 8, i % 8] + " | ";
                if (i % 8 == 0 && i != 63)
                {
                    line += "\n________________________________";
                    if(i != 0)
                    {
                        line += "\n| ";
                    }
                }
            }
            Console.WriteLine(line);
        }
        public char[,] BitboardsToArray()
        {
            char[,] board = new char[8,8];
            for (int i = 63; i >= 0; i-- )
            {
                if(Convert.ToString(BlackRook, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8,i % 8] = 'r';
                }
                else if (Convert.ToString(BlackQueen, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'q';
                }
                else if (Convert.ToString(BlackPawn, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'p';
                }
                else if (Convert.ToString(BlackKnight, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'n';
                }
                else if (Convert.ToString(BlackKing, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'k';
                }
                else if (Convert.ToString(BlackBishop, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'b';
                }
                else if (Convert.ToString(WhiteBishop, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'B';
                }
                else if (Convert.ToString(WhiteKing, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'K';
                }
                else if (Convert.ToString(WhiteRook, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'R';
                }
                else if (Convert.ToString(WhiteKnight, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'N';
                }
                else if (Convert.ToString(WhiteQueen, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'Q';
                }
                else if (Convert.ToString(WhitePawn, 2).PadLeft(64)[i] == '1')
                {
                    board[i / 8, i % 8] = 'P';
                }
                else
                {
                    board[i / 8, i % 8] = ' ';
                }
            }
            return board;
        }
    }
}
