using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class IntegralQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBr = "Find the integral of f(x) = {0}";
        private static readonly string _questionTemplateSi = "Find the integral of f(x) = {0}x + {1}";
        private static readonly string _questionTemplateGo = "Find the integral of f(x) = {0}x^2 + {1}x + {2}";
        private static readonly string _questionTemplatePl = "Find the integral of f(x) = {0}e^{1}x";

        private static readonly string _answerTemplateBr = "{0}x + c";
        private static readonly string _answerTemplateSi = "{0}x^2 + {1}x + c";
        private static readonly string _answerTemplateGo = "{0}x^3 + {1}x^2 + {2}x + c";
        private static readonly string _answerTemplatePl = "e^{0}x + c";

        public IntegralQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                    {
                        int a = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplateBr, a), string.Format(_answerTemplateBr, a));
                    }
                    case Mastery.Silver:
                    {
                        int a = 2 * random.Next(1, 11);
                        int b = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplateSi, a, b), string.Format(_answerTemplateSi, a / 2, b));
                    }
                    case Mastery.Gold:
                    {
                        int a = 3 * random.Next(1, 11);
                        int b = 2 * random.Next(1, 11);
                        int c = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplateGo, a, b, c), string.Format(_answerTemplateGo, a / 3, b / 2, c));
                    }
                    case Mastery.Platinum:
                    {
                        int a = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplatePl, a, a), string.Format(_answerTemplatePl, a));
                    }
                }
            }
            throw new ArithmeticException();
        }
    }
}
