using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Movie
{
    public class EditMovieDto
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        public long DirectorId { get; set; }

        public long CategoryId { get; set; }

        public long Duration { get; set; }

        public string Description { get; set; }
    }
}