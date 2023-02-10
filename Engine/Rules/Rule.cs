namespace Chess.Engine.Rules;
internal abstract class Rule
{
    internal abstract bool IsValid(GameState gs, Move m, out string reason);
    // validate move

    // apply move to game state

    protected bool OriginPieceIs(GameState gs, Move m, Piece p)
    {
        return (gs.Board[m.OriginX, m.OriginY] & p) > 0;
    }
    protected bool DestPieceIs(GameState gs, Move m, Piece p)
    {
        return (gs.Board[m.DestinationX, m.DestinationY] & p) > 0;
    }

    protected bool OriginPieceIsNone(GameState gs, Move m)
    {
        return gs.Board[m.OriginX, m.OriginY] == Piece.None;
    }
    protected bool DestPieceIsNone(GameState gs, Move m)
    {
        return gs.Board[m.DestinationX, m.DestinationY] == Piece.None;
    }
}




// you must own the piece at the origin 
// there must not be a piece you own at the destination
// you must not start and end on the same spot
