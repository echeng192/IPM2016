using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPM.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        public string ModuleId { set; get; }
        [MaxLength(50)]
        public string ModuleName { set; get; }
        [MaxLength(100)]
        public string ParentId { set; get; }
        public string ModuleType { get; set;}
        [MaxLength(100)]
        public string Url { set; get; }
        [MaxLength(50)]
        public string Controller { set; get; }
        public int SortOrder { set; get; }
        public string Remark { set; get; }
        public string CreatedBy { set; get; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime  CreatedDate { set; get; }

        public virtual ICollection<ModuleAction> Actions { set; get; }
        
    }
}