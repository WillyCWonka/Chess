using System;
using Chess.Engine;

namespace Chess.UI;

public class ConsolePlayer : IPlayer
{
	public ConsolePlayer()
	{
	}

    public string GetMove(GameState gs)
    {
        bool bgColor=false;
        // print the board
        
        for (var y = 8; y > 0; y--)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {y} ");
            for (var x = 0; x<8;x++)
            {
                var piece = gs.Board[x, 8 - y];
                bgColor = !bgColor;
                if (bgColor)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }

                if ((piece & Piece.Player1) !=0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (piece == Piece.None )
                {
                    Console.Write("   ");
                }
                else if ((piece & Piece.Pawn) !=0)
                {
                    Console.Write(" p ");
                }
                else if ((piece & Piece.Knight) != 0)
                {
                    Console.Write(" N ");
                }
                else if ((piece & Piece.Bishop) != 0)
                {
                    Console.Write(" B ");
                }
                else if ((piece & Piece.Rook) != 0)
                {
                    Console.Write(" R ");
                }
                else if ((piece & Piece.Queen) != 0)
                {
                    Console.Write(" Q ");
                }
                else if ((piece & Piece.King) != 0)
                {
                    Console.Write(" K ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            bgColor = !bgColor;
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("    a  b  c  d  e  f  g  h ");

        // get the user move
        string? input;
        do {
            Console.Write("? ");
            input = Console.ReadLine();
        } while (input == null);

        return input;
    }

    public void BadInput(string move)
    {
        Console.WriteLine("Please enter a validly formatted chess move.");
    }


    public void IllegalMove(string move, string reason)
    {
        Console.WriteLine("Please enter a legal move.");
    }
}

