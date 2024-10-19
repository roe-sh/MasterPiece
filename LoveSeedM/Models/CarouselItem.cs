using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class CarouselItem
{
    public long Id { get; set; }

    public string? BackgroundImage { get; set; }

    public string? Heading { get; set; }

    public string? Subheading { get; set; }

    public string? ButtonText { get; set; }

    public string? ButtonLink { get; set; }
}
