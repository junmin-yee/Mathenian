using System;

namespace Mathenian.Models
{
    public class AlgebraQuestionSet : AbstractQuestionSet
    {
        private static readonly string[] _questionTemplateBr = { "{0}x = {1}", "{0}/x = {1}" };
        private static readonly string _questionTemplateSi = "{0}x + {1} = {2}";
        private static readonly string _questionTemplateGo = "{0}x + {1} = {2}x + {3}";
        private static readonly string _questionTemplatePl = "{0}({1}x + {2}) = {3}x + {4}";

        public AlgebraQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
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
                            int x = random.Next(1, 11);
                            int b = a * x;
                            switch (random.Next(_questionTemplateBr.Length))
                            {
                                case 0:
                                    return Tuple.Create(string.Format(_questionTemplateBr[0], a, b), x.ToString());
                                case 1:
                                    return Tuple.Create(string.Format(_questionTemplateBr[1], b, a), x.ToString());
                            }
                            break;
                        }
                    case Mastery.Silver:
                        {
                            int a = random.Next(1, 11);
                            int b = random.Next(1, 11);
                            int x = random.Next(1, 11);
                            int c = a * x + b;
                            return Tuple.Create(string.Format(_questionTemplateSi, a, b, c), x.ToString());
                        }
                    case Mastery.Gold:
                        {
                            int a = random.Next(6, 11);
                            int b = random.Next(1, 11);
                            int c = random.Next(1, 6);
                            int x = random.Next(1, 6);
                            int d = (a - c) * x + b;
                            return Tuple.Create(string.Format(_questionTemplateGo, a, b, c, d), x.ToString());
                        }
                    case Mastery.Platinum:
                        {
                            int a = random.Next(1, 6);
                            int b = random.Next(6, 11);
                            int c = random.Next(1, 6);
                            int d = random.Next(1, 5);
                            int x = random.Next(1, 6);
                            int e = (a * b - d) * x + (a * c);
                            return Tuple.Create(string.Format(_questionTemplatePl, a, b, c, d, e), x.ToString());
                        }
                }
            }
            throw new ArithmeticException();
        }
    }
}
