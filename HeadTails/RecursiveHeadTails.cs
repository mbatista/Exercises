using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonInterview.HeadTails
{
    public class Solution
    {
        public delegate IEnumerable<string> PermutationGenerator(string word);

        public IEnumerable<string> FindTransformations(
            string initialWord,
            string targetWord,
            PermutationGenerator permutatorGenerator)
        {
            if(initialWord.Length != targetWord.Length)
            {
                throw new InvalidOperationException($"'{initialWord}' and '{targetWord}' don't have the same length");
            }

            return FindTransformationsRecursively(
                initialWord,
                targetWord,
                new HashSet<string>(StringComparer.InvariantCultureIgnoreCase),
                permutatorGenerator);
        }

        private IEnumerable<string> FindTransformationsRecursively(
            string currentWord,
            string targetWord,
            HashSet<string> visitedWords,
            PermutationGenerator permutationGenerator)
        {
            if(currentWord.Equals(targetWord, StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { currentWord };
            }

            if (visitedWords.Contains(currentWord))
            {
                return Enumerable.Empty<string>();
            }

            visitedWords.Add(currentWord);

            foreach (var nextWord in permutationGenerator(currentWord))
            {
                var nextResult = FindTransformationsRecursively(nextWord, targetWord, visitedWords, permutationGenerator);
                if (nextResult.Any())
                {
                    return new[] { currentWord }.Concat(nextResult);
                }
            }

            return Enumerable.Empty<string>();
        }
    }
}
