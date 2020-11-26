using System;

namespace api
{
    public class FillBlankModel
    {
        public string StudentId { get; set; }
        public string[] Answers { get; set; }
        public DateTime ArriveAt { get; set; }
    }
}