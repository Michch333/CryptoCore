using CryptoCore.Services.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models.ViewModels
{
    public class RedditViewModel
    {
        public RedditViewModel()
        {
            RedditPosts = new List<RedditModel>();
        }
        public List<RedditModel> RedditPosts { get; set; }
    }
}
