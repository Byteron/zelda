namespace Zelda.Components
{
    public struct Health
    {
        public int Value;
        public int Max;

        public Health(int value)
        {
            Max = value;
            Value = Max;
        }
    }
}