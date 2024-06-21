using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test2.Models;

[PrimaryKey(nameof(ItemId), nameof(CharacterId))]
[Table("backpacks")]
public class Backpack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = null!;
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; } = null!;
}