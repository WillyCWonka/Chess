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

        Board[7, 0] = Board[0, 0] = Piece.Rook;
        Board[6, 0] = Board[1, 0] = Piece.Knight;
        Board[5, 0] = Board[2, 0] = Piece.Bishop;
        Board[3, 0] = Piece.Queen;
        Board[4, 0] = Piece.King;
        Board[7, 1] = Board[6, 1] = Board[5, 1] = Board[4, 1] = Board[3, 1] = Board[2, 1] = Board[1, 1] = Board[0, 1] = Piece.Pawn;

        Board[7, 7] = Board[0, 7] = Piece.Rook | Piece.Player1;
        Board[6, 7] = Board[1, 7] = Piece.Knight | Piece.Player1;
        Board[5, 7] = Board[2, 7] = Piece.Bishop | Piece.Player1;
        Board[3, 7] = Piece.Queen | Piece.Player1;
        Board[4, 7] = Piece.King | Piece.Player1;
        Board[7, 6] = Board[6, 6] = Board[5, 6] = Board[4, 6] = Board[3, 6] = Board[2, 6] = Board[1, 6] = Board[0, 6] = Piece.Pawn | Piece.Player1;
    }

    internal void MovePiece(int fromX, int fromY, int toX, int toY)
    {
        Board[toX, toY] = Board[fromX, fromY];
        Board[fromX, fromY] = Piece.None;
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
    Player1 = 64
}

public enum Status
{
    Playing,
    Player1Win,
    Player2Win,
    Stalemate
}
