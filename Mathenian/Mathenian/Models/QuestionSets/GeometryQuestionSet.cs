using System;
using System.Collections.Generic;
using System.Text;

namespace Mathenian.Models
{
    public class GeometryQuestionSet : AbstractQuestionSet
    {
        private static readonly string _questionTemplateBr = "Area of rectangle with length {0} and width {1}";
        private static readonly string _questionTemplateSi = "Area of triangle with base {0} and height {1}";
        private static readonly string _questionTemplateGo = "Volume of cube with length {0}";
        private static readonly string _questionTemplatePl = "Volume of cuboid with length {0}, width {1}, and height {2}";

        public GeometryQuestionSet(int numQuestions, Mastery mastery) : base(numQuestions, mastery)
        { }

        protected override Tuple<string, string> GenerateQuestion()
        {
            lock (syncLock)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                    {
                        int l = random.Next(1, 11);
                        int w = random.Next(1, 11);
                        int a = l * w;
                        return Tuple.Create(string.Format(_questionTemplateBr, l, w), a.ToString());
                    }
                    case Mastery.Silver:
                    {
                        int b = random.Next(1, 11);
                        int h = random.Next(1, 6) * 2;
                        int a = b * h / 2;
                        return Tuple.Create(string.Format(_questionTemplateSi, b, h), a.ToString());
                    }
                    case Mastery.Gold:
                    {
                        int l = random.Next(1, 6);
                        int v = l * l * l;
                        return Tuple.Create(string.Format(_questionTemplateGo, l), v.ToString());
                    }
                    case Mastery.Platinum:
                    {
                        int l = random.Next(1, 6);
                        int w = random.Next(1, 6);
                        int h = random.Next(1, 6);
                        int v = l * w * h;
                        return Tuple.Create(string.Format(_questionTemplatePl, l, w, h), v.ToString());
                    }
                }
            }
            throw new ArithmeticException();
        }
    }
}
