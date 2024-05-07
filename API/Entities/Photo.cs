using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.Entities;

[Table("Photos")]// lets EF know to make the table this name instead
public class Photo
{
    public int Id { get; set; }

    public string Url { get; set; } 

    public bool IsMain { get; set; }

    public string PublicId { get; set; }
    public int AppUserId { get; set; }

    public AppUser AppUser { get; set; }



}
