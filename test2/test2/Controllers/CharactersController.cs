using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using test2.Models;
using test2.Models.DTOs;
using test2.Services;

namespace test2.Controllers;

[ApiController]
[Route("/api/characters")]
public class CharactersController: ControllerBase
{
    private readonly IAppService service;

    public CharactersController(IAppService service)
    {
        this.service = service;
    }

    [HttpGet("/{characterId}")]
    public async Task<IActionResult> GetCharacter(int characterId)
    {
        if (!await service.DoesCharacterExist(characterId)) return NotFound("Character with this id does not exist");
        var character = await service.GetCharacterById(characterId);
        return Ok(new GetCharacterDto()
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = character.Backpacks.Select(b => new GetBackpackDto()
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList(),
            Titles = character.CharacterTitles.Select(c => new GetTitleDto()
            {
                Title = c.Title.Name,
                AquiredAt = c.AcquiredAt
            }).ToList()
        });
    }

    [HttpPost("/{characterId}/backpacks")]
    public async Task<IActionResult> AddItems(int characterId, List<NewItemDto> list)
    {
        if (!await service.DoesCharacterExist(characterId)) return NotFound("Character with this id does not exist");
        
        ICollection<Backpack> backpacks = new List<Backpack>();
        int totalWeight = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (!await service.DoesItemExist(list[i].ItemId)) return NotFound("Items with id "+ list[i].ItemId +" does not exist");
            if (list[i].Amount <= 0) return BadRequest("Incorrect amount in Item with id "+ list[i].ItemId);
            var item = await service.GetItemById(list[i].ItemId);
            totalWeight += item.Weight;
            backpacks.Add(new Backpack()
            {
                Amount = list[i].Amount,
                ItemId = list[i].ItemId,
                CharacterId = characterId
            });
        }
        
        var character = await service.GetCharacter(characterId);
        if (character.MaxWeight - character.CurrentWeight < totalWeight) return BadRequest("Character does not has enough free weight");
        character.CurrentWeight += totalWeight;
        await service.UpdateCharacter(character);
        
        for (int i = 0; i < backpacks.Count; i++)
        {
            if (!await service.DoesCharacterHasItem(characterId, backpacks.ElementAt(i).ItemId))
            {
                await service.AddBackpack(backpacks.ElementAt(i));
            }
            else
            {
                var backpack = await service.GetBackpackByIds(characterId, backpacks.ElementAt(i).ItemId);
                backpack.Amount += backpacks.ElementAt(i).Amount;
                await service.UpdateBackpack(backpack);
            }
        }

        return Ok(backpacks.Select(b => new GetNewItemDto()
        {
            CharacterId = b.CharacterId,
            ItemId = b.ItemId,
            Amount = b.Amount
        }));
    }
}