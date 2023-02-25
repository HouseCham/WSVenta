using System;
using System.Collections.Generic;

namespace WSVenta.Models;

public partial class Navbar
{
    public int Id { get; set; }

    public string Project { get; set; } = null!;

    public virtual ICollection<NavbarSection> NavbarSections { get; } = new List<NavbarSection>();
}
