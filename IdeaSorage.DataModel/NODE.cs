//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdeaSorage.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class NODE
    {
        public NODE()
        {
            this.TAGSETS = new HashSet<TAGSET>();
        }
    
        public int NodeId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual USER USER { get; set; }
        public virtual ICollection<TAGSET> TAGSETS { get; set; }
    }
}
