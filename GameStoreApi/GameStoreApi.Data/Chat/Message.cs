using GameStoreApi.Data.Common.Interfaces;
using GameStoreApi.Data.Users;
using System;

namespace GameStoreApi.Data.Chat
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public int? FromUserId { get; set; }
        public User FromUser { get; set; }

        public int? ToUserId { get; set; }
        public User ToUser { get; set; }

        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
