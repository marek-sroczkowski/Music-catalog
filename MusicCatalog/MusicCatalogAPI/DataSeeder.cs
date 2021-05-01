using Microsoft.AspNetCore.Identity;
using MusicCatalogAPI.Data;
using MusicCatalogAPI.Entities;
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
                InsertSimpleData();
        }

        private void InsertSimpleData()
        {
            var playMusic = new Supplier
            {
                Name = "Plus Music",
                Username = "PlusMusic",
                Albums = new List<Album>
                {
                    new Album
                    {
                        Title = "Dziękuję",
                        PublicationYear = 2021,
                        Version = "v1",
                        Artist = new Artist
                        {
                            Name = "Król"
                        },
                        Songs = new List<Song>
                        {
                            new Song
                            {
                                Name = "ZBYT DOBRZE CI IDZIE",
                                PublicationYear = 2021,
                                Duration = 3.06
                            },
                            new Song
                            {
                                Name = "NA ZACHODZIE BEZ ZMIAN",
                                PublicationYear = 2021,
                                Duration = 2.11
                            },
                            new Song
                            {
                                Name = "KTO JEST CO",
                                PublicationYear = 2021,
                                Duration = 3.07
                            },
                            new Song
                            {
                                Name = "DRGAWKI I SKRĘTY",
                                PublicationYear = 2021,
                                Duration = 1.21
                            },
                            new Song
                            {
                                Name = "TAK JAK TY",
                                PublicationYear = 2021,
                                Duration = 3.30
                            }
                        }
                    },
                    new Album
                    {
                        Title = "Fight Club",
                        PublicationYear = 2021,
                        Version = "v2",
                        Artist = new Artist
                        {
                            Name = "PRO8L3M"
                        },
                        Songs = new List<Song>
                        {
                            new Song
                            {
                                Name = "Witamy, witamy, witamy",
                                PublicationYear = 2021,
                                Duration = 2.51
                            },
                            new Song
                            {
                                Name = "GeForce",
                                PublicationYear = 2021,
                                Duration = 3.32
                            },
                            new Song
                            {
                                Name = "Strange Days",
                                PublicationYear = 2021,
                                Duration = 3.34
                            },
                            new Song
                            {
                                Name = "A2",
                                PublicationYear = 2021,
                                Duration = 3.17
                            },
                            new Song
                            {
                                Name = "The End Fin Esc Abort",
                                PublicationYear = 2021,
                                Duration = 3.25
                            }
                        }
                    }
                }
            };
            playMusic.PasswordHash = passwordHasher.HashPassword(playMusic, "1234");

            dbContext.Suppliers.Add(playMusic);
            dbContext.SaveChanges();
        }
    }
}
