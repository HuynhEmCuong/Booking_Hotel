﻿using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Application.Services;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Controllers;
using Booking_Hotel.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.API.Controllers
{
    public class ContactController : BaseApiController
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] ContactViewModel model)
        {
            return StatusCodeResult(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] ContactViewModel model)
        {
            return StatusCodeResult(await _service.UpdateAsync(model));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int key)
        {
            return StatusCodeResult(await _service.DeleteAsync(key));
        }

        [HttpGet]
        public async Task<ActionResult> FindByIdAsync(int id)
        {
            return Ok(await _service.FindByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> PaginationAsync(ParamaterPagination paramater)
        {
            return Ok(await _service.PaginationAsync(paramater));
        }

        [HttpGet]
        public async Task<ActionResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(await _service.LoadDxoGridAsync(loadOptions));
        }

        [HttpGet]
        public async Task<ActionResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(await _service.LoadDxoLookupAsync(loadOptions));
        }

        //[HttpPost]
        //public async Task<ActionResult> Validate([FromBody] ContactViewModel model)
        //{
        //    return Ok(await _service.ValidateAsync(model));
        //}
    }
}
