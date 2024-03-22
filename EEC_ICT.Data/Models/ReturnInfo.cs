namespace EEC_ICT.Data.Models
{
    public class ReturnInfo
    {
        public StatusReturn Status { get; set; }
        public PaginationInfo Pagination { get; set; }
        public object Data { get; set; }
    }

    public class PaginationInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
    }

    public class StatusReturn
    {
        public int Code { get; set; }//1:Success | -1: Error
        public string Message { get; set; }
    }
}