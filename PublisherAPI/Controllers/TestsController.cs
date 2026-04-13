using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using PublisherAPI.Models;
using System.Text.Json;

namespace PublisherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Publish")]
        public async Task<IActionResult> Publish([FromBody] MessageModel request)
        {
            //if (request.LeadId == "string")
            //{



            //}
            var examples = GetExamples();

            string? msg = JsonSerializer.Serialize(request);

            string connectionString = _configuration["AzureServiceBus:ConnectionString"]!;
            string topicName = _configuration["AzureServiceBus:TopicName"]!;

            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender(topicName);

            foreach (var example in examples)
            {
                string exampleMsg = JsonSerializer.Serialize(example);
                await sender.SendMessageAsync(new ServiceBusMessage(exampleMsg));
            }

            //await sender.SendMessageAsync(new ServiceBusMessage(msg));

            return Ok(examples);
        }

        private static List<MessageModel> GetExamples()
        {
            var utcNow = DateTimeOffset.UtcNow;

            // Definimos el punto de partida (hoy a la hora actual, minutos y segundos en 0 para limpieza)
            var baseDate = new DateTimeOffset(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0, TimeSpan.Zero);

            // Ejemplo 1: Fecha Base (0 horas agregadas)
            var leadDate1 = baseDate.AddHours(-1).AddMinutes(-11).AddSeconds(24).AddMicroseconds(456);

            // Ejemplo 2: +3 horas
            var leadDate2 = baseDate.AddHours(-3).AddMinutes(22).AddSeconds(11).AddMicroseconds(123);

            // Ejemplo 3: +6 horas
            var leadDate3 = baseDate.AddHours(6).AddMinutes(-3).AddSeconds(59).AddMicroseconds(564);

            // Ejemplo 4: +9 horas
            var leadDate4 = baseDate.AddHours(-9).AddMinutes(-8).AddSeconds(18).AddMicroseconds(152);

            // Ejemplo 5: +12 horas
            var leadDate5 = baseDate.AddHours(-12).AddMinutes(6).AddSeconds(19).AddMicroseconds(457);

            // Ejemplo 6: +15 horas
            var leadDate6 = baseDate.AddHours(-15).AddMinutes(10).AddSeconds(23).AddMicroseconds(128);

            // 1. MERCEDES-BENZ - Lead de Alta Gama (Vía LinkedIn)
            var message1 = new MessageModel
            {
                SiteNumber = "20010",
                LeadId = Guid.NewGuid().ToString(),
                LeadType = "Luxury_NV",
                LeadSource = "LinkedIn_Ads",
                Status = "New",
                Brand = "Mercedes-Benz",
                Model = "EQE SUV",
                TrackingId = "LNKD-EV-772",
                LeadOwnerName = "Sofía Valenzuela",
                LeadCreationDate = leadDate1,
                LeadLastChangeDate = leadDate1,
                StatusTimestamp = utcNow,
                UtmMedium = "Social_Paid",
                CampaignId = "CAMP-EV-2026",
                CampaignDescription = "Lanzamiento Línea Eléctrica",
                Remark = "CEO de empresa logística interesado en flota eléctrica corporativa.",
            };

            // 2. HONDA - Usado Certificado (Vía Web Directa)
            var message2 = new MessageModel
            {
                SiteNumber = "15500",
                LeadId = Guid.NewGuid().ToString(),
                LeadType = "CPO_Vehicle",
                LeadSource = "Website_Direct",
                Status = "Contacted",
                Brand = "Honda",
                Model = "Civic",
                TrackingId = "WEB-DIR-110",
                LeadOwnerName = "Carlos Ruiz",
                LeadCreationDate = leadDate2,
                LeadLastChangeDate = leadDate2,
                StatusTimestamp = utcNow.AddMinutes(-10),
                UtmMedium = "Organic",
                CampaignId = "RE-MARKETING-01",
                CampaignDescription = "Retargeting de visitantes web",
                Remark = "El cliente ya tuvo un Honda, busca plan de recambio (Trade-in).",
            };

            // 3. TESLA - Test Drive solicitado (Vía Código QR en Evento)
            var message3 = new MessageModel
            {
                SiteNumber = "99001",
                LeadId = Guid.NewGuid().ToString(),
                LeadType = "Demo_Request",
                LeadSource = "Event_Expo_Auto",
                Status = "Appointment Set",
                Brand = "Tesla",
                Model = "Model 3",
                TrackingId = "QR-EXPO-2026",
                LeadOwnerName = "Elena Martínez",
                LeadCreationDate = leadDate3,
                LeadLastChangeDate = leadDate3,
                StatusTimestamp = utcNow.AddHours(-2),
                UtmMedium = "Offline_QR",
                CampaignId = "EXPO-FEB",
                CampaignDescription = "Feria del Automóvil Verano",
                Remark = "Solicitó prueba de manejo para el fin de semana. Prioridad Alta.",
            };

            // 4. CHEVROLET - Flotas/Empresarial (Vía Google Search)
            var message4 = new MessageModel
            {
                SiteNumber = "44050",
                LeadId = Guid.NewGuid().ToString(),
                LeadType = "Fleet_Sale",
                LeadSource = "Google_Ads_Search",
                Status = "In Negotiation",
                Brand = "Chevrolet",
                Model = "Silverado",
                TrackingId = "GGL-SRCH-552",
                LeadOwnerName = "Roberto Gómez",
                LeadCreationDate = leadDate4,
                LeadLastChangeDate = leadDate4,
                StatusTimestamp = utcNow.AddMinutes(-45),
                UtmMedium = "CPC",
                CampaignId = "CMP-TRUCKS-26",
                CampaignDescription = "Campaña Camionetas de Trabajo",
                Remark = "Cotización por 5 unidades para empresa minera.",
            };

            // 5. HYUNDAI - Consulta por WhatsApp (Vía Botón Web)
            var message5 = new MessageModel
            {
                SiteNumber = "30022",
                LeadId = Guid.NewGuid().ToString(),
                LeadType = "New_Vehicle",
                LeadSource = "WhatsApp_Business",
                Status = "Waiting for Info",
                Brand = "Hyundai",
                Model = "Tucson",
                TrackingId = "WABA-TUC-09",
                LeadOwnerName = "Ana Lía Castro",
                LeadCreationDate = leadDate5,
                LeadLastChangeDate = leadDate5,
                StatusTimestamp = utcNow.AddHours(-12),
                UtmMedium = "Messenger",
                CampaignId = "SOCIAL-CONVERSION",
                CampaignDescription = "Conversión directa desde RRSS",
                Remark = "Pregunta por disponibilidad de colores y entrega inmediata.",
            };

            // 6. VOLVO - Lead de Re-compra (Vía Email Marketing)
            var message6 = new MessageModel
            {
                SiteNumber = "88011",
                LeadId = Guid.NewGuid().ToString(),
                LeadType = "Loyalty_Program",
                LeadSource = "Newsletter_Feb",
                Status = "Qualified",
                Brand = "Volvo",
                Model = "XC60",
                TrackingId = "EML-LYL-202",
                LeadOwnerName = "Marcos Silveira",
                LeadCreationDate = leadDate6,
                LeadLastChangeDate = leadDate6,
                StatusTimestamp = utcNow.AddDays(-1),
                UtmMedium = "Email",
                CampaignId = "LOYALTY-2026",
                CampaignDescription = "Fidelización Clientes 2023-2024",
                Remark = "Cliente antiguo. Interesado en el nuevo modelo híbrido enchufable.",
            };


            return new List<MessageModel> { message1, message2, message3, message4, message5, message6 };
        }
    }
}

