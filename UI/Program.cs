using Chess.Engine;

var p1 = new Chess.UI.ConsolePlayer();
var p2 = new Chess.UI.ConsolePlayer();

var g = new Game(p1, p2);

var status = g.Run();
Console.Write(status);