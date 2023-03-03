namespace Chess.Engine;
public class GameState
{
    public Piece[,] Board { get; }
    public List<Move> MoveHistory { get; }

    public IPlayer CurrentPlayer { get; internal set; }
    public bool IsPlayer1 { get; internal set; }
    public bool IsInCheck { get; internal set; }

    public Status Status { get; internal set; }

    public GameState(IPlayer startingPlayer)
    {
        Board = new Piece[8, 8];
        MoveHistory = new List<Move>();
        CurrentPlayer = startingPlayer;
        IsPlayer1 = true;

    }

    public GameState(GameState gs)
    {
        Board = (Piece[,])gs.Board.Clone();
        MoveHistory = new List<Move>(gs.MoveHistory);
        CurrentPlayer = (IPlayer)gs.CurrentPlayer;
        IsPlayer1 = (bool)gs.IsPlayer1;
    }

    public void SetupDefaultBoard()
    {
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

    internal void MakeMove(Move move)
    {
        switch (move.Type)
        {
            case MoveType.Standard:
                MovePiece(move.OriginX, move.OriginY, move.DestinationX, move.DestinationY);
                break;
            case MoveType.Castle:
                if (IsPlayer1)
                {
                    //   move king from 4,7 to 6,7
                    MovePiece(4, 7, 6, 7);
                    //   move rook from 7,7 to 5,7
                    MovePiece(7, 7, 5, 7);
                }
                else
                {
                    //   move king to 4,0 to 2,0
                    MovePiece(4, 0, 6, 0);
                    //   move rook to 0,7 to 2,7
                    MovePiece(7, 0, 5, 0);
                }
                break;
            case MoveType.LongCastle:
                if (IsPlayer1)
                {
                    //   move king from 4,0 to 2,0
                    MovePiece(4, 7, 2, 7);
                    //   move rook from 0,7 to 3,7
                    MovePiece(0, 7, 3, 7);
                }
                else
                {
                    //   move king from 4,0 to 5,7
                    MovePiece(4, 0, 2, 0);
                    //   move rook from 7,7 to 4,7
                    MovePiece(7, 0, 3, 0);
                }
                break;
            case MoveType.EnPassant:
                MovePiece(move.OriginX, move.OriginY, move.DestinationX, move.DestinationY);
                // the last piece moved must have been the pawn that's being captured
                Board[MoveHistory.Last().DestinationX, MoveHistory.Last().DestinationY] = Piece.None;
                break;
            case MoveType.Concede:
                if (IsPlayer1)
                {
                    Status = Status.Player2Win;
                }
                else
                {
                    Status = Status.Player1Win;
                }
                return;
        }
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
