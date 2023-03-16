using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Services.Services
{
    public class TechnologyService : ITechnologyService
    {
        #region Field
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        #endregion

        #region Constructor
        public TechnologyService(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }
        #endregion

        public ResponseDTO GetTechnology()
        {
            var response = new ResponseDTO();
            try
            { 
                var technology = _mapper.Map<List<TechnologyDTO>>(_technologyRepository.GetTechnology().ToList());

                response.Status = 200;
                response.Data = technology;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage)
        {
            var response = new ResponseDTO();
            try
            {
                var technology = _mapper.Map<List<GetUserDTO>>(_technologyRepository.GetTechnology().ToList());
                response.Status = 200;
                response.Data = technology;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetTechnologyById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var technologyById = _technologyRepository.GetTechnologyById(id);
                if (technologyById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Technology not found";
                    return response;
                }

                var result = _mapper.Map<TechnologyDTO>(technologyById);

                response.Status = 200;
                response.Data = result;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetTechnologyByName(string name)
        {
            var response = new ResponseDTO();
            try
            {
                var technologyByName = _technologyRepository.GetTechnologyByName(name);
                if (technologyByName == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Technology not found";
                    return response;
                }

                var result = _mapper.Map<TechnologyDTO>(technologyByName);

                response.Status = 200;
                response.Data = result;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO AddTechnology(AddTechnologyDTO technology)
        {
            var response = new ResponseDTO();
            try
            {
                var technologyByName = _technologyRepository.GetTechnologyByName(technology.TechName);
                if (technologyByName != null)
                {
                    response.Status = 400;
                    response.Message = "Not created";
                    response.Error = "Technology is already exists.";
                    return response;
                }

                var addFlag = _technologyRepository.AddTechnology(_mapper.Map<Technology>(technology));

                if (addFlag)
                {
                    response.Status = 204;
                    response.Message = "Created";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Technology is not added";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO UpdateTechnology(UpdateTechnologyDTO technology)
        {
            var response = new ResponseDTO();
            try
            {
                var technologyById = _technologyRepository.GetTechnologyById(technology.Id);
                if (technologyById == null)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Technology not found";
                    return response;
                }
                var technologyByName = _technologyRepository.GetTechnologyByName(technology.TechName);
                if (technologyByName != null)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Technology already exists";
                    return response;
                }
                
                var updateFlag = _technologyRepository.UpdateTechnology(_mapper.Map<Technology>(technology));

                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Technology not Updated";
                }
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }
    }
}
