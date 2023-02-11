using System;
namespace Chess.Engine.Rules;

internal class PawnMoveRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard)
        {
            return true;
        }

        if (!OriginPieceIs(gs, m, Piece.Pawn))
        {
            return true;
        }

        int inc = OriginPieceIs(gs, m, Piece.Player1) ? -1 : 1;

        // code for when a pawn captures something
        if (m.DestinationX != m.OriginX)
        {
            if (m.DestinationY != m.OriginY + inc)
            {
                reason = "Pawns only move 1 square";
                return false;
            }

            if (Math.Abs(m.DestinationX - m.OriginX) != 1)
            {
                reason = "Pawns only move 1 square";
                return false;
            }

            if (base.DestPieceIsNone(gs, m))
            {
                reason = "Pawns only move diagnally while capturing";
                return false;
            }

            return true;
        }

        if (Math.Abs(m.OriginY - m.DestinationY) == 2)
        {
            if (m.OriginY == 1 || m.OriginY == 6)
            {
                if (gs.Board[m.DestinationX, m.DestinationY] != Piece.None || gs.Board[m.OriginX, m.OriginY + inc] != Piece.None)
                {
                    reason = "Must move to an empty square";
                    return false;
                }

                return true;
            }
        }

        if (m.DestinationY == m.OriginY + inc)
        {
            if (base.DestPieceIsNone(gs, m))
            {
                return true;
            }
        }

        return false;
    }
}