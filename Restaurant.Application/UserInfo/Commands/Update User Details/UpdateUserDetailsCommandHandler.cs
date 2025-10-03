
using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.UserInfo.Commands;

public class UpdateUserDetailsCommandHandler(IUserContext userContext, IUserStore<User> userStore /*user store Interface used to handle the idenitty user tables in DB*/) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

        if (dbUser == null)
        {
            throw new NotFoundExceptionHandler(nameof(user), user!.Id);
        }
        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
