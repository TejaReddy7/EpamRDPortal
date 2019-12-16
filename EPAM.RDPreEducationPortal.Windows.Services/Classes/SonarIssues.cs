﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.Windows.Services.Classes
{
    public class SonarIssues
    {
        public int Total { get; set; }
        public int P { get; set; }
        public int Ps { get; set; }
        public Paging Paging { get; set; }
        public IList<Issue> Issues { get; set; }
        public IList<Component> Components { get; set; }
    }

    public class Paging
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }

    public class TextRange
    {
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }
    }

    public class Issue
    {
        public string Key { get; set; }
        public string Rule { get; set; }
        public string Severity { get; set; }
        public string Component { get; set; }
        public string Project { get; set; }
        public int Line { get; set; }
        public string Hash { get; set; }
        public TextRange TextRange { get; set; }
        public IList<object> Flows { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Effort { get; set; }
        public string Debt { get; set; }
        public string Author { get; set; }
        public IList<object> Tags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Type { get; set; }
        public string Organization { get; set; }
    }

    public class Component
    {
        public string Organization { get; set; }
        public string Key { get; set; }
        public string Uuid { get; set; }
        public bool Enabled { get; set; }
        public string Qualifier { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string Path { get; set; }
    }

}
