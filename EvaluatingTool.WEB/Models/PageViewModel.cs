﻿namespace EvaluatingTool.WEB.Models
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int ResponseTime { get; set; }
        public int MaxTime { get; set; }
        public int MinTime { get; set; }
    }
}