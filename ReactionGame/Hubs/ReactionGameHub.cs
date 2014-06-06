using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ReactionGame.Hubs
{
    public class ReactionGameHub : Hub
    {
        private List<string> players = new List<string>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public void Join(string name)
        {
            players.Add(name);
            Clients.All.updateListOfPlayers(players);
        }
    }
}