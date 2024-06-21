namespace test2.Models.DTOs;

public class GetCharacterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    
    public ICollection<GetBackpackDto> BackpackItems { get; set; } = new List<GetBackpackDto>();
    public ICollection<GetTitleDto> Titles { get; set; } = new List<GetTitleDto>();
}

public class GetBackpackDto
{
    public string ItemName { get; set; } = string.Empty;
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class GetTitleDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime AquiredAt { get; set; }
}