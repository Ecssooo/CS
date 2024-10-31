using System;

namespace TU_Challenge.Heritage
{
    public class Chien : Animal
    {
        internal override int Pattes => 4;
        
        public Chien(string name)
        {
            Name = name;
        }
        
        internal override string Crier()
        {
            if (IsFeed) return "Ouaf (viens on joue ?)";
            else return "Ouaf (j'ai faim)";
        }

        internal override void Die()
        {
            base.Die();
        }
        
        internal override void Feed(Animal food = null)
        {
            base.Feed();
        }
    }
}
