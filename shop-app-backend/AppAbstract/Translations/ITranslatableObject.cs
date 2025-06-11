namespace AppAbstract.Translations;

public interface ITranslatableObject
{
    public string OriginalValue { get; }
    public IEnumerable<ITranslation> Translations { get; }
}