/*
 {
  "SITENUMBER": "29024",
  "LEADID": "18937",
  "LEADTYPE": "Offer_NV",
  "INTERESTTYPE": null,
  "BUSINESSPARTNERREFERENCE": null,
  "CAMPAIGNID": null,
  "CAMPAIGNDESCRIPTION": null,
  "LEADSOURCE": "Volkswagen_google",
  "REMARK": null,
  "STATUS": "Assigned",
  "LOSTREASON": null,
  "BRAND": "Volkswagen",
  "MODEL": "Tera",
  "MODELGROUP": null,
  "COMMISSIONNUMBER": "0",
  "COMMISSIONYEAR": "0",
  "VIN": null,
  "USEDVEHICLEBRAND": null,
  "TESTDRIVEDATE": null,
  "CARSWITCHDATE": null,
  "TRACKINGID": "LMSCL742936",
  "TRANSACTIONID": null,
  "ARCHIVEDFLAG": "false",
  "ARCHIVEDDATE": null,
  "LEADOWNERNAME": "Carla Reyes",
  "SUBBRANDCODE": null,
  "LEADCREATIONDATE": "2026-02-08T18:59:49.294+00:00",
  "LEADLASTCHANGEDATE": "2026-02-08T18:59:51.169+00:00",
  "INTERNALLYCREATED": null,
  "LEADASSIGNEDTODEALER": "2026-02-08T18:59:49.38+00:00",
  "FIRSTCONTACTATTEMPTHAPPENED": null,
  "CHANNELOFFIRSTCONTACTATTEMPT": null,
  "SUCCESSFULFIRSTCONTACTHAPPENED": null,
  "BUSINESSPARTNERASSIGNED": null,
  "FIRSTSALESPERSONCONTACTPLANNED": null,
  "CHANNELOFFIRSTSALESPERSONCONTACT": null,
  "FIRSTSALESPERSONCONTACTDONE": null,
  "FIRSTCONTACTRESULT": null,
  "TESTDRIVEDONE": null,
  "OFFERCREATED": null,
  "OFFERHANDEDOVER": null,
  "CHANNELOFFIRSTOFFERHANDEDOVER": null,
  "NUMBEROFOFFERSHANDEDOVER": "0",
  "OFFERFOLLOWUPDONE": null,
  "OFFERACCEPTED": null,
  "FINANCEAPPLICATIONSTARTED": null,
  "FINANCEAPPLICATIONAPPROVED": null,
  "CONTRACTCREATED": null,
  "CONTRACTHANDEDOVER": null,
  "CHANNELOFFIRSTCONTRACTHANDEDOVER": null,
  "CONTRACTSIGNED": null,
  "CONTRACTSENT": null,
  "HANDOVERDONE": null,
  "FOLLOWUPDONE": null,
  "STATUSTIMESTAMP": "2026-02-08T18:59:50.666+00:00",
  "REJECTREASON": null,
  "LEADWON": null,
  "LEADREJECTED": null,
  "LEADLOST": null,
  "UTMSOURCE": null,
  "UTMMEDIUM": null,
  "UTMCAMPAIGN": null,
  "UTMTERM": null,
  "UTMCONTENT": null,
  "IMPORTEDFLAG": "false",
  "CRMTYPE": null,
  "LEADORIGIN": null
}
 
 */