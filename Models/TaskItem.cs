using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace To_Do_List_Web_Api.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace To_Do_List_Web_Api.Models
    {
        public class TaskItem
        {
            [Key]
            public int ID { get; set; }

            [Required]
            public string TaskName { get; set; }

            [Required]
            [Range(1, 2, ErrorMessage = "TaskStatus must be  1 or 2.")]
            public int TaskStatusID { get; set; }
        }

        public class TaskUpdate
        {
            [Required]
            public int ID { get; set; }
            [Required]
            public string TaskName { get; set; }
            [Required]
            [Range(1, 2, ErrorMessage = "TaskStatus must be  1 or 2.")]
            public int TaskStatusID { get; set; }
        }
    }


    public class Status
    {
        [Key]
        public int Status_ID { get; set; }

        [Required]
        public string StatusName { get; set; }
    }
}
