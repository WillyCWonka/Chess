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

        //TODO
        return true;


    }
}

