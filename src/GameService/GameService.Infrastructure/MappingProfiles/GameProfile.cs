using AutoMapper;
using GameService.Domain;
using GameService.Infrastructure.Entities;

namespace GameService.Infrastructure.MappingProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameEntity>();
            CreateMap<GameEntity, Game>();
        }
    }
}
