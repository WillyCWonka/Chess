namespace Chess.Engine.Rules;

internal class CastleRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";

        if (m.Type != MoveType.Castle)
        {
            return true;
        }
        return true;
        //TODO
    }
}


