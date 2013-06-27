MuzicBrainz
============

Implementation MuzicBrainze API 2.0 (C#)

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
 
Also support next entities:

- Release (Get, Search, Subqueries)
- Recording (Get, Search, Subqueries)

More information about MuzicBrainz Service [I here](http://musicbrainz.org/doc/XML_Web_Service/Version_2).
