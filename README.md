MuzicBrainze
============

Impelmentation MuzicBrainze API 2.0 (C#)

Examples:

1. Get Artist by Id.

<code language="c#">

 static void Main(string[] args)
 {
   var artist = Hqub.MusicBrainze.API.Entities.Artist.Get("c3cceeed-3332-4cf0-8c4c-bbde425147b6");

   Console.WriteLine(artist.Name);
 }

</code>

2. Search artist by query.

<code>

 static void Main(string[] args)
 {
   var artists = Hqub.MusicBrainze.API.Entities.Artist.Search("scorpions");
   
   foreach(var artist in artists)
   {
      Console.WriteLine(artist.Name);
   }
   
   Console.ReadKey();
 }

</code>
 
