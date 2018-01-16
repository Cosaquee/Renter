using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Movie
{
    public class MovieCoverDTO
    {
        [Required]
        public string CoverURL { get; set; }

        [Required]
        public long MovieID { get; set; }

    }
}