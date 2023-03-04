namespace Chess.Engine.Rules;

internal class CheckRule : Rule
{
    Rules.Rule[] rules =
   {
        new RookMoveRule(),
        new BishopMoveRule(),
        new PawnMoveRule(),
        new QueenMoveRule(),
        new KnightMoveRule(),
        new KingMoveRule()
    };

    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";

        var newGS = new GameState(gs);
        newGS.MakeMove(m);

        return !Helper.IsPlayerCheck(newGS, newGS.IsPlayer1);
    }
}