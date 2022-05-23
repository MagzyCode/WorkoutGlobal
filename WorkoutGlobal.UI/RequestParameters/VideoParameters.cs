namespace WorkoutGlobal.UI.RequestParameters
{
    public class VideoParameters : RequestParameters
    {
        public VideoParameters(int pageNumber = 1, int pageSize = 10)
            : base(pageNumber, pageSize)
        { }

        public override int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                _pageNumber =
                  value >= 1
                  ? value
                  : throw new ArgumentException("Page number cannot be less than 1.");
            }
        }
        public override int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value >= 1
                    ? value
                    : throw new ArgumentException("Page size cannot be less than 1.");
            }
        }
    }
}
