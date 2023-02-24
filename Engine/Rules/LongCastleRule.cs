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

        if (gs.IsPlayer1)
        {
            if (gs.MoveHistory.Count(m => (m.OriginX == 4 && m.OriginY == 7) || (m.OriginX == 0 && m.OriginY == 7)) > 0)
            {
                reason = "King and Rook have to have not moved";
                return false;
            }

            if (gs.Board[3, 7] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            if (gs.Board[2, 7] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            if (gs.Board[1, 7] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }
        }
        else
        {
            if (gs.MoveHistory.Count(m => (m.OriginX == 4 && m.OriginY == 0) || (m.OriginX == 0 && m.OriginY == 0)) > 0)
            {
                reason = "King and Rook have to have not moved";
                return false;
            }

            if (gs.Board[3, 0] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            if (gs.Board[2, 0] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }

            if (gs.Board[1, 0] != Piece.None)
            {
                reason = "Piece in the way";
                return false;
            }
        }


        //TODO check on these squarew

        return true;
    }
}
