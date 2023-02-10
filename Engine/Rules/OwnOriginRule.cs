using System;
namespace Chess.Engine.Rules;

internal class OwnOriginRule : Rule
{

    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard && m.Type != MoveType.EnPassant)
        {
            return true;
        }

        if (base.OriginPieceIsNone(gs, m) || base.OriginPieceIs(gs, m, Piece.Player1) != gs.IsPlayer1)
        {
            reason = "Starting square must have a piece you own";
            return false;
        }

        return true;
    }
}

