namespace Mathenian.Models
{
    public class LessonCompletion
    {
        private const int MaxPercent = 100;

        private Mastery _mastery;
        public Mastery Mastery { get => _mastery; set => _mastery = value; }

        private int _percentCompleted;
        public int PercentCompleted { get => _percentCompleted; set => _percentCompleted = value; }

        private bool _enabled;
        public bool Enabled { get => _enabled; set => _enabled = value; }

        public LessonCompletion()
        {
            _mastery = Mastery.Bronze;
            _percentCompleted = 0;
            _enabled = false;
        }

        public void Update(int percent)
        {
            _percentCompleted += percent;
            if (_percentCompleted >= MaxPercent)
            {
                switch (_mastery)
                {
                    case Mastery.Bronze:
                        _mastery = Mastery.Silver;
                        _percentCompleted = 0;
                        break;
                    case Mastery.Silver:
                        _mastery = Mastery.Gold;
                        _percentCompleted = 0;
                        break;
                    case Mastery.Gold:
                        _mastery = Mastery.Platinum;
                        _percentCompleted = 0;
                        break;
                    case Mastery.Platinum:
                        _percentCompleted = MaxPercent;
                        break;
                }
            }
        }

        public bool IsMastered()
        {
            return _mastery == Mastery.Platinum && _percentCompleted == MaxPercent;
        }

        public bool IsCompleted()
        {
            return _mastery != Mastery.Bronze;
        }
    }
}
