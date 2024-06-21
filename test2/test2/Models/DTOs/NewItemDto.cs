using System.ComponentModel.DataAnnotations;

namespace test2.Models.DTOs;

public class NewItemDto
{
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int Amount { get; set; }
}


