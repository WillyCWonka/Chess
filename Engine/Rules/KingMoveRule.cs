using System;
namespace Chess.Engine.Rules;

internal class KingMoveRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard)
        {
            return true;
        }

        if (!OriginPieceIs(gs, m, Piece.King))
        {
            return true;
        }

        if (Math.Abs(m.OriginX - m.DestinationX) <= 1 && Math.Abs(m.OriginY - m.DestinationY) <= 1)
        {
            return true;
        }

        reason = "Kings move only 1 square";
        return false;
    }
}

