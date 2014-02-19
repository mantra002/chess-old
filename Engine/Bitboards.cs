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
        Random r = new Random();
        public void GenerateStartingBoard()
        {
            ArrayToBitboard(new char[][] {
                new char[] {'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r'},
                new char[] {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                new char[] {'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'}
            });
        }
        public void GenerateStartingChess960Board()
        {
            List<int> openSquares = new List<int>();
            openSquares.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            
            char[][] baseBoard = new char[][] {
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                new char[] {'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                new char[] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
            };
            int d4, d8, d6, n1, n2;

            d8 = r.Next(0, 8);
            d6 = r.Next(0, 6);
            d4 = r.Next(0, 4);
            n1 = r.Next(0, 5);
            n2 = r.Next(0, 4);

            Chess960PlaceStartingPeice(d8, 'b', ref openSquares, ref baseBoard);

            if(d8 % 2 == 0)
            {
                d4 = d4 * 2 + 1;
            }
            else
            {
                d4 = d4 * 2;
            }
            Chess960PlaceStartingPeice(d4, 'b', ref openSquares, ref baseBoard);
            Chess960PlaceStartingPeice(openSquares[d6], 'q', ref openSquares, ref baseBoard);
            Chess960PlaceStartingPeice(openSquares[n1], 'n', ref openSquares, ref baseBoard);
            Chess960PlaceStartingPeice(openSquares[n2], 'n', ref openSquares, ref baseBoard);
            Chess960PlaceStartingPeice(openSquares[0], 'r', ref openSquares, ref baseBoard);
            Chess960PlaceStartingPeice(openSquares[0], 'k', ref openSquares, ref baseBoard);
            Chess960PlaceStartingPeice(openSquares[0], 'r', ref openSquares, ref baseBoard);
            ArrayToBitboard(baseBoard);
        }
        private static void Chess960PlaceStartingPeice(int i, char peice, ref List<int> openSpots, ref char[][] board)
        {
            board[0][i] = peice.ToString().ToLower().ToCharArray()[0];
            board[7][i] = peice.ToString().ToUpper().ToCharArray()[0];
            openSpots.Remove(i);
        }
        public void ClearBoards()
        {
            this.BlackRook = 0;
            this.BlackKnight = 0;
            this.BlackBishop = 0;
            this.BlackQueen = 0;
            this.BlackKing = 0;  
            this.BlackPawn = 0;       
            this.WhiteRook = 0;
            this.WhiteKnight = 0;
            this.WhiteBishop = 0;
            this.WhiteQueen = 0;
            this.WhiteKing = 0;
            this.WhitePawn = 0;
        }
        public void ArrayToBitboard(char[][] board)
        {
            this.ClearBoards();
            for(int i = 0; i < 64; i++)
            {
                string binary = "0000000000000000000000000000000000000000000000000000000000000000";
                binary = binary.Substring(i + 1) + "1" + binary.Substring(0, i);
                switch (board[i / 8][i % 8])
                {
                    case 'r':
                        this.BlackRook += Convert.ToInt64(binary, 2);
                        break;
                    case 'n':
                        this.BlackKnight += Convert.ToInt64(binary, 2);
                        break;
                    case 'b':
                        this.BlackBishop += Convert.ToInt64(binary, 2);
                        break;
                    case 'q':
                        this.BlackQueen += Convert.ToInt64(binary, 2);
                        break;
                    case 'k':
                        this.BlackKing += Convert.ToInt64(binary, 2);
                        break;
                    case 'p':
                        this.BlackPawn += Convert.ToInt64(binary, 2);
                        break;
                    case 'R':
                        this.WhiteRook += Convert.ToInt64(binary, 2);
                        break;
                    case 'N':
                        this.WhiteKnight += Convert.ToInt64(binary, 2);
                        break;
                    case 'B':
                        this.WhiteBishop += Convert.ToInt64(binary, 2);
                        break;
                    case 'Q':
                        this.WhiteQueen += Convert.ToInt64(binary, 2);
                        break;
                    case 'K':
                        this.WhiteKing += Convert.ToInt64(binary, 2);
                        break;
                    case 'P':
                        this.WhitePawn += Convert.ToInt64(binary, 2);
                        break;
                    default:
                        break;
                }
            }
        }
        public void PrintBoard()
        {
            char[][] board = BitboardsToArray();
            string line = "| ";
            for (int i = 63; i >= 0; i--)
            {
                line += board[i / 8][i % 8] + " | ";
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
        public void PrintBitboard(long board)
        {
            string strBoard = Convert.ToString(board, 2).PadLeft(64, '0');
            string line = "| ";
            for (int i = 63; i >= 0; i--)
            {
                line += strBoard[i] + " | ";
                if (i % 8 == 0 && i != 63)
                {
                    line += "\n________________________________";
                    if (i != 0)
                    {
                        line += "\n| ";
                    }
                }
            }
            Console.WriteLine(line);
        }
        public char[][] BitboardsToArray()
        {
            char[][] board = new char[8][];
            for (int i = 0; i < 8; i++)
            {
                board[i] = new char[8];
            }

                for (int i = 63; i >= 0; i--)
                {
                    if (Convert.ToString(BlackRook, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'r';
                    }
                    else if (Convert.ToString(BlackQueen, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'q';
                    }
                    else if (Convert.ToString(BlackPawn, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'p';
                    }
                    else if (Convert.ToString(BlackKnight, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'n';
                    }
                    else if (Convert.ToString(BlackKing, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'k';
                    }
                    else if (Convert.ToString(BlackBishop, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'b';
                    }
                    else if (Convert.ToString(WhiteBishop, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'B';
                    }
                    else if (Convert.ToString(WhiteKing, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'K';
                    }
                    else if (Convert.ToString(WhiteRook, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'R';
                    }
                    else if (Convert.ToString(WhiteKnight, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'N';
                    }
                    else if (Convert.ToString(WhiteQueen, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'Q';
                    }
                    else if (Convert.ToString(WhitePawn, 2).PadLeft(64)[i] == '1')
                    {
                        board[i / 8][i % 8] = 'P';
                    }
                    else
                    {
                        board[i / 8][i % 8] = ' ';
                    }
                }
            return board;
        }
    }
}
