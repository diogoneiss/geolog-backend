﻿using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Infraestrutura.Repositorios;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GeoLogBackend.GeoLogBackend.Infraestrutura
{
    public class UnitOfWork: IUnitOfWork
    {

        private IPaisRepository _paisRepository;

        //se o privado for nulo, cria e atribui com construtor
        public IPaisRepository Paises =>
            _paisRepository ??= new PaisRepository(_database, "paises");


        private readonly IMongoDatabase _database;


        public UnitOfWork(IConfiguration configuration)
        {
            IMongoClient client = new MongoClient(configuration["MongoSettings:ConnectionString"]);

            _database = client.GetDatabase(configuration["MongoSettings:DatabaseName"]);
        }

    }
}
