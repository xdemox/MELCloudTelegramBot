using MELCloudApiClient.Converters;
using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;

public class ClientLogin
{
    public int? ErrorId { get; set; }
    public string? ErrorMessage { get; set; }
    public int LoginStatus { get; set; }
    public int UserId { get; set; }
    public string RandomKey { get; set; }
    public string AppVersionAnnouncement { get; set; }
    public LoginData LoginData { get; set; }
    public List<object> ListPendingInvite { get; set; }
    public List<object> ListOwnershipChangeRequest { get; set; }
    public List<object> ListPendingAnnouncement { get; set; }
    public int LoginMinutes { get; set; }
    public int LoginAttempts { get; set; }
}

public class LoginData
{
    [JsonPropertyName(nameof(ContextKey))]
    public string ContextKey { get; set; }

    [JsonPropertyName(nameof(Client))]
    public int Client { get; set; }

    [JsonPropertyName(nameof(Terms))]
    public int Terms { get; set; }

    [JsonPropertyName(nameof(AL))]
    public int AL { get; set; }

    [JsonPropertyName(nameof(ML))]
    public int ML { get; set; }

    [JsonPropertyName(nameof(CMI))]
    public bool CMI { get; set; }

    [JsonPropertyName(nameof(IsStaff))]
    public bool IsStaff { get; set; }

    [JsonPropertyName(nameof(CUTF))]
    public bool CUTF { get; set; }

    [JsonPropertyName(nameof(CAA))]
    public bool CAA { get; set; }

    [JsonPropertyName(nameof(ReceiveCountryNotifications))]
    public bool ReceiveCountryNotifications { get; set; }

    [JsonPropertyName(nameof(ReceiveAllNotifications))]
    public bool ReceiveAllNotifications { get; set; }

    [JsonPropertyName(nameof(CACA))]
    public bool CACA { get; set; }

    [JsonPropertyName(nameof(CAGA))]
    public bool CAGA { get; set; }

    [JsonPropertyName(nameof(MaximumDevices))]
    public int MaximumDevices { get; set; }

    [JsonPropertyName(nameof(ShowDiagnostics))]
    public bool ShowDiagnostics { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverterWithNumberSupport))]
    [JsonPropertyName(nameof(Language))]
    public Enums.Language Language { get; set; }

    [JsonPropertyName(nameof(Country))]
    public int Country { get; set; }

    [JsonPropertyName(nameof(RealClient))]
    public int RealClient { get; set; }

    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }

    [JsonPropertyName(nameof(UseFahrenheit))]
    public bool UseFahrenheit { get; set; }

    [JsonPropertyName(nameof(Duration))]
    public int Duration { get; set; }

    [JsonPropertyName(nameof(Expiry))]
    public string Expiry { get; set; }

    [JsonPropertyName(nameof(CMSC))]
    public bool CMSC { get; set; }

    [JsonPropertyName(nameof(PartnerApplicationVersion))]
    public string? PartnerApplicationVersion { get; set; }

    [JsonPropertyName(nameof(EmailSettingsReminderShown))]
    public bool EmailSettingsReminderShown { get; set; }

    [JsonPropertyName(nameof(EmailUnitErrors))]
    public int EmailUnitErrors { get; set; }

    [JsonPropertyName(nameof(EmailCommsErrors))]
    public int EmailCommsErrors { get; set; }

    [JsonPropertyName(nameof(ChartSeriesHidden))]
    public int ChartSeriesHidden { get; set; }

    [JsonPropertyName(nameof(DeletePending))]
    public bool DeletePending { get; set; }

    [JsonPropertyName(nameof(IsImpersonated))]
    public bool IsImpersonated { get; set; }

    [JsonPropertyName(nameof(LanguageCode))]
    public string LanguageCode { get; set; }

    [JsonPropertyName(nameof(CountryName))]
    public string CountryName { get; set; }

    [JsonPropertyName(nameof(CurrencySymbol))]
    public string CurrencySymbol { get; set; }

    [JsonPropertyName(nameof(SupportEmailAddress))]
    public string? SupportEmailAddress { get; set; }

    [JsonPropertyName(nameof(DateSeperator))]
    public string? DateSeperator { get; set; }

    [JsonPropertyName(nameof(TimeSeperator))]
    public string? TimeSeperator { get; set; }

    [JsonPropertyName(nameof(AtwLogoFile))]
    public string? AtwLogoFile { get; set; }

    [JsonPropertyName(nameof(DECCReport))]
    public bool DECCReport { get; set; }

    [JsonPropertyName(nameof(CSVReport1min))]
    public bool CSVReport1min { get; set; }

    [JsonPropertyName(nameof(HidePresetPanel))]
    public bool HidePresetPanel { get; set; }

    [JsonPropertyName(nameof(EmailSettingsReminderRequired))]
    public bool EmailSettingsReminderRequired { get; set; }

    [JsonPropertyName(nameof(TermsText))]
    public string? TermsText { get; set; }

    [JsonPropertyName(nameof(MapView))]
    public bool MapView { get; set; }

    [JsonPropertyName(nameof(MapZoom))]
    public int? MapZoom { get; set; }

    [JsonPropertyName(nameof(MapLongitude))]
    public double MapLongitude { get; set; }

    [JsonPropertyName(nameof(MapLatitude))]
    public double MapLatitude { get; set; }
}

public class ClientLoginRequest
{
    [JsonPropertyName(nameof(AppVersion))]
    public string AppVersion { get; set; }

    [JsonPropertyName(nameof(CaptchaResponse))]
    public string? CaptchaResponse { get; set; }

    [JsonPropertyName(nameof(Email))]
    public string Email { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverterWithNumberSupport))]
    [JsonPropertyName(nameof(Language))]
    public Enums.Language Language { get; set; }

    [JsonPropertyName(nameof(Password))]
    public string Password { get; set; }

    [JsonPropertyName(nameof(Persist))]
    public bool Persist { get; set; }
}