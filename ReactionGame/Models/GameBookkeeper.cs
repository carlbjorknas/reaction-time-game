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
        private Random _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
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
                    _gameStatus = GameStatus.Stop;                    
                    _timer = new Timer(ChangeGameStatus, null, 10000, Timeout.Infinite);
                    break;
                case GameStatus.Stop:
                    _gameStatus = GameStatus.GetReady;
                    _timer.Change(_random.Next(5000) + 1000, Timeout.Infinite);
                    break;
                case GameStatus.GetReady:
                    _gameStatus = GameStatus.Go;
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    break;
            }
            _clients.All.updateGameStatus(_gameStatus.ToString());     
        }
    }
}