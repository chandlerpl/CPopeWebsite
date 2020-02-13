using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.Blog
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Posted { get; set; }
        public string Post { get; set; }
        public string PostSummary
        {
            get
            {
                int sentence = Post.IndexOf('.');
                if (sentence != 0)
                    return Post.Substring(0, sentence + 1);

                if (Post.Length > 100)
                    return Post.Substring(0, 100);

                return Post;
            }
        }
    }
}