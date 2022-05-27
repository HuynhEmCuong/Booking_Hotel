using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Articles;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services
{ 
    public interface IArticleService : IBaseService<ArticleViewModel>
    {
        Task<List<ArticleViewModel>> GetAllByCatId(int catId);

    }
    public class ArticleService : BaseService<Article, ArticleViewModel>, IArticleService
    {
        private readonly IRepository<Article> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ArticleService(IRepository<Article> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configMapper = configMapper;
        }

        public async Task<List<ArticleViewModel>> GetAllByCatId(int catId)
        {
            var data = await _repository.FindAll(x=> x.ArticleCateId == catId).AsNoTracking().ToListAsync();
            return _mapper.Map<List<ArticleViewModel>>(data);
        }
    }
}
