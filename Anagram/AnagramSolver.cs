namespace Anagram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AnagramSolver
    {
        private readonly IEnumerable<string> wordList;
        private readonly Action<string> printAction;

        public AnagramSolver(IEnumerable<string> words, Action<string> printAction)
        {
            this.wordList = words;
            this.printAction = printAction;
        }

        public void Execute(string phrase)
        {
            var letters = new LetterInventory(phrase);

            var dictionaryWords = from word in this.wordList
                                  where word.Length <= letters.Size
                                  let letterInventory = new LetterInventory(word)
                                  let subtractInventory = letters.Subtract(letterInventory)
                                  where subtractInventory != null
                                  select new KeyValuePair<string, LetterInventory>(word, letterInventory);

            this.Generate(dictionaryWords, new Stack<string>(), letters);
        }

        private void Generate(IEnumerable<KeyValuePair<string, LetterInventory>> dictionaryWords, Stack<string> words, LetterInventory letters)
        {
            if (letters.Size == 0)
            {
                this.printAction(string.Join(" ", words));
            }
            else
            {
                var pairs = dictionaryWords.ToList();
                var dictionaryWordsList = from dictionaryWord in pairs
                                          let subtractInventory = letters.Subtract(dictionaryWord.Value)
                                          where subtractInventory != null
                                          select new KeyValuePair<string, LetterInventory>(dictionaryWord.Key, subtractInventory);

                foreach (var word in dictionaryWordsList)
                {
                    words.Push(word.Key);
                    this.Generate(pairs, words, word.Value);
                    words.Pop();
                }
            }
        }
    }
}
