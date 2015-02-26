using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.Resources
{
    public sealed class Constants
    {
        public const string ArtistQueryParams = "-area-beginarea-endarea-arid-artist-artistaccent-alias-begin-comment-country-end-ended-gender-ipi-sortname-tag-type-";
        public const string RecordingQueryParams = "-arid-artist-artistname-creditname-comment-country-date-dur-format-isrc-number-position-primarytype-puid-qdur-recording-recordingaccent-reid-release-rgid--rid-secondarytype-status-tid-tnum-tracks-tracksrelease-tag-type-video-";
        public const string ReleaseGroupQueryParams = "-arid-artist-artistname-comment-creditname-primarytype-rgid-releasegroup-releasegroupaccent-releases-release-reid-secondarytype-status-tag-type-";
        public const string ReleaseQueryParams = "-arid-artist-artistname-asin-barcode-catno-comment-country-creditname-date-discids-discidsmedium-format-laid-label-lang-mediums-primarytype-puid-quality-reid-release-releaseaccent-rgid-script-secondarytype-status-tag-tracks-tracksmedium-type-";
    }
}
