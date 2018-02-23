﻿using System;
using Microsoft.AspNetCore.Mvc;
using RCM.Application.ApplicationInterfaces;
using RCM.Application.ViewModels;
using RCM.Domain.DomainNotificationHandlers;

namespace RCM.Presentation.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Cheques")]
    public class ChequesController : ApiController
    {
        private readonly IChequeApplicationService _chequeApplicationService;

        public ChequesController(IChequeApplicationService chequeApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _chequeApplicationService = chequeApplicationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response(_chequeApplicationService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ChequeViewModel model = new ChequeViewModel()
            {
                BancoId = 1,
                Agencia = "4202",
                Conta = "256-9",
                ClienteId = 1,
                DataEmissao = DateTime.Now.AddDays(-1),
                DataVencimento = DateTime.Now.AddDays(1),
                NumeroCheque = "4515153",
                Valor = 122.51m,
            };
            _chequeApplicationService.Add(model);

            return Response(_chequeApplicationService.GetById(id));
        }
        
        [HttpPost]
        public void Post(ChequeViewModel model)
        {
            _chequeApplicationService.Add(model);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, ChequeViewModel model)
        {
            _chequeApplicationService.Update(model);
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _chequeApplicationService.Remove(new ChequeViewModel { Id = id });
        }
    }
}