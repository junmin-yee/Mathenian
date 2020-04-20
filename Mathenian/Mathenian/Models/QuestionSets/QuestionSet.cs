using System.Collections.Generic;

namespace Mathenian.Models
{
    public enum Topic
    {
        Arithmetic,
        Algebra,
        Geometry,
        Series,
        Differential,
        Integral,
        Sets,
        Probability,
        Statistics
    }

    public class QuestionSet
    {
        private readonly Dictionary<Topic, QuestionSetFactory> _factories;

        public QuestionSet()
        {
            _factories = new Dictionary<Topic, QuestionSetFactory>
            {
                { Topic.Arithmetic, new ArithmeticFactory() },
                { Topic.Algebra, new AlgebraFactory() },
                { Topic.Geometry, new GeometryFactory() },
                { Topic.Series, new SeriesFactory() },
                { Topic.Differential, new DifferentialFactory() },
                { Topic.Integral, new IntegralFactory() },
                { Topic.Sets, new SetsFactory() },
                { Topic.Probability, new ProbabilityFactory() },
                { Topic.Statistics, new StatisticsFactory() }
            };
        }

        public AbstractQuestionSet ExecuteCreate(Topic topic, int numQuestions, Mastery mastery) => _factories[topic].Create(numQuestions, mastery);
    }
}
