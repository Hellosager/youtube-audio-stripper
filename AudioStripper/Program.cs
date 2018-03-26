using System;
using System.IO;
using VideoLibrary;
using System.Diagnostics;

namespace AudioStripper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string FILENAME = Path.GetDirectoryName(args[0] + @"\");
                string TARGET_DIR = args[1] + @"\Musik\";
                string VIDEO_DIR = TARGET_DIR + @"Videos\";

                if (!Directory.Exists(TARGET_DIR))
                {
                    Directory.CreateDirectory(TARGET_DIR);
                }
                Directory.CreateDirectory(VIDEO_DIR);
                Console.WriteLine("Reading Youtube links from: " + FILENAME);

                var youtube = YouTube.Default;
                string[] videoURLs = File.ReadAllLines(FILENAME);

                foreach (string URL in videoURLs)
                {
                    Console.WriteLine("Processing URL: " + URL);
                    var video = youtube.GetVideo(URL);
                    string videoPath = VIDEO_DIR + video.FullName;
                    string musicPath = TARGET_DIR + video.Title + ".mp3";

                    Console.WriteLine("Downloading video to: " +  videoPath);
                    File.WriteAllBytes(videoPath, video.GetBytes());
                    Console.WriteLine("Downloaded video successful");
                    Console.WriteLine("Extract audio to: " + musicPath);

                    Process process = new Process();
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = "ffmpeg.exe";
                    process.StartInfo.Arguments = "-i \"" + videoPath + "\" -y \"" + musicPath + "\"";
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Console.WriteLine("ffmpeg: ---- " + output);
                    Console.WriteLine("Audio extraction successful!\nCleanup, deleting downloaded video...");
                    File.Delete(videoPath);
                    Console.WriteLine("Clean!");
                }

                Directory.Delete(VIDEO_DIR, true);
                Console.WriteLine("All audios have been extracted");

            }
            catch (Exception e)
            {

                Console.WriteLine("\nSomething went wrong.\n Use: dotnet AudioStripper.dll <pathToTextFile> <targetDirForMusic>\n");
                Console.WriteLine("<pathToTextFile> should only contain links to youtube videos, each link on its own line.\n");
                Console.WriteLine(e.Message);
                Console.ReadKey(true);
            }
        }
    }
}
