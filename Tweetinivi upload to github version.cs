using System;
using Tweetinvi;
using Tweetinvi.Models;

namespace tweetinvi_Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await EventStream("BitCoin");
            

        }


        //streams
        public static async Task<Tweetinvi.Streaming.ISampleStream> SampleStream()
        {
            var sampleStream = UserClient().Streams.CreateSampleStream();
            sampleStream.TweetReceived += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Tweet);
            };

            await sampleStream.StartAsync();
            return sampleStream;
        }

        public static async Task<Tweetinvi.Streaming.IFilteredStream> EventStream(string filter)
        {
            var stream = UserClient().Streams.CreateFilteredStream();
            stream.AddTrack(filter);

            stream.MatchingTweetReceived += (sender, eventReceived) =>
            {
                Console.WriteLine(eventReceived.Tweet);
            };

            await stream.StartMatchingAnyConditionAsync();
            return stream;
        }




        public static TwitterClient UserClient()
        {
            var userClient = new TwitterClient(Tweet.Key, Tweet.Secret, Tweet.AToken, Tweet.AcsTknScrt);
            return userClient;

        }
        public static TwitterClient AppClient()
        {
            var appClient = new TwitterClient(Tweet.Key, Tweet.Secret);
            return appClient;
        }
        public static async Task<IAuthenticatedUser> User()
        {
            var user = await UserClient().Users.GetAuthenticatedUserAsync();
            Console.WriteLine(user);
            return user;
        }


        
        public static async Task<IUser> GetUser(string user)
        {
            // grabs a user per the string input
            var search = await UserClient().Users.GetUserAsync(user);
            Console.WriteLine(search);
            return search;
        }
        public static async Task<ITweet> PublishTweet(string text)
        { 
            var tweet = await UserClient().Tweets.PublishTweetAsync(text);
            Console.WriteLine($"Tweet ({text}) was published ");
            return tweet;

        }
        public static async Task<ITweet[]> UserTimeLine(string user)
        {
            // return a searched users time line
            var userTimeLineTweets = await UserClient().Timelines.GetUserTimelineAsync(user);
            foreach (var tweet in userTimeLineTweets)
            {
                Console.WriteLine(tweet);
            }
            return userTimeLineTweets;
        }



        

        //USer own actions
        public static async Task<ITweet[]> RetweetsOfMeTimeLine()
        {
            var retweetsOfMeTimeLine = await UserClient().Timelines.GetRetweetsOfMeTimelineAsync();
            foreach (var tweet in retweetsOfMeTimeLine)
            {
                Console.WriteLine(tweet);
            }
            return retweetsOfMeTimeLine;
        }
        public static async Task<ITweet[]> SelfMention()
        {
            var selfMentions = await UserClient().Timelines.GetMentionsTimelineAsync();
            foreach (var mention in selfMentions)
            {
                Console.Write(mention);
            }
            return selfMentions;
        }
        public static async Task<ITweet[]> HomeTimeLine()
        {
           var homeTimelineTweets = await UserClient().Timelines.GetHomeTimelineAsync();
            foreach (var tweet in homeTimelineTweets)
            {
                Console.WriteLine(tweet);
            }
            return homeTimelineTweets;
        }








        



    }
}