using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ReactionGame.Models;
using System.Threading.Tasks;

namespace ReactionGame.Hubs
{
    public class ReactionGameHub : Hub
    {
        public override Task OnDisconnected()
        {
            GameBookkeeper.Instance.RemovePlayer(Context.ConnectionId);
            return base.OnDisconnected();
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