MuzicBrainz
============

Implementation MuzicBrainze API 2.0 (C#)

[![Build Status](https://travis-ci.org/avatar29A/MusicBrainz.svg)](https://travis-ci.org/avatar29A/MTGO-Free)
[![NuGet](https://img.shields.io/nuget/dt/MusicBrainzAPI.svg)](https://www.nuget.org/packages/MusicBrainzAPI/1.0.0)

##Examples:

######Get Artist by Id.

```c#
 static void Main(string[] args)
 {
   var artist = Hqub.MusicBrainze.API.Entities.Artist.Get("c3cceeed-3332-4cf0-8c4c-bbde425147b6");

   Console.WriteLine(artist.Name);
 }
```

######Search artist by query.

```c#

 static void Main(string[] args)
 {
   var artists = Hqub.MusicBrainze.API.Entities.Artist.Search("scorpions");
   
   foreach(var artist in artists)
   {
      Console.WriteLine(artist.Name);
   }
   
   Console.ReadKey();
 }

```

###### Get Artist with Tags.

```c#
using Hqub.MusicBrainze.API.Entities;

static void Main(string[] args)
{
  var artist = Artist.Get("c3cceeed-3332-4cf0-8c4c-bbde425147b6", API.Entities.Include.ArtistIncludeEntityHelper.Tags);

  Console.WriteLine(artist.Name);
	
  foreach (var tag in artist.Tags)
  {
    Console.WriteLine("\t{0}", tag.Name);
  }

  Console.ReadKey();
}
```

###### Show Artist -> Album -> Tracks.

```c#
private static void Main(string[] args)
{
	try
	{
		var task = ShowAlbumTracks("The Rolling Stones", "Beggars Banquet");

		// The synchronous Wait() will wrap any exception in an AggregateException,
		// so be sure to catch it properly.
		task.Wait();
	}
	catch (AggregateException e)
	{
		Console.WriteLine(e.InnerException.Message);
	}

	Console.ReadKey();
}

private static async Task ShowAlbumTracks(string artistName, string albumName)
{
	var artist = (await Artist.SearchAsync(artistName)).First();

	Console.WriteLine(artist.Name);

	var query = string.Format("aid=({0}) release=({1})", artist.Id, albumName);
	var album = (await Release.SearchAsync(Uri.EscapeUriString(query), 10)).First();

	Console.WriteLine("\t{0}", album.Title);

	var release = await Release.GetAsync(album.Id, "recordings");

	int i = 1;

	foreach (var medium in release.MediumList)
	{
		foreach (var track in medium.Tracks)
		{
			var recording = track.Recordring;
			var length = TimeSpan.FromMilliseconds(recording.Length).ToString("m\\:ss");

			Console.WriteLine("\t\t{0,3:##} {1,6}  {2}", i++, length, recording.Title);
		}
	}
}
```
 
Also support next entities:

- Release (Get, Search, Subqueries)
- Recording (Get, Search, Subqueries)

More information about MuzicBrainz Service [I here](http://musicbrainz.org/doc/XML_Web_Service/Version_2).
