using System;
namespace Chess.Engine.Rules;

internal class StartEndRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";

        if (m.Type != MoveType.Standard)
        {
            return true;
        }
        if (m.DestinationX == m.OriginX && m.DestinationY == m.OriginY)
        {
            reason = "You have to move a piece";
            return false;
        }
        return true;
    }
}

