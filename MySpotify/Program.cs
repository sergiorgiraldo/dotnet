using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MySpotify
{
    class Program
    {
        private static SpotifyWebAPI _spotify;
        private static PublicProfile _user;
        private static string[] parameters;
        private static List<SimplePlaylist> myPlaylists = new List<SimplePlaylist>();

        static void Main(string[] args)
        {
            parameters = args;

            ImplicitGrantAuth auth = new ImplicitGrantAuth(
                "6124e12cbb1c4e779faf7f0b52f42c51",
                "http://localhost:4002",
                "http://localhost:4002",
                Scope.UserReadPrivate | Scope.PlaylistModifyPublic | Scope.PlaylistModifyPrivate
            );
            auth.AuthReceived += async (sender, payload) =>
            {
                Console.Clear();
                auth.Stop(); 
                _spotify = new SpotifyWebAPI()
                {
                    TokenType = payload.TokenType,
                    AccessToken = payload.AccessToken
                };
                _user = _spotify.GetPublicProfile("sergiorgiraldo");
                DoThings();
            };
            auth.Start(); 
            auth.OpenBrowser();
            Console.ReadLine();
            auth.Stop(0);
        }

        private static void DoThings()
        {
            if (parameters.Length == 0)
            {
                Help();
            }
            else
            {
                if (parameters[1] == "/h") //calling with dotnet run it is parameters[1], directly calling it is parameters[0]
                {
                    Help();
                }
                else if (parameters[1] == "/l")
                {
                    ListPlaylists(50000, 0);
                }
                else if (parameters[1] == "/f")
                {
                    ListPlaylists(50000, 0, true);
                    FindInPlaylists(parameters[2]);
                }
                else if (parameters[1] == "/d")
                {
                    DeletePlaylist();
                }
                else
                {
                    CreatePlaylist();
                }
            }
        }

        private static bool foundInPlaylist = false;
        private static void FindInPlaylists(string what)
        {
            foreach (SimplePlaylist playlist in myPlaylists)
            {
                foundInPlaylist = false;
                FindInPlaylist(what, 500000, 0, playlist);
            }
            Console.WriteLine("*****END******");
        }

        private static async void FindInPlaylist(string what, int total_, int offset, SimplePlaylist playlist)
        {
            if (total_ <= offset || foundInPlaylist) return;

            var t =_spotify.GetPlaylistTracks(playlist.Id, "", 50, offset, "");
            var total = t.Total;
            foreach(var track in t.Items)
            {
                if (track.Track.Album.ToString().ToLower().Contains(what.ToLower()) ||
                track.Track.Artists.Find(a => a.Name.ToLower().Contains(what.ToLower())) != null ||
                track.Track.Name.ToString().ToLower().Contains(what.ToLower())) 
                {
                    foundInPlaylist = true;
                    Task<FullTrack> fullTrackTask = GetTrack(track.Track.Id, playlist.Name);
                    FullTrack fullTrack = await fullTrackTask;
                    Console.WriteLine("On " + playlist.Name + ":: song '" 
                                + fullTrack.Name + "' from album '" 
                                + fullTrack.Album.Name 
                                + "' (" + GetArtists(fullTrack.Artists) + ")");
                    break;
                } 
            }
            FindInPlaylist(what, total, offset + 50, playlist);
        }

        private static string GetArtists(List<SimpleArtist> artists)
        {
            string result = "";
            foreach (SimpleArtist artist in artists)
            {
                result += artist.Name + ", ";
            }
            return result.Substring(0, result.Length - 2);
        }

        private static async Task<FullTrack> GetTrack(string id, string playlistName)
        {
            await Task.Delay(3000);
            var fullTrack = _spotify.GetTrack(id);
            return fullTrack;   
        }

        private static void ListPlaylists(int total_, int offset, bool retrieveIds = false)
        {
            if (total_ <= offset) return;

            var playlists =_spotify.GetUserPlaylists(_user.Id, 50, offset);
            var total = playlists.Total;

            foreach(var playlist in playlists.Items)
            {
                if (retrieveIds){
                    myPlaylists.Add(playlist);
                }
                else 
                { 
                    Console.WriteLine(playlist.Name);
                }
            }
            ListPlaylists(total, offset + 50, retrieveIds);
        }

        private static void DeletePlaylist()
        {
            throw new NotImplementedException();
        }

        private static void CreatePlaylist()
        {
            var currentColor = Console.ForegroundColor;
            var songsToAdd = File.ReadAllLines(parameters[0]);
            var playlist = _spotify.CreatePlaylist(
                _user.Id,
                parameters[1], 
                true, 
                false, 
                parameters[2]);
            if (!playlist.HasError())
            {
                foreach (var song_ in songsToAdd)
                {
                    var song = song_.Replace("\"", "").Replace("\t", " - ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(">>>>" + song);
                    SearchItem item = _spotify.SearchItems(song, SearchType.Track, 1);
                    if (item.Tracks.Total > 0)
                    {
                        _spotify.AddPlaylistTrack(playlist.Id, item.Tracks.Items[0].Uri);
                        Console.ForegroundColor = ConsoleColor.Green;
                    	Console.WriteLine("Added");
                    }
                    else //not found
                    {
                        if (song.Contains("-"))
                        {
                            var parts = song.Split("-");
                            //artist - track
                            var auxSong = parts[1];
                            item = _spotify.SearchItems(auxSong, SearchType.Track, 1);
                            if (item.Tracks.Total > 0)
                            {
                                _spotify.AddPlaylistTrack(playlist.Id, item.Tracks.Items[0].Uri);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Added");
                            }
                            else
                            {
                                //track - artist
                                auxSong = parts[0];
                                item = _spotify.SearchItems(auxSong, SearchType.Track, 1);
                                if (item.Tracks.Total > 0)
                                {
                                    _spotify.AddPlaylistTrack(playlist.Id, item.Tracks.Items[0].Uri);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Added");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("**** Song not found ****");
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("**** Song not found ****");
                        }
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(playlist.Error.Message);
            }
            Console.ForegroundColor = currentColor;
        }

        private static void Help()
        {
            Console.WriteLine("MySpotify <FILE_WITH_MUSICS_TO ADD> <NAME_OF_PLAYLIST> <DESCRIPTION_OF_PLAYLIST>");
        }
    }
}
