using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class SeriesQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBrGo = "What is the next term in the sequence: {0}, {1}, {2}?";
        private static readonly string _questionTemplateSiPl = "What is the sum of the series: {0}, {1}, ..., {2}";

        public SeriesQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                    {
                        int u = random.Next(1, 6);
                        int d = random.Next(1, 11);
                        int x = u + 3 * d;
                        return Tuple.Create(string.Format(_questionTemplateBrGo, u, u + d, u + 2 * d), x.ToString());
                    }
                    case Mastery.Silver:
                    {
                        int u = random.Next(1, 6);
                        int d = random.Next(1, 11);
                        int n = random.Next(4, 7);
                        int x = n / 2 * (2 * u + (n - 1) * d);
                        return Tuple.Create(string.Format(_questionTemplateSiPl, u, u + d, u + (n - 1) * d), x.ToString());
                    }
                    case Mastery.Gold:
                    {
                        int u = random.Next(1, 6);
                        int r = random.Next(2, 5);
                        int x = u + (int)Math.Pow(r, 3);
                        return Tuple.Create(string.Format(_questionTemplateBrGo, u, u * r, u * r * r), x.ToString());
                    }
                    case Mastery.Platinum:
                    {
                        int u = random.Next(1, 6);
                        int r = random.Next(2, 5);
                        int n = random.Next(4, 7);
                        int x = u * (int)((1 - Math.Pow(r, n)) / (1 - r));
                        return Tuple.Create(string.Format(_questionTemplateSiPl, u, u * r, u * (int)Math.Pow(r, (n - 1))), x.ToString());
                    }
                }
            }
            throw new ArithmeticException();
        }
    }
}
