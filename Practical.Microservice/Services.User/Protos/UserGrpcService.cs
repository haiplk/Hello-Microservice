using Google.Protobuf.Collections;
using Grpc.Core;
using UserManagement.Services;

namespace UserManagement.Protos
{
    public class UserGrpcService : UserServiceGrpc.UserServiceGrpcBase
    {
        private readonly ILogger<UserGrpcService> _logger;
        private readonly IUserService userService;

        public UserGrpcService(ILogger<UserGrpcService> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        public override async Task<UserResponse> AddUser(AddUserRequest request, ServerCallContext context)
        {
            var user = await userService.AddUserAsync(new Models.AddUserModel
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth == null ? null : request.DateOfBirth.ToDateTime()
            });

            return new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth.HasValue ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(user.DateOfBirth.Value) : null
            };
        }

        public override async Task<ListUsersResponse> GetAll(GetAllUserRequest request, ServerCallContext context)
        {
            var users = await userService.GetAllAsync();
            var usersInProtoBuff = users.Select(user => new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth.HasValue ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(user.DateOfBirth.Value) : null
            });

            var result = new ListUsersResponse();
            result.Users.AddRange(usersInProtoBuff);

            return result;
        }

        public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await userService.GetUserAsync(Guid.Parse(request.Id));
            return new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth.HasValue ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(user.DateOfBirth.Value) : null
            };
        }
    }
}
