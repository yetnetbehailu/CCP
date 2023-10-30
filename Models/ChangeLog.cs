using CCP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCP.Models
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public DateTime ChangeTime { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public CCPUser User { get; set; }
        public string ChangeType { get; set; } //Create, edit, delete
        public string ModelName { get; set; } //Dog, kennel, breeder
        public string? OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
