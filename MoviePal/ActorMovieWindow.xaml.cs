using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using MoviePal.Data;
using MoviePal.Models;

namespace MoviePal
{
    /// <summary>
    /// Interaction logic for ActorMovieWindow.xaml
    /// </summary>
    public partial class ActorMovieWindow : Window
    {
        public ActorMovieWindow(int actorId)
        {
            InitializeComponent();

            using (AppDbContext context = new())
            {
                Actor dbActor = context.Actors.Include(a => a.Movies).FirstOrDefault(a => a.ActorId == actorId);

                foreach(Movie actorMovie in dbActor.Movies) // För varje film som finns i skådisens movie property lista == displaya titeln i lv.
                {
                    lvActorMovies.Items.Add(actorMovie.Title);
                }
            }
        }
    }
}
