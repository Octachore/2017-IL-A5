namespace Algo
{
    internal class SimilarUser
    {
        public User User { get; }

        public double SimilarityCoef { get; }

        public SimilarUser(User user, double similarityCoef)
        {
            User = user;
            SimilarityCoef = similarityCoef;
        }
    }
}