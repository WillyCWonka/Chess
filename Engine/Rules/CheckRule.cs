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

        for (int y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                if ((gs.Board[x, y] & Piece.Player1) == 0 && gs.Board[x, y] != Piece.None)
                {
                    var fakeM1 = new Move("", x, y, 4, 7, MoveType.Standard);

                    if (rules.All(r => r.IsValid(gs, fakeM1, out string fakeReason)))
                    {
                        reason = "You You can't put your King in check";
                        return false;
                    }
                }
            }
        }
    }
}

