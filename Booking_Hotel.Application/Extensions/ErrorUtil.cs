using Booking_Hotel.Application.Const;
using Booking_Hotel.Utilities.Dtos;
using System;

namespace Booking_Hotel.Application.Extensions
{
    public static class ErrorUtil
    {
        public static OperationResult GetMessageError(this Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += " \n " + ex.InnerException.Message;
            }

            return new OperationResult { StatusCode = StatusCode.InternalServerError, Message = message, Success = false };

        }
    }
}
