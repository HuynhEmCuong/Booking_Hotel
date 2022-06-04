using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels.Articles;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Articles.Files;
using Booking_Hotel.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Booking_Hotel.Application.Services.Articles
{
    public interface IArticleFileService : IBaseService<ArticleFileViewModel>
    {

    }
    public class ArticleFileService : BaseService<ArticleFile, ArticleFileViewModel>, IArticleFileService
    {
        private readonly IRepository<ArticleFile> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;

        public ArticleFileService(IRepository<ArticleFile> repo, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
            : base(repo, unitOfWork, mapper, configMapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

       
    }
}
