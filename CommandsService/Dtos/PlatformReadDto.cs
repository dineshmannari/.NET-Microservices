using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos{
    public class PlatformReadDto{
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}