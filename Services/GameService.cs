using GameStore.Contexts;
using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services
{
    public class GameService
    {
        private readonly GameStoreDBContext context;
        public GameService(GameStoreDBContext context)
        {
            this.context = context;
        }

        #region GetMethods
        public Game GetGameById(int id) => context.Games.SingleOrDefault(x => x.Id == id);

        public List<Game> GetAllGames() => context.Games.ToList();
        #endregion
        #region CreateMethods
        public Game CreateGame(Game game)
        {
            if(IsGameExists(game.Name))
            {
                throw new Exception("This game alreaty exists");
            }

            context.Games.Add(game);
            context.SaveChanges();

            return game;
        }
        private bool IsGameExists(string name)
        {
            var games = context.Games.ToList();
            foreach(var game in games)
            {
                if(game.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region RemoveMethods
        public void RemoveGameById(int id)
        {
            var game = context.Games.SingleOrDefault(x => x.Id == id);

            context.Games.Remove(game);
            context.SaveChanges();
        }
        #endregion
        #region UpdateMethods
        public void UpdateGame(int id, Game newGame)
        {
            var game = context.Games.SingleOrDefault(x => x.Id == id);
            game = newGame;

            context.SaveChanges();
        }
        public void UpdateGameDescripion(int id, string description)
        {
            var game = context.Games.SingleOrDefault(x => x.Id == id);
            game.Description = description;

            context.Update<Game>(game);
            context.SaveChanges();
        }
        #endregion
    }
}
