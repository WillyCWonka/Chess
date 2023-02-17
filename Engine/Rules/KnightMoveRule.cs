namespace Chess.Engine.Rules;

internal class KnightMoveRule : Rule
{
    internal override bool IsValid(GameState gs, Move m, out string reason)
    {
        reason = "";
        if (m.Type != MoveType.Standard)
        {
            return true;
        }

        if (!OriginPieceIs(gs, m, Piece.Knight))
        {
            return true;
        }

        if (Math.Abs(m.OriginX - m.DestinationX) == 2 && Math.Abs(m.OriginY - m.DestinationY) == 1
            ||
            Math.Abs(m.OriginX - m.DestinationX) == 1 && Math.Abs(m.OriginY - m.DestinationY) == 2)
        {
            return true;
        }

        reason = "Knights move in 'L'";
        return false;
    }
}
