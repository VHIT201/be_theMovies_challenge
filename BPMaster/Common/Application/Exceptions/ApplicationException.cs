using System.Net;
using Common.Application.Models;


namespace Common.Application.Exceptions
{
	public abstract class ApplicationException : Exception
	{
		public abstract HttpStatusCode HttpStatusCode { get; }
		protected virtual string ErrorCode { get; } = string.Empty;
		protected ApplicationException(string message) : base(message)
		{ 
		}
		public ResponseModel GetErrorResponse()
		{
			return ResponseModel.Error(this.Message, HttpStatusCode, ErrorCode);
		}
	}
}
