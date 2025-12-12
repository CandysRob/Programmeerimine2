using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class ToDoList
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Title { get; set; }

        public IList<ToDoItem> Items { get; set; }
    }
}
