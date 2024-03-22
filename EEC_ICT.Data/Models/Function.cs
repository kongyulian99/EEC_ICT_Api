using System.Collections.Generic;

namespace EEC_ICT.Data.Models
{
    public class Function
    {
        public Function()
        {
            Children = new List<Function>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconCss { get; set; }
        public int SortOrder { get; set; }
        public bool Status { get; set; }
        public string ParentId { get; set; }
        public List<Function> Children { get; set; }
        
    }
}