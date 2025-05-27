using System.Text.Json;
using System.Text;

namespace PokerGameRSF.Server
{
    /// <summary>
    /// Класс, предоставляющий методы для сериализации и десериализации объектов типа GameMessage.
    /// Этот класс используется для преобразования объектов GameMessage в массив байтов
    /// и обратного преобразования массива байтов в объект GameMessage.
    /// </summary>
    public static class MessageSerializer
    {
        /// <summary>
        /// Сериализует объект GameMessage в массив байтов.
        /// </summary>
        /// <param name="message">Объект GameMessage, который нужно сериализовать.</param>
        /// <returns>Массив байтов, представляющий сериализованный объект GameMessage.</returns>
        public static byte[] Serialize(GameMessage message)
        {
            var json = JsonSerializer.Serialize(message);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Десериализует массив байтов обратно в объект GameMessage.
        /// </summary>
        /// <param name="data">Массив байтов, содержащий сериализованные данные GameMessage.</param>
        /// <returns>Объект GameMessage, восстановленный из массива байтов.</returns>
        public static GameMessage Deserialize(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<GameMessage>(json);

        }
    }
}
