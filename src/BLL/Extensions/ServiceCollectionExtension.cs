using BLL.Dto;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repository;
using DAL.Repository.Interfaces;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions
{
    public static class ServiceCollectionExtension
    {
        private static void RegisterMapsterConfiguration()
        {
            TypeAdapterConfig<Collection, CollectionDto>.NewConfig()
            .Map(dest => dest.CollectionImageUrl, src => src.CollectionImage.Url);
            TypeAdapterConfig<Item, ItemDto>.NewConfig()
            .Map(dest => dest.ItemImageUrl, src => src.ItemImage.Url);
        }

        public static void AddBllServices(this IServiceCollection services)
        {
            RegisterMapsterConfiguration();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICollectionImageRepository, CollectionImageRepository>();
            services.AddScoped<IItemImageRepository, ItemImageRepository>();

            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IUserLikeService, UserLikeService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IDbInitializerService, DbInitializerService>();
            services.AddScoped<ICloudinaryImageService, CloudinaryImageService>();
        }
    }
}
