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

            var message = new MessageModel
            {
                SiteNumber = "29024",
                LeadId = "18937",
                LeadType = "Offer_NV",
                LeadSource = "Volkswagen_google",
                Status = "Assigned",
                Brand = "Volkswagen",
                Model = "Tera",
                CommissionNumber = "0",
                CommissionYear = "0",
                TrackingId = "LMSCL742936",
                ArchivedFlag = "false",
                LeadOwnerName = "Carla Reyes",

                // Usamos las variables de fecha directamente
                LeadCreationDate = utcNow.AddDays(-1),
                LeadLastChangeDate = utcNow.AddDays(-1),
                LeadAssignedToDealer = DateTimeOffset.UtcNow,
                StatusTimestamp = DateTimeOffset.UtcNow,

                ImportedFlag = "false",
                NumberOfOffersHandedOver = "0"
            };


            // Ejemplo 1: Lead de Ford - En Proceso (Vía Facebook Ads)
            var message1 = new MessageModel
            {
                SiteNumber = "10050",
                LeadId = "22445",
                LeadType = "Used_Car",
                LeadSource = "Facebook_Ads",
                Status = "In Progress",
                Brand = "Ford",
                Model = "Ranger",
                TrackingId = "FB-TRK-9921",
                LeadOwnerName = "Juan Pérez",
                LeadCreationDate = utcNow.AddDays(-3),
                LeadLastChangeDate = utcNow.AddDays(-3),
                StatusTimestamp = utcNow.AddHours(-5)
            };

            // Ejemplo 2: Lead de Audi - Ganado (Búsqueda Orgánica)
            var message2 = new MessageModel
            {
                SiteNumber = "33001",
                LeadId = "55920",
                LeadType = "Luxury_NV",
                LeadSource = "Organic_Search",
                Status = "Won",
                Brand = "Audi",
                Model = "Q5",
                CommissionNumber = "887766",
                LeadOwnerName = "Maria Garcia",
                LeadCreationDate = utcNow.AddDays(-5),
                LeadLastChangeDate = utcNow.AddDays(-5),
                StatusTimestamp = utcNow,
                LeadWon = "true"
            };

            // Ejemplo 3: Lead de Toyota - Rechazado (Instagram)
            var message3 = new MessageModel
            {
                SiteNumber = "44022",
                LeadId = "11223",
                LeadType = "Offer_Hybrid",
                LeadSource = "Instagram_Promo",
                Status = "Rejected",
                Brand = "Toyota",
                Model = "Corolla Cross",
                RejectReason = "Customer not interested in financing",
                LeadOwnerName = "Ricardo Soto",
                LeadCreationDate = utcNow.AddDays(-8),
                LeadLastChangeDate = utcNow.AddDays(-8),
                StatusTimestamp = utcNow.AddMinutes(-30),
                LeadRejected = "true"
            };

            // Ejemplo 4: Lead de BMW - Test Drive (Showroom)
            var message4 = new MessageModel
            {
                SiteNumber = "29024",
                LeadId = "99887",
                LeadType = "Demo_Request",
                LeadSource = "Showroom_Walkin",
                Status = "Test Drive Scheduled",
                Brand = "BMW",
                Model = "Series 3",
                TestDriveDate = utcNow.AddDays(3),
                LeadOwnerName = "Carla Reyes",
                LeadCreationDate = utcNow.AddDays(-12),
                LeadLastChangeDate = utcNow.AddDays(-12),
                StatusTimestamp = utcNow.AddHours(-1)
            };

            // Ejemplo 5: Lead de Jeep - Perdido (Campaña Email)
            var message5 = new MessageModel
            {
                SiteNumber = "50505",
                LeadId = "33441",
                LeadType = "Fleet_Sale",
                LeadSource = "Email_Marketing_Jan",
                Status = "Lost",
                Brand = "Jeep",
                Model = "Compass",
                LostReason = "Bought competitor brand",
                TrackingId = "EML-771122",
                LeadOwnerName = "Andrés Molina",
                LeadCreationDate = utcNow.AddDays(-20),
                LeadLastChangeDate = utcNow.AddDays(-20),
                StatusTimestamp = utcNow.AddDays(-1),
                LeadLost = "true"
            };
            return new List<MessageModel> { message, message1, message2, message3, message4, message5 };
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