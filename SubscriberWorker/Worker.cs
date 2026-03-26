using Azure.Messaging.ServiceBus;

namespace SubscriberWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            // 1. IMPORTANTE: La cadena de conexión debe incluir UseDevelopmentEmulator=true
            // En un entorno real, esto iría en appsettings.json
            string connectionString = "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";

            _client = new ServiceBusClient(connectionString);

            // 2. Creamos el procesador una sola vez
            _processor = _client.CreateProcessor("mi-topico-local", "SuscripcionA", new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false, // Lo manejamos manualmente para mayor control
                MaxConcurrentCalls = 1        // Procesar de 1 en 1 (puedes subirlo)
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            // 3. Registramos los manejadores de eventos
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            _logger.LogInformation("Iniciando el procesador de mensajes...");

            // 4. Arrancamos el procesador (esto corre en segundo plano)
            await _processor.StartProcessingAsync(stoppingToken);

            // 5. Mantenemos el Worker vivo hasta que se solicite la cancelación
            // No necesitamos un bucle con Delay, el procesador ya gestiona la conexión
            try
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Deteniendo el Worker...");
            }

            // 6. Limpieza al cerrar
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
            await _client.DisposeAsync();
        }

        // Manejador de mensajes exitosos
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation("Mensaje Recibido: {body}", body);

            // Confirmamos al emulador que procesamos el mensaje correctamente
            await args.CompleteMessageAsync(args.Message);
        }

        // Manejador de errores (Obligatorio por el SDK)
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Error en el procesador: {source}", args.ErrorSource);
            return Task.CompletedTask;
        }
    }
}
