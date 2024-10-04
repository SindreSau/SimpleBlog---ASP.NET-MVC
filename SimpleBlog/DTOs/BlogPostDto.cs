namespace SimpleBlog.DTOs;

public class BlogPostInputDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;
}

public class BlogPostOutputDto
{
    public int BlogPostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}