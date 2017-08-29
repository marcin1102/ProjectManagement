using Infrastructure.Message;

namespace ProjectManagement.Contracts
{
    public class TestQuery : IQuery<TestResponse>
    {
    }

    public class TestResponse
    {
        public TestResponse(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }
}
