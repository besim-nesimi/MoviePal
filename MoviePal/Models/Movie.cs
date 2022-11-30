using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePal.Models;

internal class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;
    public int? Rating { get; set; }
    public bool IsWatched { get; set; }

    public int DirectorId { get; set; } // FK
    public string? Genre { get; set; }
    public Director Director { get; set; } = null!;

    public List<Actor>? Actors { get; set; } = new(); // Lista är en kollektion av entities - den är tom men förutsättningar för att lägga till finns.
}
