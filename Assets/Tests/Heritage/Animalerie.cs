using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge.Heritage
{
    public class Animalerie
    {
        protected List<Animal> _animals;

        public event Action<Animal> OnAddAnimal;
        
        public Animalerie()
        {
            _animals = new List<Animal>();
        }
        
        internal virtual void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
            OnAddAnimal?.Invoke(animal);
            if (animal is Chat && !(animal is ChatQuiBoite))
            {
                Poisson poisson = (Poisson)_animals.Find(a => a is Poisson);
                if (poisson == null) return;
                poisson.IsAlive = false;
                animal.Feed(poisson);
            }else if (animal is Poisson)
            {
                Chat chat = (Chat)_animals.Find(a => a is Chat);
                if (chat == null) return;
                animal.IsAlive = false;
                chat.Feed(animal);
            }
        }

        internal virtual bool Contains(Animal animal)
        {
            return _animals.Contains(animal);
        }

        internal virtual Animal GetAnimal(int index)
        {
            if (index < 0 || index > _animals.Count)
            {
                return null;
            }
            return _animals[index];
        }

        internal virtual void FeedAll()
        {
            foreach (Animal animal in _animals)
            {
                animal.Feed();
            }
        }
    }
}
