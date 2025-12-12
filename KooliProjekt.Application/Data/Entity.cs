using System;

namespace KooliProjekt.Application.Data
{
    // 28.11
    // Baasklass kõikidele klassidele, mille jaoks on
    // ApplicationDbContextis oma DbSet
    public abstract class Entity
    {
        public int Id { get; set; }
    }
}
