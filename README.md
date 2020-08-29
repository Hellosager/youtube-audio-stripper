# youtube-audio-stripper
C# dotnet implementation that reads youtube links from a textfile and extract the audio

This C# library can take a .txt-File with youtube links, and extract the audio of it. It's perfect for downloading music from youtube.

Get newest version here: https://github.com/Hellosager/youtube-audio-stripper/releases

After downloading AudioStripper.zip you have to unpack the 3 includes files into a folder. Then you can move into this folder on the command line and use AudioStripper like this:

<code>dotnet AudioStripper.dll \<PathToLinkTextFile\> \<DirectoryToStoreAudio\></code>

There are a lot of public solutions to download Youtube audio. I created this library to make it possible to download more audios in one go.

* no multithreading
* will fail for some Youtube videos with some special characters in it's name
* may fail in future if Youtube updates some of their js-functions
* downloads video first, but deletes it after extraction
* uses ffmpeg to extract audio from downloaded videos: https://www.ffmpeg.org/
* uses this fork of libvideo: https://github.com/omansak/libvideo/
