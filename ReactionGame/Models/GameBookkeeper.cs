using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ReactionGame.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ReactionGame.Models
{
    internal class GameBookkeeper
    {
        enum GameStatus
        {
            GetReady = 1,
            Go = 2,
            Stop = 3,
            NewGame = 4
        }

        private IHubConnectionContext _clients;
        private readonly List<string> _players = new List<string>();
        private GameStatus _gameStatus = GameStatus.NewGame;
        private Timer _timer;
        private readonly static Lazy<GameBookkeeper> _instance =
            new Lazy<GameBookkeeper>(() => new GameBookkeeper(GlobalHost.ConnectionManager.GetHubContext<ReactionGameHub>().Clients));

        private GameBookkeeper(IHubConnectionContext clients)
        {
            _clients = clients;
            ChangeGameStatus(null);
        }

        public static GameBookkeeper Instance { get { return _instance.Value; } }

        public void AddPlayer(string name)
        {
            _players.Add(name);
            _clients.All.updateListOfPlayers(_players);
        }

        private void ChangeGameStatus(object state){
            switch (_gameStatus)
            {
                case GameStatus.NewGame:
                    _gameStatus = GameStatus.GetReady;
                    _clients.All.updateGameStatus(_gameStatus.ToString());
                    //_timer = new Timer(ChangeGameStatus, null, 0, 10000);
                    break;
            }                      
        }
    }
}