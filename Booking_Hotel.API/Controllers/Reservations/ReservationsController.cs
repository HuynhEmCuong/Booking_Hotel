﻿using Booking_Hotel.Application.Configuration;
using Booking_Hotel.Application.Services;
using Booking_Hotel.Application.ViewModels;
using Booking_Hotel.Controllers;
using Booking_Hotel.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Hotel.API.Controllers
{
    public class ReservationController : BaseApiController
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] ReservationViewModel model)
        {
            return StatusCodeResult(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] ReservationViewModel model)
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
        //public async Task<ActionResult> Validate([FromBody] ReservationViewModel model)
        //{
        //    return Ok(await _service.ValidateAsync(model));
        //}
    }
}
