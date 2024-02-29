using System;

namespace VariousExceptions
{
    [Serializable]
    public class ErrorConnectionException: Exception{
        public ErrorConnectionException():base(){}
        public ErrorConnectionException(String error):base(error){}
        public ErrorConnectionException(String error,Exception innerException):base(error, innerException){}
    }
}