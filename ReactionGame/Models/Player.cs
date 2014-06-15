using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReactionGame.Models
{
    public class Player
    {
        public Player(string id, string name)
        {
            Id = id;
            Name = name;            
        }
        public string Id { get; private set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}