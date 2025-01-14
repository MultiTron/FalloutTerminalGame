using FTG.Data.Models;

namespace FTG.Services.Interfaces;
public interface IWordService
{
    public Task CreateWord(string value);
    public Task DeleteWord(int id);
    public Task<List<Word>> GetRandomWords(int count);
    public Task<List<Word>> GetAllWords();
}
