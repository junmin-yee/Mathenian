using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class StatisticsQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBr = "What is the median of {0} {1} {2} {3} {4}";
        private static readonly string _questionTemplateSi = "What is the mode of {0} {1} {2} {3} {4}";
        private static readonly string _questionTemplateGo = "What is the mean of {0} {1} {2} {3} {4}";
        private static readonly string _questionTemplatePl = "What is the range of {0} {1} {2} {3} {4}";

        public StatisticsQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                    {
                        int a = random.Next(1, 21);
                        int b = random.Next(31, 51);
                        int c = random.Next(21, 31);
                        int d = random.Next(1, 21);
                        int e = random.Next(31, 51);
                        return Tuple.Create(string.Format(_questionTemplateBr, a, b, c, d, e), c.ToString());
                    }
                    case Mastery.Silver:
                    {
                        int a = random.Next(1, 21);
                        int b = random.Next(21, 51);
                        int c = random.Next(21, 51);
                        int d = random.Next(1, 21);
                        return Tuple.Create(string.Format(_questionTemplateSi, a, d, c, b, d), d.ToString());
                    }
                    case Mastery.Gold:
                    {
                        int m = random.Next(5, 11);
                        int s = random.Next(1, 5);
                        int t = random.Next(1, 5);
                        return Tuple.Create(string.Format(_questionTemplateGo, m + t, m - s, m - t, m, m + s), m.ToString());
                    }
                    case Mastery.Platinum:
                    {
                        int a = random.Next(1, 21);
                        int b = random.Next(31, 51);
                        int c = random.Next(21, 31);
                        int d = random.Next(1, 21);
                        int e = random.Next(31, 51);
                        int[] set = { a, b, c, d, e };
                        return Tuple.Create(string.Format(_questionTemplatePl, a, b, c, d, e), (set.Max() - set.Min()).ToString());
                    }
                }
            }
            throw new ArithmeticException();
        }
    }
}
