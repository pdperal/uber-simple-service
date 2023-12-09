using Domain.Interfaces.Mensageria;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobs;

public class CorridaConsumerJob : BackgroundService
{
    private readonly ICorridaQueueConsumer _corridaQueueConsumer;
    private readonly ICorridaService _corridaService;

    public CorridaConsumerJob(ICorridaQueueConsumer corridaQueueConsumer, ICorridaService corridaService)
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
