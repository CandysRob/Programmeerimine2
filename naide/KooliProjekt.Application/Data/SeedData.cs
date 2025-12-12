using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    /// <summary>
    /// 14.11.2025
    /// Testandmete generaator
    /// 
    /// Testandmed genereeritakse ainult siis kui mõni oluline 
    /// tabel on tühi.
    /// </summary>
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Genereerib andmed
        /// </summary>
        public void Generate()
        {
            if(_dbContext.ToDoLists.Any())
            {
                return;
            }

            for(var i = 0; i < 10; i++)
            {
                var list = new ToDoList { Title = "List " + (i + 1) };
                _dbContext.ToDoLists.Add(list);

                for(var j = 0; j < 5; j++)
                {
                    var item = new ToDoItem
                    {
                        Title = "Item " + (i + 1) + "." + (j + 1),
                        IsDone = false,
                        ToDoList = list
                    };
                    _dbContext.ToDoItems.Add(item);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
