using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales
{
    public class PaginatedListTests
    {
        [Fact]
        public void Create_WithValidInput_ShouldReturnCorrectPagination()
        {
            var items = Enumerable.Range(1, 15).ToList(); 
            var pageNumber = 2;
            var pageSize = 5;
            var totalCount = items.Count;

            var result = PaginatedList<int>.Create(items.AsQueryable(), pageNumber, pageSize);

            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(totalCount, result.TotalCount);
            Assert.Equal(3, result.TotalPages);
            Assert.True(result.HasPrevious);
            Assert.True(result.HasNext);
            Assert.Equal(5, result.Items.Count); 
            Assert.Equal(6, result.Items.First()); 
        }
    }
}
