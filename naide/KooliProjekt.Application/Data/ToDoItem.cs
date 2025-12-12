using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public ToDoList ToDoList { get; set; }
        public int ToDoListId { get; set; }
    }
}