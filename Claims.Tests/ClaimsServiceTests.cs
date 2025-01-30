using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace Claims.Service.Tests
{
    public class ClaimsServiceTests
    {
        private readonly ClaimsService _claimsService;

        public ClaimsServiceTests()
        {
            _claimsService = new ClaimsService();
        }

        [Fact]
        public void BuildHierarchy_EmptyList_ReturnsEmptyList()
        {
            var result = _claimsService.BuildHierarchy(new List<Parser.Department>());
            Assert.Empty(result);
        }

        [Fact]
        public void BuildHierarchy_FlatList_ReturnsCorrectHierarchy()
        {
            var departments = new List<Parser.Department>
            {
                new Parser.Department { Id = 1, DepartmentParent_OID = null },
                new Parser.Department { Id = 2, DepartmentParent_OID = 1 },
                new Parser.Department { Id = 3, DepartmentParent_OID = 1 },
                new Parser.Department { Id = 4, DepartmentParent_OID = 2 }
            };

            var result = _claimsService.BuildHierarchy(departments).ToList();

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[0].Children.Count);
            Assert.Single(result[0].Children.First(c => c.Id == 2).Children);
        }
    }
}
