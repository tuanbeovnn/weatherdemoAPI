using IntegrationDtos;
using IntegrationDtos.Request;
using IntegrationDtos.Responses;

namespace IntegrationBusiness.Services;

public interface IPostService
{
    Task<Response<PostResponse>> InsertNewPost(PostRequest postRequest);
}