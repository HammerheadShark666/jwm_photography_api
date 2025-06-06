﻿using System.ComponentModel.DataAnnotations.Schema;

namespace jwm_photography_api.Domain;

[Table("JWM_PHOTO_Photo")]
public class Photo
{
    public long Id { get; set; }
    public required string FileName { get; set; }
    public string? Title { get; set; }
    public string? Camera { get; set; }
    public string? Lens { get; set; }
    public string? ExposureTime { get; set; }
    public string? ApertureValue { get; set; }
    public string? ExposureProgram { get; set; }
    public int? Iso { get; set; }
    public DateTime? DateTaken { get; set; }
    public string? FocalLength { get; set; }
    public int? Orientation { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
    public Boolean UseInMontage { get; set; }
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public int? PaletteId { get; set; }
    public Palette? Palette { get; set; }
    public List<Favourite> Favourites { get; set; } = [];
    public List<UserGalleryPhoto> UserGalleryPhotos { get; set; } = [];
}