using Microsoft.AspNetCore.Identity;
using MusicCatalogAPI.Data;
using MusicCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalogAPI
{
    public class DataSeeder
    {
        private readonly AppDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;

        public DataSeeder(AppDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (!dbContext.Users.Any())
                InsertSampleData();
        }

        private void InsertSampleData()
        {
            dbContext.Artists.AddRange(GetSampleArtists());
            dbContext.SaveChanges();

            var supplier1 = new Supplier
            {
                Name = "Super dostawca",
                Username = "SuperDostawca",
            };
            supplier1.Albums = GetSampleAlbums();
            supplier1.PasswordHash = passwordHasher.HashPassword(supplier1, "1234");

            dbContext.Suppliers.Add(supplier1);
            dbContext.SaveChanges();
        }

        private List<Album> GetSampleAlbums()
        {
            List<Album> albums = new List<Album>();
            for (int i = 0; i < 30; i++)
            {
                var artist = dbContext.Artists.FirstOrDefault(a => a.Name.Equals(GetSampleArtistName()));
                albums.Add(new Album
                {
                    Songs = GetSampleSongs(),
                    Title = $"Album{i}",
                    PublicationYear = (new Random()).Next(2017, 2022),
                    Version = GetSampleVersion(),
                    Artist = artist
                });
            }
            return albums;
        }

        private string GetSampleVersion()
        {
            string[] versions = new string[] { "Studyjny", "Demo", "Remix", "Studyjny", "Kompilacja", "Studyjny" };
            return versions[(new Random()).Next(0, versions.Length)];
        }

        private string GetSampleArtistName()
        {
            var artistNames = new List<string> { "Jan Kowalski", "Jacek Nowak", "Monika Adamczyk", "Kooovalsy", "Mr Grzechu" };
            return artistNames[(new Random()).Next(0, artistNames.Count)];
        }

        private List<Artist> GetSampleArtists()
        {
            var artistNames = new List<string> { "Jan Kowalski", "Jacek Nowak", "Monika Adamczyk", "Kooovalsy", "Mr Grzechu" };
            var artists = new List<Artist>();
            foreach (var name in artistNames)
                artists.Add(new Artist { Name = name });
            return artists;
        }

        private List<Song> GetSampleSongs()
        {
            List<Song> songs = new List<Song>();
            int size = (new Random()).Next(1, 20);
            for(int i = 0; i< size; i++)
            {
                songs.Add(new Song
                {
                    Name = $"Piosenka{i}",
                    PublicationYear = (new Random()).Next(2010, 2022),
                    Duration = Math.Round((new Random()).NextDouble() + 2.6, 2)
                }); ;
            }
            return songs;
        }
    }
}
