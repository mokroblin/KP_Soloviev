﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KP_Soloviev.Models;

public partial class Rating
{
    public int RatingID { get; set; }

    public string RatingType { get; set; }

    public string NameOfList { get; set; }

    public string Criteria { get; set; }

    public DateOnly UpdateDate { get; set; }

    public virtual ICollection<Anime_Rating> Anime_Ratings { get; set; } = new List<Anime_Rating>();
}