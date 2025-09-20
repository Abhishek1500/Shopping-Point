using MediatR;
using UserMicroService.Data;
using UserMicroService.DTOS;
using UserMicroService.Helper;
using Week3Assignment.ExceptionHandler;

namespace UserMicroService.Commands
{
    public class PasswordChangeCommand :IRequest<Unit>
    {
        public ChangePasswordDto ChangePass;
        public int Id;

        public PasswordChangeCommand(ChangePasswordDto changePass,int id)
        {
            ChangePass = changePass;
            Id = id;
        }
    }

    public class PasswordChangehandler : IRequestHandler<PasswordChangeCommand, Unit>
    {

        DataContext _dbCon;
        public PasswordChangehandler(DataContext dbCon)
        {
            _dbCon = dbCon;
        }


        public async Task<Unit> Handle(PasswordChangeCommand request, CancellationToken cancellationToken)
        {

            var user = await _dbCon.Users.FindAsync(request.Id);
            if (user == null)
            {
                throw new CustomException(404, "User Not Found");
            }


            if (!user.Password.isHashOf(request.ChangePass.CurrentPassword,user.PasswordSalt))
            {
                throw new CustomException(401, "Current Password is Wrong");
            }

            if (user.Password.isHashOf(request.ChangePass.NewPassword,user.PasswordSalt))
            {
                throw new CustomException(400,"New Password cant be same as Current Password");
            }

            user.Password = request.ChangePass.NewPassword.hashPassword(user.PasswordSalt);
            await _dbCon.SaveChangesAsync();
            return Unit.Value;


        }
    }
}
