﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Bitboards bb = new Engine.Bitboards();
            bb.GenerateStartingBoard();
            bb.PrintBoard();
            Console.Read();
        }
    }
}