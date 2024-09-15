using System.ComponentModel.DataAnnotations;

namespace haproco_backend_core.Entities
{
    public class TestTable
    {
        [Key]
        public int id { get; set; }

        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public string? department { get; set; }

        public decimal? salary { get; set; }
    }
}
