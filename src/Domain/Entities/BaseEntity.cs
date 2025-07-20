using Maggsoft.Data.Mssql;

namespace MinimalAirbnb.Domain.Entities;

/// <summary>
/// Tüm entity'ler için temel sınıf
/// Maggsoft.Data.Mssql.BaseEntity'den inherit eder
/// </summary>
public abstract class BaseEntity : Maggsoft.Data.Mssql.BaseEntity
{
    /// <summary>
    /// Geri yükleme işlemi
    /// </summary>
    public virtual void Restore()
    {
        IsPublish = true;
        IsDeleted = false;
        ModifiedDate = DateTime.UtcNow;
    }
} 