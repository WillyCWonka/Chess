namespace Chess.Engine.Rules;

internal class CastleRule : Rule
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

        if (m.Type != MoveType.Castle)
        {
            return true;
        }

        if (gs.IsPlayer1)
        {
            if (gs.MoveHistory.Count(m => (m.OriginX == 4 && m.OriginY == 7) || (m.OriginX == 7 && m.OriginY == 7)) > 0)
            {
                reason = "King and Rook have to have not moved";
                return false;
            }

            if (gs.Board[5, 7] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            if (gs.Board[6, 7] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            for (int y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if ((gs.Board[x, y] & Piece.Player1) == 0 && gs.Board[x, y] != Piece.None)
                    {
                        var fakeM1 = new Move("", x, y, 4, 7, MoveType.Standard);
                        var fakeM2 = new Move("", x, y, 5, 7, MoveType.Standard);
                        var fakeM3 = new Move("", x, y, 6, 7, MoveType.Standard);

                        if (rules.All(r => r.IsValid(gs, fakeM1, out string fakeReason)) || rules.All(r => r.IsValid(gs, fakeM2, out string fakeReason)) || rules.All(r => r.IsValid(gs, fakeM3, out string fakeReason)))
                        {
                            reason = "You can't castle through check";
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        else
        {
            if (gs.MoveHistory.Count(m => (m.OriginX == 4 && m.OriginY == 0) || (m.OriginX == 7 && m.OriginY == 0)) > 0)
            {
                reason = "King and Rook have to have not moved";
                return false;
            }

            if (gs.Board[5, 0] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            if (gs.Board[6, 0] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            for (int y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if (gs.Board[x, y] == Piece.Player1)
                    {
                        var fakeM1 = new Move("", x, y, 4, 0, MoveType.Standard);
                        var fakeM2 = new Move("", x, y, 5, 0, MoveType.Standard);
                        var fakeM3 = new Move("", x, y, 6, 0, MoveType.Standard);

                        if (rules.All(r => r.IsValid(gs, fakeM1, out string fakeReason)) || rules.All(r => r.IsValid(gs, fakeM2, out string fakeReason)) || rules.All(r => r.IsValid(gs, fakeM3, out string fakeReason)))
                        {
                            reason = "You can't castle through check";
                            return false;
                        }

                        return true;
                    }
                }
            }

            return true;
        }
    }
}