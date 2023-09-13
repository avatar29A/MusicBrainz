MusicBrainz
============

[![Build Status](https://img.shields.io/github/actions/workflow/status/avatar29A/MusicBrainz/dotnet.yml?style=flat-square)](https://github.com/avatar29A/MusicBrainz/actions/workflows/dotnet.yml)
[![Issues](https://img.shields.io/github/issues/avatar29A/MusicBrainz.svg?style=flat-square)](https://github.com/avatar29A/MusicBrainz/issues)
[![NuGet](https://img.shields.io/nuget/v/MusicBrainzAPI.svg?style=flat-square)](https://www.nuget.org/packages/MusicBrainzAPI)
[![Gitter](https://img.shields.io/gitter/room/avatar29A/MusicBrainz?color=%2346BC99&style=flat-square)](https://gitter.im/avatar29A/MusicBrainz)

Implementation of the [MusicBrainz](https://musicbrainz.org/) API version 2 for .NET

## Features

- First class MusicBrainz entities `Artist`, `ReleaseGroup`, `Release` and `Recording` supporting asynchronous `Get` (lookup by MBID), `Search` and `Browse`.
- Advanced `Search` using Lucene query syntax (see [search documentation](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search) for supported fields).
- Support for sub-queries in lookup requests (see [MusicBrainz wiki](https://wiki.musicbrainz.org/User:Nikki/ws/2) for a list of supported `inc` parameters).
- Limited support for entity relationships.

More information about the MusicBrainz webservice can be found [here](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2).

## Examples

Take a look at the [wiki](https://github.com/avatar29A/MusicBrainz/wiki) or the [Hqub.MusicBrainz.Client](https://github.com/avatar29A/MusicBrainz/tree/master/Hqub.MusicBrainz/Hqub.MusicBrainz.Client) example project.

## Breaking changes in v3

Version 3 removes all code that was marked obsolete in previous versions, specifically the static API and configuration. You should now use the [MusicBrainzClient](https://github.com/avatar29A/MusicBrainz/blob/master/src/Hqub.MusicBrainz/MusicBrainzClient.cs) class instead. Take a look at the wiki or the examples!

Additionally, the `.API` suffix was removed from the assembly and all namespaces. Fix the namespace change by removing the `.API` suffix in your using statements, for example `using Hqub.MusicBrainz.API` now becomes `using Hqub.MusicBrainz`. The name change of the assembly should be automatically picked up if you are using Nuget to manage package dependencies. Otherwise, you will have to manually fix the reference in your project files.
