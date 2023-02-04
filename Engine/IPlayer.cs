namespace Chess.Engine;
public interface IPlayer
{
    string GetMove(GameState gs);

    void BadInput(string move);
    void IllegalMove(string move, string reason);
}