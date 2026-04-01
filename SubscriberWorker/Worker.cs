using Azure.Messaging.ServiceBus;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace SubscriberWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        // the client that owns the connection and can be used to create senders and receivers
        private readonly ServiceBusClient _client;

        // the processor that reads and processes messages from the subscription
        private readonly ServiceBusProcessor _processor;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read regularly.
            // Create the clients that we'll use for sending and processing messages.
            // Replace the <NAMESPACE-CONNECTION-STRING> placeholder

            // 1. IMPORTANTE: La cadena de conexión debe incluir UseDevelopmentEmulator=true
            string connectionString = _configuration["AzureServiceBus:ConnectionString"]!;
            string topicName = _configuration["AzureServiceBus:TopicName"]!;
            string subscriptionName = _configuration["AzureServiceBus:SubscriptionName"]!;

            _client = new ServiceBusClient(connectionString);

            // create a processor that we can use to process the messages
            // Replace the <TOPIC-NAME> and <SUBSCRIPTION-NAME> placeholders
            // 2. Creamos el procesador una sola vez
            _processor = _client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false, // Lo manejamos manualmente para mayor control
                MaxConcurrentCalls = 1        // Procesar de 1 en 1 (puedes subirlo)
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            // 3. Registramos los manejadores de eventos
            // add handler to process messages
            _processor.ProcessMessageAsync += MessageHandler;
            // add handler to process any errors
            _processor.ProcessErrorAsync += ErrorHandler;

            _logger.LogInformation("Iniciando el procesador de mensajes...");

            // start processing
            // 4. Arrancamos el procesador (esto corre en segundo plano)
            await _processor.StartProcessingAsync(stoppingToken);

            //Console.WriteLine("Wait for a minute and then press any key to end the processing");
            //Console.ReadKey();
            // stop processing
            //Console.WriteLine("\nStopping the receiver...");
            //await _processor.StopProcessingAsync();
            //Console.WriteLine("Stopped receiving messages");

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

            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            // 6. Limpieza al cerrar
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
            await _client.DisposeAsync();
        }

        // handle received messages
        // Manejador de mensajes exitosos
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            //_logger.LogInformation("Mensaje Recibido: {body}", body);

            //Console.WriteLine($"Received: {body} from subscription.");

            if (string.IsNullOrWhiteSpace(body)) return;

            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
            var obj = JsonSerializer.Deserialize<LeadModel>(body);
            //throw new Exception("Error de prueba para el manejador de errores");
            _logger.LogInformation($"Lead Id: {obj.LeadId}");

            bool sent = await SendWebHook(obj);
            if (sent)
            {
                // complete the message. messages is deleted from the subscription.
                // Confirmamos al emulador que procesamos el mensaje correctamente
                await args.CompleteMessageAsync(args.Message);
            }
            else
            {
                await args.AbandonMessageAsync(args.Message);
            }


        }

        // handle any errors when receiving messages
        // Manejador de errores (Obligatorio por el SDK)
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Error en el procesador: {source}", args.ErrorSource);
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task<bool> SendWebHook(LeadModel lead)
        {
            if (lead != null)
            {
                // 3. Crear el cliente HTTP y enviar el POST
                var httpClient = _httpClientFactory.CreateClient();

                // URL de tu API externa
                string url = _configuration["WebhookUrl"]!;

                _logger.LogInformation("Enviando POST a {url}...", url);
                var body = new
                {
                    LEADID = lead.LeadId,
                    SOURCE = $"{Guid.NewGuid()}_nombre_plataforma"
                };

                var response = await httpClient.PostAsJsonAsync(url, body);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("POST exitoso. Status: {code}", response.StatusCode);

                    //// 4. Completar el mensaje en Service Bus solo si el POST fue exitoso
                    //await args.CompleteMessageAsync(args.Message);
                    return true;
                }
                else
                {
                    _logger.LogError("Error en el POST: {code}", response.StatusCode);
                    // Opcional: Abandonar el mensaje para reintentar más tarde
                    //await args.AbandonMessageAsync(args.Message);
                    return false;
                }

            }

            return true;
        }
    }
}
