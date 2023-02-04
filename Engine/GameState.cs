namespace Chess.Engine;
public class GameState
{
    public Piece[,] Board { get; }
    public List<Move> MoveHistory { get; }

    public IPlayer CurrentPlayer { get; internal set; }
    public bool IsInCheck { get; internal set; }

    public Status Status { get; internal set; }

    public GameState(IPlayer startingPlayer)
    {
        Board = new Piece[8, 8];
        MoveHistory = new List<Move>();
        CurrentPlayer = startingPlayer;
    }
}

[Flags]
public enum Piece : byte
{
    None = 0,
    Pawn = 1,
    Knight = 2,
    Bishop = 4,
    Rook = 8,
    Queen = 16,
    King = 32,
    Player1 = 64,
    Player2 = 128
}

public enum Status
{
    Playing,
    Player1Win,
    Player2Win,
    Stalemate
}
