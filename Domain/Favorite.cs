using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Favorite:BaseEntity
{
    public int BookId { get; set; }
    [ForeignKey("BooKId")] public Book? Book { get; set; }
    public int ClientId { get; set; }
    [ForeignKey("ClientId")] public Client? Client { get; set; }
}