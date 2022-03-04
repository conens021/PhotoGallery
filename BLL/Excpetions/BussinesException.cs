namespace BLL.Excpetions
{
    public class BussinesException : Exception
    {
        public BussinesException() : base() { }

        public BussinesException(string message, int StatusCode) : base(message)
        {
            this.StatusCode = StatusCode;
        }

        public int StatusCode { get; set; }

    }
}
