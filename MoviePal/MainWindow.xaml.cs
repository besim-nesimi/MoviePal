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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using MoviePal.Data;
using MoviePal.Models;

namespace MoviePal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Populera combobox med directors

            using (AppDbContext context = new())
            {
                // Populera directors
                List<Director> directors = context.Directors.ToList();

                foreach(Director director in directors)
                {

                    ComboBoxItem item = new(); // Skapar ett item som ska containa vår director

                    item.Content = $"{director.FirstName} {director.LastName}";
                    item.Tag = director;

                    cbDirectors.Items.Add(item); // Lägger till vårt item i combobox listan

                    // Funkar precis som ett listview item när man ska lägga till i listview, men ist för lv så e det för CB.
                }

                // Populera actors

                List<Actor> actors = context.Actors.ToList();

                foreach (Actor actor in actors)
                {
                    ListViewItem item = new();

                    item.Content = $"{actor.FirstName} {actor.LastName}";
                    item.Tag = actor;

                    lvActors.Items.Add(item);
                }

                // Populera movies
                
                List<Movie> movies = context.Movies.ToList();

                foreach (Movie movie in movies)
                {
                    ComboBoxItem item = new();

                    item.Content = movie.Title;
                    item.Tag = movie;

                    cbMovies.Items.Add(item);
                }
            }
        }

        private void btnAddDirector_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                Director newDirector = new();

                newDirector.FirstName = txtDirectorFirstName.Text;
                newDirector.LastName = txtDirectorLastName.Text;

                context.Directors.Add(newDirector);
                context.SaveChanges();
            }

            txtDirectorFirstName.Clear();
            txtDirectorLastName.Clear();
        }

        private void btnAddActor_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                Actor newActor = new();

                newActor.FirstName = txtActorFirstName.Text;
                newActor.LastName = txtActorLastName.Text;

                context.Actors.Add(newActor);
                context.SaveChanges();


                //context.Actors.Add(new Actor() <--- Detta är det andra sättet
                //{
                //    FirstName = txtActorFirstName.Text,
                //    LastName = txtActorLastName.Text
                //});

                //context.SaveChanges();
            }

            txtActorFirstName.Clear();
            txtActorLastName.Clear();
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                Movie newMovie = new();
                
                newMovie.Title = txtMovieTitle.Text; // Sätter titeln på filmen
                newMovie.IsWatched = (bool)xbMovieWatched.IsChecked!; // måste castas till en bool, lägger till utropstecken på slutet så att ifall den ej icheckad = false, om checkad = true!
                
                ComboBoxItem selectedItem = cbDirectors.SelectedItem as ComboBoxItem; // Vi hämtar comboboxens innehåll 
                Director selectedDirector = selectedItem.Tag as Director; // Vi sätter directorn till selectedDirector med selectedItem.Tag as Director objekt

                Director dbDirector = context.Directors.FirstOrDefault(d => d.DirectorId == selectedDirector.DirectorId); // Så att vi inte skapar en ny director, utan väljer den som finns i db med samma id.

                newMovie.Director = dbDirector; // Här anger vi vem som är directorn.

                context.Movies.Add(newMovie);
                context.SaveChanges();

            }

            txtActorFirstName.Clear();
            txtActorLastName.Clear();
        }

        private void btnAddActorMovie_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                ListViewItem selectedListViewItem = lvActors.SelectedItem as ListViewItem; // Vilken actor är det?
                Actor selectedActor = (Actor)selectedListViewItem.Tag; // selectedListViewItem.Tag är t.ex. detta item.Content = $"{director.FirstName} {director.LastName}";

                Actor dbActor = context.Actors.Include(a => a.Movies).FirstOrDefault(a => a.ActorId == selectedActor.ActorId); // Actor i listview, i kodform, knyts samman med actor i Db genom ActorId. Går också skriva .Include(a => a.Movies) när vi populatear listan för att slippa denna linjen kod ?

                ComboBoxItem selectedComboBoxItem = cbMovies.SelectedItem as ComboBoxItem;
                Movie selectedMovie = selectedComboBoxItem.Tag as Movie;

                Movie? dbMovie = context.Movies.FirstOrDefault(m => m.MovieId == selectedMovie.MovieId);

                dbActor.Movies.Add(dbMovie);
                context.Actors.Update(dbActor);
                context.SaveChanges();

            }


        }

        private void btnShowActorMovies_Click(object sender, RoutedEventArgs e)
        {

            using (AppDbContext context = new())
            {
                ListViewItem selectedListViewItem = lvActors.SelectedItem as ListViewItem;
                Actor selectedActor = (Actor)selectedListViewItem.Tag;

                ActorMovieWindow actorMovieWindow = new(selectedActor.ActorId);

                actorMovieWindow.Show();
                // Actor? dbActor = context.Actors.Include(a => a.Movies).FirstOrDefault(a => a.ActorId == selectedActor.ActorId);
            }
            
        }
    }
}
