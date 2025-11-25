namespace PhrasesApi.Models
{
    public class Phrase
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
