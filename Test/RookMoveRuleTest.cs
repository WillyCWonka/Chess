namespace Chess.Engine.Test;

[TestClass]
public class RookMoveRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.RookMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Castle);

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [TestMethod]
    public void IsValid_WrongPiece()
    {
        // arrange
        var r = new Rules.KnightMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Bishop;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [DataTestMethod]
    [DataRow(4, 4, 5, 4, true)]
    [DataRow(4, 4, 7, 4, true)]
    [DataRow(4, 4, 4, 1, true)]
    [DataRow(4, 2, 6, 2, true)]
    [DataRow(3, 2, 3, 7, true)]
    [DataRow(4, 4, 5, 6, false)]
    [DataRow(4, 4, 1, 2, false)]
    [DataRow(4, 4, 6, 5, false)]
    [DataRow(0, 0, 1, 1, false)]
    public void IsValid_MoveDataDirection(int origX, int origY, int destX, int destY, bool expected)
    {
        // arrange
        var r = new Rules.RookMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", origX, origY, destX, destY, MoveType.Standard);
        gs.Board[origX, origY] = Piece.Rook;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.AreEqual(expected, resp, "resp");
        Assert.AreEqual(resp ? "" : "Invalid move direction", reason, "reason");
    }

    [TestMethod]
    public void IsValid_PieceInWayY()
    {
        // arrange
        var r = new Rules.RookMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 6, MoveType.Standard);
        gs.Board[0, 0] = Piece.Rook;
        gs.Board[0, 2] = Piece.Pawn;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("Piece in the way", reason, "reason");
    }

    [TestMethod]
    public void IsValid_PieceInWayX()
    {
        // arrange
        var r = new Rules.RookMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 6, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Rook;
        gs.Board[2, 0] = Piece.Pawn;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("Piece in the way", reason, "reason");
    }
}