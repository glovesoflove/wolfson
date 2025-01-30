using Claims;
using Claims.Parser;

using CsvHelper;
using CsvHelper.Configuration;

using System.Globalization;

namespace Claims.Service
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(
          )
        {
        }

        public async Task<IEnumerable<Parser.Department>> Extract()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            try
            {
                using (var reader = new StreamReader("/home/b0ef/proj/2025-01-28.initech-wolf/Claims/department.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = await Task.Run(() => csv.GetRecords<Parser.Department>().ToList());
                    return records;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"CSV file not found: {ex.Message}");
                return new List<Parser.Department>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                return new List<Parser.Department>();
            }
            catch (CsvHelperException ex)
            {
                Console.WriteLine($"Error parsing CSV: {ex.Message}");
                return new List<Parser.Department>();
            }
        }


        public IEnumerable<Parser.Department> BuildHierarchy(IEnumerable<Parser.Department> flatList)
        {
            var lookup = flatList.Where(d => d.Id.HasValue).ToDictionary(d => d.Id!.Value);

            var rootDepartments = new List<Parser.Department>();

            foreach (var dept in flatList)
            {
                if (dept.DepartmentParent_OID.HasValue && lookup.TryGetValue(dept.DepartmentParent_OID.Value, out var parent))
                {
                    parent.Children.Add(dept);
                }
                else
                {
                    rootDepartments.Add(dept);
                }
            }

            CalculateNumDescendants(rootDepartments);
            return rootDepartments;
        }

        public async Task<IEnumerable<Parser.Department>> ExtractHierarchy()
        {
            var flatDepartments = await Extract();

            return BuildHierarchy(flatDepartments);
        }

        public void CalculateNumDescendants(IEnumerable<Parser.Department> departments)
        {
            foreach (var dept in departments)
            {
                dept.NumDescendants = CalculateDescendantsRecursive(dept);
            }
        }

        private int CalculateDescendantsRecursive(Parser.Department department)
        {
            int count = department.Children.Count;
            foreach (var child in department.Children)
            {
                count += CalculateDescendantsRecursive(child);
            }
            department.NumDescendants = count;
            return count;
        }
    }
}
