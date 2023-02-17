namespace Chess.Engine.Test;

[TestClass]
public class QueenMoveRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.QueenMoveRule();
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
        var r = new Rules.QueenMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Rook;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [TestMethod]
    public void IsValid_PieceInWayDiagnal()
    {
        // arrange
        var r = new Rules.QueenMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 7, 7, MoveType.Standard);
        gs.Board[0, 0] = Piece.Queen;
        gs.Board[2, 2] = Piece.Pawn;

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
        var r = new Rules.QueenMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 1, 0, 7, MoveType.Standard);
        gs.Board[0, 1] = Piece.Queen;
        gs.Board[0, 6] = Piece.Pawn;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("Piece in the way", reason, "reason");
    }

    [TestMethod]
    public void IsValid_PieceInWayY()
    {
        // arrange
        var r = new Rules.QueenMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 1, 0, 7, 0, MoveType.Standard);
        gs.Board[1, 0] = Piece.Queen;
        gs.Board[6, 0] = Piece.Pawn;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("Piece in the way", reason, "reason");
    }

    [DataTestMethod]
    [DataRow(2, 4, 5, 7, true)]
    [DataRow(4, 4, 4, 6, true)]
    [DataRow(4, 4, 5, 6, false)]
    [DataRow(0, 0, 5, 1, false)]
    public void IsValid_MoveDataDirection(int origX, int origY, int destX, int destY, bool expected)
    {
        // arrange
        var r = new Rules.QueenMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", origX, origY, destX, destY, MoveType.Standard);
        gs.Board[origX, origY] = Piece.Queen;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.AreEqual(expected, resp, "resp");
        Assert.AreEqual(resp ? "" : "Queens must move in a straight line", reason, "reason");
    }
}