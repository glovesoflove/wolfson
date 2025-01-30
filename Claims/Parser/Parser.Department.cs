using System.ComponentModel.DataAnnotations;

using CsvHelper.Configuration.Attributes;

using Claims;

namespace Claims.Parser
{
    public record Department
    {
        [Index(0)]
        public int? Id { get; set; }

        [Index(1)]
        public string? Title { get; set; }

        [Index(2)]
        public string? Color { get; set; }

        [Index(3)]
        public int? DepartmentParent_OID { get; set; }

        [Ignore]
        public int? NumDescendants { get; set; }

        [Ignore]
        public List<Department> Children { get; set; } = new List<Department>();
    }
}
