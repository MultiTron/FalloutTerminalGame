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
        var wordLength = Random.Shared.Next(4, 11);
        var list = new List<Word>();
        foreach (var word in _context.Words)
        {
            if (word.Value.Length == wordLength)
                list.Add(word);
        }
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(0, i + 1);
            Word temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list.Take(count <= list.Count ? count : list.Count).ToList();
    }
}
