using FluentScheduler;

namespace Integration.Salesforce.Context
{
    public class UpdateRegistry : Registry
    {
        public UpdateRegistry()
        {
            Schedule<UpdateJob>().ToRunNow().AndEvery(5).Minutes();
        }

    }
}