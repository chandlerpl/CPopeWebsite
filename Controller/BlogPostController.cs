using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPopeWebsite.Data.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CPopeWebsite.Controller
{
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly BlogPostService _blogPostService;

        public BlogPostController(BlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        // GET: api/BlogPost
        [Route(Urls.BlogPosts)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_blogPostService.GetBlogPosts());
        }

        // GET: api/BlogPost/5
        [Route(Urls.BlogPost)]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var blogPost = _blogPostService.GetBlogPost(id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }

        // POST: api/BlogPost
        [Route(Urls.PostBlogPost)]
        [HttpPost]
        public IActionResult AddBlogPost([FromBody]BlogPost newBlogPost)
        {
            var savedBlogPost = _blogPostService.AddBlogPost(newBlogPost);

            if(savedBlogPost != null)
            {
                return Created(new Uri(Urls.BlogPost.Replace("{id}", savedBlogPost.Id.ToString()), UriKind.Relative), savedBlogPost);
            } else
            {
                return NotFound();
            }
        }

        // PUT: api/BlogPost/5
        [Route(Urls.UpdateBlogPost)]
        [HttpPut]
        public IActionResult Put(int id, [FromBody]BlogPost value)
        {
            if (_blogPostService.UpdateBlogPost(id, value))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route(Urls.DeleteBlogPost)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if(_blogPostService.DeleteBlogPost(id))
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
    }
}
