using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Controller
{
    public static class Urls
    {
        public const string BlogPosts = "api/blogposts";
        public const string BlogPost = "api/blogposts/{id}";
        public const string PostBlogPost = "api/blogposts";
        public const string UpdateBlogPost = "api/blogposts/{id}";
        public const string DeleteBlogPost = "api/blogposts/{id}";

        public const string StaticImage = "api/staticsimplexnoise";
        public const string UploadImage = "api/uploadimage";
    }
}
