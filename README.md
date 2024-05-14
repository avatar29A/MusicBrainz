# MusicBrainz

[![Build Status](https://img.shields.io/github/actions/workflow/status/avatar29A/MusicBrainz/dotnet.yml?style=flat-square)](https://github.com/avatar29A/MusicBrainz/actions/workflows/dotnet.yml)
[![Issues](https://img.shields.io/github/issues/avatar29A/MusicBrainz.svg?style=flat-square)](https://github.com/avatar29A/MusicBrainz/issues)
[![NuGet](https://img.shields.io/nuget/v/MusicBrainzAPI.svg?style=flat-square)](https://www.nuget.org/packages/MusicBrainzAPI)
[![Gitter](https://img.shields.io/gitter/room/avatar29A/MusicBrainz?color=%2346BC99&style=flat-square)](https://gitter.im/avatar29A/MusicBrainz)

Implementation of the [MusicBrainz](https://musicbrainz.org/) API version 2 for .NET

## Features

- First class MusicBrainz entities `Artist`, `ReleaseGroup`, `Release` and `Recording` supporting asynchronous `Get` (lookup by MBID), `Search` and `Browse`.
- Advanced `Search` using Lucene query syntax (see [search documentation](https://musicbrainz.org/doc/MusicBrainz_API/Search) for supported fields).
- Support for sub-queries in lookup requests (see [MusicBrainz wiki](https://wiki.musicbrainz.org/User:Nikki/ws/2) for a list of supported `inc` parameters).
- Limited support for entity relationships.

More information about the MusicBrainz API can be found [here](https://musicbrainz.org/doc/MusicBrainz_API).

## Examples

Take a look at the [wiki](https://github.com/avatar29A/MusicBrainz/wiki) or the [Hqub.MusicBrainz.Client](https://github.com/avatar29A/MusicBrainz/tree/master/src/Hqub.MusicBrainz.Client) example project.
