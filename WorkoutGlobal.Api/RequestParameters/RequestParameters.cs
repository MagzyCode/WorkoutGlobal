namespace WorkoutGlobal.Api.RequestParameters
{
    public abstract class RequestParameters
    {
        private protected int _pageNumber;
        private protected int _pageSize;

        public RequestParameters(int pageNumber, int pageSize)
        { 
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public abstract int PageNumber { get; set; }
        public abstract int PageSize { get; set; }
    }
}
