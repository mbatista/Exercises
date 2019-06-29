using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonInterview.HeadTails
{
    public class Program
    {
        static void Main(string[] args)
        {
            var alphabet = Enumerable.Range(0, 'z' - 'a' + 1)
                .Select(i => (char)('a' + i)).ToArray();

            var englishWords = ReadWords();

            var sol = new Solution();

            while (true)
            {
                Console.Write("Initial word: ");
                var initialWord = Console.ReadLine();

                Console.Write("Target word: ");
                var targetWord = Console.ReadLine();

                var permutator = new ClosestFirstWordMutator(alphabet, englishWords, targetWord);

                var start = DateTime.UtcNow;

                IList<string> result;
                try
                {
                    result = sol
                        .FindTransformations(initialWord, targetWord, permutator.FindValidPermutations)
                        .Select(word => word.ToLower())
                        .ToList();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    continue;
                }

                var duration = DateTime.UtcNow - start;

                if (result.Count != 0)
                {
                    Console.WriteLine(string.Join("->", result));
                }
                else
                {
                    Console.WriteLine("Could not find valid permutations for this pair");
                }

                Console.WriteLine($"Execution time: {duration.TotalMilliseconds} ms");
                Console.WriteLine();
            }
        }

        private static HashSet<string> ReadWords()
        {
            var words = Properties.Resources.EnglishWords.Split('\r', '\n');
            return new HashSet<string>(words, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
