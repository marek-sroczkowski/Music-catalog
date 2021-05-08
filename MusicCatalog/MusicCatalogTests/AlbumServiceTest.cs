using AutoMapper;
using Moq;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Mapping;
using MusicCatalogAPI.Models.AlbumDtos;
using MusicCatalogAPI.Models.ArtistDtos;
using MusicCatalogAPI.Models.SongDtos;
using MusicCatalogAPI.Models.SupplierDtos;
using MusicCatalogAPI.Repositories.Interfaces;
using MusicCatalogAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MusicCatalogTests
{
    public class AlbumServiceTest
    {
        private readonly Mock<IAlbumRepository> _albumRepoMock = new Mock<IAlbumRepository>();
        private readonly IMapper _mapper;

        public AlbumServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task Album_Should_Be_Get_By_Id()
        {
            _albumRepoMock.Setup(r => r.GetAlbumAsync(1)).ReturnsAsync(album);
            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);

            var result = await albumService.GetAlbumByIdAsync(1);

            var expected = JsonConvert.SerializeObject(albumDto);
            var actual = JsonConvert.SerializeObject(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Album_Can_Be_Created()
        {
            var createAlbumDto = new CreateAlbumDto
            {
                Title = "Album1",
                PublicationYear = 2021,
                Version = "demo",
                Artist = new CreateUpdateArtistDto
                {
                    Name = "Artist1"
                }
            };
            var album = _mapper.Map<Album>(createAlbumDto);
            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);

            var result = await albumService.AddAlbumAsync("user1", createAlbumDto);

            Assert.Equal(album.Title, result.Title);
            Assert.Equal(album.PublicationYear, result.PublicationYear);
            Assert.Equal(album.Version, result.Version);
            Assert.Equal(album.Artist.Name, result.Artist.Name);
        }

        [Fact]
        public async Task Album_Can_Be_Updated()
        {
            var existingAlbum = new Album { Title = "Album1", PublicationYear = 2021, Version = "Version1"};
            var updatedAlbum = new UpdateAlbumDto { Title = "UpdatedAlbum", PublicationYear = 2010, Version = "UpdatedVersion"};
            _albumRepoMock.Setup(r => r.GetAlbumAsync(1)).ReturnsAsync(existingAlbum);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            await albumService.UpdateAlbumAsync(1, updatedAlbum);

            Assert.Equal(updatedAlbum.Title, existingAlbum.Title);
            Assert.Equal(updatedAlbum.PublicationYear, existingAlbum.PublicationYear);
            Assert.Equal(updatedAlbum.Version, existingAlbum.Version);
        }

        [Fact]
        public async Task Album_Can_Be_Deleted()
        {
            var album = new Album { Id = 1 };
            _albumRepoMock.Setup(r => r.GetAlbumAsync(1)).ReturnsAsync(album);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            await albumService.DeleteAlbumAsync(1);

            _albumRepoMock.Verify(r => r.DeleteAlbumAsync(album));
        }

        [Fact]
        public async Task Albums_Can_Be_Paging_With_Page_Size()
        {
            var albumParameters = new AlbumParameters { PageSize = 2 };
            _albumRepoMock.Setup(r => r.GetAlbumsAsync(It.IsAny<string>()))
                .ReturnsAsync(albums);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            var result = await albumService.GetAlbumsAsync("user1", albumParameters);

            Assert.Equal(albumParameters.PageSize, result.Count);
        }

        [Fact]
        public async Task Albums_Can_Be_Paging_With_Page_Number()
        {
            var albumParameters = new AlbumParameters { PageSize = 2 };
            _albumRepoMock.Setup(r => r.GetAlbumsAsync(It.IsAny<string>()))
                .ReturnsAsync(albums);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            var result = await albumService.GetAlbumsAsync("user1", albumParameters);

            Assert.Equal(albumParameters.PageNumber, result.CurrentPage);
        }

        [Fact]
        public async Task Albums_Can_Be_Filtering_By_Title()
        {
            var albumParameters = new AlbumParameters { Title = "Title1" };
            _albumRepoMock.Setup(r => r.GetAlbumsAsync(It.IsAny<string>()))
                .ReturnsAsync(albums);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            var result = await albumService.GetAlbumsAsync("user1", albumParameters);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Albums_Can_Be_Filtering_By_Publication_Year()
        {
            var albumParameters = new AlbumParameters { PublicationYear = 2020};
            _albumRepoMock.Setup(r => r.GetAlbumsAsync(It.IsAny<string>()))
                .ReturnsAsync(albums);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            var result = await albumService.GetAlbumsAsync("user1", albumParameters);

            Assert.Single(result);
        }

        [Fact]
        public async Task Albums_Can_Be_Filtering_By_Two_Criteria()
        {
            var albumParameters = new AlbumParameters { PublicationYear = 2021, Title = "Title1" };
            _albumRepoMock.Setup(r => r.GetAlbumsAsync(It.IsAny<string>()))
                .ReturnsAsync(albums);

            AlbumService albumService = new AlbumService(_albumRepoMock.Object, _mapper);
            var result = await albumService.GetAlbumsAsync("user1", albumParameters);

            Assert.Equal(2, result.Count);
        }

        private Album album = new Album
        {
            Id = 1,
            Title = "Album1",
            PublicationYear = 2021,
            Version = "demo",
            Songs = new List<Song>
            {
                new Song
                {
                    Id = 1,
                    Name = "Song1",
                    PublicationYear = 2021,
                    Duration = 3.11
                },
                new Song
                {
                    Id = 2,
                    Name = "Song2",
                    PublicationYear = 2020,
                    Duration = 3.44
                },
                new Song
                {
                    Id = 3,
                    Name = "Song3",
                    PublicationYear = 2021,
                    Duration = 2.57
                }
            },
            Artist = new Artist
            {
                Name = "Arstist1"
            },
            Supplier = new Supplier
            {
                Name = "Supplier1"
            }
        };

        private AlbumDetailsDto albumDto = new AlbumDetailsDto
        {
            Id = 1,
            Title = "Album1",
            PublicationYear = 2021,
            Version = "demo",
            Songs = new List<SongDto>
            {
                new SongDto
                {
                    Id = 1,
                    Name = "Song1",
                    PublicationYear = 2021,
                    Duration = 3.11
                },
                new SongDto
                {
                    Id = 2,
                    Name = "Song2",
                    PublicationYear = 2020,
                    Duration = 3.44
                },
                new SongDto
                {
                    Id = 3,
                    Name = "Song3",
                    PublicationYear = 2021,
                    Duration = 2.57
                }
            },
            Artist = new ArtistDto
            {
                Name = "Arstist1"
            },
            Supplier = new SupplierDto
            {
                Name = "Supplier1"
            }
        };

        private List<Album> albums = new List<Album>
        {
             new Album(){Title = "Title1", PublicationYear = 2021},
             new Album(){Title = "Title2", PublicationYear = 2021},
             new Album(){Title = "Title1", PublicationYear = 2021},
             new Album(){Title = "Title1", PublicationYear = 2020},
             new Album(){Title = "Title3", PublicationYear = 2021}
        };
    }
}
