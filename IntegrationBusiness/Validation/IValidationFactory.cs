using IntegrationDtos;

namespace IntegrationBusiness.Validation;

public interface IValidationFactory
{
    Response Validate<T>(T data) where T : class;
    Task<Response> ValidateAsync<T>(T data) where T : class;
}