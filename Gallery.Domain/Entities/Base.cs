namespace Gallery.Domain.Entities;

public abstract class Base<T> where T : Base<T>
{
    public static readonly List<T> Items = new();

    public Guid Id { get; }

    protected Base()
    {
        Id = Guid.NewGuid();
        Items.Add((T)this);
    }
}