using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Resources;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.NameErrorMessage), ErrorMessageResourceType = typeof(MovieResources)), MaxLength(100)]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.GenreIdErrorMessage), ErrorMessageResourceType = typeof(MovieResources)),
            Display(Name = nameof(MovieResources.GenreId), ResourceType = typeof(MovieResources))]
        public int GenreId { get; set; }
        [Required(ErrorMessageResourceName = nameof(MovieResources.ReleaseDateNullErrorMessage),ErrorMessageResourceType =typeof(MovieResources)),
            Display(Name = nameof(MovieResources.ReleaseDate), ResourceType = typeof(MovieResources))]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = nameof(MovieResources.Added), ResourceType = typeof(MovieResources))]
        public DateTime Added { get; set; }
        [Required(ErrorMessageResourceName =nameof(MovieResources.StockNullErrorMessage), ErrorMessageResourceType =typeof(MovieResources)),
            Range(1,20,ErrorMessageResourceName = nameof(MovieResources.StockRangeErrorMessage), ErrorMessageResourceType = typeof(MovieResources)),
            Display(Name = nameof(MovieResources.Stock), ResourceType = typeof(MovieResources))]
        public int Stock { get; set; }
    }
}