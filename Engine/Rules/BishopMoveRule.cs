using System;
namespace Chess.Engine.Rules;

internal class BishopMoveRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard)
        {
            return true;
        }

        if (!OriginPieceIs(gs, m, Piece.Bishop))
        {
            return true;
        }

        if (Math.Abs(m.OriginX - m.DestinationX) != Math.Abs(m.OriginY - m.DestinationY))
        {
            reason = "Must move on a diagnal";
            return false;
        }

        int incX = 1;
        int incY = 1;

        if (m.DestinationX < m.OriginX)
        {
            incX = -1;
        }
        if (m.DestinationY < m.OriginY)
        {
            incY = -1;
        }

        for (int x = m.OriginX + incX, y = m.OriginY + incY; x != m.DestinationX; x += incX, y += incY)
        {
            if (gs.Board[x, y] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }
        }

        return true;
    }
}

