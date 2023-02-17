using System;
namespace Chess.Engine.Rules;

internal class QueenMoveRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard)
        {
            return true;
        }

        if (!OriginPieceIs(gs, m, Piece.Queen))
        {
            return true;
        }

        if (Math.Abs(m.OriginX - m.DestinationX) == Math.Abs(m.OriginY - m.DestinationY))
        {
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
        else if (m.OriginX == m.DestinationX)
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
            return true;
        }
        else if (m.OriginY == m.DestinationY)
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
            return true;
        }

        reason = "Queens must move in a straight line";
        return false;

    }
}


