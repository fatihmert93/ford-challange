using System.Linq;
using System.Threading.Tasks;
using Ford.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Ford.Api.Middlewares
{
    public class UserAuthorizeRequirement : AuthorizationHandler<UserAuthorizeRequirement>, IAuthorizationRequirement
    {
       
        public UserAuthorizeRequirement()
        {
            
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizeRequirement requirement)
        {
            IHostingEnvironment environment = ServiceLocator.Current.GetInstance<IHostingEnvironment>();

            if (environment.IsDevelopment())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            
            if (context.User.Claims.Any(v => v.Type == "username"))
            {
                context.Succeed(requirement);
                
                return Task.CompletedTask;
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }


            //if (!ClientUserIsInRole)
            //{
            //    context.Fail();
            //}


        }
    }
}