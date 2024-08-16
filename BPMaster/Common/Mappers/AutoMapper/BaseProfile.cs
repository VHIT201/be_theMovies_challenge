using AutoMapper;

namespace Common.Mappers.AutoMapper
{
	public abstract class BaseProfile : Profile
	{
		protected abstract void CreateMaps();
		protected BaseProfile() {
			CreateMaps();
		}
	}
}
