namespace Chess.Engine.Test;

[TestClass]
public class BishopMoveRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.BishopMoveRule();
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
        var r = new Rules.BishopMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Knight;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [DataTestMethod]
    [DataRow(4, 4, 5, 3, true)]
    [DataRow(4, 4, 6, 6, true)]
    [DataRow(4, 4, 7, 7, true)]
    [DataRow(4, 4, 5, 6, false)]
    [DataRow(0, 0, 5, 1, false)]
    public void IsValid_MoveDataDirection(int origX, int origY, int destX, int destY, bool expected)
    {
        // arrange
        var r = new Rules.BishopMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", origX, origY, destX, destY, MoveType.Standard);
        gs.Board[origX, origY] = Piece.Bishop;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.AreEqual(expected, resp, "resp");
        Assert.AreEqual(resp ? "" : "Must move on a diagnal", reason, "reason");
    }

    [TestMethod]
    public void IsValid_PieceInWay()
    {
        // arrange
        var r = new Rules.BishopMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 7, 7, MoveType.Standard);
        gs.Board[0, 0] = Piece.Bishop;
        gs.Board[2, 2] = Piece.Pawn;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("Piece in the way", reason, "reason");
    }
}