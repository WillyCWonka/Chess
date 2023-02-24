namespace Chess.Engine.Test;

[TestClass]
public class PawnMoveRuleTest
{
    [TestMethod]
    public void IsValid_WrongType()
    {
        // arrange
        var r = new Rules.PawnMoveRule();
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
        var r = new Rules.PawnMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 0, 0, 0, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Knight;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [TestMethod]
    public void IsValid_WrongDirection()
    {
        // arrange
        var r = new Rules.PawnMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 6, 0, 5, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Pawn | Piece.Player1;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }

    [TestMethod]
    public void IsValid_twoSpaces()
    {
        // arrange
        var r = new Rules.PawnMoveRule();
        var gs = new GameState(new Moq.Mock<IPlayer>().Object);
        var m = new Move("", 6, 0, 4, 0, MoveType.Standard);
        gs.Board[0, 0] = Piece.Pawn | Piece.Player1;

        // act
        var resp = r.IsValid(gs, m, out string reason);

        // assert
        Assert.IsTrue(resp, "resp");
        Assert.AreEqual("", reason, "reason");
    }
}