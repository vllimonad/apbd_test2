using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Models;

namespace test2.Services;

public class AppService: IAppService
{
    private readonly ApplicationContext _context;

    public AppService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<bool> DoesCharacterExist(int id)
    {
        return await _context.Characters.AnyAsync(c => c.Id == id);
    }
    
    public async Task<Character> GetCharacterById(int id)
    {
        return await _context.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(c => c.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(c => c.Title)
            .Where(c => c.Id == id)
            .FirstAsync();
    }
    
    public async Task<bool> DoesItemExist(int id)
    {
        return await _context.Items.AnyAsync(c => c.Id == id);
    }
    
    public async Task<Item> GetItemById(int id)
    {
        return await _context.Items.FindAsync(id);
    }
    
    public async Task AddBackpack(Backpack backpack)
    {
        await _context.Backpacks.AddAsync(backpack);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> DoesCharacterHasItem(int characterId, int itemId)
    {
        return await _context.Backpacks.AnyAsync(b => b.CharacterId == characterId & b.ItemId == itemId);
    }
    
    public async Task<Backpack> GetBackpackByIds(int characterId, int itemId)
    {
        return await _context.Backpacks.Where(b => b.CharacterId == characterId & b.ItemId == itemId).FirstAsync();
    }
    
    public async Task UpdateBackpack(Backpack backpack)
    {
        _context.Backpacks.Update(backpack);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateCharacter(Character character)
    {
        _context.Characters.Update(character);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Character> GetCharacter(int id)
    {
        return await _context.Characters.FindAsync(id);
    }
    
}