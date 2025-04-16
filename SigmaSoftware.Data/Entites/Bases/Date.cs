using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigmaSoftware.Data.Entites.Bases;

public abstract class Date
{
    [Column("created_at")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; }
}
