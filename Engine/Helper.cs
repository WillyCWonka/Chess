using System.Net.NetworkInformation;
using Chess.Engine.Rules;

namespace Chess.Engine;
public class Helper
{
    static Rules.Rule[] rules =
  {
        new RookMoveRule(),
        new BishopMoveRule(),
        new PawnMoveRule(),
        new QueenMoveRule(),
        new KnightMoveRule(),
        new KingMoveRule()
    };

    internal static bool IsPlayerCheck(GameState gs, bool isPlayer1)
    {
        var king = Piece.King;
        var pieces = Piece.Player1;
        if (isPlayer1)
        {
            king = Piece.King | Piece.Player1;
            pieces = 0;
        }

        int kingX = 0;
        int kingY = 0;
        for (int y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                if (gs.Board[x, y] == king)
                {
                    kingX = x;
                    kingY = y;
                }
            }
        }

        for (int y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                if ((gs.Board[x, y] & Piece.Player1) == pieces && gs.Board[x, y] != Piece.None)
                {
                    var fakeM = new Move("", x, y, kingX, kingY, MoveType.Standard);

                    if (rules.All(r => r.IsValid(gs, fakeM, out string fakeReason)))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    internal static bool NoLegalMoves(GameState gs, bool isPlayer1)
    {
        var pieces = Piece.Player1;
        if (isPlayer1)
        {
            pieces = 0;
        }

        for (int startY = 0; startY < 8; startY++)
        {
            for (var startX = 0; startX < 8; startX++)
            {
                if ((gs.Board[startX, startY] & Piece.Player1) == pieces && gs.Board[startX, startY] != Piece.None)
                {
                    for (var endY = 0; endY < 8; endY++)
                    {
                        for (var endX = 0; endX < 8; endX++)
                        {
                            var fakeM = new Move("", startX, startY, endX, endY, MoveType.Standard);

                            if (rules.All(r => r.IsValid(gs, fakeM, out string fakeReason)))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }

        return true;
    }
}

