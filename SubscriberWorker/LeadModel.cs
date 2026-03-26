using System.Text.Json.Serialization;

namespace SubscriberWorker;

public class LeadModel
{
    [JsonPropertyName("SITENUMBER")]
    public string? SiteNumber { get; set; }

    [JsonPropertyName("LEADID")]
    public string? LeadId { get; set; }

    [JsonPropertyName("LEADTYPE")]
    public string? LeadType { get; set; }

    [JsonPropertyName("INTERESTTYPE")]
    public string? InterestType { get; set; }

    [JsonPropertyName("BUSINESSPARTNERREFERENCE")]
    public string? BusinessPartnerReference { get; set; }

    [JsonPropertyName("CAMPAIGNID")]
    public string? CampaignId { get; set; }

    [JsonPropertyName("CAMPAIGNDESCRIPTION")]
    public string? CampaignDescription { get; set; }

    [JsonPropertyName("LEADSOURCE")]
    public string? LeadSource { get; set; }

    [JsonPropertyName("REMARK")]
    public string? Remark { get; set; }

    [JsonPropertyName("STATUS")]
    public string? Status { get; set; }

    [JsonPropertyName("LOSTREASON")]
    public string? LostReason { get; set; }

    [JsonPropertyName("BRAND")]
    public string? Brand { get; set; }

    [JsonPropertyName("MODEL")]
    public string? Model { get; set; }

    [JsonPropertyName("MODELGROUP")]
    public string? ModelGroup { get; set; }

    [JsonPropertyName("COMMISSIONNUMBER")]
    public string? CommissionNumber { get; set; }

    [JsonPropertyName("COMMISSIONYEAR")]
    public string? CommissionYear { get; set; }

    [JsonPropertyName("VIN")]
    public string? Vin { get; set; }

    [JsonPropertyName("USEDVEHICLEBRAND")]
    public string? UsedVehicleBrand { get; set; }

    [JsonPropertyName("TESTDRIVEDATE")]
    public DateTimeOffset? TestDriveDate { get; set; }

    [JsonPropertyName("CARSWITCHDATE")]
    public DateTimeOffset? CarSwitchDate { get; set; }

    [JsonPropertyName("TRACKINGID")]
    public string? TrackingId { get; set; }

    [JsonPropertyName("TRANSACTIONID")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("ARCHIVEDFLAG")]
    public string? ArchivedFlag { get; set; }

    [JsonPropertyName("ARCHIVEDDATE")]
    public DateTimeOffset? ArchivedDate { get; set; }

    [JsonPropertyName("LEADOWNERNAME")]
    public string? LeadOwnerName { get; set; }

    [JsonPropertyName("SUBBRANDCODE")]
    public string? SubBrandCode { get; set; }

    [JsonPropertyName("LEADCREATIONDATE")]
    public DateTimeOffset? LeadCreationDate { get; set; }

    [JsonPropertyName("LEADLASTCHANGEDATE")]
    public DateTimeOffset? LeadLastChangeDate { get; set; }

    [JsonPropertyName("INTERNALLYCREATED")]
    public string? InternallyCreated { get; set; }

    [JsonPropertyName("LEADASSIGNEDTODEALER")]
    public DateTimeOffset? LeadAssignedToDealer { get; set; }

    [JsonPropertyName("FIRSTCONTACTATTEMPTHAPPENED")]
    public string? FirstContactAttemptHappened { get; set; }

    [JsonPropertyName("CHANNELOFFIRSTCONTACTATTEMPT")]
    public string? ChannelOfFirstContactAttempt { get; set; }

    [JsonPropertyName("SUCCESSFULFIRSTCONTACTHAPPENED")]
    public string? SuccessfulFirstContactHappened { get; set; }

    [JsonPropertyName("BUSINESSPARTNERASSIGNED")]
    public string? BusinessPartnerAssigned { get; set; }

    [JsonPropertyName("FIRSTSALESPERSONCONTACTPLANNED")]
    public string? FirstSalespersonContactPlanned { get; set; }

    [JsonPropertyName("CHANNELOFFIRSTSALESPERSONCONTACT")]
    public string? ChannelOfFirstSalespersonContact { get; set; }

    [JsonPropertyName("FIRSTSALESPERSONCONTACTDONE")]
    public string? FirstSalespersonContactDone { get; set; }

    [JsonPropertyName("FIRSTCONTACTRESULT")]
    public string? FirstContactResult { get; set; }

    [JsonPropertyName("TESTDRIVEDONE")]
    public string? TestDriveDone { get; set; }

    [JsonPropertyName("OFFERCREATED")]
    public string? OfferCreated { get; set; }

    [JsonPropertyName("OFFERHANDEDOVER")]
    public string? OfferHandedOver { get; set; }

    [JsonPropertyName("CHANNELOFFIRSTOFFERHANDEDOVER")]
    public string? ChannelOfFirstOfferHandedOver { get; set; }

    [JsonPropertyName("NUMBEROFOFFERSHANDEDOVER")]
    public string? NumberOfOffersHandedOver { get; set; }

    [JsonPropertyName("OFFERFOLLOWUPDONE")]
    public string? OfferFollowUpDone { get; set; }

    [JsonPropertyName("OFFERACCEPTED")]
    public string? OfferAccepted { get; set; }

    [JsonPropertyName("FINANCEAPPLICATIONSTARTED")]
    public string? FinanceApplicationStarted { get; set; }

    [JsonPropertyName("FINANCEAPPLICATIONAPPROVED")]
    public string? FinanceApplicationApproved { get; set; }

    [JsonPropertyName("CONTRACTCREATED")]
    public string? ContractCreated { get; set; }

    [JsonPropertyName("CONTRACTHANDEDOVER")]
    public string? ContractHandedOver { get; set; }

    [JsonPropertyName("CHANNELOFFIRSTCONTRACTHANDEDOVER")]
    public string? ChannelOfFirstContractHandedOver { get; set; }

    [JsonPropertyName("CONTRACTSIGNED")]
    public string? ContractSigned { get; set; }

    [JsonPropertyName("CONTRACTSENT")]
    public string? ContractSent { get; set; }

    [JsonPropertyName("HANDOVERDONE")]
    public string? HandoverDone { get; set; }

    [JsonPropertyName("FOLLOWUPDONE")]
    public string? FollowUpDone { get; set; }

    [JsonPropertyName("STATUSTIMESTAMP")]
    public DateTimeOffset? StatusTimestamp { get; set; }

    [JsonPropertyName("REJECTREASON")]
    public string? RejectReason { get; set; }

    [JsonPropertyName("LEADWON")]
    public string? LeadWon { get; set; }

    [JsonPropertyName("LEADREJECTED")]
    public string? LeadRejected { get; set; }

    [JsonPropertyName("LEADLOST")]
    public string? LeadLost { get; set; }

    [JsonPropertyName("UTMSOURCE")]
    public string? UtmSource { get; set; }

    [JsonPropertyName("UTMMEDIUM")]
    public string? UtmMedium { get; set; }

    [JsonPropertyName("UTMCAMPAIGN")]
    public string? UtmCampaign { get; set; }

    [JsonPropertyName("UTMTERM")]
    public string? UtmTerm { get; set; }

    [JsonPropertyName("UTMCONTENT")]
    public string? UtmContent { get; set; }

    [JsonPropertyName("IMPORTEDFLAG")]
    public string? ImportedFlag { get; set; }

    [JsonPropertyName("CRMTYPE")]
    public string? CrmType { get; set; }

    [JsonPropertyName("LEADORIGIN")]
    public string? LeadOrigin { get; set; }
}
