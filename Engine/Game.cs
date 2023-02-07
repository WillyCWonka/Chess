namespace Chess.Engine;
public class Game
{
    private GameState gs;

    private IPlayer player1;
    private IPlayer player2;

    public Game(IPlayer player1, IPlayer player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        gs = new GameState(player1);
    }

    public Status Run()
    {
        //TODO: game run logic
        while(gs.Status == Status.Playing)
        {
            var playerMove = gs.CurrentPlayer.GetMove(gs);

            var move = ParseMove(playerMove);
            if (move == null)
            {
                gs.CurrentPlayer.BadInput(playerMove);
                continue;
            }

            // validate the move
            if (!ValidateMove(move.Value, out string reason))
            {
                gs.CurrentPlayer.IllegalMove(playerMove, reason);
                continue;
            }

            ExecuteMove(move.Value);

            // next players turn
            if (IsPlayer1CurrentPlayer())
            {
                gs.CurrentPlayer = player2;
            }
            else
            {
                gs.CurrentPlayer = player1;
            }
        }

        return gs.Status;
    }

    private bool IsPlayer1CurrentPlayer()
    {
        return gs.CurrentPlayer == player1;
    }

    private Move? ParseMove(string chessNotation)
    {
        //TODO: convert chess string to move class
        return null;
    }

    private bool ValidateMove(Move move, out string reason)
    {
        //TODO: make sure move doesn't break any rules
        reason = "";
        return true;
    }


    private void ExecuteMove(Move move)
    {
        // update our game state with the new move
        switch (move.Type)
        {
            case MoveType.Standard:
                gs.MovePiece(move.OriginX, move.OriginY, move.DestinationX, move.DestinationY);
                break;
            case MoveType.Castle:
                if (IsPlayer1CurrentPlayer())
                {
                    //   move king from 4,0 to 6,0
                    gs.MovePiece(4, 0, 6, 0);
                    //   move rook from 7,0 to 5,0
                    gs.MovePiece(7, 0, 5, 0);
                }
                else
                {
                    //   move king to 3,7 to 1,7
                    gs.MovePiece(3, 7, 1, 7);
                    //   move rook to 0,7 to 2,7
                    gs.MovePiece(0, 7, 2, 7);
                }
                break;
            case MoveType.LongCastle:
                if (IsPlayer1CurrentPlayer())
                {
                    //   move king from 4,0 to 2,0
                    gs.MovePiece(4, 0, 2, 0);
                    //   move rook from 0,0 to 3,0
                    gs.MovePiece(0, 0, 3, 0);
                }
                else
                {
                    //   move king from 3,7 to 5,7
                    gs.MovePiece(3, 7, 5, 7);
                    //   move rook from 7,7 to 4,7
                    gs.MovePiece(7, 7, 4, 7);
                }
                break;
            case MoveType.EnPassant:
                gs.MovePiece(move.OriginX, move.OriginY, move.DestinationX, move.DestinationY);
                // the last piece moved must have been the pawn that's being captured
                gs.Board[gs.MoveHistory.Last().DestinationX, gs.MoveHistory.Last().DestinationY] = Piece.None;
                break;
            case MoveType.Concede:
                if (IsPlayer1CurrentPlayer())
                {
                    gs.Status = Status.Player2Win;
                }
                else
                {
                    gs.Status = Status.Player1Win;
                }
                return;
        }

        gs.IsInCheck = IsCheck();
        if (gs.IsInCheck)
        {
            if (IsCheckMate())
            {
                if (IsPlayer1CurrentPlayer())
                {
                    gs.Status = Status.Player2Win;
                }
                else
                {
                    gs.Status = Status.Player1Win;
                }
            }
        }
        else if (IsStalemate())
        {
            gs.Status = Status.Stalemate;
        }

        gs.MoveHistory.Add(move);
    }

    private bool IsCheck()
    {
        //TODO: check non-current player for check
        return false;
    }

    private bool IsCheckMate()
    {
        //TODO: check non-current player for checkmate
        return false;
    }

    private bool IsStalemate()
    {
        //TODO: check non-current player for stalemate
        return false;
    }
}
