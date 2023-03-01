using System;
namespace Chess.Engine.Rules;

internal class EnPassantRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.EnPassant)
        {
            return true;
        }

        int inc = OriginPieceIs(gs, m, Piece.Player1) ? -1 : 1;

        if (m.DestinationY == m.OriginY + inc && Math.Abs(m.OriginX - m.DestinationX) == 1)
        {
            if (gs.Board[gs.MoveHistory.Last().DestinationX, gs.MoveHistory.Last().DestinationY] != Piece.Pawn
                && Math.Abs(gs.MoveHistory.Last().OriginY - gs.MoveHistory.Last().DestinationY) == 2)
            {
                if (gs.MoveHistory.Last().DestinationX == m.DestinationX)
                {
                    return true;
                }

                reason = "You have to move to the same column the other pawn did";
            }

            reason = "The last most have been a double pawn move";
            return false;
        }

        reason = "Pawns only go forwards";
        return false;
    }
}
