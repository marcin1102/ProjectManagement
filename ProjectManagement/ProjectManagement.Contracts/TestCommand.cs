using System;
using Infrastructure.Message;

namespace ProjectManagement.Contracts
{
    public class TestCommand : ICommand
    {
        public TestCommand(int testValue)
        {
            TestValue = testValue;
        }

        public int TestValue { get; private set; }
    }
}
