using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos.Movie
{
    public class EditMovieDto
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        public long DirectorId { get; set; }
        public long CategoryId { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }
}
