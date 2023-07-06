using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using com.sun.xml.@internal.fastinfoset.sax;
using edu.stanford.nlp.util;
using System.Net.Http;
using System.Security.Cryptography;
using Google.Apis.Upload;
using java.util;
using javax.xml.stream.events;
using sun.security.util;

namespace YoutubeApi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string videoId = "ccewwizWzAk";  //  j6muwUGdvXw
            string apikey = "AIzaSyC9O_Y7y8fZ1pk2NcbZAWtm4sXGMbGem-g";
            //7xPsl7ltKnM -Y6Vq3tdQsp4 
            //GetVideoComments(videoId);
            //GetYoubeComments();
            //GetYoutubeIdByEmail();
            //GetYoutubeVideoView(videoId);
            //GetYouTubeVideos();
            //ExtracatYouTubeVideosTags(apikey);
            //YouTubeThumbnailDownload(apikey);
            //YouTubeVideosTagsGenerate(apikey);
            //GetChannelId(apikey);
            //GetWhatsappChat();
            //UploadYoutubeVideo();
            //GeneratePassword();
            //RandomNumber();
            //RandomWord();
            RandomWord1();
            //RandomName();
            //RandomNameGenerate();
            //RandomCountryNameGenerate();
            //WordCountofText();
            //CombinedWordofText();
        }


        //public static void GetYoubeComments()
        //{
        //    try
        //    {
        //        UserCredential credential;
        //        using (var stream = new FileStream(@"D:\youtube\youtubeapi.json", FileMode.Open, FileAccess.Read))
        //        {
        //            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //                GoogleClientSecrets.Load(stream).Secrets,
        //                new[] { YouTubeService.Scope.YoutubeForceSsl },
        //                "user",
        //                System.Threading.CancellationToken.None,
        //                new FileDataStore("YouTubeAPI")
        //            ).Result;
        //        }

        //        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = "Calendar"
        //        });

        //        string videoId = "Y6Vq3tdQsp4";
        //        var commentsListRequest = youtubeService.CommentThreads.List("snippet");
        //        commentsListRequest.VideoId = videoId;

        //        var commentsListResponse = commentsListRequest.Execute();

        //        foreach (var commentThread in commentsListResponse.Items)
        //        {
        //            var topLevelComment = commentThread.Snippet.TopLevelComment.Snippet;
        //            string author = topLevelComment.AuthorDisplayName;
        //            string text = topLevelComment.TextDisplay;

        //            Console.WriteLine($"Author: {author}");
        //            Console.WriteLine($"Comment: {text}");
        //            Console.WriteLine();
        //        }

        //        Console.ReadLine();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadLine();
        //    }
        //}
        private static YouTubeService GetYouTubeService()
        {
            var apiKey = "AIzaSyC9O_Y7y8fZ1pk2NcbZAWtm4sXGMbGem-g"; // Replace with your YouTube Data API key
            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "Calendar"
            });

            return service;
        }
        //Working
        private static void GetVideoComments(string videoId)
        {
            try
            {
                var youtubeService = GetYouTubeService();

                var commentsRequest = youtubeService.CommentThreads.List("snippet");
                commentsRequest.VideoId = videoId;
                commentsRequest.MaxResults = 50; // Increase the maximum results per request

                string nextPageToken = null;
                int count = 0;

                do
                {
                    commentsRequest.PageToken = nextPageToken;

                    var commentsResponse = commentsRequest.Execute();

                    foreach (var comment in commentsResponse.Items)
                    {
                        count++;
                        var commenterName = comment.Snippet.TopLevelComment.Snippet.AuthorDisplayName;
                        var commentText = comment.Snippet.TopLevelComment.Snippet.TextDisplay;
                        Console.WriteLine("Comment " + count + ":-" + commentText);
                        Console.WriteLine();
                    }

                    nextPageToken = commentsResponse.NextPageToken;
                } while (nextPageToken != null);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
           
        }

        private static void GetChannelIdByEmail()
        {
            try
            {
                var youtubeService = GetYouTubeService();

                var channelsRequest = youtubeService.Channels.List("id");
                channelsRequest.Mine = true;

                var channelsResponse = channelsRequest.Execute();
                var channel = channelsResponse.Items.FirstOrDefault();
                string channelId= channel.Id;
                if (channelId != null)
                {
                    Console.WriteLine($"Channel ID: {channelId}");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Channel not found.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

       // private const string apiKey = "AIzaSyC9O_Y7y8fZ1pk2NcbZAWtm4sXGMbGem-g"; // Replace with your own API key
        public static void GetYoutubeIdByEmail()
        {
            try
            {
                string apiKey = "AIzaSyC9O_Y7y8fZ1pk2NcbZAWtm4sXGMbGem-g";
                string email = "siteshbranda@gmail.com";
                // Create the YouTube service.
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey, // Replace with your YouTube Data API key.
                    ApplicationName = "YoutubeApi"
                });


                // Construct the search query using the email address.
                string searchQuery = $"channel {email}";

                // Execute the search request.
                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = searchQuery;
                searchListRequest.MaxResults = 1;
                var searchListResponse = searchListRequest.Execute();

                // Retrieve the channel ID from the search results.
                string channelId = searchListResponse.Items.FirstOrDefault()?.Snippet.ChannelId;

                Console.WriteLine("Your channel Id:- "+channelId);
                Console.ReadLine();


                //var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                //{
                //    ApiKey = apiKey,
                //    ApplicationName = "YoutubeApi"
                //});

                //string email = "siteshbranda@gmail.com";
                //ChannelsResource.ListRequest channelsListRequest = youtubeService.Channels.List("snippet");
                //channelsListRequest.ForUsername = email;
                //ChannelListResponse response = channelsListRequest.Execute();
                //if (response != null )
                //{
                //    Channel channel = response.Items.FirstOrDefault();
                //    string channelId = channel.Id;
                //    string channelTitle = channel.Snippet.Title;
                //    Console.WriteLine($"Channel ID: {channelId}");
                //    Console.WriteLine($"Channel Title: {channelTitle}");
                //    Console.ReadLine();
                //}
                //else
                //{
                //    Console.WriteLine("Channel not found for the given email.");
                //    Console.ReadLine();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        //working
        public static void GetYoutubeVideoView(string videoId)
        {
            try
            {
                string apiKey = "AIzaSyC9O_Y7y8fZ1pk2NcbZAWtm4sXGMbGem-g";
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                    ApplicationName = "YoutubeApi"
                });
                //string videoId = "Y6Vq3tdQsp4";
                // Prepare the request
                VideosResource.ListRequest listRequest = youtubeService.Videos.List("snippet,contentDetails,statistics,status");
                listRequest.Id = videoId;
                VideoListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    Console.WriteLine("Title: " + item.Snippet.Title);
                    Console.WriteLine("Description: " + item.Snippet.Description);
                    Console.WriteLine("View Count: " + item.Statistics.ViewCount);
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        //working
        private static void GetYouTubeVideos()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyC9O_Y7y8fZ1pk2NcbZAWtm4sXGMbGem-g"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
           // searchListRequest.Q = "My Videos"; // Replace with your search term.
            searchListRequest.ChannelId = "UCUv1H-ZEbtPjo4LLSwrmm7g"; // Replace with your channel id. //UCUv1H-ZEbtPjo4LLSwrmm7g //UCwfu-wRxjJwujwDveAgnJkQ
            searchListRequest.MaxResults = 50;

            var searchListResponse = searchListRequest.Execute();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add($"{searchResult.Snippet.Title} ({searchResult.Id.VideoId})");
                        break;
                    case "youtube#channel":
                        channels.Add($"{searchResult.Snippet.Title} ({searchResult.Id.ChannelId})");
                        break;
                    case "youtube#playlist":
                        playlists.Add($"{searchResult.Snippet.Title} ({searchResult.Id.PlaylistId})");
                        break;
                }
            }

            Console.WriteLine($"Videos:\n{string.Join("\n", videos)}\n");
            Console.WriteLine($"Channels:\n{string.Join("\n", channels)}\n");
            Console.WriteLine($"Playlists:\n{string.Join("\n", playlists)}\n");
            Console.ReadLine();
        }
        //working
        private static void ExtracatYouTubeVideosTags(string key)
        {
            try
            {
                // Set up the API service.
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = key // Replace with your API key.
                });

                // Specify the video ID for which you want to retrieve tags.
                string videoId = "TE66McLMMEw";

                // Define the request to retrieve video details.
                var videoRequest = youtubeService.Videos.List("snippet");
                videoRequest.Id = videoId;

                // Execute the request and retrieve the video details.
                var videoResponse = videoRequest.Execute();

                // Extract the tags from the video response.
                var videoTags = videoResponse.Items[0].Snippet.Tags;

                // Print the tags to the console.
                Console.WriteLine("Your video tags are: ");
                foreach (var tag in videoTags)
                {
                    Console.WriteLine(tag);
                }

                Console.ReadLine();
            }
            catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        //working
        private static void YouTubeVideosTagsGenerate(string key)
        {
            try
            {
                Console.WriteLine("Please Enter Your Video Title");
                string videoTitle = Console.ReadLine();
                // Set up the YouTube service
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = key// Replace with your API key
                   
                });

                // Create a request to search for videos based on the video title
                var searchRequest = youtubeService.Search.List("snippet");
                searchRequest.Q = videoTitle;
                searchRequest.Type = "video";
                searchRequest.MaxResults = 50;

                // Execute the search request
                var searchResponse = searchRequest.Execute();

                // Extract the video tags from the search results
                var videoTags = new List<string>();
                foreach (var searchResult in searchResponse.Items)
                {
                    // Get the video details
                    var videoId = searchResult.Id.VideoId;
                    var videoRequest = youtubeService.Videos.List("snippet");
                    videoRequest.Id = videoId;
                    var videoResponse = videoRequest.Execute();

                    // Extract the tags from the video details
                    var tags = videoResponse.Items[0].Snippet.Tags;
                    if (tags != null)
                    {
                        videoTags.AddRange(tags);
                    }
                }
                Console.WriteLine("Tags for your video are:- ");
                Console.WriteLine();
                foreach (var tag in videoTags)
                {
                    Console.WriteLine(tag);
                }
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        //working
        private static void YouTubeThumbnailDownload(string key)
        {
            try
            {
                // Set up the API service.
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = key // Replace with your API key.
                });

                // Specify the video ID for which you want to retrieve tags.
                string videoId = "Y6Vq3tdQsp4";

                // Define the request to retrieve video details.
                var videoRequest = youtubeService.Videos.List("snippet");
                videoRequest.Id = videoId;

                // Execute the request and retrieve the video details.
                var videoResponse = videoRequest.Execute();

                // Extract the thumbnail URL from the video response.
                string thumbnailUrl = videoResponse.Items[0].Snippet.Thumbnails.Maxres?.Url;

                // If "maxres" thumbnail is not available, fall back to "high" thumbnail.
                if (thumbnailUrl == null)
                {
                    thumbnailUrl = videoResponse.Items[0].Snippet.Thumbnails.High?.Url;
                }

                // If "high" thumbnail is not available, fall back to "default" thumbnail.
                if (thumbnailUrl == null)
                {
                    thumbnailUrl = videoResponse.Items[0].Snippet.Thumbnails.Default__?.Url;
                }
                string savePath = @"D:/youtube/thumbnail.jpg";
                // Download the thumbnail.
                using (var client = new WebClient())
                {
                    client.DownloadFile(thumbnailUrl, savePath); // Specify the desired file name for the downloaded thumbnail.
                }
                Console.WriteLine("Thumbnail downloaded successfully");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        //working
        public static void GetChannelId(string key)
        {
            Console.WriteLine("Enter Your Channel Name:- ");
            string channelName = Console.ReadLine();
            // Set up the YouTube service
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = key // Replace with your API key
               
            });

            // Create a request to search for channels based on the channel name
            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = channelName;
            searchRequest.Type = "channel";
            searchRequest.MaxResults = 1;

            // Execute the search request
            var searchResponse = searchRequest.Execute();

            // Extract the channel ID from the search results
            var channelId = searchResponse.Items[0].Id.ChannelId;
            Console.WriteLine("Your channel Id:- "+channelId);
            Console.ReadLine();
        }

        public static void GetWhatsappChat()
        {
            try
            {
                //phone no id:- 112303521902889
                // business acount id:- 106740289135432
                //sitesh business id: 100928133055691  239147455532217
                //whatsapp business id: 100928133055691 //106740289135432
                var httpClient = new HttpClient();
                var endpoint = "https://api.whatsapp.com/v1/chats";
                var clientId = "737957195001452";
                var clientToken = "EAAKfKwx3rmwBAMF9hQfe8jgZCSYTmw7menLOB4WAqyq9msxBiZA0OftuZBjbhZBkCkUZCbkbImZBIj3IVUV39vyIabr6T8xsnyuWZBEZACcYFDEAPXk7u65yX6WT4FB51LnJAZCgNH1dkcwyFb4bJLn34UnFNZBsSTPEbZBWDOktVBt4vYRDjjedMmyxkeMTMpzGYbMF5wpjfBLigZDZD";

                httpClient.DefaultRequestHeaders.Add("X-WM-CLIENT-ID", clientId);
                httpClient.DefaultRequestHeaders.Add("X-WM-CLIENT-TOKEN", clientToken);

                HttpResponseMessage response = httpClient.GetAsync(endpoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseBody);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Failed to retrieve chats. Status code: " + response.StatusCode);
                    Console.ReadLine();
                }

                httpClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void UploadYoutubeVideo()
        {
            try
            {
                UserCredential credential;

                using (var stream = new FileStream(@"D:/youtube/client_secret_876878268528-vtan8duikf27e7c7bh7sfmd0vhfgq1hl.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { YouTubeService.Scope.YoutubeUpload },
                        "user",
                        CancellationToken.None,
                        new FileDataStore("YouTube.Auth")).Result;
                }

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "YouTubeUploader"
                });

                var video = new Google.Apis.YouTube.v3.Data.Video();
                video.Snippet = new VideoSnippet();
                video.Snippet.Title = "My First Video";
                video.Snippet.Description = "My Video Description";
                video.Snippet.Tags = new string[] { "tag1", "tag2" };
                video.Status = new VideoStatus();
                video.Status.PrivacyStatus = "public";

                var filePath = @"D:/youtube/video.mp4";
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    videosInsertRequest.Upload();
                }
                Console.WriteLine("Video uploaded successfully!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static readonly char[] SpecialCharacters = "@#$&*".ToCharArray();

        public static void GeneratePassword()
        {

            Console.WriteLine("Enter your length of password:");
            int length = int.Parse( Console.ReadLine());

            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var validCharCount = validChars.Length;

            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                var chars = new char[length];
                var specialCharCount = SpecialCharacters.Length;

                for (int i = 0; i < length; i++)
                {
                    if (i % 3 == 0)
                    {
                        chars[i] = SpecialCharacters[randomBytes[i] % specialCharCount];
                    }
                    else
                    {
                        chars[i] = validChars[randomBytes[i] % validCharCount];
                    }
                }

                var pass= new string(chars);
                Console.WriteLine("Your Random Password is:" + pass);
                Console.ReadKey();
            }
        }

        public static void ScheduleVideo()
        {
            try
            {

                string apiKey = "YOUR_API_KEY";
                string broadcastTitle = "Your Broadcast Title";
                DateTime scheduledStartTime = DateTime.Now.AddHours(2); //new DateTime(2023, 7, 10, 12, 0, 0);

                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                });

                LiveBroadcast broadcast = new LiveBroadcast
                {
                    Snippet = new LiveBroadcastSnippet
                    {
                        Title = broadcastTitle,
                        ScheduledStartTime = scheduledStartTime,
                    },
                    Status = new LiveBroadcastStatus
                    {
                        PrivacyStatus = "private", // Set this to "public" if you want the broadcast to be public
                    },
                };

                var broadcastInsertRequest = youtubeService.LiveBroadcasts.Insert(broadcast, "snippet,status");
                var broadcastInsertResponse = broadcastInsertRequest.Execute();

                if (broadcastInsertResponse != null)
                {
                    Console.WriteLine("Broadcast scheduled successfully! Broadcast ID: " + broadcastInsertResponse.Id);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Broadcast scheduling failed.");
                    Console.ReadLine();
                }

                Console.ReadLine();


                //UserCredential credential;
                //using (var stream = new FileStream("path/to/client_secrets.json", FileMode.Open, FileAccess.Read))
                //{
                //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //        GoogleClientSecrets.Load(stream).Secrets,
                //        new[] { YouTubeService.Scope.Youtube },
                //        "user",
                //        CancellationToken.None,
                //        new FileDataStore("path/to/tokens")
                //    ).Result;
                //}

                //// Create the YouTube service
                //var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                //{
                //    HttpClientInitializer = credential,
                //    ApplicationName = "Your Application Name"
                //});
                //var video = new Video();
                //video.Snippet = new VideoSnippet();
                //video.Snippet.Title = "Your video title";
                //video.Snippet.Description = "Your video description";
                //video.Snippet.Tags = new string[] { "tag1", "tag2", "tag3" };
                //video.Status = new VideoStatus();
                //video.Status.PrivacyStatus = "private"; // "private", "public", or "unlisted"
                //video.Status.PublishAt = DateTime.Now.AddHours(1); // Specify the scheduled date and time in ISO 8601 format
                //var videoInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", File.OpenRead("path/to/video/file"), "video/*");
                //videoInsertRequest.Upload();

                //// Wait for the upload process to complete
                //while (videoInsertRequest.ResponseBody.ProcessingDetails.ProcessingStatus.UploadStatus != "processed")
                //{
                //    Thread.Sleep(1000);
                //}

                //// Retrieve the scheduled video ID
                //var scheduledVideoId = videoInsertRequest.ResponseBody.Id;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        public static void RandomNumber()
        {
            try
            {
                // Create a new instance of the Random class
                System.Random random = new System.Random();
                Console.WriteLine("Enter your range");
                int rndm = int.Parse(Console.ReadLine());
                // Generate a random number between 1 and 100 (inclusive)
                int randomNumber = random.Next(1, rndm);

                Console.WriteLine("Random number: " + randomNumber);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void RandomWord()
        {
            try
            {
                // Define the alphabet
                string alphabet = "abcdefghijklmnopqrstuvwxyz";
                // Create an instance of the random number generator
                System.Random random = new System.Random();
                Console.WriteLine("Enter your word length");
                // Generate a random word of length 5
                int wordLength =int.Parse(Console.ReadLine());
                StringBuilder wordBuilder = new StringBuilder(wordLength);
                for (int i = 0; i < wordLength; i++)
                {
                    int randomIndex = random.Next(0, alphabet.Length);
                    char randomLetter = alphabet[randomIndex];
                    wordBuilder.Append(randomLetter);
                }
                string randomWord = wordBuilder.ToString();
                Console.WriteLine("Your random word is: "+randomWord);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void RandomName()
        {
            try
            {

                System.Random random = new System.Random();

                string alphabet = "abcdefghijklmnopqrstuvwxyz";

                Console.WriteLine("Enter the number of names to generate");
                int nameCount = int.Parse(Console.ReadLine());

                string randomNames = "";

                for (int i = 0; i < nameCount; i++)
                {
                    int nameLength = random.Next(4, 8); // Generate a random name length between 4 and 7 letters

                    string name = "";

                    for (int j = 0; j < nameLength; j++)
                    {
                        int randomIndex = random.Next(alphabet.Length);
                        char letter = alphabet[randomIndex];
                        name += letter;
                    }

                    // Capitalize the first letter of the name
                    name = char.ToUpper(name[0]) + name.Substring(1);

                    randomNames += name + ", ";
                }

                randomNames = randomNames.TrimEnd(',', ' ');
                Console.WriteLine("Random Indian boy names: " + randomNames);
                Console.ReadLine();



                //System.Random random = new System.Random();
                //// Define the alphabet
                //string characters = "abcdefghijklmnopqrstuvwxyz";
                //Console.WriteLine("Enter your name count");
                //// Generate a random word of length 5
                //int wordCount = int.Parse(Console.ReadLine());
                //string randomName = "";

                //for (int i = 0; i < wordCount; i++)
                //{
                //    int wordLength = random.Next(3, 8); // Generate a random word length between 3 and 7 characters
                //    string word = "";

                //    for (int j = 0; j < wordLength; j++)
                //    {
                //        int randomIndex = random.Next(characters.Length);
                //        word += characters[randomIndex];
                //    }

                //    randomName += char.ToUpper(word[0]) + word.Substring(1) + " ";
                //}

                //randomName.Trim();
                //Console.WriteLine("Your random name is: " + randomName);
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void RandomNameGenerate()
        {
            try
            {
                System.Random random = new System.Random();
                Console.Write("Enter the starting word for the name: ");
                string startingWord = Console.ReadLine();
                Console.Write("Enter the numbers of the name: ");
                int count =int.Parse( Console.ReadLine());  
                while (count>0)
                {
                    string randomName = GenerateRandomName(startingWord, random);
                    Console.WriteLine(randomName);
                    count--;
                }
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static string GenerateRandomName(string startingWord, System.Random random)
        {
            string vowels = "aeiou";
            string consonants = "bcdfghjklmnpqrstvwxyz";

            int length = random.Next(4, 10); // Random name length between 4 and 9 characters
            string name = startingWord.ToLower();

            // Generate the rest of the name randomly
            while (name.Length < length)
            {
                string characterSet = (name.Length % 2 == 0) ? consonants : vowels;
                name += characterSet[random.Next(characterSet.Length)];
            }

            return char.ToUpper(name[0]) + name.Substring(1);
        }

        public static void RandomCountryNameGenerate()
        {
            try
            {
                Console.Write("Enter the number of random country names to generate: ");
                int count = int.Parse(Console.ReadLine());

                List<string> countryNames = GetCountryNames();
                List<string> randomCountryNames = GetRandomCountryNames(countryNames, count);
                Console.WriteLine("Your random countries: ");
                foreach (string countryName in randomCountryNames)
                {
                    Console.WriteLine(countryName);
                }
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static List<string> GetCountryNames()
        {
            List<string> countryNames = new List<string>
        {
            "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antigua and Barbuda", "Argentina", "Armenia", "Australia",
            "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin",
            "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
            "Cabo Verde", "Cambodia", "Cameroon", "Canada", "Central African Republic", "Chad", "Chile", "China", "Colombia",
            "Comoros", "Congo", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica",
            "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia",
            "Eswatini", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece",
            "Grenada", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India",
            "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya",
            "Kiribati", "Korea, North", "Korea, South", "Kosovo", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon",
            "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Madagascar", "Malawi", "Malaysia",
            "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova",
            "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands",
            "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Macedonia", "Norway", "Oman", "Pakistan", "Palau", "Panama",
            "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Qatar", "Romania", "Russia", "Rwanda",
            "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino",
            "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore",
            "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Sudan", "Spain", "Sri Lanka",
            "Sudan", "Suriname", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Togo",
            "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Tuvalu", "Uganda", "Ukraine",
            "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City",
            "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe"
        };

            return countryNames;
        }

        static List<string> GetRandomCountryNames(List<string> countryNames, int count)
        {
            System.Random random = new System.Random();
            List<string> randomCountryNames = new List<string>();

            for (int i = 0; i < count; i++)
            {
                int index = random.Next(countryNames.Count);
                string randomCountryName = countryNames[index];
                randomCountryNames.Add(randomCountryName);
            }

            return randomCountryNames;
        }

        public static void WordCountofText()
        {
            try
            {
                Console.WriteLine("Enter your text: ");
               // string text = "This is a sample text with #hashtags. Counting words and characters is important.";
                string text = Console.ReadLine();
                int wordCount = CountWords(text);
                int characterCount = CountCharacters(text);
                int hashtagCount = CountHashtags(text);

                Console.WriteLine("Word count: " + wordCount);
                Console.WriteLine("Character count: " + characterCount);
                Console.WriteLine("Hashtag count: " + hashtagCount);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static int CountWords(string text)
        {
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        static int CountCharacters(string text)
        {
            return text.Length;
        }

        static int CountHashtags(string text)
        {
            // Split the text into words
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Count the number of words starting with #
            int hashtagCount = words.Count(word => word.StartsWith("#"));

            return hashtagCount;
        }

        public static void CombinedWordofText()
        {
            try
            {
                Console.WriteLine("Enter the first text1:");
                string[] text1 = Console.ReadLine().Split();

                Console.WriteLine("Enter the second text2:");
                string[] text2 = Console.ReadLine().Split();

                foreach (string t1 in text1)
                {
                    foreach (string t2 in text2)
                    {
                        string combination = t1 + " " + t2;
                        Console.WriteLine(combination);
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void RandomWord1()
        {
            try
            {
                Console.Write("Enter the desired word length: ");
                int wordLength = int.Parse(Console.ReadLine());

                string randomWord = GenerateRandomWord(wordLength);
                Console.WriteLine("Random Word: " + randomWord);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static string GenerateRandomWord(int length)
        {
            string[] vowels = { "a", "e", "i", "o", "u" };
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

            var random = new System.Random();
            string randomWord = "";

            while (randomWord.Length < length)
            {
                if (randomWord.Length % 2 == 0)
                {
                    randomWord += consonants[random.Next(consonants.Length)];
                }
                else
                {
                    randomWord += vowels[random.Next(vowels.Length)];
                }
            }

            return randomWord;
        }

    }
}
