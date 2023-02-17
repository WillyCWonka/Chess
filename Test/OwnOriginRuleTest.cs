namespace Chess.Engine.Test;

[TestClass]
public class OwnOriginRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.OwnOriginRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Castle);

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [TestMethod]
    public void IsValid_NotYourPiece()
    {
        // arrange
        var r = new Rules.OwnOriginRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Knight | Piece.Player1;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }
}
