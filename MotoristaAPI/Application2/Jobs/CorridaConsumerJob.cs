using Domain.Interfaces.Mensageria;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Mensageria.Queue;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application2.Jobs
{
    public class CorridaConsumerJob : BackgroundService
    {
        private readonly CorridaQueueConsumer _corridaQueueConsumer;
        private readonly ICorridaService _corridaService;

        public CorridaConsumerJob(CorridaQueueConsumer corridaQueueConsumer, ICorridaService corridaService)
        {
            _corridaQueueConsumer = corridaQueueConsumer;
            _corridaService = corridaService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await ProcessarMensagem();
            }
            catch
            {
                throw;
            }
        }

        private async Task ProcessarMensagem()
        {
            await _corridaQueueConsumer.ConsumirMensagem(x =>
            {
                _corridaService.ProcessarSolicitacaoCorrida(x);
            });
        }
    }
}
