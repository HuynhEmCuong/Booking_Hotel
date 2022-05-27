using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Articles;

namespace Booking_Hotel.Application.Services
{
    public interface IArticleCategoryService:  IBaseService<ArticleCategoryViewModel>
    {

    }
    public class ArticleCategoryService : BaseService<ArticleCategory, ArticleCategoryViewModel>, IArticleCategoryService
    {
        private readonly IRepository<ArticleCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ArticleCategoryService(IRepository<ArticleCategory> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }
    }
}
