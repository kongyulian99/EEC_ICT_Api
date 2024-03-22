namespace EEC_ICT.Data.Models
{
    public class FunctionWCommand : Function
    {
        public bool HasCreate { get; set; }
        public bool ValueCreate { get; set; } = false;
        public bool HasUpdate { get; set; }
        public bool ValueUpdate { get; set; } = false;
        public bool HasDelete { get; set; }
        public bool ValueDelete { get; set; } = false;
        public bool HasView { get; set; }
        public bool ValueView { get; set; } = false;
        public bool HasUpload { get; set; }
        public bool ValueUpload { get; set; } = false;
        public bool HasDownload { get; set; }
        public bool ValueDownload { get; set; } = false;
        public bool HasApprove { get; set; }
        public bool ValueApprove { get; set; } = false;
        //public bool HasUnApprove { get; set; }
        //public bool ValueUnApprove { get; set; } = false;
    }
}