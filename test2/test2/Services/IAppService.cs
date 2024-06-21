using test2.Models;

namespace test2.Services;

public interface IAppService
{
    Task<bool> DoesCharacterExist(int id);
    Task<Character> GetCharacterById(int id);
    Task<bool> DoesItemExist(int id);
    Task<Item> GetItemById(int id);
    Task AddBackpack(Backpack backpack);
    Task<bool> CharacterHasItem(int characterId, int itemId);
    Task<Backpack> GetBackpack(int characterId, int itemId);
    Task UpdateBackpack(Backpack backpack);
    Task UpdateCharacter(Character character);
    Task<Character> GetCharacter(int id);
}