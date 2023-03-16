using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Services.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyService(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public ResponseDTO GetTechnology()
        {
            var response = new ResponseDTO();
            try
            {
                var technology = _technologyRepository.GetTechnology().Select(tech => new TechnologyDTO()
                {
                    Id = tech.Id,
                    TechName = tech.TechName
                }).ToList();
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
                var technology = _technologyRepository.GetAllTechnologyUsingPagination(PageNo, RowsPerPage).Select(tech => new TechnologyDTO()
                {
                    Id = tech.Id,
                    TechName = tech.TechName
                }).ToList();
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

                var result = new TechnologyDTO()
                {
                    Id = technologyById.Id,
                    TechName = technologyById.TechName
                };
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
                var result = new TechnologyDTO()
                {
                    Id = technologyByName.Id,
                    TechName = technologyByName.TechName
                };
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

        public ResponseDTO AddTechnology(TechnologyDTO technology)
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
  
                var addFlag = _technologyRepository.AddTechnology(new Technology
                {
                    TechName = technology.TechName,
                    CreatedBy = technology.CreatedBy,
                    CreatedOn = DateTime.UtcNow
                });

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

        public ResponseDTO UpdateTechnology(TechnologyDTO technology)
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
                var updateFlag = _technologyRepository.UpdateTechnology(new Technology
                {
                    Id = technology.Id,
                    TechName = technology.TechName,
                    ModifiedBy = technology.ModifiedBy,
                    ModifiedOn = DateTime.UtcNow
                });
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
