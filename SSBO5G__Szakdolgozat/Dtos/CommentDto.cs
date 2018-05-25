using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CommentText { get; set; }
        public virtual HikerDto Author { get; set; }
    }
}
