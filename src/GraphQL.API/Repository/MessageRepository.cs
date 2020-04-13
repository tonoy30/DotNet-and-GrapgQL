﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQL.API.Repository
{
    public class MessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageRepository(IMongoCollection<Message> messageCollection)
        {
            _messageCollection = messageCollection ?? throw new ArgumentNullException(nameof(messageCollection));
        }

        public IQueryable<Message> GetAllMessages() => _messageCollection.AsQueryable();

        public Task<Message> GetMessageById(ObjectId messageId) =>
            _messageCollection.AsQueryable().FirstOrDefaultAsync(t => t.Id == messageId);

        public Task CreateMessageAsync(Message message, CancellationToken cancellationToken)
        {
            return _messageCollection.InsertOneAsync(message, new InsertOneOptions(), cancellationToken);
        }
    }
}