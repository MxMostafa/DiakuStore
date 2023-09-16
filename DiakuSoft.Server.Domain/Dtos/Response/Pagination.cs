namespace DiakuSoft.Server.Domain.Dtos.Response;
public class Pagination
{
    public Pagination(int currentPage,int pageSize,int totalCount)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
