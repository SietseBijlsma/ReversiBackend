using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ReversiRestApi.Models;

namespace ReversiRestApi.Test
{
    [TestFixture]
    public class GameRepositoryTests
    {
        private GameRepository repository;

        [SetUp]
        public void SetUp()
        {
            repository = new GameRepository();
        }


        [Test]
        public void GamesCanBeAdded()
        {
            Game game = new Game();
            Assert.AreEqual(3, repository.GetGames().Count);
            repository.AddGame(game);
            Assert.AreEqual(4, repository.GetGames().Count);
        }

        [Test]
        public void GetGameWithToken()
        {
            Assert.AreEqual("aaaaa", repository.GetGames().FirstOrDefault().Token);
        }
    }
}
