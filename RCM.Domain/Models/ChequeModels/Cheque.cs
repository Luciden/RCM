﻿using System;
using RCM.Domain.Core.Models;
using RCM.Domain.Models.BancoModels;
using RCM.Domain.Models.ChequeModels.ChequeStates;
using RCM.Domain.Models.ClienteModels;

namespace RCM.Domain.Models.ChequeModels
{
    public class Cheque : Entity<Cheque>
    {
        public Guid BancoId { get; set; }
        public virtual Banco Banco { get; private set; }
        public string Agencia { get; private set; }
        public string Conta { get; private set; }
        public string NumeroCheque { get; private set; }
        public string Observacao { get; private set; }

        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public decimal Valor { get; private set; }
        
        private EstadoCheque _estadoCheque;
        public virtual EstadoCheque EstadoCheque
        {
            get
            {
                return _estadoCheque;
            }
        }

        protected Cheque() { }

        public Cheque(Guid id, Banco banco, string agencia, string conta, string numeroCheque, Cliente cliente, DateTime dataEmissao, DateTime dataVencimento, decimal valor)
        {
            Id = id;

            Banco = banco;

            Agencia = agencia;
            Conta = conta;
            NumeroCheque = numeroCheque;

            Cliente = cliente;

            DataEmissao = dataEmissao;
            DataVencimento = dataVencimento;
            Valor = valor;

            _estadoCheque = _estadoCheque ?? new ChequeBloqueado();
        }

        public Cheque(Banco banco, string agencia, string conta, string numeroCheque, Cliente cliente, DateTime dataEmissao, DateTime dataVencimento, decimal valor)
        {
            Banco = banco;

            Agencia = agencia;
            Conta = conta;
            NumeroCheque = numeroCheque;

            Cliente = cliente;
            
            DataEmissao = dataEmissao;
            DataVencimento = dataVencimento;
            Valor = valor;

            _estadoCheque = _estadoCheque ?? new ChequeBloqueado();
        }

        public void MudarEstado(EstadoCheque state)
        {
            _estadoCheque = state;
        }

        public void Bloquear()
        {
            _estadoCheque.Bloquear(this);
        }

        public void Compensar(DateTime data)
        {
            _estadoCheque.Compensar(this, data);
        }

        public void Repassar(DateTime data, Cliente cliente)
        {
            _estadoCheque.Repassar(this, data, cliente);
        }

        public void Sustar(DateTime data, string motivo)
        {
            _estadoCheque.Sustar(this, data, motivo);
        }

        public void Devolver(DateTime data, string motivo)
        {
            _estadoCheque.Devolver(this, data, motivo);
        }
    }
}
