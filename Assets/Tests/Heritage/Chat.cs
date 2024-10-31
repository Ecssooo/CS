using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TU_Challenge.Heritage
{
    public class Chat : Animal
    {
       private bool _isFeedByPoisson;
        
        
        internal override int Pattes => 4;
        
        public bool IsFeedByPoisson { get => _isFeedByPoisson; set => _isFeedByPoisson = value; }

        public Chat(string name)
        {
            Name = name;
        }
        
        internal override string Crier()
        {
            if (IsFeed && !_isFeedByPoisson) return "Miaou (c'est bon laisse moi tranquille humain)";
            else if (_isFeedByPoisson && !IsFeed) return "Miaou (Le poisson etait bon)";
            else return "Miaou (j'ai faim)";
        }

        internal override void Die()
        {
            base.Die();
        }

        internal override void Feed(Animal food = null)
        {
            if (food is Poisson)
            {
                _isFeedByPoisson = true;
            }
            else
            {
                base.Feed();
            }
        }
    }
}
