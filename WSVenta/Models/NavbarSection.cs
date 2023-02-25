using System;
using System.Collections.Generic;

namespace WSVenta.Models;

public partial class NavbarSection
{
    public int Id { get; set; }

    public int NavbarId { get; set; }

    public int IsActive { get; set; }

    public string SectionName { get; set; } = null!;

    public string SectionUrl { get; set; } = null!;

    public virtual Navbar Navbar { get; set; } = null!;
}
