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
            if (!ValidateMove(move, out string reason))
            {
                gs.CurrentPlayer.IllegalMove(playerMove, reason);
                continue;
            }

            ExecuteMove(move);

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
        // convert chess string to move class
    }

    private bool ValidateMove(Move move, out string reason)
    {
        // make sure move doesn't break any rules
    }


    private void ExecuteMove(Move move)
    {
        // update our game state with the new move
        //gs.Board;
        //gs.IsInCheck;
        //gs.Status;
        switch (move.Type)
        {
            case MoveType.Standard:
                break;
            case MoveType.Castle:
                break;
            case MoveType.LongCastle:
                break;
            case MoveType.EnPassant:
                break;
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
        // check non-current player for check
    }

    private bool IsCheckMate()
    {
        // check non-current player for checkmate
    }

    private bool IsStalemate()
    {
        // check non-current player for stalemate
    }
}
