using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class ChatQuiBoite : Chat
    { 
        internal override int Pattes => 3;
        
        public ChatQuiBoite(string name) : base(name)
        {
            Name = name;
        }
    }
}
