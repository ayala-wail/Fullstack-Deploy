namespace TodoApi.Models // תשני את זה לשם ה־namespace של הפרויקט שלך
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
