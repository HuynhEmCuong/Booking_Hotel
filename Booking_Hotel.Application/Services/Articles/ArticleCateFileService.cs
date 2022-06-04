using AutoMapper;
using Booking_Hotel.Application.Service;
using Booking_Hotel.Application.ViewModels.Articles;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.Articles.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Hotel.Application.Services.Articles
{
    public interface IArticleCateFileService : IBaseService<ArticleCateFileViewModel>
    {

    }
    public class ArticleCateFileService : BaseService<ArticleCateFile, ArticleCateFileViewModel>, IArticleCateFileService
    {
        private readonly IRepository<ArticleCateFile> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;

        public ArticleCateFileService(IRepository<ArticleCateFile> repo, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
        : base(repo, unitOfWork, mapper, configMapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }
    }
}
