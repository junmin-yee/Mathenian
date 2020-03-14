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
}
