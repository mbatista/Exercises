using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonInterview.HeadTails
{
    public class ClosestFirstWordMutator
    {
        private readonly char[] _alphabet;

        private readonly HashSet<string> _dictionary;

        private readonly string _targetWord;

        public ClosestFirstWordMutator(
            char[] alphabet,
            HashSet<string> dictionary,
            string targetWord)
        {
            _alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            _targetWord = targetWord ?? throw new ArgumentNullException(nameof(targetWord));
        }

        public IEnumerable<string> FindValidPermutations(string word)
        {
            var prioritiezed = Enumerable.Range(0, word.Length).Select(index =>
            {
                var copy = word.ToArray();
                copy[index] = _targetWord[index];
                return new string(copy);
            }).Where(_dictionary.Contains);

            var otherWords = Enumerable.Range(0, word.Length)
                .SelectMany(index => FindPermutations(word, index, _targetWord[index]))
                .Where(_dictionary.Contains);

            return prioritiezed.Concat(otherWords);
        }

        private IEnumerable<string> FindPermutations(string word, int index, char skip)
        {
            foreach (var letter in _alphabet)
            {
                if (letter != skip && word[index] != letter)
                {
                    var copy = word.ToArray();
                    copy[index] = letter;
                    yield return new string(copy);
                }
            }
        }
    }
}
