using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Lambda.S3Events;
using Amazon.S3.Util;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace VanityNumberProject
{
    public class Function
    {
             
        IAmazonS3 S3Client { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            S3Client = new AmazonS3Client();
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }

        public async Task<string> S3FileReader(string file)
        {
            try
            {

                string bucketName = "amazon-connect-298ac3567b1e";
                //string awsAccessKey = "";
                //string awsSecretKey = "";
                //string folderPath = "/connect/kelseyshaaktest/" ;
                string filekey = "connect/kelseyshaaktest/dictionary/dictonary.txt";

                // Create a client
                AmazonS3Client client = new AmazonS3Client();

                // Create a GetObject request
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = filekey
                };

                // Issue request and remember to dispose of the response
                Console.WriteLine(request);
                var response = await client.GetObjectAsync(request);
                
                using (response)
                {
                    using StreamReader reader = new StreamReader(response.ResponseStream);
                    string contents = reader.ReadToEnd();
                    return contents;
                }

                // var response = await this.S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);
                // return response.Headers.ContentType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DatabaseHandler(int ID, string number, string vnumber)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tableName = "VanityNumbers";

            Table vanityNumber = Table.LoadTable(client, tableName);
            ID = GetMaxID();
            InsertTable(vanityNumber, ID, number, vnumber); 
        }

        public static void InsertTable(Table vanityNumber, int ID, string number, string vnumber)
        {
            var entry = new Document();
            entry["ID"] = ID;
            entry["number"] = number;
            entry["VanityNumber"] = vnumber;

            vanityNumber.PutItemAsync(entry);

        }

        public int GetMaxID()
        {
            AmazonDynamoDBClient Client = new AmazonDynamoDBClient();
            DynamoDBContext context = new DynamoDBContext(Client);
            var cancellationToken = new CancellationToken();
            var request = new ScanRequest("VanityNumbers") { Select = Select.COUNT };
            var result = Client.ScanAsync(request, cancellationToken).Result;

            int totalCount = result.Count;

            return totalCount;
        }

        public string FunctionHandler(string input, ILambdaContext context)
        {

            input = NumbersLogic(input);
            
            return input?.ToUpper();
        }

        public string NumbersLogic(string text)
        {
            /* string bucketName = "amazon-connect-298ac3567b1e";
             //string awsAccessKey = "";
             //string awsSecretKey = "";
             //string folderPath = "connect/kelseyshaaktest/";
             string encrypt = "SSE-S3";

             // Create a client
             AmazonS3Client client = new AmazonS3Client();

             // Create a GetObject request
             GetObjectRequest request = new GetObjectRequest
             {
                 BucketName = bucketName,
                 Key = encrypt
             };

             // Issue request and remember to dispose of the response
             var response = client.GetObjectAsync(request);
             using (response)
             {
                 using (var reader = new StreamReader(response.ResponseStream))
                 {
                     string contents = reader.ReadToEnd();
                 }
             }*/

            //Gives direction to users, reads in the input given
            //Console.WriteLine("Please enter your 7 digit phone number (exclude the area code): ");
            String contents = S3FileReader("dictionary.txt").Result;
            string number = text; //Console.ReadLine();

            //Reads the dictionary txt file and stores each word in the string array
            string[] dictonary = contents.Split('\n');
            //System.IO.File.ReadAllLines(@"C:\Users\Scar\source\repos\VanityNumberProject\dictonary.txt");
            //Creates dictionaries that will be used later
            Dictionary<string, string> patterns = new Dictionary<string, string>();
            Dictionary<string, string> dict2 = new Dictionary<string, string>();
            //This will store the top 5 results of the Vanity Numbers
            String[] topFive = new string[5];

            //Case Break statement that changes each word in the dictionary
            //into numbers based on the letters in the word
            //appends the numbers together in a string
            for (int i = 0; i < dictonary.Length; i++)
            {
                char[] word = dictonary[i].ToCharArray();
                String x = "";

                for (int j = 0; j < word.Length; j++)
                {
                    switch (word[j])
                    {
                        case 'a':
                        case 'b':
                        case 'c':
                            x += "2";
                            break;

                        case 'd':
                        case 'e':
                        case 'f':
                            x += "3";
                            break;

                        case 'g':
                        case 'h':
                        case 'i':
                            x += "4";
                            break;

                        case 'j':
                        case 'k':
                        case 'l':
                            x += "5";
                            break;

                        case 'm':
                        case 'n':
                        case 'o':
                            x += "6";
                            break;

                        case 'p':
                        case 'q':
                        case 'r':
                        case 's':
                            x += "7";
                            break;

                        case 't':
                        case 'u':
                        case 'v':
                            x += "8";
                            break;

                        case 'w':
                        case 'x':
                        case 'y':
                        case 'z':
                            x += "9";
                            break;
                    }
                }

                //stores the newly created string of numbers with the actual word
                // inside of the dictionary patterns
                patterns.Add(dictonary[i], x);
            }

            //Goes through each of the number strings in the patterns dictionary
            // and checks if the string is found in the phone number that was entered
            foreach (var value in patterns.Values)
            {
                //if the number string is found in the phone number then
                //it checks if the word is already in the second dictionary if it is
                // it moves to the next pattern, if it isn't it addes it to the second dictionary
                if (number.Contains(value))
                {
                    string key = patterns.FirstOrDefault(x => x.Value == value).Key;
                    bool dkey = dict2.ContainsKey(key);
                    if (!dkey)
                    {
                        dict2.Add(key, value);
                    }
                }
            }

            //orders the second dictionary by the key length
            //initalizes needed variables for future use
            dict2.OrderBy(i => i.Key.Length);
            string num2 = "";
            int z = 0;

            //goes through the second dictionary that houses the found patterns in the
            //phone number and replaces the pattern with the word in the phone number then inserts into the 
            //topFive array
            foreach (var value in dict2.Take(5))
            {
                foreach (var value2 in dict2.Values)
                {

                    string key = patterns.FirstOrDefault(x => x.Value == value2).Key;
                    num2 = number.Replace(value2, key);
                    topFive[z] = num2;
                    if (z == 4)
                    {
                        break;
                    }
                    z++;
                    // Console.WriteLine(num2);

                }
            }

            //Prints the top five results found
            Console.WriteLine("Array: ");
            string p = "";
            for (int i = 0; i < topFive.Length; i++)
            {
                //Console.WriteLine(topFive[i]);
                string u =  topFive[i].ToString();
                int x = GetMaxID() + 1;
                DatabaseHandler(x, number, u);
                p += u + " ";
               // return p;
            }
            return p; //reader.ReadToEnd();
        }
    }
}