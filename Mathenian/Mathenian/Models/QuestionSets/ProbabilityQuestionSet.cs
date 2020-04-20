using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class ProbabilityQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBr = "How many outcomes are there in {0} coin tosses";
        private static readonly string _questionTemplateSi = "What's the probability of getting {0} heads in {1} coin tosses";
        private static readonly string _questionTemplateGo = "How many outcomes are there in {0} dice throws";
        private static readonly string _questionTemplatePl = "How many outcomes are there in {0} coin toss(es) and {1} dice throw(s)";

        private static readonly string _fraction = "{0}/{1}";

        public ProbabilityQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                    {
                        int n = random.Next(2, 7);
                        return Tuple.Create(string.Format(_questionTemplateBr, n), Math.Pow(2, n).ToString());
                    }
                    case Mastery.Silver:
                    {
                        int x = random.Next(1, 4);
                        int n = x + random.Next(0, 4);
                        return Tuple.Create(string.Format(_questionTemplateSi, x, n), string.Format(_fraction, Combination(n, x), Math.Pow(2, n)));
                    }
                    case Mastery.Gold:
                    {
                        int n = random.Next(1, 4);
                        return Tuple.Create(string.Format(_questionTemplateGo, n), Math.Pow(6, n).ToString());
                    }
                    case Mastery.Platinum:
                    {
                        int c = random.Next(1, 4);
                        int d = random.Next(1, 3);
                        return Tuple.Create(string.Format(_questionTemplatePl, c, d), (Math.Pow(2, c) * Math.Pow(6, d)).ToString());
                    }
                }
            }
            throw new ArithmeticException();
        }

        private int Combination(int n, int k)
        {
            decimal result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }
            return (int)result;
        }
    }
}
