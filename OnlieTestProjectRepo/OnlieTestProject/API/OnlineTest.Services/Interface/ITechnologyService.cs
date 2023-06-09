﻿using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;

namespace OnlineTest.Services.Interface
{
    public interface ITechnologyService
    {
        ResponseDTO GetTechnology();
        ResponseDTO GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage);
        ResponseDTO GetTechnologyById(int id);
        ResponseDTO GetTechnologyByName(string name);
        ResponseDTO AddTechnology(int userId, AddTechnologyDTO technology);
        ResponseDTO UpdateTechnology(int userId, UpdateTechnologyDTO technology);
        ResponseDTO DeleteTechnology(int id);
    }
}
