using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MoviePal.Models;

namespace MoviePal.Data;

internal class AppDbContext : DbContext
{
	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Movie> Movies{ get; set; }
	public DbSet<Director> Directors { get; set; }
	public DbSet<Actor> Actors { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MoviePalDb;Trusted_Connection=True;");
	}

}
