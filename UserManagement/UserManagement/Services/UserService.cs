using Grpc.Core;

namespace UserManagement.Services
{
    public class UserService : UserServiceGrpc.UserServiceGrpcBase
    {
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public override Task<GetUserRequest> AddUser(AddUserRequest request, ServerCallContext context)
        {
            return base.AddUser(request, context);
        }

        public override Task<ListUsersResponse> GetAll(GetAllUserRequest request, ServerCallContext context)
        {
            return base.GetAll(request, context);
        }

        public override Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            return base.GetUser(request, context);
        }
    }
}
