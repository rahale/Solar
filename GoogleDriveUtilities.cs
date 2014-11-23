﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;

namespace SolarFileReader
{
    class DriveCommandLineSample
    {
        public static void Test()// Main(string[] args)
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
     //               ClientId = "CLIENT_ID_HERE",
     //               ClientSecret = "CLIENT_SECRET_HERE",
      ClientId = "924792357693-5kfjij7s9lv2tk8oob28o6cpiugcos1h.apps.googleusercontent.com",
      ClientSecret =  "" //"kaM5POzhkyBUaAxykQcMiFop"
                },
                new[] { DriveService.Scope.Drive },
                "user",
                CancellationToken.None).Result;

            // Create the service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });

            File body = new File();
            body.Title = "My document";
            body.Description = "A test document";
            body.MimeType = "text/plain";

            byte[] byteArray = System.IO.File.ReadAllBytes("document.txt");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
            request.Upload();

            File file = request.ResponseBody;
                Console.WriteLine("File id: " + file.Id);
            Console.WriteLine("Press Enter to end this process.");
            Console.ReadLine();
        }
    }
}