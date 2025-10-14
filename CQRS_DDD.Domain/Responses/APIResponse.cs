#pragma warning disable
namespace CQRS_DDD.Domain.Responses
{
    public class APIResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public void setErrorReponsePlain(string message)
        #region MyRegion
        {
            this.status = 1;
            this.message = message;
            this.data = null;
        }
        #endregion

        public void setSuccessResponse(string message, object data)
        #region MyRegion
        {
            this.status = 0;
            this.message = message;
            this.data = data;
        }
        #endregion

        public void setSuccessResponsePlain(string message)
        #region MyRegion
        {
            this.status = 0;
            this.message = message;
            this.data = null;
        }
        #endregion

        public bool isErrorResponse()
        #region MyRegion
        {
            return this.status == 1;
        } 
        #endregion

        public object getData()
        #region MyRegion
        {
            return this.data;
        } 
        #endregion
    }
}
