using System.Net;

namespace Common.Application.Exceptions
{
	public class ValidationException : ApplicationException
	{
		protected override string ErrorCode { get; } = KMS.Common.Constants.ErrorCode.ValidationFailed;
		public ValidationException(string message) : base(message)
		{
		}

		public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
	}
}
