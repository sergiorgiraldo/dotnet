using System;
using System.IO;
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
                if (parameters[0] == "/h")
                {
                    Help();
                }
                else
                {
                    CreatePlaylist();
                }
            }
        }

        private static void CreatePlaylist()
        {
            var currentColor = Console.ForegroundColor;
            var songsToAdd = File.ReadAllLines(parameters[1]);
            var playlist = _spotify.CreatePlaylist(
                _user.Id,
                parameters[2], 
                true, 
                false, 
                parameters[3]);
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
                                DoThings();
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
