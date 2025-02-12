using AutoMapper;
using IntegrationApplication;
using IntegrationBusiness.Validation;
using IntegrationDtos;
using IntegrationDtos.Request;
using IntegrationDtos.Responses;
using IntegrationModels;

namespace IntegrationBusiness.Services.Impl;

public class PostServiceImpl : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidationFactory _validationFactory;

    public PostServiceImpl(IUnitOfWork unitOfWork, IMapper mapper, IValidationFactory validationFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationFactory = validationFactory;
    }

    public async Task<Response<PostResponse>> InsertNewPost(PostRequest postRequest)
    {
        var validate = await _validationFactory.ValidateAsync(postRequest);
        if (!validate.Success)
            return new Response<PostResponse>(false, validate.Message) { Data = null };

        var newPost = _mapper.Map<PostEntity>(postRequest);

        _unitOfWork.PostRepository.Insert(newPost);
        await _unitOfWork.SaveChangeAsync();

        return new Response<PostResponse>(true, "Post created successfully")
        {
            Data = _mapper.Map<PostResponse>(newPost)
        };
    }

    public async Task<Response<bool>> removePost(long id)
    {
        var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
        if (post == null)
        {
            return new Response<bool>(false, $"Post with ID {id} not found.") { Data = false };
        }

        _unitOfWork.PostRepository.Delete(post);
        var saved = await _unitOfWork.SaveChangeAsync();
        return new Response<bool>(saved, saved ? "Post deleted successfully" : "Failed to delete post")
            { Data = saved };
    }

    public async Task<Response<PostResponse>> FindById(long id)
    {
        var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
        if (post == null)
        {
            return new Response<PostResponse>(false, $"Could not find post with ID {id}");
        }

        var mappedPost = _mapper.Map<PostResponse>(post);
        return new Response<PostResponse>(true, "Post retrieved successfully") { Data = mappedPost };
    }

}