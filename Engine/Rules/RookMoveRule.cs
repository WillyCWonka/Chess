using System;
namespace Chess.Engine.Rules;

internal class RookMoveRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard)
        {
            return true;
        }

        if (!OriginPieceIs(gs, m, Piece.Rook))
        {
            return true;
        }

        if (m.OriginX != m.DestinationX && m.OriginY != m.DestinationY)
        {
            reason = "Invalid move direction";
            return false;
        }

        if (m.OriginX == m.DestinationX)
        {
            int inc = 1;
            if (m.DestinationY < m.OriginY)
            {
                inc = -1;
            }
            for (var y = m.OriginY + inc; y != m.DestinationY; y += inc)
            {
                if (gs.Board[m.OriginX, y] != Piece.None)
                {
                    reason = "Piece in the way";
                    return false;
                }
            }
        }
        else
        {
            int inc = 1;
            if (m.DestinationX < m.OriginX)
            {
                inc = -1;
            }
            for (var x = m.OriginX + inc; x != m.DestinationX; x += inc)
            {
                if (gs.Board[x, m.OriginY] != Piece.None)
                {
                    reason = "Piece in the way";
                    return false;
                }
            }
        }
        return true;
    }
}


