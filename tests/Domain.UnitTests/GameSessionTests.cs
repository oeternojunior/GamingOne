using GamingOne.Modules.Gaming.Domain.Enums;
using GamingOne.Modules.Gaming.Domain.Models;
using GamingOne.Modules.Gaming.Domain.Services;
using Moq;

namespace Domain.UnitTests
{
    public class GameSessionTests
    {
        private readonly Mock<IRandomNumberGenerator> _rngMock = new();
        private readonly Guid _gameId = Guid.NewGuid();
        private const int Min = 1, Max = 100;

        public GameSessionTests()
        {
            _rngMock.Setup(x => x.Generate(Min, Max)).Returns(50);
        }

        private GameSession CreateGameSession(int playerCount = 1)
        {
            var game = GameSession.Create(Min, Max, _rngMock.Object);

            for (int i = 0; i < playerCount; i++)
            {
                game.AddPlayer(Player.Create($"Player{i + 1}"));
            }

            game.StartGame();
            return game;
        }

        [Fact]
        public void SubmitGuess_SinglePlayerCorrectGuess_EndsGame()
        {
            // Arrange
            var game = CreateGameSession();
            var player = game.Players[0];

            // Act
            var result = game.SubmitGuess(player, 50);

            // Assert
            Assert.Equal(GuessResult.Correct, result);
            Assert.Equal(GameStatus.Completed, game.Status);
            Assert.Equal(player, game.Winner);
        }

        [Fact]
        public void SubmitGuess_SinglePlayerConsecutiveGuesses_Allowed()
        {
            // Arrange
            var game = CreateGameSession();
            var player = game.Players[0];

            // Act & Assert
            game.SubmitGuess(player, 30);
            var result = game.SubmitGuess(player, 50);

            Assert.Equal(GuessResult.Correct, result);
        }

        [Fact]
        public void SubmitGuess_MultiplayerConsecutiveGuesses_ThrowsException()
        {
            // Arrange
            var game = CreateGameSession(2);
            var player1 = game.Players[0];
            var player2 = game.Players[1];

            game.SubmitGuess(player1, 30);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => game.SubmitGuess(player1, 40));
            Assert.Contains("consecutive guesses", ex.Message);
        }

        [Fact]
        public void SubmitGuess_MultiplayerTurnOrder_EnforcesRotation()
        {
            // Arrange
            var game = CreateGameSession(3);
            var players = game.Players;

            // Round 1 - Valid turns
            game.SubmitGuess(players[0], 30);  // Player1
            game.SubmitGuess(players[1], 40);  // Player2
            game.SubmitGuess(players[2], 60);  // Player3

            // Round 2 - Should be Player1's turn again
            // Try to have Player2 submit instead of Player1
            var ex = Assert.Throws<InvalidOperationException>(() =>
                game.SubmitGuess(players[1], 70));

            // Verify the error mentions Player1
            Assert.Contains("Player1's turn", ex.Message);
        }

        [Fact]
        public void SubmitGuess_MultiplayerCorrectGuess_EndsGame()
        {
            // Arrange
            var game = CreateGameSession(2);
            var player1 = game.Players[0];
            var player2 = game.Players[1];

            // Player1's turn first (wrong guess)
            game.SubmitGuess(player1, 30);

            // Act - Player2's turn with correct guess
            var result = game.SubmitGuess(player2, 50);

            // Assert
            Assert.Equal(GuessResult.Correct, result);
            Assert.Equal(GameStatus.Completed, game.Status);
            Assert.Equal(player2, game.Winner);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void SubmitGuess_InvalidGuess_ThrowsException(int guess)
        {
            // Arrange
            var game = CreateGameSession();
            var player = game.Players[0];

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => game.SubmitGuess(player, guess));
            Assert.Contains("valid range", ex.Message);
        }

        [Fact]
        public void SubmitGuess_PlayerNotInSession_ThrowsException()
        {
            // Arrange
            var game = CreateGameSession();
            var stranger = Player.Create("Stranger");

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => game.SubmitGuess(stranger, 50));
            Assert.Contains("not part", ex.Message);
        }

        [Fact]
        public void SubmitGuess_GameNotActive_ThrowsException()
        {
            // Arrange
            var game = CreateGameSession();
            var player = game.Players[0];
            game.SubmitGuess(player, 50); // Ends game

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => game.SubmitGuess(player, 50));
            Assert.Contains("not active", ex.Message);
        }

        [Fact]
        public void AddPlayer_AfterGameStart_ThrowsException()
        {
            // Arrange
            var game = CreateGameSession();
            var newPlayer = Player.Create("NewPlayer");

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => game.AddPlayer(newPlayer));
            Assert.Contains("after the game has started", ex.Message);
        }
    }
}