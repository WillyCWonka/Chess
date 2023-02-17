namespace Chess.Engine.Test;

[TestClass]
public class KnightMoveRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.KnightMoveRule();
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
    [DataRow(4, 4, 2, 3, true)]
    [DataRow(4, 4, 2, 5, true)]
    [DataRow(4, 4, 3, 2, true)]
    [DataRow(4, 4, 3, 6, true)]
    [DataRow(4, 4, 5, 2, true)]
    [DataRow(4, 4, 5, 6, true)]
    [DataRow(4, 4, 6, 3, true)]
    [DataRow(4, 4, 6, 5, true)]
    [DataRow(0, 0, 1, 1, false)]
    public void IsValid_MoveData(int origX, int origY, int destX, int destY, bool expected)
    {
        // arrange
        var r = new Rules.KnightMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", origX, origY, destX, destY, MoveType.Standard);
        gs.Board[origX, origY] = Piece.Knight;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.AreEqual(expected, resp, "resp");
        Assert.AreEqual(resp ? "" : "Knights move in 'L'", reason, "reason");
    }
}
