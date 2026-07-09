using System.Text.Json;
using Whatsapp.Application;
using Whatsapp.Application.Interfaces;
using Whatsapp.Infrastructure.Redis;

namespace Whatsapp.Worker.Services;

public class Worker(QueueService _queue, IWhatsappService _whatsapp) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var rawJob = await _queue.DequeueAsync(QueueNames.SendMessage);

            if (!rawJob.IsNullOrEmpty)
            {
                await Task.Delay(1000, stoppingToken);
                continue;
            }

            var job = JsonSerializer.Deserialize<SendMessageJob>((string)rawJob!);
            Console.WriteLine($"Processing job: {job?.Id}");

            if (job != null)
            {
                await _whatsapp.SendMessageAsync(
                    job.Phone,
                    job.Message
                );
            }
        }
    }
}
