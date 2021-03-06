﻿using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using Breeze.Sharp;
using Breeze.Sharp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Models;

namespace Test_NetClient
{
    [TestClass]
    public class CreateEntityTests
    {
        private String _serviceName;

        [TestInitialize]
        public void TestInitializeMethod()
        {
            Configuration.Instance.ProbeAssemblies(typeof(Customer).Assembly);
            _serviceName = "http://localhost:56337/breeze/Northwind/";
        }

        [TestMethod]
        public async Task CreatingEntities()
        {
            var manager = new EntityManager(_serviceName);

            // Metadata must be fetched before CreateEntity() can be called
            await manager.FetchMetadata();

            //Snippet1
            var newCustomer = new Customer(); // rarely done in Breeze

            //Snippet2
            // Order uses an auto-generated key value
            var order = manager.CreateEntity<Order>();

            //Snippet3
            // If the key is not auto generated, it must be initialized by CreateEntity()
            var alpha = manager.CreateEntity<Customer>(new { CustomerID = Guid.NewGuid(), CompanyName = "Alpha" });

            //Snippet4
            // Unattached new customer so you can keep configuring it and add/attach it later
            // Key value initializer not required because new entity is not attached to entity manager
            var beta = manager.CreateEntity<Customer>(new { CompanyName = "Beta" }, EntityState.Detached);

            // Attached customer, as if retrieved from the database
            // Note that the key must be initialized when new entity will be in an attached state
            var gamma = manager.CreateEntity<Customer>(new { CustomerID = Guid.NewGuid(), CompanyName = "Gamma" }, EntityState.Unchanged);

            //Snippet5
            // Only need to do this once
            var metadataStore = manager.MetadataStore;                           // The model metadata known to this EntityManager instance
            var customerType = metadataStore.GetEntityType(typeof(Customer));    // Metadata about the Customer type

            // Do this for each customer to be created
            var acme = customerType.CreateEntity() as Customer;     // Returns Customer as IEntity
            acme.CompanyName = "Acme";                              // CompanyName is a required field
            acme.CustomerID = Guid.NewGuid();                       // Must set the key field before attaching to entity manager
            manager.AddEntity(acme);                                // Attach the entity as a new entity; it's EntityState is "Added"
        }
    }
}
