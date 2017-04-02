using System;
using System.Runtime.Serialization;

namespace SudokuCore {
    [Serializable]
    internal class InvalidCoordinatesException : Exception {
        public InvalidCoordinatesException() {
        }

        public InvalidCoordinatesException(string message) : base(message) {
        }

        public InvalidCoordinatesException(string message, Exception innerException) : base(message, innerException) {
        }

        protected InvalidCoordinatesException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}