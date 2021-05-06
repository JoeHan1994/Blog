namespace Blog.JWT.Utility.APIResult
{
    public static class APIResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Result = new Result() { data = data,Total =0 },
                Msg = "Succeeded",
            };
        }
        public static ApiResult Success(dynamic data, int total)
        {
            return new ApiResult
            {
                Code = 200,
                Result = new Result() { data = data, Total = total },
                Msg = "Succeeded",
            };
        }
        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Result = new Result() { data = string.Empty, Total = 0 },
                Msg = msg,
            };
        }
    }
}
