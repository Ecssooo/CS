using System;

namespace TU_Challenge.Heritage
{

    public abstract class Animal
    {
        private string _name;
        private int _pattes;
        private bool _isFeed;
        private bool _isAlive;
        public event Action OnDie;

        internal virtual string Name { get => _name; set => _name = value; }
        internal virtual int Pattes { get => _pattes; set => _pattes = value; }
        internal virtual bool IsFeed { get => _isFeed; set => _isFeed = value; }
        
        internal virtual bool IsAlive { get => _isAlive; set => _isAlive = value; }
        
        internal abstract string Crier();

        internal virtual void Die()
        {
            _isAlive = false;
            OnDie?.Invoke();
        }

        internal virtual void Feed(Animal food = null)
        {
            _isFeed = true;
        }

    }
}
