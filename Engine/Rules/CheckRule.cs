namespace Chess.Engine.Rules;

internal class CheckRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        return true;
        //TODO
    }
}

