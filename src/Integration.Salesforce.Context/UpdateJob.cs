using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentScheduler;
using Integration.Salesforce.Library.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Integration.Salesforce.Context
{
    public class UpdateJob : IJob
    {
        //Dictionary holding names of tables to query from salesforce
        private Dictionary<string,string> categories = new Dictionary<string, string>(){
            {"Contacts","Contact"}, {"Training","Training__c"}, 
            {"HousingComplexes","HousingComplex__c"}, {"HousingAssignments","HousingAssignment__c"}, 
            {"HousingBeds","HousingBed__c"}, {"HousingUnits","HousingUnit__c"}
        };

        private IOptions<Settings> _settings;

       public UpdateJob(IOptions<Settings> settings)
       {
          _settings = settings;
       }


        public void Execute()
        {
            //Pull salesforce data
            var personContext = new SalesforceContext<Person>(_settings);
            IEnumerable<Person> personList = personContext.RetrieveFromSalesforce(categories["Contacts"]).GetAwaiter().GetResult();
            
            //Update mongodb database
            var persondb = new DbContext<Person>(_settings);
            persondb.UpdateMongoDB(personList);
            
        }
    }
}