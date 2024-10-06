using Microsoft.AspNetCore.Mvc;

namespace SimpleBlog.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages, string actionName)
        {
            var model = new PaginationViewModel
            {
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ActionName = actionName
            };

            return View(model);
        }
    }
}

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string ActionName { get; set; }
}