using System.Diagnostics.CodeAnalysis;

namespace plantool.Domain.Entities;

public interface IAuditable
{
    public string Key { get; set; }
    public bool IsArchived { get; set; }
}

public class AuditableComparer : IEqualityComparer<IAuditable>
{
    public bool Equals(IAuditable? x, IAuditable? y)
    {
        if (x == null || y == null) return false;
        return x.Key == y.Key;
    }

    public int GetHashCode([DisallowNull] IAuditable obj)
    {
        return obj.Key.GetHashCode();
    }
}