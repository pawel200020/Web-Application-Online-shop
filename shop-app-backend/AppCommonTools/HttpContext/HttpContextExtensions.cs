using System.Globalization;

namespace AppCommonTools.HttpContext;

public static class HttpContextExtensions
{
    public static void InsertParametersPaginationInHeader<T>(this Microsoft.AspNetCore.Http.HttpContext httpContext,
        IQueryable<T> queryable)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        double count = queryable.Count();
        httpContext.Response.Headers.Add("totalAmountOfRecords",count.ToString(CultureInfo.InvariantCulture));
    }
    public static void InsertParametersPaginationInHeader(this Microsoft.AspNetCore.Http.HttpContext httpContext, int count)
    {
        if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

        httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString(CultureInfo.InvariantCulture));
    }
}