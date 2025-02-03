namespace IntegrationDtos;

public class SuccessResponse : Response
{
    public new bool Success => base.Success;

    public SuccessResponse(string message = "Success") : base(true, message)
    {
    }
}

public class SuccessResponse<T> : Response<T>
{
    public new bool Success => base.Success;

    public SuccessResponse(string message = "Success") : base(true, message)
    {
    }
}