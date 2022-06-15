using AutoMapper;
using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Reservations;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services
{
    public interface IReservationService : IBaseService<ReservationViewModel>
    {

    }
    public class ReservationService : BaseService<Reservation, ReservationViewModel>, IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ReservationService(IRepository<Reservation> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }

        public override async Task<LoadResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            var query =  _repository.FindAll().Include(x => x.Guest).Include(x => x.Room).ThenInclude(x => x.RoomCategory);
            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }
    }
}
