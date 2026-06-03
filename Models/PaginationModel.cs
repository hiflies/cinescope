namespace CineScope.Models;

public class PaginationModel
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public string UrlTemplate { get; set; } = "";

    public int From => Math.Max((Page - 1) * Size + 1, 1);
    public int To => Math.Min(Page * Size, Count);
    public int PageCount => (int)Math.Ceiling(Count / (double)Size);

    public bool RenderLeftDots => PageCount > 9 && Page >= 5;
    public bool RenderRightDots => PageCount > 9 && Page <= PageCount - 4;

    public int PaginationStart => PageCount > 9 && Page >= 5 ? Page - 2 : 2;
    public int PaginationEnd => PageCount > 9 && Page <= PageCount - 4 ? Page + 2 : PageCount - 1;
}
