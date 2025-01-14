
using System.ComponentModel.DataAnnotations;

namespace FTG.Data.Models;
public class Entity
{
    [Key]
    public int Id { get; set; }
    public required DateTime CreatedOn { get; set; }
}
