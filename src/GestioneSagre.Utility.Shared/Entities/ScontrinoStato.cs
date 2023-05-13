namespace GestioneSagre.Utility.Shared.Entities;

public class ScontrinoStato : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}