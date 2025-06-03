using System;
using System.Collections.Generic;

namespace distribuidora.Models.DB;

public partial class Pedido
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
