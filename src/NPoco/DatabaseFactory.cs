﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco.FluentMappings;

namespace NPoco
{
    public class DatabaseFactory
    {
        private DatabaseFactoryConfigOptions _options;

        public DatabaseFactoryConfig Config()
        {
            _options = new DatabaseFactoryConfigOptions();
            return new DatabaseFactoryConfig(_options);
        }

        public Database Build(Database database)
        {
            if (_options.Mapper != null)
                database.Mapper = _options.Mapper;

            if (_options.PocoDataFactory != null)
                database.PocoDataFactory = _options.PocoDataFactory;

            return database;
        }

        public Database GetDatabase()
        {
            if (_options.Database == null)
                throw new NullReferenceException("Database cannot be null. Use UsingDatabase()");

            var db = _options.Database();
            Build(db);
            return db; 
        }
    }

    public class DatabaseFactoryConfigOptions
    {
        public Func<Database> Database { get; set; }
        public IMapper Mapper { get; set; }
        public Func<Type, PocoData> PocoDataFactory { get; set; }
    }

    public class DatabaseFactoryConfig
    {
        private readonly DatabaseFactoryConfigOptions _options;
        
        public DatabaseFactoryConfig(DatabaseFactoryConfigOptions options)
        {
            _options = options;
        }
        
        public DatabaseFactoryConfig UsingDatabase(Func<Database> database)
        {
            _options.Database = database;
            return this;
        }

        public DatabaseFactoryConfig WithMapper(IMapper mapper)
        {
            _options.Mapper = mapper;
            return this;
        }

        public DatabaseFactoryConfig WithFluentConfig(FluentConfig pocoDataFactory)
        {
            _options.PocoDataFactory = pocoDataFactory.Config(_options.Mapper);
            return this;
        }
    }
}
