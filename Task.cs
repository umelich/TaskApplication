using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplication
{
    [Serializable]
    public class Task
    {
        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public string Description { get; set; }
    }
}
