using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestions.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string InterviewQuestion { get; set; }
        public string InterviewAnswer { get; set; }
        
        public Interview() 
        { 
            
        }
    }
}
