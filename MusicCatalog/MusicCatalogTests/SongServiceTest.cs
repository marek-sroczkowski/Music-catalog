using AutoMapper;
using Moq;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using MusicCatalogAPI.Models.SongDtos;
using MusicCatalogAPI.Repositories.Interfaces;
using MusicCatalogAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MusicCatalogTests
{
    public class SongServiceTest
    {
        private readonly Mock<ISongRepository> _songRepoMock = new Mock<ISongRepository>();
        private readonly IMapper _mapper;

        public SongServiceTest()
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
        public async Task Song_Should_Be_Get_By_Id()
        {
            Song expectedSong = new Song { Name = "Song1", PublicationYear = 2021, Duration = 2.11 };
            _songRepoMock.Setup(r => r.GetSongAsync(1, 1)).ReturnsAsync(expectedSong);
            SongService songService = new SongService(_songRepoMock.Object, _mapper);

            var result = await songService.GetSongByIdAsync(1, 1);

            var expected = JsonConvert.SerializeObject(_mapper.Map<SongDto>(expectedSong));
            var actual = JsonConvert.SerializeObject(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Song_Can_Be_Created()
        {
            List<Song> songs = new List<Song>();
            _songRepoMock.Setup(r => r.AddSongAsync(It.IsAny<int>(), It.IsAny<Song>()))
                .Callback((int a, Song song) => songs.Add(song));
            SongService songService = new SongService(_songRepoMock.Object, _mapper);

            var result = await songService.AddSongAsync(1, new CreateUpdateSongDto());

            Assert.NotEmpty(songs);
        }

        [Fact]
        public async Task Song_Can_Be_Updated()
        {
            var song = new Song { Name = "Song1", PublicationYear = 2021, Duration = 3.0 };
            var updatedSong = new CreateUpdateSongDto { Name = "UpdatedSong", PublicationYear = 2010, Duration = 1.0 };

            _songRepoMock.Setup(r => r.GetSongAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(song);

            SongService songService = new SongService(_songRepoMock.Object, _mapper);
            await songService.UpdateSongAsync(1, 1, updatedSong);

            Assert.Equal(updatedSong.Name, song.Name);
            Assert.Equal(updatedSong.PublicationYear, song.PublicationYear);
            Assert.Equal(updatedSong.Duration, song.Duration);
        }

        [Fact]
        public async Task Song_Can_Be_Deleted()
        {
            var song = new Song() { Id = 1 };
            var songs = new List<Song> { song, new Song(), new Song() };
            _songRepoMock.Setup(r => r.GetSongAsync(It.IsAny<int>(), song.Id)).ReturnsAsync(song);
            _songRepoMock.Setup(r => r.DeleteSongAsync(song)).Callback((Song song) => songs.Remove(song));

            SongService songService = new SongService(_songRepoMock.Object, _mapper);
            await songService.DeleteSongAsync(1, song.Id);

            Assert.Equal(2, songs.Count);
        }

        [Fact]
        public async Task All_Songs_Can_Be_Deleted()
        {
            var songs = new List<Song> { new Song(), new Song() };
            _songRepoMock.Setup(r => r.DeleteSongsAsync(It.IsAny<int>())).Callback((int a) => songs.Clear());

            SongService songService = new SongService(_songRepoMock.Object, _mapper);
            await songService.DeleteAllSongsAsync(1);

            Assert.Empty(songs);
        }
    }
}
