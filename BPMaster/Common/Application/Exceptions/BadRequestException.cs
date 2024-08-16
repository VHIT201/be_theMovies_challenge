using System.Net;

namespace Common.Application.Exceptions
{
	public class BadRequestException : ApplicationException
	{
		private static readonly string _defaultErrorMsg = "Bad Request";
		public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
		public BadRequestException() : base(_defaultErrorMsg) { }
		public BadRequestException(string message) : base(message) { }
	}
}
