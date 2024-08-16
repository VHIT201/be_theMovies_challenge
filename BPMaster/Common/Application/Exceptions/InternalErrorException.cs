using System.Net;


namespace Common.Application.Exceptions
{
  public class InternalErrorException : ApplicationException
  {
    private static readonly string _defaultErrorMsg = "An error occurred while processing your request.";
		public override HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;
		public InternalErrorException() : base(_defaultErrorMsg) { }
    public InternalErrorException(string message) : base(message) { }		
	}
}
