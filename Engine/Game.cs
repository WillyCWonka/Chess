using System.Text.RegularExpressions;
using Chess.Engine.Rules;

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
        gs.SetupDefaultBoard();
    }

    public Status Run()
    {
        //game run logic
        while (gs.Status == Status.Playing)
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
            gs.IsPlayer1 = !gs.IsPlayer1;
            if (gs.IsPlayer1)
            {
                gs.CurrentPlayer = player1;
            }
            else
            {
                gs.CurrentPlayer = player2;
            }
        }

        return gs.Status;
    }

    private bool IsPlayer1CurrentPlayer()
    {
        return gs.CurrentPlayer == player1;
    }

    private static Regex pattern = new Regex(@"^(?<origX>[a-h])(?<origY>[1-8])\s*(?<destX>[a-h])(?<destY>[1-8])$");

    private Move? ParseMove(string chessNotation)
    {

        // Bd3 bishop to d3
        // b3 pawn to b3
        // Ncb3 knight from c to b3, only required if another knight can move to b3
        // O-O castle
        // O-O-O queen side castle
        // concede
        if (chessNotation == "O-O")
        {
            return new Move(chessNotation, 0, 0, 0, 0, MoveType.Castle);
        }
        else if (chessNotation == "O-O-O")
        {
            return new Move(chessNotation, 0, 0, 0, 0, MoveType.LongCastle);
        }
        else if (chessNotation == "concede")
        {
            return new Move(chessNotation, 0, 0, 0, 0, MoveType.Concede);
        }

        var m = pattern.Match(chessNotation);
        if (!m.Success)
        {
            return null;
        }

        int origX = "abcdefgh".IndexOf(m.Groups["origX"].Value);
        int origY = "87654321".IndexOf(m.Groups["origY"].Value);

        int destX = "abcdefgh".IndexOf(m.Groups["destX"].Value);
        int destY = "87654321".IndexOf(m.Groups["destY"].Value);

        // moving a pawn into an adjacent column with no taking piece
        if ((gs.Board[origX, origY] & Piece.Pawn) > 0 &&
            gs.Board[destX, destY] == Piece.None &&
            origX != destX)
        {
            return new Move(chessNotation, origX, origY, destX, destY, MoveType.EnPassant);
        }

        return new Move(chessNotation, origX, origY, destX, destY, MoveType.Standard);
    }

    static readonly Rules.Rule[] rules =
    {
        new StartEndRule(),
        new OwnOriginRule(),
        new OwnDestRule(),
        new RookMoveRule(),
        new BishopMoveRule(),
        new PawnMoveRule(),
        new QueenMoveRule(),
        new KnightMoveRule(),
        new KingMoveRule(),
        new CastleRule(),
        new EnPassantRule(),
        new CheckRule(),
        new LongCastleRule()

    };


    private bool ValidateMove(Move move, out string reason)
    {
        foreach (var rule in rules)
        {
            if (!rule.IsValid(gs, move, out reason))
            {
                return false;
            }
        }
        reason = "";
        return true;
    }


    private void ExecuteMove(Move move)
    {
        // update our game state with the new move
        gs.MakeMove(move);

        if (gs.Status != Status.Playing)
        {
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
