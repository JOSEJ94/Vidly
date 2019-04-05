using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Resources;

namespace Vidly.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.NameErrorMessage), ErrorMessageResourceType = typeof(MovieResources)), MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.GenreIdErrorMessage), ErrorMessageResourceType = typeof(MovieResources))]
        public int GenreId { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.ReleaseDateNullErrorMessage), ErrorMessageResourceType = typeof(MovieResources))]
        public DateTime ReleaseDate { get; set; }
        public DateTime Added { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.StockNullErrorMessage), ErrorMessageResourceType = typeof(MovieResources)),
            Range(1, 20, ErrorMessageResourceName = nameof(MovieResources.StockRangeErrorMessage), ErrorMessageResourceType = typeof(MovieResources))]
        public int Stock { get; set; }
    }
}