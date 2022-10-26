using BLL.Interfaces;
using BLL.Services;
using DAL.Repository;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IUserLikeService, UserLikeService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IDbInitializerService, DbInitializerService>();
        }
    }
}
