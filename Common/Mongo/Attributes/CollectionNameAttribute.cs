namespace GitArApi.Common.Mongo.Attributes;
public sealed class CollectionNameAttribute : Attribute
{
    public string Name { get; }

    public CollectionNameAttribute(string name)
    {
        Name = name;
    }
}
