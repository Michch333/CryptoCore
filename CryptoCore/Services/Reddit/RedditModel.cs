﻿using CryptoCore.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services.Reddit
{
    public class RedditModel
    {
        public string SubReddit { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string PermaLink { get; set; }
        public int Ups { get; set; }
        public int Downs { get; set; }

    }
}

