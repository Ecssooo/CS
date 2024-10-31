using System;

namespace TU_Challenge.Heritage
{
    public class Poisson : Animal
    {
        internal override int Pattes => 0;

        public Poisson(string name)
        {
            Name = name + " le poisson";
            IsAlive = true;
        }
        
        internal override string Crier()
        {
            return "Je suis un poisson";
        }

        internal override void Die()
        {
            base.Die();
        }

        internal override void Feed(Animal food = null)
        {
            base.Feed(food);
        }
    }
}

