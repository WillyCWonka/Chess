namespace Chess.Engine;

public readonly record struct Move(string ChessNotation, int OriginX, int OriginY, int DestinationX, int DestinationY, MoveType Type);

public enum MoveType
{
    Standard,
    Castle,
    LongCastle,
    EnPassant,
    Concede,
}