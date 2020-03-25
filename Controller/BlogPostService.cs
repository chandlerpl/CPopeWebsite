using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.Blog
{
    public class BlogPostService
    {
        private const string READALL = "SELECT * FROM [dbo].[BlogPosts];";
        private const string SETID = "SET IDENTITY_INSERT [dbo].[BlogPosts] ON";
        private const string INSERT = "INSERT INTO [dbo].[BlogPosts] (ID, Author, Title, Posted, Post, Publish) VALUES (@ID, @Author, @Title, @Posted, @Post, @Publish);";
        private const string UPDATE = "UPDATE [dbo].[BlogPosts] SET Title=@Title, Post=@Post WHERE ID=@ID;";
        private const string UPDATEPUBLISH = "UPDATE [dbo].[BlogPosts] SET Title=@Title, Post=@Post, Publish=@Publish, Posted=@Posted WHERE ID=@ID;";
        private const string DELETE = "DELETE FROM [dbo].[BlogPosts] WHERE ID=@ID;";

        private List<BlogPost> _blogPosts;
        private readonly SqlConnection sql;

        public BlogPostService(SqlConnection sql)
        {
            this.sql = sql;

            if (_blogPosts == null)
            {
                _blogPosts = new List<BlogPost>();

                using SqlCommand command = new SqlCommand(READALL, sql);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _blogPosts.Add(new BlogPost()
                    {
                        Id = reader.GetInt32(0),
                        Author = !reader.IsDBNull(1) ? reader.GetString(1) : "Missing Author Data",
                        Title = !reader.IsDBNull(2) ? reader.GetString(2) == "" ? "Title" : reader.GetString(2) : "Missing Title Data",
                        Posted = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Parse("1900-01-01T00:00:00:00.0000000"),
                        Post = !reader.IsDBNull(4) ? reader.GetString(4) : "Missing Post Data",
                        Publish = !reader.IsDBNull(5) ? reader.GetBoolean(5) : false,
                    });
                }
            }
        }

        public List<BlogPost> GetBlogPosts()
        {
            return _blogPosts;
        }

        public BlogPost GetBlogPost(int id)
        {
            return _blogPosts.SingleOrDefault(x => x.Id == id);
        }

        public BlogPost AddBlogPost(BlogPost newBlogPost)
        {
            if (_blogPosts.Count > 0)
                newBlogPost.Id = _blogPosts.Last().Id + 1;
            else
                newBlogPost.Id = 0;

            //using SqlCommand set = new SqlCommand(SETID, sql);
            //set.ExecuteNonQuery();

            using SqlCommand command = new SqlCommand(INSERT, sql);
            command.Parameters.AddWithValue("@ID", newBlogPost.Id);
            command.Parameters.AddWithValue("@Author", newBlogPost.Author);
            command.Parameters.AddWithValue("@Title", newBlogPost.Title);
            command.Parameters.AddWithValue("@Posted", newBlogPost.Posted);
            command.Parameters.AddWithValue("@Post", newBlogPost.Post);
            command.Parameters.AddWithValue("@Publish", newBlogPost.Publish);
            //command.ExecuteReader
            command.ExecuteNonQuery();

            _blogPosts.Add(newBlogPost);
            return newBlogPost;

        }

        public bool UpdateBlogPost(int postId, BlogPost updatedBlogPost)
        {
            try
            {
                var originalBlogPost = _blogPosts.Find(x => x.Id == postId);

                if(originalBlogPost.Publish == updatedBlogPost.Publish)
                {
                    using SqlCommand command = new SqlCommand(UPDATE, sql);
                    command.Parameters.AddWithValue("@ID", postId);
                    command.Parameters.AddWithValue("@Title", updatedBlogPost.Title);
                    command.Parameters.AddWithValue("@Post", updatedBlogPost.Post);
                    command.ExecuteNonQuery();

                    originalBlogPost.Post = updatedBlogPost.Post;
                    originalBlogPost.Title = updatedBlogPost.Title;
                } else
                {
                    DateTime dtNow = DateTime.UtcNow;
                    using SqlCommand command = new SqlCommand(UPDATEPUBLISH, sql);
                    command.Parameters.AddWithValue("@ID", postId);
                    command.Parameters.AddWithValue("@Title", updatedBlogPost.Title);
                    command.Parameters.AddWithValue("@Post", updatedBlogPost.Post);
                    command.Parameters.AddWithValue("@Posted", dtNow);
                    command.Parameters.AddWithValue("@Publish", updatedBlogPost.Publish);
                    command.ExecuteNonQuery();

                    originalBlogPost.Post = updatedBlogPost.Post;
                    originalBlogPost.Title = updatedBlogPost.Title;
                    originalBlogPost.Posted = dtNow;
                    originalBlogPost.Publish = updatedBlogPost.Publish;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBlogPost(int postId)
        {
            try
            {
                var blogPost = _blogPosts.Find(x => x.Id == postId);

                using SqlCommand command = new SqlCommand(DELETE, sql);
                command.Parameters.AddWithValue("@ID", postId);
                command.ExecuteNonQuery();

                _blogPosts.Remove(blogPost);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
