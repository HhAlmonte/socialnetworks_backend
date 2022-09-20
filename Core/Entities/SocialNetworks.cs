namespace Core.Entities
{
    public class SocialNetworks
    {
        public SocialNetworks(int status)
        {
            Status = status;
        }

        public string projectName = "socialnetworks_backend";
        public string Author = "Hector Almonte";
        public string Created = "8/30/2022";
        public string Contact = "Hbalmontess272@gmail.com";
        public int Status { get; set; }

        public override string ToString()
        {
            return
                $"Project name: {projectName} \n"+
                $"Author: {Author} \n" +
                $"Created: {Created} \n" +
                $"Contact: {Contact} \n" +
                $"Status: {Status} \n";
        }
    }
}
