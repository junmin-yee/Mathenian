using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class DifferentialQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBr = "Find the derivative of f(x) = {0}x + {1}";
        private static readonly string _questionTemplateSi = "Find the derivative of f(x) = {0}x^2 + {1}x + {2}";
        private static readonly string _questionTemplateGo = "Find the derivative of f(x) = {0}x^3 + {1}x^2 + {2}x + {3}";
        private static readonly string _questionTemplatePl = "Find the derivative of f(x) = e^{0}x";

        private static readonly string _answerTemplateSi = "{0}x + {1}";
        private static readonly string _answerTemplateGo = "{0}x^2 + {1}x + {2}";
        private static readonly string _answerTemplatePl = "{0}e^{1}x";

        public DifferentialQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
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
                        int b = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplateBr, a, b), a.ToString());
                    }
                    case Mastery.Silver:
                    {
                        int a = random.Next(1, 11);
                        int b = random.Next(1, 11);
                        int c = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplateSi, a, b, c), string.Format(_answerTemplateSi, 2 * a, b));
                    }
                    case Mastery.Gold:
                    {
                        int a = random.Next(1, 11);
                        int b = random.Next(1, 11);
                        int c = random.Next(1, 11);
                        int d = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplateGo, a, b, c, d), string.Format(_answerTemplateGo, 3 * a, 2 * b, c));
                    }
                    case Mastery.Platinum:
                    {
                        int a = random.Next(1, 11);
                        return Tuple.Create(string.Format(_questionTemplatePl, a), string.Format(_answerTemplatePl, a, a));
                    }
                }
            }
            throw new ArithmeticException();
        }
    }
}
