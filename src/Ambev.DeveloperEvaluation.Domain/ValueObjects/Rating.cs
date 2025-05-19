namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Rating
    {
        public int Rate { get; set; }
        public string Count { get; set; }
        public Rating() { }
        public Rating(int rate, string count)
        {
            Rate = rate;
            Count = count;
        }

    }
}
