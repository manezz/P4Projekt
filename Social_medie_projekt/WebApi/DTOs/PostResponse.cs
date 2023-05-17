namespace WebApi.DTOs
{
    public class PostResponse
    {
        public int PostId { get; set; }

        public string? Title { get; set; }

        public string? Desc { get; set; }

        public DateTime Date { get; set; }

        public int? LikeUserId { get; set; }

        public PostPostLikesResponse PostLikes { get; set; } = null!;

        public PostUserResponse User { get; set; } = null!;

        public List<PostResponseTag>? Tags { get; set; }
    }

    public class PostPostLikesResponse
    {
        public int Likes { get; set; }
    }

    public class PostUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public PostUserUserImageResponse UserImage { get; set; } = null!;
    }

    public class PostUserUserImageResponse
    {
        public string Image { get; set; } = string.Empty;
    }

    public class PostResponseTag
    {
        public int TagId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}


