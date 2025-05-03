using AppAbstract.Pagination;

namespace AppCommonTools.Linq;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, IPaginationModel paginationViewModel) =>
        queryable.Skip((paginationViewModel.Page - 1) * paginationViewModel.RecordsPerPage)
            .Take(paginationViewModel.RecordsPerPage);
}