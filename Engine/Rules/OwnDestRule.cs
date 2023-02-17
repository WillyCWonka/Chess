using System;
namespace Chess.Engine.Rules;

internal class OwnDestRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard && m.Type != MoveType.EnPassant)
        {
            return true;
        }

        if (!base.DestPieceIsNone(gs, m) && base.DestPieceIs(gs, m, Piece.Player1) == gs.IsPlayer1)
        {
            reason = "Ending square must not have a piece you own";
            return false;
        }

        return true;
    }
}

