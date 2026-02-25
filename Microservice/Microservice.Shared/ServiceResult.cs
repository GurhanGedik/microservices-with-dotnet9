using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Microservice.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = "The requested resource was not found."
                }
            };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = (int)httpStatusCode
                }
            };
        }

        public static ServiceResult Error(string title, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = (int)httpStatusCode
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    StatusCode = exception.StatusCode
                };

            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult()
            {
                Fail = problemDetails,
                StatusCode = exception.StatusCode
            };

        }
        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = (int)HttpStatusCode.BadRequest

                }
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOK(T data)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = httpStatusCode,
                Fail = problemDetails
            };
        }

        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = (int)httpStatusCode
                }
            };
        }

        public new static ServiceResult<T> Error(string title, HttpStatusCode httpStatusCode)
        {
            return new ServiceResult<T>
            {
                StatusCode = httpStatusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = (int)httpStatusCode
                }
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    StatusCode = exception.StatusCode
                };

            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                StatusCode = exception.StatusCode
            };

        }
        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = (int)HttpStatusCode.BadRequest

                }
            };
        }
    }
}
