namespace Chess.Engine.Rules;

internal class LongCastleRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.LongCastle)
        {
            return true;
        }
        return true;
        //TODO
    }
}

