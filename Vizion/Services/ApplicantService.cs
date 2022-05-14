using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Vizion.Models;

namespace Vizion.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IMongoCollection<Applicant> _applicant;
        public ApplicantService(IDatabaseSettings setting)
        {
            var client = new MongoClient(setting.ConnectionString);
            var database = client.GetDatabase(setting.DatabaseName);
            _applicant = database.GetCollection<Applicant>(setting.CollectionName);
        }

        public List<Applicant> Get() =>
            _applicant.Find(book => true).ToList();

        public Applicant Get(string id) =>
            _applicant.Find(book => book.Id == id).FirstOrDefault();

        public Applicant Create(Applicant model)
        {
            _applicant.InsertOne(model);
            return model;
        }

        public void Update(string id, Applicant model) =>
            _applicant.ReplaceOne(x => x.Id == id, model);

        public void Remove(string id) => 
            _applicant.DeleteOne(x => x.Id == id);
    }
}