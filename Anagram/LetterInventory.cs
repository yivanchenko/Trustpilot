namespace Anagram
{
    using System.Collections.Generic;
    using System.Linq;

    public class LetterInventory
    {
        private readonly IDictionary<char, int> inventory = new Dictionary<char, int>("abcdefghijklmnopqrstuvwxyz".ToDictionary(c => c, c => 0));
        
        public LetterInventory(string data)
        {
            foreach (var c in data.Where(c => this.inventory.Keys.Contains(c)))
            {
                this.inventory[c]++;
            }
        }

        private LetterInventory()
            : this(string.Empty)
        {
        }

        public int Size
        {
            get
            {
                return this.inventory.Values.Sum(v => v);
            }
        }

        public LetterInventory Subtract(LetterInventory other)
        {
            var letter = new LetterInventory();

            foreach (var c in this.inventory.Keys)
            {
                var value = this.Get(c) - other.Get(c);
                if (value < 0)
                {
                    return null;
                }

                letter.Set(c, value);
            }

            return letter;
        }

        private int Get(char letter)
        {
            return this.inventory[letter];
        }

        private void Set(char letter, int value)
        {
            this.inventory[letter] = value;
        }
    }
}
