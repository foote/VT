using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR.Client;


namespace BDF.VehicleTracker.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Which operation do you wish to perform?");
            Console.WriteLine("Connect to the Channel (c)");
            Console.WriteLine("Send a message to the Channel (s)");
            Console.WriteLine("Send a message to MyChannel (m)");
            Console.WriteLine("Get my Timeline (t)");
            Console.WriteLine("Update Status (u)");
            Console.WriteLine("Exit (x)");


            string operation = Console.ReadLine();

            while (operation != "x")
            {
                switch (operation)
                {
                    case "c":
                    case "connect":
                    case "Connect":
                        ConnectToChannel();
                        break;
                    case "s":
                    case "send":
                    case "Send":
                        //SendMessageToChannel();
                        break;
                    case "m":
                        //SendMessageToMyChannel();
                        break;
                    case "t":
                        Console.WriteLine("Getting my Timeline...");
                        GetTweets("https://api.twitter.com/1.1/statuses/user_timeline.json");
                        break;
                    case "u":
                        Console.WriteLine("Updating my status...");
                        UpdateStatus("https://api.twitter.com/1.1/statuses/update.json");
                        break;
                }



                Console.WriteLine("Which operation do you wish to perform?");
                Console.WriteLine("Connect to the Channel (c)");
                Console.WriteLine("Send a message to the Channel (s)");
                Console.WriteLine("Send a message to MyChannel (m)");
                Console.WriteLine("Get my Timeline (t)");
                Console.WriteLine("Update Status (u)");
                Console.WriteLine("Exit (x)");
                operation = Console.ReadLine();
                Console.Clear();

            }


        }

        static void UpdateStatus(string url)
        {
            //var oauth_token = "474900694-Kvly5CLRxQS6qN6c3ZyWVtx7J39EQnugus7y5osQ";
            //var oauth_token_secret = "HJoW9me4R9bXOJwQWxzX5gi7HHYGUqiaaFS7rYEKY5pgR";
            //var oauth_consumer_key = "XZaycvPKiCPWYbUkpeWdExOKh";
            //var oauth_consumer_secret = "2gD6wvSlGYlBEOcXOuiJC5KCFK492YFxLf3sDbSQbc1jFgGChA";

            var oauth_token = "474900694-P2UfvgFT1gbJeg0zMHiRxzKMrzP7904hUBpCSpkG";
            var oauth_token_secret = "2MuawqyG5QBaswRBEvL0C2AEM5zlbrHFvbtCmfpG34pDZ";
            var oauth_consumer_key = "yFsNABt5m5vGCbcU0pGTNpwtf";
            var oauth_consumer_secret = "7Pq9wjJW0bc92tKSDXxwbpgmmYgilDH0xuncQy8vksgdZkQGZs";


            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));


            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // message api details
            var status = "Breakin into Twitter from C-sharp - @xiongger21";
            var resource_url = "https://api.twitter.com/1.1/statuses/update.json";

            // create oauth signature
            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";

            var baseString = string.Format(baseFormat,
                                        oauth_consumer_key,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_token,
                                        oauth_version,
                                        Uri.EscapeDataString(status)
                                        );

            baseString = string.Concat("POST&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                    "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\", " +
                               "oauth_signature_method=\"{3}\", oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_version)
                                    );

            // make the request
            var postBody = "status=" + Uri.EscapeDataString(status);

            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";


            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            try
            {
                WebResponse response = request.GetResponse();
                StreamReader oSR = new StreamReader(response.GetResponseStream());
                var responseResult = oSR.ReadToEnd().ToString();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Twitter Post Error: " + ex.Message.ToString() + ", authHeader: " + authHeader);

            }
            Console.ReadLine();
        }

        static void GetTweets(string url)
        {
            var oauth_token = "474900694-P2UfvgFT1gbJeg0zMHiRxzKMrzP7904hUBpCSpkG";
            var oauth_token_secret = "2MuawqyG5QBaswRBEvL0C2AEM5zlbrHFvbtCmfpG34pDZ";
            var oauth_consumer_key = "yFsNABt5m5vGCbcU0pGTNpwtf";
            var oauth_consumer_secret = "7Pq9wjJW0bc92tKSDXxwbpgmmYgilDH0xuncQy8vksgdZkQGZs";


            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));


            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // message api details
            var resource_url = url;

            // create oauth signature
            var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}";

            var baseString = string.Format(baseFormat,
                                        oauth_consumer_key,
                                        oauth_nonce,
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_token,
                                        oauth_version);

            baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                    "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\", " +
                               "oauth_signature_method=\"{3}\", oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_version)
                                    );

            // make the request
            //var postBody = "screen_name=footeprint&count=5";  // user_timeline
            //postBody = "count=1";                             // retweets_of_me

            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";


            //using (Stream stream = request.GetRequestStream())
            //{
            //    byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
            //    stream.Write(content, 0, content.Length);
            //}

            try
            {
                //WebResponse response = request.GetResponse();
                //StreamReader oSR = new StreamReader(response.GetResponseStream());
                //var responseResult = oSR.ReadToEnd().ToString();

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                    //object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    //Response jsonResponse = objResponse as Response;
                    //return jsonResponse;

                    StreamReader oSR = new StreamReader(response.GetResponseStream());
                    var responseResult = oSR.ReadToEnd().ToString();
                    //Console.WriteLine(responseResult);

                    dynamic resp = JsonConvert.DeserializeObject(responseResult);

                    foreach (var data in resp)
                    {
                        Console.WriteLine(data.location + " " + data.text + " (" + data.created_at + ")");
                    }
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine("Twitter Post Error: " + ex.Message.ToString() + ", authHeader: " + authHeader);

            }
            Console.ReadLine();
        }

        //static void SendMessageToChannel()
        //{



        //    //Set the necessary variables
        //    //var hubConnection = new HubConnection("http://vehicletrackerapi.azurewebsites.net/");
        //    var hubConnection = new HubConnection("http://localhost:15584/");
        //    var myHub = hubConnection.CreateHubProxy("MyChannelHub");

        //    string name = "System";

        //    //Start the connection
        //    hubConnection.Start().ContinueWith(task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            //Connection failed. Bad connection
        //            Console.WriteLine("There was an error opening the connection: {0}", task.Exception.GetBaseException());
        //        }
        //        else
        //        {
        //            //Good connection, can send out messages and recieve them
        //            string message = "Hello!";

        //            myHub.On<string, string>("addMessage", (s1, s2) =>
        //            {
        //                Console.WriteLine(s1 + ":" + s2);
        //            });

        //            for (int i = 0; i < message.Length; i++)
        //            {
        //                //string test = new string(message.Take(i + 1).ToArray());
        //                //When we send a message from this console
        //                myHub.Invoke<string>("Send", name, message).ContinueWith(messageSentTask =>
        //                {
        //                    if (messageSentTask.IsFaulted)
        //                    {
        //                        //Message didn't send
        //                        Console.WriteLine("There was an error sending the message: {0}", messageSentTask.Exception.GetBaseException());
        //                    }
        //                    else
        //                    {
        //                        //Was good, write it ou one good
        //                        Console.WriteLine(messageSentTask.Result);
        //                    }
        //                });
        //            }
        //        }
        //    }).Wait();
        //}



        //static void SendMessageToMyChannel()
        //{
        //    //Set the necessary variables
        //    //var hubConnection = new HubConnection("https://vehicletrackerapi.azurewebsites.net/");
        //    var hubConnection = new HubConnection("http://localhost:51576/");
        //    var myHub = hubConnection.CreateHubProxy("MyChannelHub");

        //    string name = "Computer A";

        //    //Start the connection
        //    hubConnection.Start().ContinueWith(task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            //Connection failed. Bad connection
        //            Console.WriteLine("There was an error opening the connection: {0}", task.Exception.GetBaseException());
        //        }
        //        else
        //        {
        //            //Good connection, can send out messages and recieve them
        //            string message = "Hello from Computer A at " + DateTime.Now.ToLongTimeString();

        //            myHub.On<string, string>("addMessage", (s1, s2) =>
        //            {
        //                Console.WriteLine("Message from Host: " + DateTime.Now.ToLongTimeString() + " : " + s1 + ":" + s2);
        //            });

        //            Console.WriteLine("Attempting to send : " + message);

        //            //for (int i = 0; i < message.Length; i++)
        //            //{
        //            //string test = new string(message.Take(i + 1).ToArray());
        //            //When we send a message from this console
        //            myHub.Invoke<string>("Send", name, message).ContinueWith(messageSentTask =>
        //            {
        //                if (messageSentTask.IsFaulted)
        //                {
        //                    //Message didn't send
        //                    Console.WriteLine("There was an error sending the message: {0}", messageSentTask.Exception.GetBaseException());
        //                }
        //                else
        //                {
        //                    //Was good, write it ou one good
        //                    //Console.WriteLine("Message from Host:" + messageSentTask.Result);
        //                }
        //            });
        //            //}
        //        }
        //    }).Wait();
        //}

        private static void OnSend(string name, string message)
        {
            Console.WriteLine(name + ": " + message);
            Log(Color.Black, name + ": " + message);
        }

        private static void Log(Color color, string message)
        {
            Action callback = () =>
            {
                Console.WriteLine(message);
                //messagesList.Items.Add(new LogMessage(color, message));
            };

            //Invoke(callback);
        }

        private class LogMessage
        {
            public Color MessageColor { get; }

            public string Content { get; }

            public LogMessage(Color messageColor, string content)
            {
                MessageColor = messageColor;
                Content = content;
            }
        }

        static void ConnectToChannel()
        {

            HubConnection _connection = new HubConnectionBuilder()
               .WithUrl("http://66cba1c36e75.ngrok.io/VehicleHub")
               .Build();

            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));
            
            _connection.StartAsync();

            string message = DateTime.Now.ToString() + ": Connected...";
            Console.WriteLine(message);

            try
            {
                _connection.InvokeAsync("SendMessage", "ConsoleApp", message);
            }
            catch (Exception ex)
            {
                Log(Color.Red, ex.ToString());
            }


        }



    }
}
