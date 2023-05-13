namespace GestioneSagre.Utility.Shared.Entities;

public class TipoPagamento : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Value { get; set; }
}