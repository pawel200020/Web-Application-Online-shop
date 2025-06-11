namespace AppAbstract.Translations;

public interface ITranslation
{
    string Locale { get; }      
    string Value { get; }
}