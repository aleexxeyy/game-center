﻿using GameHub.Hubs;
using Games.Models;
using Microsoft.AspNetCore.SignalR;

namespace Games.Services
{
    public class GameService : IGameService
    {
        private readonly IHubContext<GamesHub> _gameHubContext;

        private static readonly List<GameModel> _games = new()
        {
             new GameModel 
             { 
                 Id = "xo", 
                 Name = "Крестики-Нолики", 
                 Description = "Классическая игра 3x3" 
             }
        };
    
        public GameService(IHubContext<GamesHub> gameHubContext)
        {
            _gameHubContext = gameHubContext;
        }

        public IEnumerable<GameModel> GetAvailableGames() => _games;

        public Task<string> SelectGameAsync()
        {
            //TODOO: realize this method
            throw new NotImplementedException();
        }
    }
}
