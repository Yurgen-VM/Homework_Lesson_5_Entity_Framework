using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Task_4
{
    public enum Command
    {
        Register,
        Message,
        Confirmation,
        List // Добавляем команду List, которую каждый клиент будет отправлять серверу при подключении, тем самым получать от сервера список непрочитанных сообщений.        
    }
    
    // Класс для представления сетевого сообщения
     
    public class MessagePack
    {
        public Command Command { get; set; }
        public int? Id { get; set; }
        public string FromName { get; set; } // Имя отправителя
        public string ToName { get; set; } // Имя получателя
        public string Text { get; set; }
        

        // Метод для сериализации в JSON
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        // Статический метод для десериализации JSON в объект MyMessage

        public static MessagePack FromJson(string json)
        {
            return JsonSerializer.Deserialize<MessagePack>(json);
        }
    }
}
