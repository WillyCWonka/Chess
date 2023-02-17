namespace Chess.Engine.Test;

[TestClass]
public class KingMoveRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.KingMoveRule();
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
        var r = new Rules.KingMoveRule();
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
    [DataRow(0, 0, 0, 1, true)]
    [DataRow(4, 4, 4, 5, true)]
    [DataRow(4, 7, 5, 6, true)]
    [DataRow(2, 2, 5, 2, false)]
    [DataRow(0, 0, 2, 1, false)]
    public void IsValid_MoveDataDistance(int origX, int origY, int destX, int destY, bool expected)
    {
        // arrange
        var r = new Rules.KingMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", origX, origY, destX, destY, MoveType.Standard);
        gs.Board[origX, origY] = Piece.King;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.AreEqual(expected, resp, "resp");
        Assert.AreEqual(resp ? "" : "Kings move only 1 square", reason, "reason");
    }
}