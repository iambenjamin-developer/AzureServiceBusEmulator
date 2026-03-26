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
        string example = @"{
  ""SITENUMBER"": ""29024"",
  ""LEADID"": ""18937"",
  ""LEADTYPE"": ""Offer_NV"",
  ""INTERESTTYPE"": null,
  ""BUSINESSPARTNERREFERENCE"": null,
  ""CAMPAIGNID"": null,
  ""CAMPAIGNDESCRIPTION"": null,
  ""LEADSOURCE"": ""Volkswagen_google"",
  ""REMARK"": null,
  ""STATUS"": ""Assigned"",
  ""LOSTREASON"": null,
  ""BRAND"": ""Volkswagen"",
  ""MODEL"": ""Tera"",
  ""MODELGROUP"": null,
  ""COMMISSIONNUMBER"": ""0"",
  ""COMMISSIONYEAR"": ""0"",
  ""VIN"": null,
  ""USEDVEHICLEBRAND"": null,
  ""TESTDRIVEDATE"": null,
  ""CARSWITCHDATE"": null,
  ""TRACKINGID"": ""LMSCL742936"",
  ""TRANSACTIONID"": null,
  ""ARCHIVEDFLAG"": ""false"",
  ""ARCHIVEDDATE"": null,
  ""LEADOWNERNAME"": ""Carla Reyes"",
  ""SUBBRANDCODE"": null,
  ""LEADCREATIONDATE"": ""2026-02-08T18:59:49.294+00:00"",
  ""LEADLASTCHANGEDATE"": ""2026-02-08T18:59:51.169+00:00"",
  ""INTERNALLYCREATED"": null,
  ""LEADASSIGNEDTODEALER"": ""2026-02-08T18:59:49.38+00:00"",
  ""FIRSTCONTACTATTEMPTHAPPENED"": null,
  ""CHANNELOFFIRSTCONTACTATTEMPT"": null,
  ""SUCCESSFULFIRSTCONTACTHAPPENED"": null,
  ""BUSINESSPARTNERASSIGNED"": null,
  ""FIRSTSALESPERSONCONTACTPLANNED"": null,
  ""CHANNELOFFIRSTSALESPERSONCONTACT"": null,
  ""FIRSTSALESPERSONCONTACTDONE"": null,
  ""FIRSTCONTACTRESULT"": null,
  ""TESTDRIVEDONE"": null,
  ""OFFERCREATED"": null,
  ""OFFERHANDEDOVER"": null,
  ""CHANNELOFFIRSTOFFERHANDEDOVER"": null,
  ""NUMBEROFOFFERSHANDEDOVER"": ""0"",
  ""OFFERFOLLOWUPDONE"": null,
  ""OFFERACCEPTED"": null,
  ""FINANCEAPPLICATIONSTARTED"": null,
  ""FINANCEAPPLICATIONAPPROVED"": null,
  ""CONTRACTCREATED"": null,
  ""CONTRACTHANDEDOVER"": null,
  ""CHANNELOFFIRSTCONTRACTHANDEDOVER"": null,
  ""CONTRACTSIGNED"": null,
  ""CONTRACTSENT"": null,
  ""HANDOVERDONE"": null,
  ""FOLLOWUPDONE"": null,
  ""STATUSTIMESTAMP"": ""2026-02-08T18:59:50.666+00:00"",
  ""REJECTREASON"": null,
  ""LEADWON"": null,
  ""LEADREJECTED"": null,
  ""LEADLOST"": null,
  ""UTMSOURCE"": null,
  ""UTMMEDIUM"": null,
  ""UTMCAMPAIGN"": null,
  ""UTMTERM"": null,
  ""UTMCONTENT"": null,
  ""IMPORTEDFLAG"": ""false"",
  ""CRMTYPE"": null,
  ""LEADORIGIN"": null
}";

        [HttpPost]
        [Route("Publish")]
        public async Task<IActionResult> Publish([FromBody] MessageModel request)
        {
            if (request.LeadId == "string")
            {
                request = JsonSerializer.Deserialize<MessageModel>(example)!;
            }

            string? msg = System.Text.Json.JsonSerializer.Serialize(request);
            var client = new ServiceBusClient("Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;");
            var sender = client.CreateSender("mi-topico-local");
            await sender.SendMessageAsync(new ServiceBusMessage(msg));

            return Ok(msg);
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