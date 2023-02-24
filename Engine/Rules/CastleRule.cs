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
        }

        //TODO check on these squarew

        return true;
    }
}
