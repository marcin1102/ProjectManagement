using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts;

namespace ProjectManagement
{
    public class TestQueryHandler : IAsyncQueryHandler<TestQuery, TestResponse>
    {
        public TestQueryHandler()
        {

        }

        public async Task<TestResponse> HandleAsync(TestQuery query)
        {
            return new TestResponse(9999);
        }
    }
}
