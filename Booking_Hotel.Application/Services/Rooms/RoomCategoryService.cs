using AutoMapper;
using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Rooms;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services
{
    public interface IRoomCategoryService : IBaseService<RoomCategoryViewModel>
    {

    }
    public class RoomCategoryService : BaseService<RoomCategory, RoomCategoryViewModel>, IRoomCategoryService
    {
        private readonly IRepository<RoomCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RoomCategoryService(IRepository<RoomCategory> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }

        public async override Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            var query = _repository.FindAll().Include(x => x.RoomCateFile).ThenInclude(x => x.FileData);
            return await DataSourceLoader.LoadAsync(query, loadOptions);

        }
    }
}
