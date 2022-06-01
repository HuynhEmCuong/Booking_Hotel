using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Booking_Hotel.Application.Const;
using Booking_Hotel.Application.Extensions;
using Booking_Hotel.Ultilities;
using Booking_Hotel.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileSystem = System.IO.File;
using System.Linq;
using System.Text.RegularExpressions;
using Booking_Hotel.Data.EF.Interface;
using Booking_Hotel.Data.Entities.System;
using AutoMapper;
using AutoMapper.Configuration;
using System.Net;
using Booking_Hotel.Application.ViewModels.System;

namespace Booking_Hotel.Application.Service.SystemService
{
    public interface IFileService : IBaseService<FileDataViewModel>
    {
        Task<OperationFileResult> UploadMultipleFileAsync(List<IFormFile> files, string pathFolder);

        Task<OperationFileResult> UploadFileAsync(IFormFile file, string pathFolder);

        Task<OperationResult> OnRemoveFileAsync(string fileLocalName);

        Task<DownloadFileResult> DownloadFileAsync(string fullPath);

        bool DeleteFile(string filepath);

    }
    public class FileService : BaseService<FileData, FileDataViewModel>, IFileService
    {
        private readonly IRepository<FileData> _repository;
        private IHostingEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult operationResult;

        public FileService(IRepository<FileData> repository, IHostingEnvironment env, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _env = env;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<OperationFileResult> UploadMultipleFileAsync(List<IFormFile> files, string pathFolder)
        {
            var listFileResponse = new List<FileResponse>();
            foreach (var file in files)
            {
                var operationResult = await UploadFileAsync(file, pathFolder);
                if (operationResult.Success)
                {
                    listFileResponse.Add(operationResult.FileResponse);
                }
            }
            return new OperationFileResult() { Success = true, FileResponses = listFileResponse };
        }

        public async Task<OperationFileResult> UploadFileAsync(IFormFile file, string pathFolder)
        {
            if (file == null)
            {
                return new OperationFileResult()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = "Không có file tải lên!",
                    Success = false
                };
            }
            var operationFileResult = new OperationFileResult();
            if (pathFolder.IsNullOrEmpty())
            {
                pathFolder = "FileUpload/Commons/";
            }
            else
            {
                var firstChar = pathFolder.FirstOrDefault();
                var lastChar = pathFolder.LastOrDefault();
                if (firstChar == '/')
                {
                    pathFolder = pathFolder.TrimStart('/');
                }
                if (lastChar != '/')
                {
                    pathFolder = pathFolder + "/";
                }
            }
            try
            {
                //Khởi tạo folder
                var webRoot = _env.WebRootPath;
                if (webRoot == null)
                {
                    operationFileResult = new OperationFileResult() { Message = "Folder wwwroot not exist!", Success = false };
                }
                else
                {
                    bool exists = Directory.Exists(Path.Combine(webRoot, pathFolder));
                    if (!exists)
                        Directory.CreateDirectory(Path.Combine(webRoot, pathFolder));

                    var nowDate = DateTime.Now;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName).ToFileFormat();
                    string fileNewName = fileName + "_" + nowDate.Day + "_" + nowDate.Month + "_" + nowDate.Year + "_" + nowDate.Ticks + fileExtension;

                    using (FileStream stream = FileSystem.Create("wwwroot/" + pathFolder + fileNewName))
                    {
                        await file.CopyToAsync(stream);

                        var fileResponse = new FileResponse
                        {
                            FileLocalName = fileNewName,
                            FileOriginalName = file.FileName,
                            FileExtension = fileExtension,
                            FileType = file.ContentType,
                            Path = pathFolder,
                            FileFullPath = pathFolder + fileNewName
                        };

                        //Check is image
                        string pattern = "([^\\s]+(\\.(?i)(jpe?g|png|gif|bmp))$)";
                        Regex rgx = new Regex(pattern);
                        if (rgx.IsMatch(file.FileName))
                            fileResponse.IsImage = true;
                        else
                            fileResponse.IsImage = false;
                        operationFileResult = new OperationFileResult() { Success = true, FileResponse = fileResponse };
                    }
                }
            }
            catch (Exception ex)
            {
                operationFileResult = new OperationFileResult()
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }

            return operationFileResult;
        }

        public async Task<OperationResult> OnRemoveFileAsync(string fileLocalName)
        {
            var file = await _repository.FindSingleAsync(x => x.FileLocalName == fileLocalName);
            if (file != null)
            {
                if (DeleteFile(file.FileFullPath))
                {
                    //Delete file
                    _repository.Remove(file);
                    await _unitOfWork.SaveChangeAsync();
                    operationResult = new OperationResult() { Success = true };
                }
                else
                {
                    operationResult = new OperationResult()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Message = "Xóa file thất bại",
                        Success = false
                    };
                }
            }
            else
            {
                operationResult = new OperationResult()
                {
                    StatusCode = StatusCode.BadRequest,
                    Message = "File không tồn tại!",
                    Success = false
                };
            }

            return operationResult;
        }

        /// <summary>
        /// Hàm xóa file vật lý trong hệ thống
        /// </summary>
        /// <param name="filepath">Đường dẫn file</param>
        /// <returns></returns>
        public bool DeleteFile(string filepath)
        {
            var webRoot = _env.WebRootPath;
            var path = Path.Combine(webRoot, filepath);
            if (FileSystem.Exists(path))
            {
                try
                {
                    FileSystem.Delete(path);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<DownloadFileResult> DownloadFileAsync(string fullPath)
        {
            try
            {
                var file = await _repository.FindSingleAsync(x => x.FileFullPath == fullPath);
                if (file != null)
                {
                    var pathFile = Path.Combine(_env.WebRootPath, fullPath);
                    var net = new WebClient();
                    var data = net.DownloadData(pathFile);
                    var content = new MemoryStream(data);
                    return await Task.FromResult(new DownloadFileResult(content, file.FileType, file.FileOriginalName));
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }



    }
}
