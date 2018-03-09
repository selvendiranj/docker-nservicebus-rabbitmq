using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class DoSomething : ICommand
    {
        public string SomeProperty { get; set; }
    }

    public class DoSomethingElse : ICommand
    {
        public int SomeId { get; set; }
        public ChildClass ChildStuff { get; set; }
        public List<ChildClass> ListOfStuff { get; set; } = new List<ChildClass>();
    }

    public class ChildClass
    {
        public string SomeProperty { get; set; }
    }
}
