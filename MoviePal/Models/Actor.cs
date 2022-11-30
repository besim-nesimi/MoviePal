using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviePal.Models;

internal class Actor
{
    public int ActorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public List<Movie>? Movies { get; set; }
}
