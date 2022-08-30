namespace Core.Entities
{
    public class SocialNetworks
    {
        public SocialNetworks(int status)
        {
            Status = status;
        }

        public string Author = "Hector Almonte";
        public string Created = "8/30/2022";
        public string Contact = "Hbalmontess272@gmail.com";
        public int Status { get; set; }

        public override string ToString()
        {
            return
                $"Author: {Author} \n" +
                $"Created: {Created} \n" +
                $"Contact: {Contact} \n" +
                $"Status: {Status} \n";
        }
    }
}
