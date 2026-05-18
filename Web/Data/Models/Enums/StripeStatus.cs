namespace Web.Data.Models.Enums
{
    public enum StripeStatus
    {
        Subscribed,
        Canceled, //user canceled the subscription - stop logins 30 days after 'DateSubscribed'

    }
}
