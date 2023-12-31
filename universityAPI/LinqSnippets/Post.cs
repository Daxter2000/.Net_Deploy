﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public DateTime Created = DateTime.Now;
        public List<Comment>? Comments { get; set; } 
    }
}
