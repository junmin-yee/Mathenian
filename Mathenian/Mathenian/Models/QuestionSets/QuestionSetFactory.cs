namespace Mathenian.Models
{
    public abstract class QuestionSetFactory
    {
        public abstract AbstractQuestionSet Create(int numQuestions, Mastery mastery);
    }

    public class ArithmeticFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new ArithmeticQuestionSet(numQuestions, mastery);
    }

    public class AlgebraFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new AlgebraQuestionSet(numQuestions, mastery);
    }

    public class GeometryFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new GeometryQuestionSet(numQuestions, mastery);
    }

    public class SeriesFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new SeriesQuestionSet(numQuestions, mastery);
    }

    public class DifferentialFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new DifferentialQuestionSet(numQuestions, mastery);
    }

    public class IntegralFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new IntegralQuestionSet(numQuestions, mastery);
    }

    public class SetsFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new SetsQuestionSet(numQuestions, mastery);
    }

    public class ProbabilityFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new ProbabilityQuestionSet(numQuestions, mastery);
    }

    public class StatisticsFactory : QuestionSetFactory
    {
        public override AbstractQuestionSet Create(int numQuestions, Mastery mastery) => new StatisticsQuestionSet(numQuestions, mastery);
    }
}
