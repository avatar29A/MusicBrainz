MusicBrainz
============

[![Build Status](https://img.shields.io/travis/avatar29A/MusicBrainz.svg?style=flat-square)](https://travis-ci.com/avatar29A/MusicBrainz)
[![NuGet](https://img.shields.io/nuget/v/MusicBrainzAPI.svg?style=flat-square)](https://www.nuget.org/packages/MusicBrainzAPI)
[![Issues](https://img.shields.io/github/issues/avatar29A/MusicBrainz.svg?style=flat-square)](https://github.com/avatar29A/MusicBrainz/issues)
[![Gitter](https://img.shields.io/gitter/room/avatar29A/MusicBrainz?color=%2346BC99&style=flat-square)](https://gitter.im/avatar29A/MusicBrainz)

Implementation of the [MusicBrainz](https://musicbrainz.org/) API version 2.0 (requires .NET framework 4.5 or above).

## Features

- First class MusicBrainz entities `Artist`, `ReleaseGroup`, `Release` and `Recording` supporting asynchronous `Get` (lookup by MBID), `Search` and `Browse`.
- Advanced `Search` using Lucene query syntax (see [search documentation](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search) for supported fields).
- Support for sub-queries in lookup requests (see [MusicBrainz wiki](https://wiki.musicbrainz.org/User:Nikki/ws/2) for a list of supported `inc` parameters).
- Limited support for entity relationships.

More information about the MusicBrainz webservice can be found [here](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2).

## Examples

Take a look at the [wiki](https://github.com/avatar29A/MusicBrainz/wiki) or the [Hqub.MusicBrainz.Client](https://github.com/avatar29A/MusicBrainz/tree/master/Hqub.MusicBrainz/Hqub.MusicBrainz.Client) example project.
