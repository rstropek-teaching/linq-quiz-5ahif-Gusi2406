using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            //Enumberable.Range gibt zwischen den beiden Paramter alle int Werte in Fo rm einer Liste zurück
            //.Where ist die Abfrage, ob es sich um eine gerade nummer handelt
            //.Array man macht ein Array aus der IEnumberable
            return Enumerable.Range(1, exclusiveUpperLimit - 1).Where(i => i % 2 == 0).ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            //Array.Empty<int>() es wird ein leeres Array zurückgeben werden
            //Select(i => checked(i*i)): Integer mit 32 Bit kann die Zahl 2^31 speichern, wenn diese Zahl übertroffen wird beginnt der Integer von ganz unten zu zählen (0)
            //Falls dies die Berchnung mit der checked Methode ausgeführt wird, dann beginnt der Integer nicht wieder von 0 zu zählen, sondern es wird eine OverflowException geworfen
            //OrderByDescending(i => i): Absteigend sortieren; Alle Linq Funktionen benötigen eine Lamba Expression, um sie mit den jeweiligen Werten aufzurufen; i => i dass das Selbe Element der momentanen Stelle zurückgibt
            
            // Tip: You could add this check directly in the return statement:
            // return exclusiveUpperLimit < 1 ? Array.Empty... : Enumerable.Range...
            if (exclusiveUpperLimit < 1)
            {
                return Array.Empty<int>();
            }
            int[] sol = Enumerable.Range(1, exclusiveUpperLimit - 1).Where(i => i % 7 == 0).Select(i => checked(i*i)).OrderByDescending(i => i).ToArray();
            
            return sol;
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            if (families == null)
            {
                throw new ArgumentNullException();
            }

            // In such cases, use `var` on the left side of the assignment. With this, you do not need to
            // write the type twice.
            List<FamilySummary> sol = new List<FamilySummary>();
            foreach (var family in families)
            {
                sol.Add(new FamilySummary
                {
                    //Keine Familienmitglieder = 0; Famileienmitglieder = Average berechnen
                    //<Bedingung> ? falls war : falls falsch
                    AverageAge =  family.Persons.Count == 0 ? 0 : family.Persons.Average(p => p.Age),
                    FamilyID = family.ID,
                    NumberOfFamilyMembers = family.Persons.Count
                });
            }

            // For `sol`, you could use an Array instead of a list. With that, you would not need the
            // call to `ToArray` -> more efficient
            return sol.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            // Same as above: Prefer `var`
            char[] textLetters = text.ToUpper().ToCharArray();
            List<int> letters = Enumerable.Range('A', 'Z').ToList(); //integers sind = chars
            List<(char letter, int numberOfOccurrences)> sol = new List<(char letter, int numberOfOccurrences)>();

            foreach (var letter in letters)
            {
                var count = textLetters.Count(l => l == letter);
                if (count > 0)
                {
                    sol.Add(((char)letter, count));
                }
            }

            return sol.ToArray(); //Ein eigenes Objekt mit char und int
        }
    }
}
