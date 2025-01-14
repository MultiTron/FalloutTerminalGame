using FTG.Data;
using FTG.Data.Models;
using FTG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FTG.Services.Implementations;
public class WordService(GameDbContext _context) : IWordService
{
    public async Task CreateWord(string value)
    {
        if (string.IsNullOrEmpty(value) || _context.Words.Any(x => x.Value == value))
        {
            return;
        }

        var word = new Word
        {
            Value = value,
            CreatedOn = DateTime.UtcNow,
        };

        await _context.AddAsync(word);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWord(int id)
    {
        var entity = await _context.Words.FindAsync(id);
        if (entity != null)
        {
            _context.Words.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Word>> GetAllWords()
    {
        return await _context.Words.ToListAsync();
    }

    public async Task<List<Word>> GetRandomWords(int count)
    {
        return await _context.Words.OrderBy(x => Random.Shared.Next())
            .Take(count <= await _context.Words.CountAsync() ? count : await _context.Words.CountAsync())
            .ToListAsync();
    }
}
