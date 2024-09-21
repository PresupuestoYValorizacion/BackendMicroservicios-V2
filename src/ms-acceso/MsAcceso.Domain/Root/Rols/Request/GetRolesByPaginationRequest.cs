namespace MsAcceso.Domain.Root.Rols.Request;


public class GetRolesByPaginationRequest
{
    private const int MaxPageSize = 50;

    public int PageNumber {get;set;} = 1;

    private int _pageSize = 2;
    public int PageSize 
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? OrderBy {get;set;}   

    public bool OrderAsc {get;set;} = true;

    public string? Search {get;set;} 

    public bool IsAdmin {get;set;}
}