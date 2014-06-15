using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ReactionGame.Models;

namespace ReactionGame.Hubs
{
    public class ReactionGameHub : Hub
    {     
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Join(string name)
        {
            var player = new Player(Context.ConnectionId, name);
            GameBookkeeper.Instance.AddPlayer(player);           
        }

        public void TargetClicked()
        {
            GameBookkeeper.Instance.TargetClicked(Context.ConnectionId);
        }
    }
}