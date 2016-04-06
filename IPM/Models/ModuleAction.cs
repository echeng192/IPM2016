using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace IPM.Models
{
    public class ModuleAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [MaxLength(50)]
        public string ActionName { set; get; }
        public ActionType ActionType { set; get; }
        [MaxLength(50)]
        public string SysAction { set; get; }
        public int SortOrder { set; get; }
        [MaxLength(50)]
        public string ModuleId { set; get; }
    }
    public enum ActionType
    {
        Javascript,
        Action,
        Other
    }
}