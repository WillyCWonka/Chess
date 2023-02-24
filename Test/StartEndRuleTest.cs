namespace Chess.Engine.Test;

[TestClass]
public class StartEndRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.StartEndRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Standard);

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsFalse(resp, "resp");
        Assert.AreEqual("You have to move a piece", reason, "r");
    }
}