namespace Chess.Engine.Test;

[TestClass]
public class OwnDestRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.OwnDestRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Castle);

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [TestMethod]
    public void IsValid_OwnDest()
    {
        // arrange
        var r = new Rules.OwnDestRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 1, 1, MoveType.Standard);
        gs.Board[1, 1] = Piece.Knight | Piece.Player1;


        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("Ending square must not have a piece you own", reason, "reason");
    }
}