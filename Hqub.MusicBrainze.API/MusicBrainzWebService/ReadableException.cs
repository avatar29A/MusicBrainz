using System;
using System.Collections.Generic;
using System.Text;

namespace Hqub.MusicBrainz.API
{
    /// <summary>
    /// This exception class can be used to display message in Windows Service Apps
    /// ShowMessageAsync requires a tile and description
    /// </summary>
    public class ReadableException : Exception
    {
        protected string _errorMessage;
        protected string _errorDescription;

        public ReadableException() { }

        public ReadableException(string message, string description)
            : base(message + ": " + description)
        {
            _errorMessage = message;
            _errorDescription = description;
        }

        public ReadableException(string message, string description, Exception inner)
            : base(message + ": " + description, inner)
        {
            _errorMessage = message;
            _errorDescription = description;
        }

        public string Error { get { return _errorMessage; } }
        public string Description { get { return _errorDescription; } }
    }
}