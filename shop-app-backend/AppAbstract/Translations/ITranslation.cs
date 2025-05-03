namespace AppAbstract;

public interface ITranslation
{
    string Locale { get; }      
    string Value { get; }
}