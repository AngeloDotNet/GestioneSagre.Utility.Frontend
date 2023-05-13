namespace GestioneSagre.Utility.Shared.Entities;

public class TipoScontrino : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}