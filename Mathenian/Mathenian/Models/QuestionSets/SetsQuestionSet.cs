using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class SetsQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBr = "How many elements are in this set: {{{0}}}";
        private static readonly string _questionTemplateSi = "What is the union of {{{0} {1} {2}}} and {{{3} {4} {5}}}";
        private static readonly string _questionTemplateGo = "What is the intersection of {{{0} {1} {2}}} and {{{3} {4} {5}}}";
        private static readonly string _questionTemplatePl = "What is the difference of {{{0} {1} {2}}} and {{{3} {4} {5}}}";

        private static readonly string _answerTemplateSi = "{{{0} {1} {2} {3}}}";
        private static readonly string _answerTemplateGo = "{{{0} {1}}}";
        private static readonly string _answerTemplatePl = "{{{0} {1}}}";

        public SetsQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                    {
                        int n = random.Next(1, 6);
                        string set = "";
                        for (int i = 0; i < n; i++)
                        {
                            set += random.Next(1 + 5 * i, 6 + 5 * i);
                            if (i != n - 1)
                                set += " ";
                        }
                        return Tuple.Create(string.Format(_questionTemplateBr, set), n.ToString());
                    }
                    case Mastery.Silver:
                    {
                        int a = random.Next(1, 6);
                        int b = random.Next(6, 11);
                        int c = random.Next(11, 16);
                        int d = random.Next(16, 21);
                        return Tuple.Create(string.Format(_questionTemplateSi, a, b, c, b, c, d), string.Format(_answerTemplateSi, a, b, c, d));
                    }
                    case Mastery.Gold:
                    {
                        int a = random.Next(1, 6);
                        int b = random.Next(6, 11);
                        int c = random.Next(11, 16);
                        int d = random.Next(16, 21);
                        return Tuple.Create(string.Format(_questionTemplateGo, a, b, c, b, c, d), string.Format(_answerTemplateGo, b, c));
                    }
                    case Mastery.Platinum:
                    {
                        int a = random.Next(1, 6);
                        int b = random.Next(6, 11);
                        int c = random.Next(11, 16);
                        int d = random.Next(16, 21);
                        int e = random.Next(21, 26);
                        return Tuple.Create(string.Format(_questionTemplatePl, a, b, c, c, d, e), string.Format(_answerTemplatePl, a, b));
                    }
                }
            }
            throw new ArithmeticException();
        }
    }
}
