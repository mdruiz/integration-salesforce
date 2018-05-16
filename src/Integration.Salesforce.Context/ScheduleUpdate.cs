using System.Collections.Generic;
using System.Threading.Tasks;
using Integration.Salesforce.Library.Models;
using Microsoft.Extensions.Options;

namespace Integration.Salesforce.Context
{
    public class ScheduleUpdate
    {
        private Settings _settings;

        private Dictionary<string,string> categories = new Dictionary<string, string>(){
            {"Contacts","Contact"}, {"Training","Training__c"}, 
            {"HousingComplexes","HousingComplex__c"}, {"HousingAssignments","HousingAssignment__c"}, 
            {"HousingBeds","HousingBed__c"}, {"HousingUnits","HousingUnit__c"}
        };

        public ScheduleUpdate(Settings settings)
        {
            _settings = settings;
        }

        public async Task Update()
        {
            
            //Pull salesforce data
            var personContext = new SalesforceContext<Person>(_settings);
            IEnumerable<Person> personList = await personContext.RetrieveFromSalesforce(categories["Contacts"]);
            
            //Update mongodb database
            var persondb = new DbContext<Person>(_settings);
            persondb.UpdateMongoDB(personList);
            persondb.UpdateMongoEntries(personList);
        }
    }
    
}